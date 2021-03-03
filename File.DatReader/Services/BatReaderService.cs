using System.IO;

namespace File.DatReader.Services
{
	internal class BatReaderService : IBatReaderService
	{
		public Stream ReadAsync(string fullPath)
		{
			var fs = new FileStream(fullPath, FileMode.Open);
			var binaryReader = new BinaryReader(fs);
			return binaryReader.BaseStream; 
		}
	}
}
