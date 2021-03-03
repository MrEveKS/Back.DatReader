using System.Threading.Tasks;
using Moq;
using Xunit;

namespace File.DatReader.Test.ValidationTest
{
	public class DataHeaderValidationTest
	{
		private  readonly DatDbDataSingleton _datDbDataSingleton;

		public DataHeaderValidationTest()
		{
			_datDbDataSingleton = DatDbDataSingleton.Current;
		}

		[Fact]
		public async Task Name_NotEmpty_Test()
		{
			await _datDbDataSingleton.InitializeAsync();
			var header = _datDbDataSingleton.Head;

			Assert.NotEmpty(header.Name);
		}
	}
}
