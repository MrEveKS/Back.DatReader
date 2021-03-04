using System.IO;

namespace Geo.DatReader.Services
{
	internal interface IFileReaderService
	{
		BinaryReader ReadBinary(string fullPath);
		Stream Read(string fullPath);

		Stream ReadBuffer(string fullPath);

		Stream ReadMemoryMapped(string fullPath);
	}
}