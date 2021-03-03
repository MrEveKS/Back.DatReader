namespace File.DatReader.Models
{
	public interface IHead
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