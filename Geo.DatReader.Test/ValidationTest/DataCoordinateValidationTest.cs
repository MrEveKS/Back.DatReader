using System.Threading.Tasks;
using Xunit;

namespace Geo.DatReader.Test.ValidationTest
{
	public class DataCoordinateValidationTest
	{
		private readonly DatDbDataSingleton _datDbDataSingleton;

		public DataCoordinateValidationTest()
		{
			_datDbDataSingleton = DatDbDataSingleton.Current;
		}

		[Fact]
		public void Data_Not_Empty()
		{
			_datDbDataSingleton.InitializeAsync();
			var coordinateInformations = _datDbDataSingleton.CoordinateInformations;

			Assert.True(coordinateInformations.Count > 0);
		}

		[Fact]
		public void All_Country_Valid_Name()
		{
			_datDbDataSingleton.InitializeAsync();
			var coordinateInformations = _datDbDataSingleton.CoordinateInformations;

			Assert.All(coordinateInformations, v => v.Country.StartsWith("cou_"));
		}

		[Fact]
		public void All_City_Valid_Name()
		{
			_datDbDataSingleton.InitializeAsync();
			var coordinateInformations = _datDbDataSingleton.CoordinateInformations;

			Assert.All(coordinateInformations, v => v.City.StartsWith("cit_"));
		}

		[Fact]
		public void All_Region_Valid_Name()
		{
			_datDbDataSingleton.InitializeAsync();
			var coordinateInformations = _datDbDataSingleton.CoordinateInformations;

			Assert.All(coordinateInformations, v => v.Region.StartsWith("reg_"));
		}

		[Fact]
		public void All_Postal_Valid_Name()
		{
			_datDbDataSingleton.InitializeAsync();
			var coordinateInformations = _datDbDataSingleton.CoordinateInformations;

			Assert.All(coordinateInformations, v => v.Postal.StartsWith("pos_"));
		}

		[Fact]
		public void All_Organization_Valid_Name()
		{
			_datDbDataSingleton.InitializeAsync();
			var coordinateInformations = _datDbDataSingleton.CoordinateInformations;

			Assert.All(coordinateInformations, v => v.Organization.StartsWith("org_"));
		}
	}
}