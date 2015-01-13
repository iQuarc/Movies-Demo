using System.Data.Entity;
using iQuarc.Movies.Model.Initialization;

namespace iQuarc.Movies.Model
{
	public class MoviesContext : DbContext
	{
		static MoviesContext()
		{
			Database.SetInitializer(new MoviesDbIntializer());	
		}

		public MoviesContext()
			: base("DefaultConnection")
		{
			this.Configuration.LazyLoadingEnabled = false;
			this.Configuration.ProxyCreationEnabled = false;
		}

		public DbSet<MovieEntity> Movies { get; set; }
		public DbSet<PersonEntity> Persons { get; set; }
		public DbSet<MediaEntity> Media { get; set; }
		public DbSet<MoviePersonEntity> MoviePersons { get; set; }
		public DbSet<RoleEntity> Roles { get; set; }
		public DbSet<MediaTypeEntity> MediaTypes { get; set; }

		protected override void OnModelCreating(DbModelBuilder builder)
		{
			builder.Configurations.AddFromAssembly(GetType().Assembly);

			base.OnModelCreating(builder);
		}
	}
}
