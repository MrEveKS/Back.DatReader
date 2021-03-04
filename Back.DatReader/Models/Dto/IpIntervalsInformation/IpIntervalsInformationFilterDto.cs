namespace Back.DatReader.Models.Dto.IpIntervalsInformation
{
	public class IpIntervalsInformationFilterDto : EntityDto
	{
		public string IpAddress { get; set; }
		public uint? IpToEqual { get; set; }
		public uint? IpToContains { get; set; }
	}
}
