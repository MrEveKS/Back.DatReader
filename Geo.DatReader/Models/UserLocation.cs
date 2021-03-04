using System.IO;

namespace Geo.DatReader.Models
{
	internal unsafe struct UserLocation : IUserLocation
	{
		private const byte CountrySize = 8;

		private const byte RegionSize = 12;

		private const byte PostalSize = 12;

		private const byte CitySize = 24;

		private const byte OrganizationSize = 32;

		private const byte LatitudeSize = 4;

		private const byte LongitudeSize = 4;

		private const byte Size = CountrySize + RegionSize + PostalSize + CitySize + OrganizationSize + LatitudeSize + LongitudeSize;

		private readonly byte[] _buffer;

		private string _country;

		private string _region;

		private string _postal;

		private string _city;

		private string _organization;

		private float? _latitude;

		private float? _longitude;

		public string Country => GetCountry();

		public string Region => GetRegion();

		public string Postal => GetPostal();

		public string City => GetCity();

		public string Organization => GetOrganization();

		public float Latitude => GetLatitude();

		public float Longitude => GetLongitude();

		public UserLocation(Stream stream)
		{
			_country = null;
			_region = null;
			_postal = null;
			_city = null;
			_organization = null;
			_latitude = null;
			_longitude = null;

			_buffer = new byte[Size];
			stream.Read(_buffer, 0, Size);
		}

		private string GetCountry()
		{
			return _country ??= InitString(0);
		}

		private string GetRegion()
		{
			return _region ??= InitString(CountrySize);
		}

		private string GetPostal()
		{
			return _postal ??= InitString(CountrySize + RegionSize);
		}

		private string GetCity()
		{
			return _city ??= InitString(CountrySize + RegionSize + PostalSize);
		}

		private string GetOrganization()
		{
			return _organization ??= InitString(CountrySize + RegionSize + PostalSize + CitySize);
		}

		private float GetLatitude()
		{
			return _latitude ??= InitFloat(CountrySize + RegionSize + PostalSize + CitySize + OrganizationSize);
		}

		private float GetLongitude()
		{
			return _longitude ??= InitFloat(CountrySize + RegionSize + PostalSize + CitySize + LatitudeSize);
		}

		private string InitString(byte skip)
		{
			fixed (byte* numRef = &_buffer[0])
			{
				return new string((sbyte*) &numRef[skip]);
			}
		}

		private float InitFloat(byte skip)
		{
			fixed (byte* numRef = &_buffer[0])
			{
				return *(float*) &numRef[skip];
			}
		}
	}
}