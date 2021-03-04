using Back.DatReader.Models.Domain;

namespace Back.DatReader.Test.QueryDtoTest.Models
{
	public class OrganizationKind : DbEntity
	{
		public string Name { get; set; }

		public int? GroupId { get; set; }
	}
}