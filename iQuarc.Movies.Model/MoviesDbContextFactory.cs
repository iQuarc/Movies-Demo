using iQuarc.AppBoot;
using iQuarc.DataAccess;

namespace iQuarc.Movies.Model
{
	[Service(typeof(IDbContextFactory))]
	public class MoviesDbContextFactory : DbContextFactory<MoviesContext>
	{
	}
}