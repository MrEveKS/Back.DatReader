using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;

namespace Geo.QueryMapper
{
	/// <summary>
	/// Query dto mapper Implementation
	/// </summary>
	/// <typeparam name="TEntity"> </typeparam>
	/// <typeparam name="TResultDto"> </typeparam>
	public interface IQueryDtoMapper<TEntity, TResultDto>
		where TEntity : class, new()
		where TResultDto : class
	{
		/// <summary>
		/// Request
		/// </summary>
		IQueryable<TEntity> Query { get; set; }

		/// <summary>
		/// A query that is set to the TListDto type
		/// </summary>
		IQueryable<TResultDto> QueryResult { get; set; }

		/// <summary>
		/// Set up a projection for a single field
		/// </summary>
		/// <typeparam name="T"> field type </typeparam>
		/// <param name="selector"> selects a custom field </param>
		/// <param name="setter"> set value </param>
		/// <returns> </returns>
		IQueryDtoMapper<TEntity, TResultDto> CustomizeProjection<T>(Expression<Func<TResultDto, T>> selector,
																	Expression<Func<TEntity, T>> setter);

		/// <summary>
		/// Sets the QueryDto class that defines which records to get
		/// </summary>
		/// <typeparam name="TFilter"> the filter type specifies the filter conditions </typeparam>
		/// <param name="queryDto"> QueryDto instance </param>
		/// <param name="updateQuery"> need update query </param>
		/// <returns> the object mapper </returns>
		IQueryDtoMapper<TEntity, TResultDto> QueryDto<TFilter>(QueryDto<TFilter> queryDto, bool updateQuery = false)
			where TFilter : class;

		/// <summary>
		/// Run the query
		/// </summary>
		/// <param name="isSetProjection">
		/// do I need to perform a standard projection
		/// results of a query on ListDto
		/// </param>
		/// <param name="cancellationToken"> </param>
		/// <returns> query result </returns>
		Task<IQueryResultDto<TResultDto>> MapQueryAsync(bool isSetProjection = true,
														CancellationToken cancellationToken = default);

		/// <summary>
		/// Run the query with first entity
		/// </summary>
		/// <param name="isSetProjection"> </param>
		/// <param name="cancellationToken"> </param>
		/// <returns> </returns>
		Task<TResultDto> MapQueryOneAsync(bool isSetProjection = true,
										CancellationToken cancellationToken = default);

		/// <summary>
		/// Set the projection of query results to dto
		/// </summary>
		/// <typeparam name="T"> type of request object </typeparam>
		/// <param name="query"> request </param>
		/// <param name="entityPropertyInfo">
		/// field that corresponds to the entity type
		/// QueryDtoMapper in the request object type
		/// </param>
		/// <returns> Query set projection </returns>
		IQueryable<TResultDto> SetProjection<T>(IQueryable<T> query, PropertyInfo entityPropertyInfo = null);

		/// <summary>
		/// Apply a filter to a query
		/// </summary>
		/// <param name="updateQuery"> need update query </param>
		/// <typeparam name="TFilter"> filter type </typeparam>
		void MapFilter<TFilter>(bool updateQuery)
			where TFilter : class;
	}
}