using System.Collections.Generic;
using System.Threading.Tasks;

namespace iQuarc.Movies.Contracts
{
	public interface IMovieService
	{
		Task<IList<MovieHeader>> GetLatestAsync(int count);
		Task<Movie> GetEntity(int id);
	}
}
