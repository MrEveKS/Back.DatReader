using Back.DatReader.Models.Dto.CoordinateInformation;
using Back.DatReader.Services.CoordinateInformationServices;

namespace Back.DatReader.Controllers
{
	public class CoordinateInformationController : BaseController<CoordinateInformationDto, CoordinateInformationFilterDto>
	{
		public CoordinateInformationController(ICoordinateInformationService service) : base(service)
		{
		}
	}
}