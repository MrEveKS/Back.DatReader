using System;
using Back.DatReader.Models.Dto;

namespace Back.DatReader.Test.QueryDtoTest.Models
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