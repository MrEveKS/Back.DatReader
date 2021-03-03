using System.IO;

namespace File.DatReader
{
	internal unsafe struct Head : IHead
	{
        private const byte VERSION_SIZE = 4;
        private const byte NAME_SIZE = 32;
        private const byte TIMESTAMP_SIZE = 8;
        private const byte RECORDS_SIZE = 4;
        private const byte OFFSET_RANGES_SIZE = 4;
        private const byte OFFSET_CITIES_SIZE = 4;
        private const byte OFFSET_LOCATIONS_SIZE = 4;

        public const byte SIZE = VERSION_SIZE + NAME_SIZE + TIMESTAMP_SIZE + RECORDS_SIZE + OFFSET_RANGES_SIZE +
                                  OFFSET_CITIES_SIZE + OFFSET_LOCATIONS_SIZE;

        private readonly byte[] _buffer;

        private int? _version;
        private string _name;
        private ulong? _timestamp;
        private int? _records;
        private uint? _offsetRanges;
        private uint? _offsetCities;
        private uint? _offsetLocations;

        public int Version => GetVersion();
        public string Name => GetName();
        public ulong Timestamp => GetTimestamp();
        public int Records => GetRecords();
        public uint OffsetRanges => GetOffsetRanges();
        public uint OffsetCities => GetOffsetCities();
        public uint OffsetLocations => GetOffsetLocations();

        public Head(Stream stream)
        {
            _version = null;
            _name = null;
            _timestamp = null;
            _records = null;
            _offsetRanges = null;
            _offsetCities = null;
            _offsetLocations = null;

            _buffer = new byte[SIZE];
            stream.Read(_buffer, 0, SIZE);
        }

        private int GetVersion()
        {
            return _version ??= InitInt(0);
        }

        private string GetName()
        {
            return _name ??= InitString(VERSION_SIZE);
        }

        private ulong GetTimestamp()
        {
            return _timestamp ??= InitUlong(VERSION_SIZE + NAME_SIZE);
        }

        private int GetRecords()
        {
            return _records ??= InitInt(VERSION_SIZE + NAME_SIZE + TIMESTAMP_SIZE);
        }

        private uint GetOffsetRanges()
        {
            return _offsetRanges ??= InitUint(VERSION_SIZE + NAME_SIZE + TIMESTAMP_SIZE + RECORDS_SIZE);
        }

        private uint GetOffsetCities()
        {
            return _offsetCities ??= InitUint(VERSION_SIZE + NAME_SIZE + TIMESTAMP_SIZE + RECORDS_SIZE + OFFSET_RANGES_SIZE);
        }

        private uint GetOffsetLocations()
        {
            return _offsetLocations ??= InitUint(VERSION_SIZE + NAME_SIZE + TIMESTAMP_SIZE + RECORDS_SIZE + OFFSET_RANGES_SIZE + OFFSET_CITIES_SIZE);
        }

        private string InitString(byte skip)
        {
            fixed (byte* numRef = &(_buffer[0]))
            {
                return new string((sbyte*)&(numRef[skip]));
            }
        }

        private ulong InitUlong(byte skip)
        {
            fixed (byte* numRef = &(_buffer[0]))
            {
                return *((ulong*)&(numRef[skip]));
            }
        }

        private int InitInt(byte skip)
        {
            fixed (byte* numRef = &(_buffer[0]))
            {
                return *((int*)&(numRef[skip]));
            }
        }

        private uint InitUint(byte skip)
        {
            fixed (byte* numRef = &(_buffer[0]))
            {
                return *((uint*)&(numRef[skip]));
            }
        }

    }
}
