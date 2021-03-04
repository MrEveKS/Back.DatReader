using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Models.Domain;
using Back.DatReader.Models.Dto.IpIntervalsInformation;

namespace Back.DatReader.Services.IpIntervalsInformationServices
{
	public class IpIntervalsInformationService : BaseApiService<IpIntervalsInformation, IpIntervalsInformationDto,
													IpIntervalsInformationFilterDto>, IIpIntervalsInformationService
	{
		public IpIntervalsInformationService(IQueryDtoMapper<IpIntervalsInformation, IpIntervalsInformationDto> queryDtoMapper) : base(
			queryDtoMapper)
		{
		}
	}
}