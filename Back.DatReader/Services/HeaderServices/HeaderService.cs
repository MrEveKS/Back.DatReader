using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Models.Domain;
using Back.DatReader.Models.Dto.Header;

namespace Back.DatReader.Services.HeaderServices
{
	public class HeaderService : BaseApiService<Header, HeaderDto, HeaderFilterDto>, IHeaderService
	{
		public HeaderService(IQueryDtoMapper<Header, HeaderDto> queryDtoMapper)
			: base(queryDtoMapper)
		{
		}
	}
}