using System.IO;

namespace DatReader
{
    public unsafe class DatReaderService
    {
        public void Deserialize(BinaryReader reader)
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
        }
    }
}
