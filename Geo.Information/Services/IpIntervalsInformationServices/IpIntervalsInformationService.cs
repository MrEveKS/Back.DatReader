using Geo.Common.Domain;
using Geo.Common.Dto.IpIntervalsInformation;
using Geo.QueryMapper;

namespace Geo.Information.Services.IpIntervalsInformationServices
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