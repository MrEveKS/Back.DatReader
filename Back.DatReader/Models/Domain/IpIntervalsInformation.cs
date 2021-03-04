using File.DatReader.Models;

namespace Back.DatReader.Models.Domain
{
    public class IpIntervalsInformation : DbEntity, IIpIntervalsInformation
	{
        public uint IpFrom { get; set; }
        public uint IpTo { get; set; }
        public uint LocationIndex { get; set; }
    }
}
