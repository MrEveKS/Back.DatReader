using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("File.DatReader.Test")]
namespace File.DatReader.Services
{
	internal class BatReaderService : IBatReaderService
	{
		public async Task<Stream> ReadAsync(string fullPath)
		{
			await using var fs = new FileStream(fullPath, FileMode.Open);
			using var binaryReader = new BinaryReader(fs);
			return binaryReader.BaseStream; 
		}
	}
}
