using Geo.Common.Dto.IpIntervalsInformation;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.IpIntervalsInformationServices;

namespace Geo.Information.Controllers.GeoControllers
{
	public class IpIntervalsInformationController : BaseController<IpIntervalsInformationDto, IpIntervalsInformationFilterDto>
	{
		public IpIntervalsInformationController(IIpIntervalsInformationService service) : base(service)
		{
		}
	}
}