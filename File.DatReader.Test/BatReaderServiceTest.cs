using System.IO;
using System.Threading.Tasks;
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
		public async Task Read_NotNull_Test()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), "data/geobase.dat");
			var result = await _service.ReadAsync(path);

			Assert.NotNull(result);
		}
	}
}
