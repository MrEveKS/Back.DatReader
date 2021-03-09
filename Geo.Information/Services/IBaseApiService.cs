using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;

namespace Geo.Information.Services
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

		/// <summary>
		/// Get first entities by filter
		/// </summary>
		/// <param name="queryDto"> </param>
		/// <param name="cancellationToken"> </param>
		/// <returns> </returns>
		Task<TEntityDto> GetOne(QueryDto<TEntityFilterDto> queryDto, CancellationToken cancellationToken = default);
	}
}