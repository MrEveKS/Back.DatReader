namespace Geo.DatReader.Models
{
	public interface IIpIntervalsInformation
	{
		uint IpFrom { get; }

		uint IpTo { get; }

		uint LocationIndex { get; }
	}
}