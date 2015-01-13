using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iQuarc.Configuration;
using iQuarc.SystemEx;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace iQuarc.DataAccess.DocumentDb
{
	public sealed class DocumentRepository : IRepository, IDisposable
	{
		private readonly IDictionary<IEntity, EntityStatus> entities = new Dictionary<IEntity, EntityStatus>();
		private readonly IDictionary<Type, string> documentLinks = new Dictionary<Type, string>();
		
		private readonly DocumentConfig config;
	    private DocumentClient client;
		private Database database;

		private bool isInitialized;
		private bool isDisposed;

		public DocumentRepository(IConfigService configService)
	    {
		    config = configService.GetConfig<DocumentConfig>();
	    }
		
		public void Dispose()
		{
			if (isDisposed) return;

			if (client != null)
				client.Dispose();

			isDisposed = true;
		}

		private void CheckDisposed()
		{
			if (isDisposed)
				throw new ObjectDisposedException("Repository");
		}

		public IQueryable<T> GetEntities<T>() where T : class
		{
			CheckDisposed();

			if (!typeof (IEntity).IsAssignableFrom(typeof (T)))
				throw new ArgumentException("Type T must implement interface " + typeof (IEntity));

			string documentLink = GetDocumentLink(typeof (T));

			IOrderedQueryable<T> query = client.CreateDocumentQuery<T>(documentLink);
			return query;
		}

		public void Add<T>(T entity) where T : class
		{
			CheckDisposed();

			IEntity value = entity as IEntity;
			if (value == null)
				throw new ArgumentException("Entity must implement interface " + typeof(IEntity));
			
			entities[value] = EntityStatus.Added;
		}

		public void Update<T>(T entity) where T : class
		{
			CheckDisposed();

			IEntity value = entity as IEntity;
			if (value == null)
				throw new ArgumentException("Entity must implement interface " + typeof(IEntity));

			entities[(IEntity) entity] = EntityStatus.Updated;
		}

		public void Delete<T>(T entity) where T : class
		{
			CheckDisposed();

			IEntity value = entity as IEntity;
			if (value == null)
				throw new ArgumentException("Entity must implement interface " + typeof(IEntity));
			
			entities[(IEntity) entity] = EntityStatus.Deleted;
		}

		public async Task SaveChanges()
		{
			CheckDisposed();

			foreach (KeyValuePair<IEntity, EntityStatus> pair in entities)
			{
				IEntity entity = pair.Key;
				EntityStatus state = pair.Value;
				
				string documentLink = await GetDocumentLinkAsync(entity.GetType());

				switch (state)
				{
					case EntityStatus.Added:
						await client.CreateDocumentAsync(documentLink, entity);
						break;
					case EntityStatus.Updated:
						Document updatedDoc = GetDocument(documentLink, entity.Id);
						await client.ReplaceDocumentAsync(updatedDoc.SelfLink, entity);
						break;
					case EntityStatus.Deleted:
						Document deletedDoc = GetDocument(documentLink, entity.Id);
						await client.DeleteDocumentAsync(deletedDoc.SelfLink);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}

		private Document GetDocument(string documentLink, object idObj)
		{
			string id = idObj.ToString();

			Document document = client.CreateDocumentQuery(documentLink).FirstOrDefault(d => d.Id == id);
			
			return document;
		}

		private string GetDocumentLink(Type entityType)
		{
			string documentLink = documentLinks.GetValueOrDefault(entityType);
			if (documentLink == null)
			{
				DocumentCollection collection = GetCollection(entityType);
				documentLink = collection.DocumentsLink;

				documentLinks.Add(entityType, collection.DocumentsLink);
			}

			return documentLink;
		}

		private async Task<string> GetDocumentLinkAsync(Type entityType)
		{
			string documentLink = documentLinks.GetValueOrDefault(entityType);
			if (documentLink == null)
			{
				DocumentCollection collection = await GetOrCreateCollectionAsync(entityType);
				documentLink = collection.DocumentsLink;

				documentLinks.Add(entityType, collection.DocumentsLink);
			}
			return documentLink;
		}

		private DocumentCollection GetCollection(Type type)
		{
			Initialize();

			string collectionId = type.Name;
			DocumentCollection collection = client.CreateDocumentCollectionQuery(database.SelfLink).Where(c => c.Id == collectionId).AsEnumerable().FirstOrDefault();
			
			return collection;
		}

		private async Task<DocumentCollection> GetOrCreateCollectionAsync(Type type)
		{
			DocumentCollection collection = GetCollection(type);

			if (collection == null)
			{
				string collectionId = type.Name;
				collection = await client.CreateDocumentCollectionAsync(database.SelfLink, new DocumentCollection { Id = collectionId });
			}

			return collection;
		}

		private void Initialize()
		{
			if (isInitialized)
				return;

			client = new DocumentClient(new Uri(config.ServiceEndpoint), config.AuthorizationKey);
			database = client.CreateDatabaseQuery().Where(e => e.Id == config.DatabaseId).AsEnumerable().FirstOrDefault();

			isInitialized = true;
		}

		private enum EntityStatus
		{
			Added,
			Updated,
			Deleted
		}
	}
}
