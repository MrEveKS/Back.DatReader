using System.Linq;
using System.Threading.Tasks;
using Geo.Common.Domain;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Common.Dto.UserIp;
using Geo.Information.Services;
using Geo.QueryMapper;
using Xunit;

namespace Geo.Information.Test.IpAddressTest
{
	public class DatIpTest : BaseProjectQueryTest
	{
		private readonly IpAddressConverterService _service;

		public DatIpTest()
		{
			_service = new IpAddressConverterService();
		}

		[Fact]
		public async Task AllIpFromValid_Test()
		{
			var filter = new UserIpFilterDto();
			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);
			Assert.NotNull(result);
			Assert.True(result.Items?.Count > 0);

			Assert.True(result.Items.All(item => item.IpFrom.HasValue && !string.IsNullOrEmpty(_service.ConvertFromIntegerToIpAddress(item.IpFrom.Value))));
		}

		[Fact]
		public async Task AllIpToValid_Test()
		{
			var filter = new UserIpFilterDto();
			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);
			Assert.NotNull(result);
			Assert.True(result.Items?.Count > 0);

			Assert.True(result.Items.All(item => item.IpTo.HasValue && !string.IsNullOrEmpty(_service.ConvertFromIntegerToIpAddress(item.IpTo.Value))));
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