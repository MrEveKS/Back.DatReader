using System.IO;

namespace File.DatReader.Services
{
	internal interface IBatReaderService
	{
		Stream ReadAsync(string fullPath);
	}
}