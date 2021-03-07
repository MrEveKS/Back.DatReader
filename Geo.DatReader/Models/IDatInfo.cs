namespace Geo.DatReader.Models
{
	public interface IDatInfo
	{
		public int Id { get; }

		int Version { get; }

		string Name { get; }

		ulong Timestamp { get; }

		int Records { get; }

		uint OffsetRanges { get; }

		uint OffsetCities { get; }

		uint OffsetLocations { get; }
	}
}