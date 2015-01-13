using System.Linq;
using System.Threading.Tasks;

namespace iQuarc.DataAccess
{
	/// <summary>
	/// Generic repository contract for database operations.
	/// </summary>
	public interface IRepository
	{
		/// <summary>
		/// Gets the entities from the database.
		/// </summary>
		/// <typeparam name="T">The type of the entities to be retrieved from the database.</typeparam>
		/// <returns>A <see cref="IQueryable"/> for the entities from the database.</returns>
		IQueryable<T> GetEntities<T>() where T : class;

		/// <summary>
		/// Adds the specified entity to the database.
		/// </summary>
		/// <typeparam name="T">the type of the entity to be added.</typeparam>
		/// <param name="entity">The entity to be added.</param>
		void Add<T>(T entity) where T : class;

		/// <summary>
		/// Updates the specified entity to the database.
		/// </summary>
		/// <typeparam name="T">The type of the entity to be updated.</typeparam>
		/// <param name="entity">The entity to be updated.</param>
		void Update<T>(T entity) where T : class;

		/// <summary>
		/// Deletes the specified entity from the database.
		/// </summary>
		/// <typeparam name="T">The type of the entity to be deleted.</typeparam>
		/// <param name="entity">The entity to be deleted.</param>
		void Delete<T>(T entity) where T : class;

		/// <summary>
		/// Saves the changes.
		/// </summary>
		Task SaveChanges();
	}
}
