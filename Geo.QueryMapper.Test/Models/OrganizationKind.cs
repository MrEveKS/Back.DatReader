using Geo.Common.Domain;

namespace Geo.QueryMapper.Test.Models
{
	public class OrganizationKind : DbEntity
	{
		public string Name { get; set; }

		public int? GroupId { get; set; }
	}
}