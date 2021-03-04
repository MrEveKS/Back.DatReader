using Back.DatReader.Controllers.BaseControllers;
using Back.DatReader.Models.Dto.IpIntervalsInformation;
using Back.DatReader.Services.IpIntervalsInformationServices;

namespace Back.DatReader.Controllers.GeoControllers
{
	public class IpIntervalsInformationController : BaseController<IpIntervalsInformationDto, IpIntervalsInformationFilterDto>
	{
		public IpIntervalsInformationController(IIpIntervalsInformationService service) : base(service)
		{
		}
	}
}