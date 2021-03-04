namespace Geo.Common.Dto.DatInfo
{
	public class DatInfoDto : EntityDto
	{
		public int Version { get; set; }

		public string Name { get; set; }

		public ulong Timestamp { get; set; }
	}
}