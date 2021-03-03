using System.Diagnostics;
using System.Threading.Tasks;
using DatReader;
using Xunit;
using Xunit.Abstractions;

namespace File.DatReader.Test
{
    public class DataValidTest
    {

		private readonly ITestOutputHelper _testOutputHelper;

		public DataValidTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public async Task DatDataReader_Time_Test()
		{
			var datDataReader = new DatDataReader();

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				await datDataReader.InitializeAsync();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.000}");
			}
		}
    }
}
