using File.DatReader.Models;

namespace Back.DatReader.Models
{
    public class IpIntervalsInformation : IIpIntervalsInformation
	{
        public uint IpFrom { get; set; }
        public uint IpTo { get; set; }
        public uint LocationIndex { get; set; }
    }
}
