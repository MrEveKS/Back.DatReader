using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Models.Domain;
using Back.DatReader.Models.Dto.CoordinateInformation;

namespace Back.DatReader.Services.CoordinateInformationServices
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