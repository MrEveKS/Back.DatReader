using Geo.Common.Domain;
using Geo.Common.Dto.UserLocation;
using Geo.QueryMapper;

namespace Geo.Information.Services.UserLocationServices
{
	public class UserLocationService : BaseApiService<UserLocation, UserLocationDto,
											UserLocationFilterDto>, IUserLocationService
	{
		public UserLocationService(IQueryDtoMapper<UserLocation, UserLocationDto> queryDtoMapper) : base(queryDtoMapper)
		{
		}
	}
}