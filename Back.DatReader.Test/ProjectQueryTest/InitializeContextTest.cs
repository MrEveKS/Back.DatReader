using System.Diagnostics;
using System.Threading.Tasks;
using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Models.Domain;
using Back.DatReader.Models.Dto.Header;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;
using Xunit;
using Xunit.Abstractions;

namespace Back.DatReader.Test.ProjectQueryTest
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
			var filter = new HeaderFilterDto()
			{
			};
			var timer = Stopwatch.StartNew();

			var mapper = GetQueryDtoMapper();
			await GetResultAsync(mapper, filter);

			var time = timer.Elapsed.TotalMilliseconds;
			_testOutputHelper.WriteLine($"time: {time: 0.0000} ms");
		}


		protected IQueryDtoMapper<Header, HeaderDto> GetQueryDtoMapper()
		{
			return new QueryDtoMapper<Header, HeaderDto>(DbContext);
		}

		protected Task<IQueryResultDto<HeaderDto>> GetResultAsync(IQueryDtoMapper<Header, HeaderDto> mapper,
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
