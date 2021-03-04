using System;
using Geo.Common.Dto;

namespace Geo.QueryMapper.Test.Models
{
	public class OrganizationFilterDto : EntityDto
	{
		public string Name { get; set; }

		public int? IdEqual { get; set; }

		public DateTime? CreateDateEqual { get; set; }

		public DateTime? CreateDate { get; set; }

		public string NameEqual { get; set; }
	}
}