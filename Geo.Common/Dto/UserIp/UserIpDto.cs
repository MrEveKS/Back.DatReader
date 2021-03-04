namespace Geo.Common.Dto.UserIp
{
	public class UserIpDto : EntityDto
	{
		public uint? IpFrom { get; set; }

		public uint? IpTo { get; set; }

		public uint? LocationIndex { get; set; }
	}
}