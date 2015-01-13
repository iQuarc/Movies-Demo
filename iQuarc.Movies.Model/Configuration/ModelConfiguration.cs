using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iQuarc.Movies.Model.Configuration
{
	internal class MovieConfiguration : EntityTypeConfiguration<MovieEntity>
	{
		public MovieConfiguration()
		{
			HasKey(e => e.Id);

			Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(100);

			HasMany(e => e.MoviePersons)
				.WithRequired(e => e.Movie)
				.HasForeignKey(e => e.MovieId);

			HasMany(e => e.MediaItems)
				.WithRequired(e => e.Movie)
				.HasForeignKey(e => e.MovieId);
		}
	}

	internal class PersonConfiguration : EntityTypeConfiguration<PersonEntity>
	{
		public PersonConfiguration()
		{
			HasKey(e => e.Id);

			Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			Property(e => e.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			Property(e => e.LastName)
				.IsRequired()
				.HasMaxLength(50);

			HasMany(e => e.MoviePersons)
				.WithRequired(e => e.Person)
				.HasForeignKey(e => e.PersonId);
		}
	}

	internal class MoviePersonConfiguration : EntityTypeConfiguration<MoviePersonEntity>
	{
		public MoviePersonConfiguration()
		{
			HasKey(e => e.Id);

			Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			HasRequired(e => e.Movie)
				.WithMany(e => e.MoviePersons)
				.HasForeignKey(e => e.MovieId);

			HasRequired(e => e.Person)
				.WithMany(e => e.MoviePersons)
				.HasForeignKey(e => e.PersonId);

			HasRequired(e => e.Role)
				.WithMany(e => e.MoviePersons)
				.HasForeignKey(e => e.RoleId);
		}
	}

	internal class RoleConfiguration : EntityTypeConfiguration<RoleEntity>
	{
		public RoleConfiguration()
		{
			HasKey(e => e.Id);

			Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(50);

			HasMany(e => e.MoviePersons)
				.WithRequired(e => e.Role)
				.HasForeignKey(e => e.RoleId);
		}
	}

	internal class MediaConfiguration : EntityTypeConfiguration<MediaEntity>
	{
		public MediaConfiguration()
		{
			HasKey(e => e.Id);

			Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

			HasRequired(e => e.Movie)
				.WithMany(e => e.MediaItems)
				.HasForeignKey(e => e.MovieId);

			HasRequired(e => e.MediaType)
				.WithMany(e => e.MediaItems)
				.HasForeignKey(e => e.MediaTypeId);
		}
	}

	internal class MediaTypeConfiguration : EntityTypeConfiguration<MediaTypeEntity>
	{
		public MediaTypeConfiguration()
		{
			HasKey(e => e.Id);

			Property(e => e.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

			Property(e => e.Name)
				.HasMaxLength(50);

			HasMany(e => e.MediaItems)
				.WithRequired(e => e.MediaType)
				.HasForeignKey(e => e.MediaTypeId);
		}
	}
}
