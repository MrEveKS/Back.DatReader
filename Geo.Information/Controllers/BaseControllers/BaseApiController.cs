using Geo.Common.Dto;
using Geo.Information.Services;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.BaseControllers
{
	[Route("api/[controller]/[action]")]
	public class BaseApiController<TEntityDto, TEntityFilterDto> : BaseController<TEntityDto, TEntityFilterDto>
		where TEntityDto : EntityDto
		where TEntityFilterDto : EntityDto
	{
		public BaseApiController(IBaseApiService<TEntityDto, TEntityFilterDto> service) : base(service)
		{
		}
	}
}