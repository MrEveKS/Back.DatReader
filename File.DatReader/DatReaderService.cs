using System.IO;

namespace DatReader
{
    public unsafe class DatReaderService
    {
        public void Deserialize(BinaryReader reader, Info info)
        {
            var head = new Head(reader.BaseStream);

            var records = head.Records;
            var informations = new IpIntervalsInformation[records];
            var coordinateInformations = new CoordinateInformation[records];

            for (int i = 0; i < records; i++)
            {
                informations[i] = new IpIntervalsInformation(reader.BaseStream);
            }

            for (int i = 0; i < records; i++)
            {
                coordinateInformations[i] = new CoordinateInformation(reader.BaseStream);
            }
            info.Head = head;
            info.IpIntervalsInformations = informations;
            info.CoordinateInformations = coordinateInformations;
        }

        /*public void Deserialize2(BinaryReader reader, Info info)
        {
            using var stream = reader.BaseStream;
            var head = new Head(stream);

            var records = head.Records;
            var offsetRanges = (int)head.OffsetRanges;
            var offsetCities = (int)head.OffsetCities;

            var informations = new IpIntervalsInformation[records];
            var coordinateInformations = new CoordinateInformation[records];

            for (int index = 0; index < records; index++)
            {
                stream.Position = offsetRanges + IpIntervalsInformation.SIZE * index;
                informations[index] = new IpIntervalsInformation(reader.BaseStream);
                stream.Position = offsetRanges + offsetCities + CoordinateInformation.SIZE * index;
                coordinateInformations[index] = new CoordinateInformation(reader.BaseStream);
            }
            info.Head = head;
            info.IpIntervalsInformations = informations;
            info.CoordinateInformations = coordinateInformations;
        }*/
    }
}
