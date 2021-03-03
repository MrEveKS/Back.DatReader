using System.IO;
using System.Threading.Tasks;

namespace File.DatReader.Services
{
	internal class BatReaderService : IBatReaderService
	{
		public async Task<Stream> Read(string fullPath)
		{
			await using var fs = new FileStream(fullPath, FileMode.Open);
			using var binaryReader = new BinaryReader(fs);
			return binaryReader.BaseStream; 
		}
	}
}
