using Geo.Common.Domain;
using Geo.Common.Dto.CoordinateInformation;
using Geo.QueryMapper;

namespace Geo.Information.Services.CoordinateInformationServices
{
	public class CoordinateInformationService : BaseApiService<CoordinateInformation, CoordinateInformationDto,
													CoordinateInformationFilterDto>, ICoordinateInformationService
	{
		public CoordinateInformationService(IQueryDtoMapper<CoordinateInformation, CoordinateInformationDto> queryDtoMapper) : base(
			queryDtoMapper)
		{
		}
	}
}