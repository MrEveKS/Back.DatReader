using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.UserIp;
using Geo.Common.Dto.UserLocation;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.UserIpServices;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.GeoControllers
{
	public class UserIpController : BaseApiController<UserIpDto, UserIpFilterDto>
	{
		public UserIpController(IUserIpService service) : base(service)
		{
		}

		[HttpPost]
		public Task<UserLocationDto> GetUserLocation([FromBody] QueryDto<UserIpFilterDto> filter,
													CancellationToken cancellationToken = default)
		{
			return ((IUserIpService) Service).GetUserLocation(filter, cancellationToken);
		}
	}
}