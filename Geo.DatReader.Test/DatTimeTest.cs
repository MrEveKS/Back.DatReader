using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Geo.DatReader.Constants;
using Geo.DatReader.Services;
using Xunit;
using Xunit.Abstractions;

namespace Geo.DatReader.Test
{
	public class DatTimeTest
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public DatTimeTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public async Task DatInitialize_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				await datDataReader.InitializeAsync();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public async Task HeadInitialization_Time_Test()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), DataConstants.DAT_FILE_PATH);
			await using var stream = new FileReaderService().Read(path);

			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				datDataReader.HeadInitialize(stream);

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public async Task BodyRead_Time_Test()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), DataConstants.DAT_FILE_PATH);
			await using var stream = new FileReaderService().Read(path);

			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;
			datDataReader.HeadInitialize(stream);

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				datDataReader.BodyRead(stream);
				
				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public async Task BodyInitialize_Time_Test()
		{
			var path = Path.Combine(Directory.GetCurrentDirectory(), DataConstants.DAT_FILE_PATH);
			await using var stream = new FileReaderService().Read(path);

			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;
			datDataReader.HeadInitialize(stream);

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				await datDataReader.BodyInitialize(stream);

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}
	}
}