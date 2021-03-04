using System.IO;

namespace Geo.DatReader.Services
{
	internal class BatReaderService : IBatReaderService
	{
		public Stream ReadAsync(string fullPath)
		{
			var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			var binaryReader = new BinaryReader(fs);

			return binaryReader.BaseStream;
		}
	}
}