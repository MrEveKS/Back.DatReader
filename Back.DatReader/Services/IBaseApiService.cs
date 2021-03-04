using System.Threading;
using System.Threading.Tasks;
using Back.DatReader.Models.Dto;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;

namespace Back.DatReader.Services
{
	public interface IBaseApiService<TEntityDto, TEntityFilterDto>
		where TEntityDto : EntityDto
		where TEntityFilterDto : EntityDto
	{
		/// <summary>
		/// Get all entities by filter
		/// </summary>
		/// <param name="queryDto"> </param>
		/// <param name="cancellationToken"> </param>
		/// <returns> </returns>
		Task<IQueryResultDto<TEntityDto>> GetAll(QueryDto<TEntityFilterDto> queryDto,
												CancellationToken cancellationToken = default);
	}
}