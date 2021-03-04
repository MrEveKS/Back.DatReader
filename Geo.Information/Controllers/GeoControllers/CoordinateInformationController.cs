using Geo.Common.Dto.CoordinateInformation;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.CoordinateInformationServices;

namespace Geo.Information.Controllers.GeoControllers
{
	public class CoordinateInformationController : BaseController<CoordinateInformationDto, CoordinateInformationFilterDto>
	{
		public CoordinateInformationController(ICoordinateInformationService service) : base(service)
		{
		}
	}
}