using System.IO;

namespace Geo.DatReader.Models
{
	internal unsafe struct UserIp : IUserIp
	{
		private const byte IpFromSize = 4;

		private const byte IpToSize = 4;

		private const byte LocationIndexSize = 4;

		private const byte Size = IpFromSize + IpToSize + LocationIndexSize;

		private readonly byte[] _buffer;

		private uint? _ipFrom;

		private uint? _ipTo;

		private uint? _locationIndex;

		public int Id { get; init; }

		public uint IpFrom => GetIpFrom();

		public uint IpTo => GetIpTo();

		public uint UserLocationId => GetUserLocationId();

		public UserIp(Stream stream, int id)
		{
			Id = id;
			_ipFrom = null;
			_ipTo = null;
			_locationIndex = null;

			_buffer = new byte[Size];
			stream.Read(_buffer, 0, Size);
		}

		private uint GetIpFrom()
		{
			return _ipFrom ??= InitUint(0);
		}

		private uint GetIpTo()
		{
			return _ipTo ??= InitUint(IpFromSize);
		}

		private uint GetUserLocationId()
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