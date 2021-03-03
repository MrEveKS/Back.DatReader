using Microsoft.AspNetCore.Mvc;

namespace Back.DatReader.Controllers
{
	[Route("api/[controller]/[action]")]
	[Produces("application/json")]
	public abstract class BaseController : Controller
	{
	}
}
