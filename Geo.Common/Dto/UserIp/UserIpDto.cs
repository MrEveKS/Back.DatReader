using Geo.Common.Dto.UserLocation;

namespace Geo.Common.Dto.UserIp
{
	public class UserIpDto : EntityDto
	{
		public uint? IpFrom { get; set; }

		public uint? IpTo { get; set; }

		public uint? UserLocationId { get; set; }

		public UserLocationDto UserLocation { get; set; }
	}
}