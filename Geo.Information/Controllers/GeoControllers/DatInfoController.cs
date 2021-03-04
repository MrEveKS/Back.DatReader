using Geo.Common.Dto.DatInfo;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.HeaderServices;

namespace Geo.Information.Controllers.GeoControllers
{
	public class DatInfoController : BaseController<DatInfoDto, DatInfoFilterDto>
	{
		public DatInfoController(IDatInfoService service) : base(service)
		{
		}
	}
}