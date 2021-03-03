using System.IO;
using System.Threading.Tasks;

namespace File.DatReader.Services
{
	internal interface IBatReaderService
	{
		Task<Stream> Read(string fullPath);
	}
}