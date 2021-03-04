using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Information.Services;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.BaseControllers
{
	[Route("api/[controller]/[action]")]
	[Produces("application/json")]
	public abstract class BaseController<TEntityDto, TEntityFilterDto> : Controller
		where TEntityDto : EntityDto
		where TEntityFilterDto : EntityDto
	{
		private readonly IBaseApiService<TEntityDto, TEntityFilterDto> _service;

		protected BaseController(IBaseApiService<TEntityDto, TEntityFilterDto> service)
		{
			_service = service;
		}

		[HttpPost]
		public virtual Task<IQueryResultDto<TEntityDto>> GetAll([FromBody] QueryDto<TEntityFilterDto> filter,
																CancellationToken cancellationToken = default)
		{
			return _service.GetAll(filter, cancellationToken);
		}
	}
}