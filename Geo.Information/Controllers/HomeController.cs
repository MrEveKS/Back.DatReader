using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return PhysicalFile(Path.Combine(AppContext.BaseDirectory, "wwwroot", "index.html"), "text/HTML");
		}
	}
}
