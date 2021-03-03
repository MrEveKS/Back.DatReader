using System.Threading.Tasks;
using Xunit;

namespace File.DatReader.Test.ValidationTest
{
	public class DataHeaderValidationTest
	{
		private  readonly DatDbData _datDbData;

		public DataHeaderValidationTest()
		{
			_datDbData = new DatDbData();
		}

		[Fact]
		public async Task Name_NotEmpty_Test()
		{
			await _datDbData.InitializeAsync();
			var header = _datDbData.Head;

			Assert.NotEmpty(header.Name);
		}
	}
}
