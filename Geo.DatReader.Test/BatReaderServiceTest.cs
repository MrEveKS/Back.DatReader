using System.IO;
using Geo.DatReader.Constants;
using Geo.DatReader.Services;
using Xunit;

namespace Geo.DatReader.Test
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
			var path = Path.Combine(Directory.GetCurrentDirectory(), DataConstants.DAT_FILE_PATH);
			var result = _service.ReadAsync(path);

			Assert.NotNull(result);
		}
	}
}