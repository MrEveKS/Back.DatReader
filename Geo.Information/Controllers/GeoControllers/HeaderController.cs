using Geo.Common.Dto.Header;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.HeaderServices;

namespace Geo.Information.Controllers.GeoControllers
{
	public class HeaderController : BaseController<HeaderDto, HeaderFilterDto>
	{
		public HeaderController(IHeaderService service) : base(service)
		{
		}
	}
}