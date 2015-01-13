using System;
using System.Linq;
using System.Linq.Expressions;
using iQuarc.Movies.Contracts;
using iQuarc.Movies.Model;

namespace iQuarc.Movies.Services
{
	internal static class MovieProjections
	{
		internal static readonly Expression<Func<MovieEntity, MovieHeader>> Header = e => new MovieHeader
		                                                                                  {
			                                                                                  Id = e.Id,
			                                                                                  Name = e.Name,
			                                                                                  ReleaseDate = e.ReleaseDate,
			                                                                                  Media = e.MediaItems.Where(m => m.MediaTypeId == (int) MediaType.Image)
				                                                                                  .Select(m => new Media
				                                                                                               {
					                                                                                               Id = m.Id,
					                                                                                               Name = m.Name,
					                                                                                               MediaType = (MediaType) m.MediaTypeId
				                                                                                               })
				                                                                                  .FirstOrDefault()
		                                                                                  };

		internal static readonly Expression<Func<MovieEntity, Movie>> Movie = e => new Movie
		                                                                           {
			                                                                           Id = e.Id,
			                                                                           Name = e.Name,
			                                                                           ReleaseDate = e.ReleaseDate
		                                                                           };
	}
}