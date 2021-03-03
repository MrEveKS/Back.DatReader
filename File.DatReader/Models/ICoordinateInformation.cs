namespace File.DatReader.Models
{
	public interface ICoordinateInformation
	{
		string Country { get; }

		string Region { get; }

		string Postal { get; }

		string City { get; }

		string Organization { get; }

		float Latitude { get; }

		float Longitude { get; }
	}
}