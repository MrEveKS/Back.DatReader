using Geo.Common.Dto.UserLocation;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.UserLocationServices;

namespace Geo.Information.Controllers.GeoControllers
{
	public class UserLocationController : BaseController<UserLocationDto, UserLocationFilterDto>
	{
		public UserLocationController(IUserLocationService service) : base(service)
		{
		}
	}
}