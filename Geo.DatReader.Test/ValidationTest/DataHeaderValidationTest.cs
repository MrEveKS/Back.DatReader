using System.Threading.Tasks;
using Xunit;

namespace Geo.DatReader.Test.ValidationTest
{
	public class DataHeaderValidationTest
	{
		private readonly DatDbDataSingleton _datDbDataSingleton;

		public DataHeaderValidationTest()
		{
			_datDbDataSingleton = DatDbDataSingleton.Current;
		}

		[Fact]
		public void Name_NotEmpty_Test()
		{
			_datDbDataSingleton.InitializeAsync();
			var header = _datDbDataSingleton.Head;

			Assert.NotEmpty(header.Name);
		}
	}
}