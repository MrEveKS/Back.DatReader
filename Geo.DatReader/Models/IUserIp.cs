namespace Geo.DatReader.Models
{
	public interface IUserIp
	{
		public int Id { get; }

		uint IpFrom { get; }

		uint IpTo { get; }

		int UserLocationId { get; }
	}
}