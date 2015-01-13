using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using iQuarc.Movies.Contracts;

namespace iQuarc.Movies.Web.Controllers
{
	[RoutePrefix("api/movies")]
	public class MoviesController : ApiController
	{
		private readonly IMovieService service;

		public MoviesController(IMovieService service)
		{
			this.service = service;
		}

		[HttpGet]
		[Route("latest")]
		public async Task<IHttpActionResult> GetLatest(int? count)
		{
			IList<MovieHeader> entities = await service.GetLatestAsync(count == null ? 5 : count.GetValueOrDefault());
			return Ok(entities);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IHttpActionResult> Get(int id)
		{
			Movie movie = await service.GetEntity(id);
			if (movie == null)
				return NotFound();

			return Ok(movie);
		}
	}
}
