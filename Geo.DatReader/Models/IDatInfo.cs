namespace Geo.DatReader.Models
{
	public interface IDatInfo
	{
		int Version { get; }

		string Name { get; }

		ulong Timestamp { get; }

		int Records { get; }

		uint OffsetRanges { get; }

		uint OffsetCities { get; }

		uint OffsetLocations { get; }
	}
}