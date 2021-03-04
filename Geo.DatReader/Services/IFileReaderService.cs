using System.IO;

namespace Geo.DatReader.Services
{
	internal interface IFileReaderService
	{
		Stream Read(string fullPath);
	}
}