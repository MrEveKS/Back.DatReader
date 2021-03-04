using System.Diagnostics;
using System.Threading.Tasks;
using Geo.Common.Domain;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Common.Dto.UserLocation;
using Geo.QueryMapper;
using Xunit;
using Xunit.Abstractions;

namespace Geo.Information.Test.ProjectQueryTest
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
			var filter = new UserLocationFilterDto();
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
			var filter = new UserLocationFilterDto
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

		private IQueryDtoMapper<UserLocation, UserLocationDto> GetQueryDtoMapper()
		{
			return new QueryDtoMapper<UserLocation, UserLocationDto>(DbContext);
		}

		private Task<IQueryResultDto<UserLocationDto>> GetResultAsync(IQueryDtoMapper<UserLocation, UserLocationDto> mapper,
																	UserLocationFilterDto filter)
		{
			var queryDto = new QueryDto<UserLocationFilterDto>
			{
				Filter = filter,
				WithCount = true
			};

			return mapper.QueryDto(queryDto).MapQueryAsync();
		}
	}
}