using System.Diagnostics;
using System.Threading.Tasks;
using Geo.Common.Domain;
using Geo.Common.Dto.DatInfo;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.QueryMapper;
using Xunit;
using Xunit.Abstractions;

namespace Geo.Information.Test.ProjectQueryTest
{
	public class InitializeContextTest : BaseProjectQueryTest
	{
		private readonly ITestOutputHelper _testOutputHelper;

		public InitializeContextTest(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		[Fact]
		public async Task InitializeTime_Test()
		{
			var filter = new DatInfoFilterDto();
			var timer = Stopwatch.StartNew();

			var mapper = GetQueryDtoMapper();
			await GetResultAsync(mapper, filter);

			var time = timer.Elapsed.TotalMilliseconds;
			_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
		}

		private IQueryDtoMapper<DatInfo, DatInfoDto> GetQueryDtoMapper()
		{
			return new QueryDtoMapper<DatInfo, DatInfoDto>(DbContext);
		}

		private Task<IQueryResultDto<DatInfoDto>> GetResultAsync(IQueryDtoMapper<DatInfo, DatInfoDto> mapper,
																DatInfoFilterDto filter)
		{
			var queryDto = new QueryDto<DatInfoFilterDto>
			{
				Filter = filter,
				WithCount = true
			};

			return mapper.QueryDto(queryDto).MapQueryAsync();
		}
	}
}