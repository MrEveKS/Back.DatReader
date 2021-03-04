using System.Threading.Tasks;
using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;
using Back.DatReader.Test.QueryDtoTest.Models;

namespace Back.DatReader.Test.QueryDtoTest
{
	public class BaseQueryDtoMapperTest
	{
		public QueryDtoMapperTestDbContext DbContext = QueryDtoMapperTestDbContext.GetDbContext();

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