using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Domain;
using Geo.Common.Dto;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.QueryMapper;

namespace Geo.Information.Services
{
	public abstract class BaseApiService<TEntity, TEntityDto, TEntityFilterDto> : IBaseApiService<TEntityDto, TEntityFilterDto>
		where TEntity : DbEntity, new()
		where TEntityDto : EntityDto
		where TEntityFilterDto : EntityDto
	{
		protected readonly IQueryDtoMapper<TEntity, TEntityDto> _queryDtoMapper;

		protected BaseApiService(IQueryDtoMapper<TEntity, TEntityDto> queryDtoMapper)
		{
			_queryDtoMapper = queryDtoMapper;
		}

		/// <summary>
		/// Get all entities by filter
		/// </summary>
		/// <param name="queryDto"> </param>
		/// <param name="cancellationToken"> </param>
		/// <returns> </returns>
		public virtual Task<IQueryResultDto<TEntityDto>> GetAll(QueryDto<TEntityFilterDto> queryDto,
																CancellationToken cancellationToken = default)
		{
			return _queryDtoMapper
				.QueryDto(queryDto)
				.MapQueryAsync(cancellationToken: cancellationToken);
		}
	}
}