using Back.DatReader.Controllers.BaseControllers;
using Back.DatReader.Models.Dto.CoordinateInformation;
using Back.DatReader.Services.CoordinateInformationServices;

namespace Back.DatReader.Controllers.GeoControllers
{
	public class CoordinateInformationController : BaseController<CoordinateInformationDto, CoordinateInformationFilterDto>
	{
		public CoordinateInformationController(ICoordinateInformationService service) : base(service)
		{
		}
	}
}