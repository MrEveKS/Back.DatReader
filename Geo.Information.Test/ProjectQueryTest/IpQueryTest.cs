using System.Linq;
using System.Threading.Tasks;
using Geo.Common.Domain;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Common.Dto.UserIp;
using Geo.QueryMapper;
using Xunit;

namespace Geo.Information.Test.ProjectQueryTest
{
	public class IpQueryTest : BaseProjectQueryTest
	{
		[Theory]
		[InlineData(9595959)]
		[InlineData(100000)]
		[InlineData(457823)]
		[InlineData(55555)]
		public async Task Query_IpFrom_GreaterEqual_Test(uint ipFromGreaterEqual)
		{
			var filter = new UserIpFilterDto
			{
				IpFromGreaterEqual = ipFromGreaterEqual
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotNull(result);
			Assert.True(result.Items?.Count > 0);
			Assert.True(result.Items.All(item => item.IpFrom.HasValue && item.IpFrom >= ipFromGreaterEqual));
		}

		[Theory]
		[InlineData(9595959)]
		[InlineData(100000)]
		[InlineData(457823)]
		[InlineData(55555)]
		public async Task Query_IpTo_LessEqual_Test(uint ipToLessEqual)
		{
			var filter = new UserIpFilterDto
			{
				IpToLessEqual = ipToLessEqual
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotNull(result);
			Assert.True(result.Items?.Count > 0);
			Assert.True(result.Items.All(item => item.IpFrom.HasValue && item.IpTo <= ipToLessEqual));
		}

		private IQueryDtoMapper<UserIp, UserIpDto> GetQueryDtoMapper()
		{
			return new QueryDtoMapper<UserIp, UserIpDto>(DbContext);
		}

		private Task<IQueryResultDto<UserIpDto>> GetResultAsync(IQueryDtoMapper<UserIp, UserIpDto> mapper,
																UserIpFilterDto filter)
		{
			var queryDto = new QueryDto<UserIpFilterDto>
			{
				Filter = filter,
				WithCount = true
			};

			return mapper.QueryDto(queryDto).MapQueryAsync();
		}
	}
}