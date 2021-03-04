namespace Geo.Common.Dto.CoordinateInformation
{
	public class CoordinateInformationFilterDto : EntityDto
	{
		public string CityEqual { get; set; }

		public string CityContains { get; set; }

		public string CountryContains { get; set; }

		public string OrganizationContains { get; set; }
	}
}