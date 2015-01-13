using System.Data.Entity;

namespace iQuarc.Movies.Model.Initialization
{
	public class MoviesDbIntializer : DropCreateDatabaseAlways<MoviesContext>
	{
		protected override void Seed(MoviesContext context)
		{
			MediaTypeInitializer.Initialize(context);
			DataInitializer.Intialize(context);

			base.Seed(context);
		}
	}
}