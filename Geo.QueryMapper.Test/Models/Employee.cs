using Geo.Common.Domain;

namespace Geo.QueryMapper.Test.Models
{
	public class Employee : DbEntity
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public int? OrganizationId { get; set; }
	}
}