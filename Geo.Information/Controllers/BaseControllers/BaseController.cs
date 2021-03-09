using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Information.Services;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.BaseControllers
{
	[Produces("application/json")]
	public abstract class BaseController<TEntityDto, TEntityFilterDto> : Controller
		where TEntityDto : EntityDto
		where TEntityFilterDto : EntityDto
	{
		protected readonly IBaseApiService<TEntityDto, TEntityFilterDto> Service;

		protected BaseController(IBaseApiService<TEntityDto, TEntityFilterDto> service)
		{
			Service = service;
		}

		[HttpPost]
		public virtual Task<IQueryResultDto<TEntityDto>> GetAll([FromBody] QueryDto<TEntityFilterDto> filter,
																CancellationToken cancellationToken = default)
		{
			return Service.GetAll(filter, cancellationToken);
		}
	}
}