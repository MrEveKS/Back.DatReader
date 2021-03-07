using System.Collections.Generic;
using System.Threading.Tasks;
using Geo.DatReader.Models;

namespace Geo.DatReader
{
	public interface IDatDbData
	{
		IReadOnlyCollection<IUserLocation> UserLocations { get; }

		IDatInfo DatInfo { get; }

		IReadOnlyCollection<IUserIp> UserIps { get; }

		Task InitializeAsync();
	}
}