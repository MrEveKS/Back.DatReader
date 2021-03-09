namespace Geo.Common.Domain
{
	public class UserIp : DbEntity
	{
		public uint IpFrom { get; set; }

		public uint IpTo { get; set; }

		public int UserLocationId { get; set; }

		public UserLocation UserLocation { get; set; }
	}
}