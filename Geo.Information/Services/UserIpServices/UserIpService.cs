using Geo.Common.Domain;
using Geo.Common.Dto.UserIp;
using Geo.QueryMapper;

namespace Geo.Information.Services.UserIpServices
{
	public class UserIpService : BaseApiService<UserIp, UserIpDto,
									UserIpFilterDto>, IUserIpService
	{
		public UserIpService(IQueryDtoMapper<UserIp, UserIpDto> queryDtoMapper) : base(queryDtoMapper)
		{
		}
	}
}