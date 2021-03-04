namespace Back.DatReader.Models.Dto.CoordinateInformation
{
	public class CoordinateInformationFilterDto : EntityDto
	{
		public string CountryContains { get; set; }
		public string OrganizationContains { get; set; }
	}
}
