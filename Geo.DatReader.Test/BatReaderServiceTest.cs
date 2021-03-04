using System.IO;
using Geo.DatReader.Constants;
using Geo.DatReader.Services;
using Xunit;

namespace Geo.DatReader.Test
{
	public class BatReaderServiceTest
	{
		private readonly IFileReaderService _service;

		public BatReaderServiceTest()
		{
			_service = new FileReaderService();
		}

		[Fact]
		public void Read_NotNull_Test()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), DataConstants.DAT_FILE_PATH);
			var result = _service.Read(path);

			Assert.NotNull(result);
		}
	}
}