using System.Diagnostics;
using System.Threading.Tasks;
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
		public async Task DatDataReader_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipLock = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				await datDataReader.InitializeAsync();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}
	}
}