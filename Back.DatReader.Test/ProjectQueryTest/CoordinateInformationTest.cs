using System.Diagnostics;
using System.Threading.Tasks;
using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Models.Domain;
using Back.DatReader.Models.Dto.CoordinateInformation;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;
using Xunit;
using Xunit.Abstractions;

namespace Back.DatReader.Test.ProjectQueryTest
{
	public class CoordinateInformationTest : BaseProjectQueryTest
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public CoordinateInformationTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public async Task Get_Test()
		{
			var filter = new CoordinateInformationFilterDto()
			{
			};
			var timer = Stopwatch.StartNew();

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			var time = timer.Elapsed.TotalMilliseconds;
			_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			Assert.NotNull(result);
			Assert.True(result.Count > 0);
		}

		[Fact]
		public async Task Get_With_Filter_Test()
		{
			var filter = new CoordinateInformationFilterDto()
			{
				CountryContains = "EV"
			};
			var timer = Stopwatch.StartNew();

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			var time = timer.Elapsed.TotalMilliseconds;
			_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
			Assert.NotNull(result);
			Assert.True(result.Count > 0);
		}


		protected IQueryDtoMapper<CoordinateInformation, CoordinateInformationDto> GetQueryDtoMapper()
		{
			return new QueryDtoMapper<CoordinateInformation, CoordinateInformationDto>(DbContext);
		}

		protected Task<IQueryResultDto<CoordinateInformationDto>> GetResultAsync(IQueryDtoMapper<CoordinateInformation, CoordinateInformationDto> mapper,
																				CoordinateInformationFilterDto filter)
		{
			var queryDto = new QueryDto<CoordinateInformationFilterDto>
			{
				Filter = filter,
				WithCount = true
			};

			return mapper.QueryDto(queryDto).MapQueryAsync();
		}
	}
}
