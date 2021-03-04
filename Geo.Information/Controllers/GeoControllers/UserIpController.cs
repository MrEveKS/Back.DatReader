using Geo.Common.Dto.UserIp;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.UserIpServices;

namespace Geo.Information.Controllers.GeoControllers
{
	public class UserIpController : BaseController<UserIpDto, UserIpFilterDto>
	{
		public UserIpController(IUserIpService service) : base(service)
		{
		}
	}
}