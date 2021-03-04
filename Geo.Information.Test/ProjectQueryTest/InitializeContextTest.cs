using System.Diagnostics;
using System.Threading.Tasks;
using Geo.Common.Domain;
using Geo.Common.Dto.Header;
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
			var filter = new HeaderFilterDto();
			var timer = Stopwatch.StartNew();

			var mapper = GetQueryDtoMapper();
			await GetResultAsync(mapper, filter);

			var time = timer.Elapsed.TotalMilliseconds;
			_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
		}

		private IQueryDtoMapper<Header, HeaderDto> GetQueryDtoMapper()
		{
			return new QueryDtoMapper<Header, HeaderDto>(DbContext);
		}

		private Task<IQueryResultDto<HeaderDto>> GetResultAsync(IQueryDtoMapper<Header, HeaderDto> mapper,
																HeaderFilterDto filter)
		{
			var queryDto = new QueryDto<HeaderFilterDto>
			{
				Filter = filter,
				WithCount = true
			};

			return mapper.QueryDto(queryDto).MapQueryAsync();
		}
	}
}