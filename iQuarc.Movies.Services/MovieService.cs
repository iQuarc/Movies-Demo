using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iQuarc.AppBoot;
using iQuarc.Configuration;
using iQuarc.DataAccess;
using iQuarc.DataAccess.DocumentDb;
using iQuarc.Movies.Contracts;
using iQuarc.Movies.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace iQuarc.Movies.Services
{
	[Service(typeof(IMovieService))]
	public class MovieService : IMovieService
    {
	    private readonly IRepository repository;
		private DocumentRepository docRepository;

	    public MovieService(IRepository repository, IConfigService configService)
	    {
		    this.repository = repository;
			this.docRepository = new DocumentRepository(configService);
	    }

	    public async Task<IList<MovieHeader>> GetLatestAsync(int count)
	    {
		    List<MovieHeader> entities = await repository.GetEntities<MovieEntity>()
			    .OrderByDescending(e => e.ReleaseDate)
			    .Take(count)
			    .Select(MovieProjections.Header)
			    .ToListAsync();

		    return entities;
	    }

		public async Task<Movie> GetEntity(int id)
		{
			Movie movie = await repository.GetEntities<MovieEntity>()
				.Where(e => e.Id == id)
				.Select(MovieProjections.Movie)
				.FirstOrDefaultAsync();

			string idStr = id.ToString();

			Movies docMovie = docRepository.GetEntities<Movies>()
				.Where(e => e.Id == idStr)
				.AsEnumerable()
				.FirstOrDefault();

			if (docMovie != null)
			{
				movie.Details = new MovieDetails();
				movie.Details.Description = docMovie.Description;
				movie.Details.Storyline = docMovie.Storyline;
				movie.Details.Genres = docMovie.Genres;
				movie.Details.Countries = docMovie.Countries;
			}
			return movie;
		}

    }

	public class Movies : IEntity
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		public string Description { get; set; }

		public string Storyline { get; set; }

		public IList<string> Genres { get; set; }

		public IList<string> Countries { get; set; }

		object IEntity.Id
		{
			get { return this.Id; }
		}
	}
}
