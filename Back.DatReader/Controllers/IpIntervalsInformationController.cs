using Back.DatReader.Models.Dto.IpIntervalsInformation;
using Back.DatReader.Services.IpIntervalsInformationServices;

namespace Back.DatReader.Controllers
{
	public class IpIntervalsInformationController : BaseController<IpIntervalsInformationDto, IpIntervalsInformationFilterDto>
	{
		public IpIntervalsInformationController(IIpIntervalsInformationService service) : base(service)
		{
		}
	}
}