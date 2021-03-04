using System.IO;
using System.Threading.Tasks;
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
		public async Task Read_NotNull_Test()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), DataConstants.DAT_FILE_PATH);
			await using var result = _service.Read(path);

			Assert.NotNull(result);
		}
	}
}