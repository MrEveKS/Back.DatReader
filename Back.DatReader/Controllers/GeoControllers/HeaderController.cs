using Back.DatReader.Controllers.BaseControllers;
using Back.DatReader.Models.Dto.Header;
using Back.DatReader.Services.HeaderServices;

namespace Back.DatReader.Controllers.GeoControllers
{
	public class HeaderController : BaseController<HeaderDto, HeaderFilterDto>
	{
		public HeaderController(IHeaderService service) : base(service)
		{
		}
	}
}