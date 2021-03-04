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
		public void DatDataReader_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				datDataReader.InitializeAsync();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact (Skip = "test")]
		public void DatDataReader_1_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				datDataReader.ArrayInitialization();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public void DatDataReader_2_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				datDataReader.ArrayInitialization2();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public void DatDataReader_3_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				datDataReader.ArrayInitialization3();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public void DatDataReader_4_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				datDataReader.ArrayInitialization4();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public async Task DatDataReader_5_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				await datDataReader.ArrayInitialization5();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}

		[Fact]
		public async Task DatDataReader_6_Time_Test()
		{
			var datDataReader = DatDbDataSingleton.Current;
			datDataReader.SkipNoInitialize = true;

			for (var index = 0; index < 10; index++)
			{
				var timer = Stopwatch.StartNew();

				await datDataReader.InitializeAsync2();

				var time = timer.Elapsed.TotalMilliseconds;

				_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			}
		}
	}
}