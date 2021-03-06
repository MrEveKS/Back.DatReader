using System;
using System.Net;

namespace Geo.Information.Services
{
	public class IpAddressConverterService : IIpAddressConverterService
	{
		/// <inheritdoc cref="IIpAddressConverterService.ConvertFromIpAddressToInteger" />
		public uint? ConvertFromIpAddressToInteger(string ipAddress)
		{
			if (string.IsNullOrEmpty(ipAddress) || !IPAddress.TryParse(ipAddress, out var address))
			{
				return null;
			}

			var bytes = address.GetAddressBytes();

			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}

			return BitConverter.ToUInt32(bytes, 0);

		}

		/// <inheritdoc cref="IIpAddressConverterService.ConvertFromIntegerToIpAddress" />
		public string ConvertFromIntegerToIpAddress(uint ipAddress)
		{
			var bytes = BitConverter.GetBytes(ipAddress);

			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}

			return new IPAddress(bytes).ToString();
		}
	}
}