using Back.DatReader.Models.Domain;

namespace Back.DatReader.Test.QueryDtoTest.Models
{
	public class Employee : DbEntity
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int? OrganizationId { get; set; }
	}
}
