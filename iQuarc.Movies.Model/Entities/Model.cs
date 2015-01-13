using System;
using System.Collections.Generic;

namespace iQuarc.Movies.Model
{
    public class MovieEntity
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime ReleaseDate { get; set; }

		public virtual IList<MoviePersonEntity> MoviePersons { get; set; }
		public virtual IList<MediaEntity> MediaItems { get; set; } 
    }

	public class PersonEntity
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }

		public virtual IList<MoviePersonEntity> MoviePersons { get; set; }
	}

	public class MoviePersonEntity
	{
		public int Id { get; set; }
		public int MovieId { get; set; }
		public int PersonId { get; set; }
		public int RoleId { get; set; }

		public virtual MovieEntity Movie { get; set; }
		public virtual PersonEntity Person { get; set; }
		public virtual RoleEntity Role { get; set; }
	}

	public class RoleEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual IList<MoviePersonEntity> MoviePersons { get; set; } 
	}

	public class MediaEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string MediaUrl { get; set; }

		public int MovieId { get; set; }
		public int MediaTypeId { get; set; }

		public virtual MovieEntity Movie { get; set; }
		public virtual MediaTypeEntity MediaType { get; set; }
	}

	public class MediaTypeEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual IList<MediaEntity> MediaItems { get; set; } 
	}
}
