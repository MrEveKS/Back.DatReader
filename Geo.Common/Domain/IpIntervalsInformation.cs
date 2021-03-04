namespace Geo.Common.Domain
{
	public class IpIntervalsInformation : DbEntity
	{
		public uint IpFrom { get; set; }

		public uint IpTo { get; set; }

		public uint LocationIndex { get; set; }
	}
}