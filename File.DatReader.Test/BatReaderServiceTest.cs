using System.IO;
using File.DatReader.Constants;
using File.DatReader.Services;
using Xunit;

namespace File.DatReader.Test
{
	public class BatReaderServiceTest
	{
		private readonly IBatReaderService _service;

		public BatReaderServiceTest()
		{
			_service = new BatReaderService();
		}

		[Fact]
		public void Read_NotNull_Test()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), DataConstatns.BatFilePath);
			var result = _service.ReadAsync(path);

			Assert.NotNull(result);
		}
	}
}
