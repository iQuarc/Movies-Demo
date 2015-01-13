using System.Runtime.Serialization;

namespace iQuarc.Movies.Contracts
{
	[DataContract(IsReference = true)]
	public class Media
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public MediaType MediaType { get; set; }
	}
}