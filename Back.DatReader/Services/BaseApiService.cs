using System.Threading;
using System.Threading.Tasks;
using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Models.Domain;
using Back.DatReader.Models.Dto;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;

namespace Back.DatReader.Services
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