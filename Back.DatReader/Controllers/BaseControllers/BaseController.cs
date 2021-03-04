using System.Threading;
using System.Threading.Tasks;
using Back.DatReader.Models.Dto;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;
using Back.DatReader.Services;
using Microsoft.AspNetCore.Mvc;

namespace Back.DatReader.Controllers.BaseControllers
{
	[Route("api/[controller]/[action]")]
	[Produces("application/json")]
	public abstract class BaseController<TEntityDto, TEntityFilterDto> : Controller
		where TEntityDto : EntityDto
		where TEntityFilterDto : EntityDto
	{
		protected readonly IBaseApiService<TEntityDto, TEntityFilterDto> _service;

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