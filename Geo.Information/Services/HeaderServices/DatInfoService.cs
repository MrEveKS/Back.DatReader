using Geo.Common.Domain;
using Geo.Common.Dto.DatInfo;
using Geo.QueryMapper;

namespace Geo.Information.Services.HeaderServices
{
	public class DatInfoService : BaseApiService<DatInfo, DatInfoDto, DatInfoFilterDto>, IDatInfoService
	{
		public DatInfoService(IQueryDtoMapper<DatInfo, DatInfoDto> queryDtoMapper)
			: base(queryDtoMapper)
		{
		}
	}
}