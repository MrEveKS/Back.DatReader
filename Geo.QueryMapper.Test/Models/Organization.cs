using System;
using Geo.Common.Domain;

namespace Geo.QueryMapper.Test.Models
{
	public class Organization : DbEntity
	{
		public static readonly DateTime CreateDateConst = DateTime.Now.Date;

		public string Name { get; set; }

		public string Description { get; set; }

		public int? KindId { get; set; }

		public OrganizationKind Kind { get; set; }

		public DateTime? CreateDate { get; set; }

		public DateTime? UpdateDate { get; set; }
	}
}