namespace Geo.Common.Dto.UserLocation
{
	public class UserLocationFilterDto : EntityDto
	{
		public int[] IdIn { get; set; }

		public string CityEqual { get; set; }

		public string CityContains { get; set; }

		public string CountryContains { get; set; }

		public string OrganizationContains { get; set; }
	}
}