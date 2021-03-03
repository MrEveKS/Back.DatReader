using System.IO;

namespace File.DatReader.Models
{
	internal unsafe struct IpIntervalsInformation : IIpIntervalsInformation
	{
        private const byte IP_FROM_SIZE = 4;
        private const byte IP_TO_SIZE = 4;
        private const byte LOCATION_INDEX_SIZE = 4;

        public const byte SIZE = IP_FROM_SIZE + IP_TO_SIZE + LOCATION_INDEX_SIZE;

        private readonly byte[] _buffer;

        private uint? _ipFrom;
        private uint? _ipTo;
        private uint? _locationIndex;

        public uint IpFrom => GetIpFrom();
        public uint IpTo => GetIpTo();
        public uint LocationIndex => GetLocationIndex();

        public IpIntervalsInformation(Stream stream)
        {
            _ipFrom = null;
            _ipTo = null;
            _locationIndex = null;

            _buffer = new byte[SIZE];
            stream.Read(_buffer, 0, SIZE);
        }

        private uint GetIpFrom()
        {
            return _ipFrom ??= InitUint(0);
        }

        private uint GetIpTo()
        {
            return _ipFrom ??= InitUint(IP_FROM_SIZE);
        }

        private uint GetLocationIndex()
        {
            return _ipFrom ??= InitUint(IP_FROM_SIZE + IP_TO_SIZE);
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
