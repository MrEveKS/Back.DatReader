using System;
using Geo.Common.Dto;

namespace Geo.QueryMapper.Test.Models
{
	public class OrganizationListDto : EntityDto
	{
		public string Name { get; set; }

		public DateTime? CreateDate { get; set; }

		public int CustomBindInt { get; set; }

		public string CustomBindString { get; set; }
	}
}