namespace Geo.Common.Domain
{
	public class DatInfo : DbEntity
	{
		public int Version { get; set; }

		public string Name { get; set; }

		public ulong Timestamp { get; set; }

		public int Records { get; set; }

		public uint OffsetRanges { get; set; }

		public uint OffsetCities { get; set; }

		public uint OffsetLocations { get; set; }
	}
}