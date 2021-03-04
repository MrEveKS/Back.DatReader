namespace Geo.Common.Domain
{
	public class CoordinateInformation : DbEntity
	{
		public string Country { get; set; }

		public string Region { get; set; }

		public string Postal { get; set; }

		public string City { get; set; }

		public string Organization { get; set; }

		public float Latitude { get; set; }

		public float Longitude { get; set; }
	}
}