namespace Geo.DatReader.Models
{
	public interface IUserIp
	{
		uint IpFrom { get; }

		uint IpTo { get; }

		uint LocationIndex { get; }
	}
}