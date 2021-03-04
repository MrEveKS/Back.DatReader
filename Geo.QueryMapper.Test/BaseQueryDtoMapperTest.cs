using System.Threading.Tasks;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.QueryMapper.Test.Models;

namespace Geo.QueryMapper.Test
{
	public class BaseQueryDtoMapperTest
	{
		private readonly QueryDtoMapperTestDbContext DbContext = QueryDtoMapperTestDbContext.GetDbContext();

		protected IQueryDtoMapper<Organization, OrganizationListDto> GetQueryDtoMapper()
		{
			return new QueryDtoMapper<Organization, OrganizationListDto>(DbContext);
		}

		protected Task<IQueryResultDto<OrganizationListDto>> GetResultAsync(IQueryDtoMapper<Organization, OrganizationListDto> mapper,
																			OrganizationFilterDto filter)
		{
			var queryDto = new QueryDto<OrganizationFilterDto>
			{
				Filter = filter,
				WithCount = true
			};

			return mapper.QueryDto(queryDto).MapQueryAsync();
		}
	}
}