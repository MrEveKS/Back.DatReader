using System.IO;

namespace DatReader
{
    public unsafe struct CoordinateInformation
    {
        private const byte COUNTRY_SIZE = 8;
        private const byte REGION_SIZE = 12;
        private const byte POSTAL_SIZE = 12;
        private const byte CITY_SIZE = 24;
        private const byte ORGANIZATION_SIZE = 32;
        private const byte LATITUDE_SIZE = 4;
        private const byte LONGITUDE_SIZE = 4;

        public const byte SIZE = COUNTRY_SIZE + REGION_SIZE + POSTAL_SIZE + CITY_SIZE + ORGANIZATION_SIZE + LATITUDE_SIZE + LONGITUDE_SIZE;

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

        public CoordinateInformation(Stream stream)
        {
            _country = null;
            _region = null;
            _postal = null;
            _city = null;
            _organization = null;
            _latitude = null;
            _longitude = null;

            _buffer = new byte[SIZE];
            stream.Read(_buffer, 0, SIZE);
        }

        private string GetCountry()
        {
            return _country ??= InitString(0);
        }

        private string GetRegion()
        {
            return _region ??= InitString(COUNTRY_SIZE);
        }

        private string GetPostal()
        {
            return _postal ??= InitString(COUNTRY_SIZE + REGION_SIZE);
        }

        private string GetCity()
        {
            return _city ??= InitString(COUNTRY_SIZE + REGION_SIZE + POSTAL_SIZE);
        }

        private string GetOrganization()
        {
            return _organization ??= InitString(COUNTRY_SIZE + REGION_SIZE + POSTAL_SIZE + CITY_SIZE);
        }

        private float GetLatitude()
        {
            return _latitude ??= InitFloat(COUNTRY_SIZE + REGION_SIZE + POSTAL_SIZE + CITY_SIZE + ORGANIZATION_SIZE);
        }

        private float GetLongitude()
        {
            return _longitude ??= InitFloat(COUNTRY_SIZE + REGION_SIZE + POSTAL_SIZE + CITY_SIZE + LATITUDE_SIZE);
        }

        private string InitString(byte skip)
        {
            fixed (byte* numRef = &(_buffer[0]))
            {
                return new string((sbyte*)&(numRef[skip]));
            }
        }

        private float InitFloat(byte skip)
        {
            fixed (byte* numRef = &(_buffer[0]))
            {
                return *((float*)&(numRef[skip]));
            }
        }

    }
}
