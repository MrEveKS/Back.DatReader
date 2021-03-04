using Geo.Common.Domain;
using Geo.Common.Dto.Header;
using Geo.QueryMapper;

namespace Geo.Information.Services.HeaderServices
{
	public class HeaderService : BaseApiService<Header, HeaderDto, HeaderFilterDto>, IHeaderService
	{
		public HeaderService(IQueryDtoMapper<Header, HeaderDto> queryDtoMapper)
			: base(queryDtoMapper)
		{
		}
	}
}