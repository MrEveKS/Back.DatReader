using System.IO;

namespace Geo.DatReader.Models
{
	internal unsafe struct IpIntervalsInformation : IIpIntervalsInformation
	{
		private const byte IpFromSize = 4;

		private const byte IpToSize = 4;

		private const byte LocationIndexSize = 4;

		public const byte Size = IpFromSize + IpToSize + LocationIndexSize;

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

			_buffer = new byte[Size];
			stream.Read(_buffer, 0, Size);
		}

		public IpIntervalsInformation(bool test = false)
		{
			_ipFrom = null;
			_ipTo = null;
			_locationIndex = null;

			 _buffer = null;
		}

		public IpIntervalsInformation(byte[] buffer)
		{
			_ipFrom = null;
			_ipTo = null;
			_locationIndex = null;

			_buffer = buffer;
		}

		private uint GetIpFrom()
		{
			return _ipFrom ??= InitUint(0);
		}

		private uint GetIpTo()
		{
			return _ipTo ??= InitUint(IpFromSize);
		}

		private uint GetLocationIndex()
		{
			return _locationIndex ??= InitUint(IpFromSize + IpToSize);
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