namespace Geo.Common.Dto.IpIntervalsInformation
{
	public class IpIntervalsInformationDto : EntityDto
	{
		public uint? IpFrom { get; set; }

		public uint? IpTo { get; set; }

		public uint? LocationIndex { get; set; }
	}
}