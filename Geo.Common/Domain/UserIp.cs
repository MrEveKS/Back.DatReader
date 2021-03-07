namespace Geo.Common.Domain
{
	public class UserIp : DbEntity
	{
		public uint IpFrom { get; set; }

		public uint IpTo { get; set; }

		public uint UserLocationId { get; set; }
	}
}