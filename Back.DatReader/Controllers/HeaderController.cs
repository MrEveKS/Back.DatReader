using Microsoft.AspNetCore.Mvc;

namespace Back.DatReader.Controllers
{
	public class HeaderController : BaseController
	{
		[HttpGet]
		public JsonResult GetEntity()
		{
			return Json("");
		}
	}
}
