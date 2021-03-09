using System.IO;

namespace Geo.DatReader.Models
{
	internal unsafe struct DatInfo : IDatInfo
	{
		private const byte VersionSize = 4;

		private const byte NameSize = 32;

		private const byte TimestampSize = 8;

		private const byte RecordsSize = 4;

		private const byte OffsetRangesSize = 4;

		private const byte OffsetCitiesSize = 4;

		private const byte OffsetLocationsSize = 4;

		private const byte Size = VersionSize
								+ NameSize
								+ TimestampSize
								+ RecordsSize
								+ OffsetRangesSize
								+ OffsetCitiesSize
								+ OffsetLocationsSize;

		private readonly byte[] _buffer;

		private int? _version;

		private string _name;

		private ulong? _timestamp;

		private int? _records;

		private uint? _offsetRanges;

		private uint? _offsetCities;

		private uint? _offsetLocations;

		public int Version => GetVersion();

		public int Id { get; init; }

		public string Name => GetName();

		public ulong Timestamp => GetTimestamp();

		public int Records => GetRecords();

		public uint OffsetRanges => GetOffsetRanges();

		public uint OffsetCities => GetOffsetCities();

		public uint OffsetLocations => GetOffsetLocations();

		public DatInfo(Stream stream, int id)
		{
			Id = id;
			_version = null;
			_name = null;
			_timestamp = null;
			_records = null;
			_offsetRanges = null;
			_offsetCities = null;
			_offsetLocations = null;

			_buffer = new byte[Size];
			stream.Read(_buffer, 0, Size);
		}

		private int GetVersion()
		{
			return _version ??= InitInt(0);
		}

		private string GetName()
		{
			return _name ??= InitString(VersionSize);
		}

		private ulong GetTimestamp()
		{
			return _timestamp ??= InitUlong(VersionSize + NameSize);
		}

		private int GetRecords()
		{
			return _records ??= InitInt(VersionSize + NameSize + TimestampSize);
		}

		private uint GetOffsetRanges()
		{
			return _offsetRanges ??= InitUint(VersionSize + NameSize + TimestampSize + RecordsSize);
		}

		private uint GetOffsetCities()
		{
			return _offsetCities ??= InitUint(VersionSize + NameSize + TimestampSize + RecordsSize + OffsetRangesSize);
		}

		private uint GetOffsetLocations()
		{
			return _offsetLocations ??=
				InitUint(VersionSize + NameSize + TimestampSize + RecordsSize + OffsetRangesSize + OffsetCitiesSize);
		}

		private string InitString(byte skip)
		{
			fixed (byte* numRef = &_buffer[0])
			{
				return new string((sbyte*) &numRef[skip]).TrimEnd();
			}
		}

		private ulong InitUlong(byte skip)
		{
			fixed (byte* numRef = &_buffer[0])
			{
				return *(ulong*) &numRef[skip];
			}
		}

		private int InitInt(byte skip)
		{
			fixed (byte* numRef = &_buffer[0])
			{
				return *(int*) &numRef[skip];
			}
		}

		private uint InitUint(byte skip)
		{
			fixed (byte* numRef = &_buffer[0])
			{
				return *(uint*) &numRef[skip];
			}
		}
	}
}