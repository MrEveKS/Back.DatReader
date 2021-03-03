using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatReader
{
    public class Program
    {
        private static int _size = 100;
        public static List<double> results = new List<double>(_size);

        static async Task Main(string[] args)
        {
            /*var datReader = new DatReader();
            for (int i = 0; i < 3; i++)
            {
                await datReader.Read();
            }*/

            var reader = new DatReaderService();

            for (int i = 0; i < _size; i++)
            {
                await using var fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "geobase.dat"), FileMode.Open);
                using var br = new BinaryReader(fs);
                var timer = Stopwatch.StartNew();
                var info = new Info();
                br.BaseStream.Position = 0;
                reader.Deserialize(br, info);
                var result = timer.Elapsed.TotalMilliseconds;
                Program.results.Add(result);
            }

            var middle = results.Sum() / _size;
        }
    }

    public unsafe class Test
    {
        public void Deserialize(BinaryReader reader, Info info)
        {
            var timer = Stopwatch.StartNew();

            var head = new Head(reader.BaseStream);

            /*byte[] buffer = new byte[Head.SIZE];
            reader.BaseStream.Read(buffer, 0, buffer.Length);

            var head = new Head(buffer);*/

            var records = head.Records;
            var informations = new IpIntervalsInformation[records];
            for (int i = 0; i < records; i++)
            {
                informations[i] = new IpIntervalsInformation(reader.BaseStream);
            }
            var coordinateInformations = new CoordinateInformation[records];
            for (int i = 0; i < records; i++)
            {
                coordinateInformations[i] = new CoordinateInformation(reader.BaseStream);
            }
            info.Head = head;
            info.IpIntervalsInformations = informations;
            info.CoordinateInformations = coordinateInformations;

            var result = timer.Elapsed.TotalMilliseconds;
            Program.results.Add(result);

        }
    }

    public unsafe class BatBinaryReader : IDisposable
    {
        private readonly byte[] _buffer = new byte[50];

        public Stream BaseStream { get; private set; }

        public BatBinaryReader(Stream stream)
        {
            BaseStream = stream;
        }


        public int ReadInt32()
        {
            BaseStream.Read(_buffer, 0, 4);

            fixed (byte* numRef = &(_buffer[0]))
            {
                return *(((int*)numRef));
            }
        }

        public void Dispose()
        {
            BaseStream?.Dispose();
        }
    }

    public class Info
    {
        public Head Head { get; set; }
        public IpIntervalsInformation[] IpIntervalsInformations { get; set; }
        public CoordinateInformation[] CoordinateInformations { get; set; }
    }

    public readonly unsafe struct Head1
    {
        private readonly byte* _version;
        private readonly sbyte* _name;
        private readonly ulong* _timestamp;
        private readonly int* _records;
        private readonly uint* _offsetRange;
        private readonly uint* _offsetCities;
        private readonly uint* _offsetLocations;

        public int Version => *((int*)_version);
        public string Name => new string(_name);
        public ulong Timestamp => *_timestamp;
        public int Records => *_records;
        public uint OffsetRanges => *_offsetRange;
        public uint OffsetCities => *_offsetCities;
        public uint OffsetLocations => *_offsetLocations;

        public Head1(byte* version, sbyte* name, ulong* timestamp, int* records, uint* offsetRange, uint* offsetCities, uint* offsetLocations)
        {
            _version = version;
            _name = name;
            _timestamp = timestamp;
            _records = records;
            _offsetRange = offsetRange;
            _offsetCities = offsetCities;
            _offsetLocations = offsetLocations;
        }
    }

    public readonly unsafe struct IpIntervalsInformation2
    {
        private readonly uint* _ipFrom;
        private readonly uint* _ipTo;
        private readonly uint* _locationIndex;

        public uint IpFrom => *_ipFrom;
        public uint IpTo => *_ipTo;
        public uint LocationIndex => *_locationIndex;

        public IpIntervalsInformation2(uint* ipFrom, uint* ipTo, uint* locationIndex)
        {
            _ipFrom = ipFrom;
            _ipTo = ipTo;
            _locationIndex = locationIndex;
        }
    }

    public readonly unsafe struct CoordinateInformation1
    {
        private readonly sbyte* _country;
        private readonly sbyte* _region;
        private readonly sbyte* _postal;
        private readonly sbyte* _city;
        private readonly sbyte* _organization;
        private readonly float* _latitude;
        private readonly float* _longitude;

        public string Country => new string(_country);
        public string Region => new string(_region);
        public string Postal => new string(_postal);
        public string City => new string(_city);
        public string Organization => new string(_organization);
        public float Latitude => *_latitude;
        public float Longitude => *_longitude;

        public CoordinateInformation1(sbyte* country, sbyte* region, sbyte* postal, sbyte* city, sbyte* organization, float* latitude, float* longitude)
        {
            _country = country;
            _region = region;
            _postal = postal;
            _city = city;
            _organization = organization;
            _latitude = latitude;
            _longitude = longitude;
        }
    }

    /*public class DatReader
    {
        public async Task Read()
        {
            var timer = Stopwatch.StartNew();

            var header = new Head();
            await using var fs = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "geobase.dat"), FileMode.Open);
            using (var br = new BinaryReader(fs))
            {

                header.Version = br.ReadInt32();
                var name = new sbyte[32];

                header.Name = "";
                header.Timestamp = br.ReadUInt64();
                header.OffsetRanges = br.ReadUInt32();
                header.OffsetCities = br.ReadUInt32();
                header.OffsetLocations = br.ReadUInt32();
            }
            
            var ipInfos = new IpIntervalsInformation[header.Records];
            var coordinateInfos = new CoordinateInformation[header.Records];

            var bytes = new byte[0];

            var skip = header.Records * 12 + 60;

            for (var index = 0; index < header.Records; index++)
            {
                var startIndex = 60 + index * 12;
                ipInfos[index] = new IpIntervalsInformation()
                {
                    IpFrom = BitConverter.ToUInt32(ArrayCopy(bytes, startIndex, 4)),
                    IpTo = BitConverter.ToUInt32(ArrayCopy(bytes, startIndex + 4, 4)),
                    LocationIndex = BitConverter.ToUInt32(ArrayCopy(bytes, startIndex + 8, 4))
                };

                var coordinateInfo = coordinateInfos[index] = new CoordinateInformation();
                startIndex = skip + index * 96;
                coordinateInfo.Country = Encoding.UTF8.GetString(bytes, startIndex, 8);
                startIndex += 8;
                coordinateInfo.Region = Encoding.UTF8.GetString(bytes, startIndex, 12);
                startIndex += 12;
                coordinateInfo.Postal = Encoding.UTF8.GetString(bytes, startIndex, 12);
                startIndex += 12;
                coordinateInfo.City = Encoding.UTF8.GetString(bytes, startIndex, 24);
                startIndex += 24;
                coordinateInfo.Organization = Encoding.UTF8.GetString(bytes, startIndex, 32);
                startIndex += 32;
                coordinateInfo.Latitude = BitConverter.ToSingle(bytes, startIndex);
                startIndex += 4;
                coordinateInfo.Longitude = BitConverter.ToSingle(bytes, startIndex);
            }

            var result = timer.Elapsed.TotalMilliseconds;
        }

        private byte[] ArrayCopy(Array sourceArray, int sourceIndex, int length)
        {
            var result = new byte[length];
            Array.Copy(sourceArray, sourceIndex, result, 0, length);
            return result;
        }
    }*/
}
