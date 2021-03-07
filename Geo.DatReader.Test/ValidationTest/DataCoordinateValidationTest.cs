using System.Threading.Tasks;
using Xunit;

namespace Geo.DatReader.Test.ValidationTest
{
	public class DataCoordinateValidationTest
	{
		private readonly DatDbData _datDbData;

		public DataCoordinateValidationTest()
		{
			_datDbData = new DatDbData();
		}

		[Fact]
		public async Task Data_Not_Empty()
		{
			await _datDbData.InitializeAsync();
			var coordinateInformations = _datDbData.UserLocations;

			Assert.True(coordinateInformations.Count > 0);
		}

		[Fact]
		public async Task All_Country_Valid_Name()
		{
			await _datDbData.InitializeAsync();
			var coordinateInformations = _datDbData.UserLocations;

			Assert.All(coordinateInformations, v => v.Country.StartsWith("cou_"));
		}

		[Fact]
		public async Task All_City_Valid_Name()
		{
			await _datDbData.InitializeAsync();
			var coordinateInformations = _datDbData.UserLocations;

			Assert.All(coordinateInformations, v => v.City.StartsWith("cit_"));
		}

		[Fact]
		public async Task All_Region_Valid_Name()
		{
			await _datDbData.InitializeAsync();
			var coordinateInformations = _datDbData.UserLocations;

			Assert.All(coordinateInformations, v => v.Region.StartsWith("reg_"));
		}

		[Fact]
		public async Task All_Postal_Valid_Name()
		{
			await _datDbData.InitializeAsync();
			var coordinateInformations = _datDbData.UserLocations;

			Assert.All(coordinateInformations, v => v.Postal.StartsWith("pos_"));
		}

		[Fact]
		public async Task All_Organization_Valid_Name()
		{
			await _datDbData.InitializeAsync();
			var coordinateInformations = _datDbData.UserLocations;

			Assert.All(coordinateInformations, v => v.Organization.StartsWith("org_"));
		}
	}
}