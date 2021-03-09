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
		protected readonly IQueryDtoMapper<TEntity, TEntityDto> QueryDtoMapper;

		protected BaseApiService(IQueryDtoMapper<TEntity, TEntityDto> queryDtoMapper)
		{
			QueryDtoMapper = queryDtoMapper;
		}

		/// <inheritdoc />
		public virtual Task<IQueryResultDto<TEntityDto>> GetAll(QueryDto<TEntityFilterDto> queryDto,
																CancellationToken cancellationToken = default)
		{
			return QueryDtoMapper
				.QueryDto(queryDto)
				.MapQueryAsync(cancellationToken: cancellationToken);
		}

		/// <inheritdoc />
		public virtual Task<TEntityDto> GetOne(QueryDto<TEntityFilterDto> queryDto,
												CancellationToken cancellationToken = default)
		{
			return QueryDtoMapper
				.QueryDto(queryDto)
				.MapQueryOneAsync(cancellationToken: cancellationToken);
		}
	}
}