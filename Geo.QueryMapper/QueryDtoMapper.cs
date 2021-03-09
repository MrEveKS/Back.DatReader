using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Constants;
using Geo.Common.Domain;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.QueryMapper.Enums;
using Geo.QueryMapper.Extensions;
using Geo.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Geo.QueryMapper
{
	/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}" />
	/// />
	public class QueryDtoMapper<TEntity, TResultDto> : IQueryDtoMapper<TEntity, TResultDto>
		where TEntity : class, new()
		where TResultDto : class
	{
		private readonly DbContext _dbContext;

		private readonly string _entityKeyPropertyName;

		private readonly PropertyInfo[] _entityProperties = typeof(TEntity).GetProperties();

		private readonly PropertyInfo[] _listProperties = typeof(TResultDto).GetProperties();

		/// <summary>
		/// Projection settings
		/// </summary>
		private IDictionary<PropertyInfo, Expression> _customProjections;

		/// <summary>
		/// Object defining filter settings
		/// </summary>
		private QueryDtoBase _queryDto;

		public QueryDtoMapper(DbContext dbContext)
		{
			_dbContext = dbContext;
			_entityKeyPropertyName = GetPrimaryKey(typeof(TEntity));
		}

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.Query" />
		/// />
		public IQueryable<TEntity> Query { get; set; }

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.QueryResult" />
		/// />
		public IQueryable<TResultDto> QueryResult { get; set; }

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.QueryDto{TFilter}" />
		/// />
		public virtual IQueryDtoMapper<TEntity, TResultDto> QueryDto<TFilter>(QueryDto<TFilter> queryDto, bool updateQuery)
			where TFilter : class
		{
			_queryDto = queryDto;

			MapFilter<TFilter>(updateQuery);

			return this;
		}

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.MapQueryAsync" />
		/// />
		public virtual async Task<IQueryResultDto<TResultDto>> MapQueryAsync(bool isSetProjection = true,
																			CancellationToken cancellationToken = default)
		{
			QueryResult = isSetProjection ? SetProjection(Query) : QueryResult;

			QueryResult = SetOrder(QueryResult);

			var result = new QueryResultDto<TResultDto>();

			if (_queryDto != null && _queryDto.WithCount == true)
			{
				result.Count = await QueryResult.LongCountAsync(cancellationToken);
			}

			QueryResult = QueryResult.Skip(_queryDto?.Skip.GetValueOrDefault() ?? 0);

			QueryResult = QueryResult.Take(_queryDto?.Take ?? QueryConstants.TAKE_DEFAULT_COUNT);

			cancellationToken.ThrowIfCancellationRequested();

			result.Items = await QueryResult.ToListAsync(cancellationToken);

			return result;
		}

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.MapQueryOneAsync" />
		public virtual Task<TResultDto> MapQueryOneAsync(bool isSetProjection = true,
														CancellationToken cancellationToken = default)
		{
			QueryResult = isSetProjection ? SetProjection(Query) : QueryResult;

			QueryResult = SetOrder(QueryResult);

			cancellationToken.ThrowIfCancellationRequested();

			return QueryResult.FirstOrDefaultAsync(cancellationToken);
		}

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.CustomizeProjection{T}" />
		/// />
		public IQueryDtoMapper<TEntity, TResultDto> CustomizeProjection<T>(Expression<Func<TResultDto, T>> selector,
																			Expression<Func<TEntity, T>> setter)
		{
			var propertyName = GetCorrectPropertyName(selector);
			var property = typeof(TResultDto).GetProperty(propertyName);

			_customProjections ??= new Dictionary<PropertyInfo, Expression>();

			if (_customProjections.ContainsKey(property!))
			{
				_customProjections[property] = setter;
			} else
			{
				_customProjections.Add(property, setter);
			}

			return this;
		}

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.SetProjection{T}" />
		/// />
		public virtual IQueryable<TResultDto> SetProjection<T>(IQueryable<T> query, PropertyInfo entityPropertyInfo = null)
		{
			var entityParameter = Expression.Parameter(typeof(T));
			var properties = typeof(T).GetProperties();

			var result = Expression.New(typeof(TResultDto));

			var memberAssignments = new List<MemberAssignment>();

			foreach (var listPropertyInfo in _listProperties)
			{
				if (_customProjections != null && _customProjections.TryGetValue(listPropertyInfo, out var bindExpression))
				{
					if (bindExpression != null)
					{
						var bindCall = Expression.Invoke(bindExpression, entityParameter);
						var bind = Expression.Bind(listPropertyInfo, bindCall);
						memberAssignments.Add(bind);
					}

					continue;
				}

				var entityProperty = _entityProperties.FirstOrDefault(ep => listPropertyInfo.Name == ep.Name)
									?? _entityProperties.FirstOrDefault(ep => IsMatchProperty(listPropertyInfo.Name, ep.Name));

				MemberExpression entityPropertyExpression = null;

				if (entityProperty == null)
				{
					if (entityPropertyInfo == null)
					{
						continue;
					}

					entityProperty =
						properties.FirstOrDefault(e =>
							string.Equals(e.Name, listPropertyInfo.Name, StringComparison.CurrentCultureIgnoreCase));

					if (entityProperty == null)
					{
						continue;
					}

					entityPropertyExpression = Expression.Property(entityParameter, entityProperty);
				}

				if (entityPropertyExpression == null)
				{
					if (entityPropertyInfo == null)
					{
						entityPropertyExpression = Expression.Property(entityParameter, entityProperty);
					} else
					{
						var exp = Expression.Property(entityParameter, entityPropertyInfo);
						entityPropertyExpression = Expression.Property(exp, entityProperty);
					}
				}

				MemberAssignment propertyBind = null;

				if (typeof(DbEntity).IsAssignableFrom(entityProperty.PropertyType))
				{
					propertyBind = SetEntityProjection(entityProperty, entityPropertyExpression, listPropertyInfo);
				} else if (!(typeof(IEnumerable).IsAssignableFrom(entityProperty.PropertyType)
							&& typeof(string) != entityProperty.PropertyType))
				{
					propertyBind = Expression.Bind(listPropertyInfo,
						listPropertyInfo.PropertyType == entityProperty.PropertyType
							? entityPropertyExpression
							: (Expression) Expression.Convert(entityPropertyExpression, listPropertyInfo.PropertyType));
				}

				if (propertyBind != null)
				{
					memberAssignments.Add(propertyBind);
				}
			}

			var memberInit = Expression.MemberInit(result, memberAssignments);
			var expression = Expression.Lambda<Func<T, TResultDto>>(memberInit, entityParameter);

			return query.Select(expression);
		}

		/// <inheritdoc cref="IQueryDtoMapper{TEntity,TResultDto}.MapFilter{TFilter}" />
		/// />
		public virtual void MapFilter<TFilter>(bool updateQuery)
			where TFilter : class
		{
			if (updateQuery || Query == null)
			{
				Query = _dbContext.Set<TEntity>().AsNoTracking();
			}

			var filter = (_queryDto as QueryDto<TFilter>)?.Filter;

			if (filter == null)
			{
				return;
			}

			foreach (var filterProperty in GetFilterProperties(filter))
			{
				foreach (var entityProperty in _entityProperties.Where(ep => IsMatchProperty(filterProperty.Name, ep.Name)))
				{
					if (SetFilterValue(filter, filterProperty, entityProperty))
					{
						break;
					}
				}
			}
		}

		/// <summary>
		/// Get correct property name
		/// </summary>
		/// <typeparam name="T"> </typeparam>
		/// <param name="expression"> </param>
		/// <returns> </returns>
		/// <remarks> long / int </remarks>
		private string GetCorrectPropertyName<T>(Expression<Func<TResultDto, T>> expression)
		{
			if (expression.Body is MemberExpression memberExpression)
			{
				return memberExpression.Member.Name;
			}

			var op = ((UnaryExpression) expression.Body).Operand;

			return ((MemberExpression) op).Member.Name;
		}

		/// <summary>
		/// Apply projection for single fields
		/// </summary>
		/// <param name="entityProperty"> </param>
		/// <param name="entityPropertyExpression"> </param>
		/// <param name="listProperty"> </param>
		/// <returns> </returns>
		private MemberAssignment SetEntityProjection(PropertyInfo entityProperty,
													MemberExpression entityPropertyExpression, PropertyInfo listProperty)
		{
			var propertyType = entityProperty.PropertyType;
			var startPropertyExpression = entityPropertyExpression;

			var childFieldTail = UpdateName(listProperty.Name, entityProperty.Name);

			var childFieldProperty = propertyType.GetProperty(childFieldTail);

			while (childFieldProperty == null && childFieldTail.Length > 0)
			{
				childFieldProperty = propertyType.GetProperties().FirstOrDefault(ep => IsMatchProperty(childFieldTail, ep.Name));

				if (childFieldProperty == null)
				{
					break;
				}

				startPropertyExpression = Expression.Property(startPropertyExpression, childFieldProperty);

				propertyType = childFieldProperty.PropertyType;

				childFieldTail = childFieldTail.Remove(0, childFieldProperty.Name.Length);
				childFieldProperty = propertyType.GetProperty(childFieldTail);
			}

			if (childFieldProperty == null)
			{
				return null;
			}

			var childProperty = Expression.Property(startPropertyExpression, childFieldProperty);

			var result = Expression.Bind(listProperty,
				listProperty.PropertyType == childFieldProperty.PropertyType
					? childProperty
					: (Expression) Expression.Convert(childProperty, listProperty.PropertyType));

			return result;
		}

		/// <summary>
		/// Set the sorting order for query results
		/// </summary>
		/// <param name="query"> query to be sorted </param>
		/// <returns> the query is sorted </returns>
		private IQueryable<TResultDto> SetOrder(IQueryable<TResultDto> query)
		{
			if (_queryDto == null || _queryDto.Order.IsEmpty())
			{
				return query;
			}

			query = query.OrderByProperty(_queryDto.Order);

			return query;
		}

		/// <summary>
		/// Set filter to property
		/// </summary>
		/// <typeparam name="TFilter"> </typeparam>
		/// <param name="filter"> </param>
		/// <param name="filterProperty"> </param>
		/// <param name="entityProperty"> </param>
		/// <returns> </returns>
		private bool SetFilterValue<TFilter>(TFilter filter, PropertyInfo filterProperty, PropertyInfo entityProperty)
			where TFilter : class
		{
			var filterValue = filterProperty.GetValue(filter);

			var (expression, parameterExpression) = ApplyFilter(filterValue, filterProperty, entityProperty);

			if (expression == null)
			{
				return false;
			}

			var filterLambda = Expression.Lambda<Func<TEntity, bool>>(expression, parameterExpression);
			Query = Query.Where(filterLambda);

			return true;
		}

		private (Expression, ParameterExpression) ApplyFilter(object filterValue, PropertyInfo filterProperty, PropertyInfo entityProperty)
		{
			var entityPropertyParameter = Expression.Parameter(typeof(TEntity));
			var entityPropertyExpression = Expression.Property(entityPropertyParameter, entityProperty);

			if (typeof(DbEntity).IsAssignableFrom(entityProperty.PropertyType))
			{
				var propertyType = entityProperty.PropertyType;
				var startExpression = entityPropertyExpression;

				var tail = GetTail(filterProperty.Name);

				var childFieldName = UpdateName(filterProperty.Name
						.Remove(filterProperty.Name.Length - tail.Length, tail.Length),
					entityProperty.Name);

				var childFieldProperty = propertyType.GetProperty(childFieldName);

				while (childFieldProperty == null && childFieldName.Length > 0)
				{
					childFieldProperty = propertyType
						.GetProperties()
						.FirstOrDefault(ep => IsMatchProperty(childFieldName, ep.Name));

					if (childFieldProperty == null)
					{
						break;
					}

					startExpression = Expression.Property(startExpression, childFieldProperty);
					propertyType = childFieldProperty.PropertyType;

					childFieldName = childFieldName.Remove(0, childFieldProperty.Name.Length);
					childFieldProperty = propertyType.GetProperty(childFieldName);
				}

				if (childFieldProperty == null)
				{
					return (null, null);
				}

				var childProperty = Expression.Property(startExpression, childFieldProperty);

				return (GetOperation(tail, childProperty, filterValue), entityPropertyParameter);
			} else
			{
				var tail = UpdateName(filterProperty.Name, entityProperty.Name);

				return (GetOperation(tail, entityPropertyExpression, filterValue), entityPropertyParameter);
			}
		}

		/// <summary>
		/// Get the last part of the field name that matches the restriction
		/// </summary>
		/// <param name="value"> </param>
		/// <returns> </returns>
		private string GetTail(string value)
		{
			var parts = value.SplitByCamelCase();

			var count = parts.Length > 2 ? 2 : parts.Length;

			while (count > 0)
			{
				var tail = string.Join("", parts.Skip(parts.Length - count));
				var result = GetRestriction(tail);

				if (result != null)
				{
					return tail;
				}

				count--;
			}

			return "";
		}

		/// <summary>
		/// Forms an expression that performs a restriction on the field depending on the
		/// last one
		/// parts of the field name
		/// </summary>
		/// <param name="tail">
		/// the last part of the DTO field name that remains after deleting the field name
		/// entities
		/// </param>
		/// <param name="property"> an expression that identifies the field </param>
		/// <param name="filterValue"> filter value </param>
		/// <returns> Expression that applies the corresponding constraint </returns>
		private Expression GetOperation(string tail, Expression property, object filterValue)
		{
			Expression op = null;
			Expression value = Expression.Constant(filterValue);

			if (tail == "")
			{
				ExpressionExtension.ConvertToCommonNullable(ref property, ref value);

				return Expression.Equal(property, value);
			}

			var restriction = GetRestriction(tail);

			switch (restriction)
			{
				case EntityRestrictions.Contains:
					value = Expression.Constant($"%{filterValue}%");
					op = ApplyLike(property, value);

					break;

				case EntityRestrictions.Equal:
					ExpressionExtension.ConvertToCommonNullable(ref property, ref value);
					op = Expression.Equal(property, value);

					break;

				case EntityRestrictions.Greater:
					ExpressionExtension.ConvertToCommonNullable(ref property, ref value);
					op = Expression.GreaterThan(property, value);

					break;

				case EntityRestrictions.GreaterEqual:
					ExpressionExtension.ConvertToCommonNullable(ref property, ref value);
					op = Expression.GreaterThanOrEqual(property, value);

					break;

				case EntityRestrictions.Less:
					ExpressionExtension.ConvertToCommonNullable(ref property, ref value);
					op = Expression.LessThan(property, value);

					break;

				case EntityRestrictions.LessEqual:
					ExpressionExtension.ConvertToCommonNullable(ref property, ref value);
					op = Expression.LessThanOrEqual(property, value);

					break;

				case EntityRestrictions.In:
				{
					var elementType = filterValue.GetType().GetElementType();

					op = Expression.Equal(Expression.Call(typeof(Enumerable), "Contains", new[] { elementType }, value, property),
						Expression.Constant(true));
				}

					break;
			}

			return op;
		}

		/// <summary>
		/// Apply the like operation
		/// </summary>
		/// <param name="property"> property </param>
		/// <param name="value"> filter value </param>
		/// <returns> </returns>
		private Expression ApplyLike(Expression property, Expression value)
		{
			var like = typeof(DbFunctionsExtensions).GetMethod("Like",
				new[] { typeof(DbFunctions), typeof(string), typeof(string) });

			return Expression.Equal(Expression.Call(null, like!, Expression.Constant(EF.Functions), property, value),
				Expression.Constant(true));
		}

		/// <summary>
		/// Restriction corresponding to the specified string
		/// </summary>
		/// <param name="value"> string that sets the limit </param>
		/// <returns> Suitable restriction </returns>
		private EntityRestrictions? GetRestriction(string value)
		{
			return Enum.GetValues(typeof(EntityRestrictions))
				.Cast<EntityRestrictions?>()
				.FirstOrDefault(e => Enum.GetName(typeof(EntityRestrictions), e ?? EntityRestrictions.Equal)?.ToLower() == value.ToLower());
		}

		/// <summary>
		/// To get the latest filters
		/// </summary>
		/// <typeparam name="TFilter"> </typeparam>
		/// <param name="filter"> </param>
		/// <returns> </returns>
		private IEnumerable<PropertyInfo> GetFilterProperties<TFilter>(TFilter filter)
		{
			return filter.GetType()
				.GetProperties()
				.Where(x =>
				{
					var value = x.GetValue(filter);

					return value switch
					{
						null => false,
						string s => s.Length > 0,
						var _ => true
					};
				});
		}

		/// <summary>
		/// Are the first parts of field names identical
		/// </summary>
		/// <param name="source"> field in dto </param>
		/// <param name="destination"> field in the entity </param>
		/// <returns> true if the DTO field can relate to the corresponding entity field </returns>
		private bool IsMatchProperty(string source, string destination)
		{
			return Regex.IsMatch(source, $"^{destination}([A-Z]+[A-Za-z0-9]*)*$");
		}

		private string GetPrimaryKey(Type entityType)
		{
			return _dbContext.Model.FindEntityType(entityType)
				.FindPrimaryKey()
				.Properties.Select(x => x.Name)
				.Single();
		}

		private string UpdateName(string sourceName, string entityPropertyName)
		{
			sourceName = sourceName.ToPascalCase();
			entityPropertyName = entityPropertyName.ToPascalCase();

			return sourceName.Remove(0, entityPropertyName == _entityKeyPropertyName ? 2 : entityPropertyName.Length);
		}
	}
}