using Geo.Common.Dto.UserLocation;

namespace Geo.Information.Services.UserLocationServices
{
	public interface IUserLocationService : IBaseApiService<UserLocationDto, UserLocationFilterDto>
	{
	}
}