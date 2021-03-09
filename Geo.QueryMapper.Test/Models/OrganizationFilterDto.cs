using System;
using Geo.Common.Dto;

namespace Geo.QueryMapper.Test.Models
{
	public class OrganizationFilterDto : EntityDto
	{
		public int? IdEqual { get; set; }

		public int? IdLess { get; set; }

		public int? IdLessEqual { get; set; }

		public int? IdGreater { get; set; }

		public int[] IdIn { get; set; }

		public int?[] KindIdIn { get; set; }

		public int?[] KindGroupIdIn { get; set; }

		public string[] NameIn { get; set; }

		public string[] KindGroupNameIn { get; set; }

		public int? IdGreaterEqual { get; set; }

		public string Name { get; set; }

		public DateTime? CreateDate { get; set; }

		public DateTime? CreateDateEqual { get; set; }

		public DateTime? CreateDateLess { get; set; }

		public DateTime? CreateDateLessEqual { get; set; }

		public DateTime? CreateDateGreater { get; set; }

		public DateTime? CreateDateGreaterEqual { get; set; }

		public string NameEqual { get; set; }
	}
}