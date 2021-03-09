namespace Geo.Common.Dto.UserIp
{
	public class UserIpFilterDto : EntityDto
	{
		public string IpAddress { get; set; }

		public uint? IpFromGreaterEqual { get; set; }

		public uint? IpToLessEqual { get; set; }

		public uint? IpToEqual { get; set; }

		public uint? IpToContains { get; set; }
	}
}