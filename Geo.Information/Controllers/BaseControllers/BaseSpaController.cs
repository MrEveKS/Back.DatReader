using Geo.Common.Dto;
using Geo.Information.Services;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.BaseControllers
{
	[Route("[controller]/[action]")]
	public class BaseSpaController<TEntityDto, TEntityFilterDto> : BaseController<TEntityDto, TEntityFilterDto>
		where TEntityDto : EntityDto
		where TEntityFilterDto : EntityDto
	{
		public BaseSpaController(IBaseApiService<TEntityDto, TEntityFilterDto> service) : base(service)
		{
		}
	}
}