using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.UserIp;
using Geo.Common.Dto.UserLocation;

namespace Geo.Information.Services.UserIpServices
{
	public interface IUserIpService : IBaseApiService<UserIpDto, UserIpFilterDto>
	{
		Task<UserLocationDto> GetUserLocation(QueryDto<UserIpFilterDto> queryDto,
											CancellationToken cancellationToken = default);
	}
}