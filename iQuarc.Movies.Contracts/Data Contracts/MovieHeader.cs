using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace iQuarc.Movies.Contracts
{
	[DataContract(IsReference = true)]
    public class MovieHeader
    {
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public DateTime ReleaseDate { get; set; }

		[DataMember]
		public Media Media { get; set; }
    }

	[DataContract(IsReference = true)]
	public class Movie
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public DateTime ReleaseDate { get; set; }

		[DataMember]
		public MovieDetails Details { get; set; }
	}

	[DataContract(IsReference = true)]
	public class MovieDetails
	{
		[DataMember]
		public string Description { get; set; }

		[DataMember]
		public string Storyline { get; set; }

		[DataMember]
		public IList<string> Genres { get; set; }

		[DataMember]
		public IList<string> Countries { get; set; }
	}
}
