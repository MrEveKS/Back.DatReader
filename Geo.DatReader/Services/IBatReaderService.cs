using System.IO;

namespace Geo.DatReader.Services
{
	internal interface IBatReaderService
	{
		Stream ReadAsync(string fullPath);
	}
}