namespace Geo.DatReader.Models
{
	public interface IUserLocation
	{
		public int Id { get; }

		string Country { get; }

		string Region { get; }

		string Postal { get; }

		string City { get; }

		string Organization { get; }

		float Latitude { get; }

		float Longitude { get; }
	}
}