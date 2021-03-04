using System;
using Back.DatReader.Models.Dto;

namespace Back.DatReader.Test.QueryDtoTest.Models
{
	public class OrganizationListDto : EntityDto
	{
		public string Name { get; set; }

		public DateTime? CreateDate { get; set; }

		public int CustomBindInt { get; set; }

		public string CustomBindString { get; set; }
	}
}