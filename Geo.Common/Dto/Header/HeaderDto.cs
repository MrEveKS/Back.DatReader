namespace Geo.Common.Dto.Header
{
	public class HeaderDto : EntityDto
	{
		public int Version { get; set; }

		public string Name { get; set; }

		public ulong Timestamp { get; set; }

		public int Records { get; set; }
	}
}