using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Geo.DatReader.Constants;

namespace Geo.DatReader.Services
{
	internal class FileReaderService : IFileReaderService
	{
		public Stream Read(string fullPath)
		{
			var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			var binaryReader = new BinaryReader(fs);

			return binaryReader.BaseStream;
		}

		public BinaryReader ReadBinary(string fullPath)
		{
			var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			return new BinaryReader(fs);
		}

		public Stream ReadBuffer(string fullPath)
		{
			var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
			var bs = new BufferedStream(fs);
			var sr = new StreamReader(bs);

			return sr.BaseStream;
		}

		public Stream ReadMemoryMapped(string fullPath)
		{
			MemoryMappedFile mmf = MemoryMappedFile.CreateFromFile(
				Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH));
			MemoryMappedViewStream mms = mmf.CreateViewStream();
			BinaryReader b = new BinaryReader(mms);
			return b.BaseStream;
		}
	}
}