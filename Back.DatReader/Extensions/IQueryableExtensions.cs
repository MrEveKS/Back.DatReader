using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Back.DatReader.Models.Dto.Query;

namespace Back.DatReader.Extensions
{
	public static class QueryableExtensions
	{
		public static IQueryable<TEntity> OrderByProperty<TEntity>(this IQueryable<TEntity> source, List<OrderDto> order)
			where TEntity : class
		{
			var expression = source.Expression;
			var type = typeof(TEntity);
			var parameter = Expression.Parameter(type, "p");
			var thenBy = false;

			foreach (var item in order)
			{
				var propertyName = item.Field;

				var command = item.Type == OrderType.Desc
					? thenBy ? "ThenByDescending" : "OrderByDescending"
					: thenBy
						? "ThenBy"
						: "OrderBy";

				expression = expression
					.OrderByExpressionBuilder<TEntity>(parameter, propertyName, command);

				thenBy = true;
			}

			return source.Provider.CreateQuery<TEntity>(expression);
		}

		private static Expression OrderByExpressionBuilder<TEntity>(this Expression queryExpression,
																	ParameterExpression parameter, string propertyName, string command)
			where TEntity : class
		{
			var type = typeof(TEntity);

			PropertyInfo property;
			MemberExpression propertyAccess;

			if (propertyName.Contains('.'))
			{
				var childProperties = propertyName.Split('.');
				property = SearchProperty(typeof(TEntity), childProperties[0]);

				if (property == null)
				{
					throw new ArgumentException(childProperties[0]);
				}

				propertyAccess = Expression.MakeMemberAccess(parameter, property);

				for (var index = 1; index < childProperties.Length; index++)
				{
					var childType = property.PropertyType;
					property = SearchProperty(childType, childProperties[index]);

					if (property == null)
					{
						throw new ArgumentException(childProperties[index]);
					}

					propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
				}
			} else
			{
				property = SearchProperty(type, propertyName);

				if (property == null)
				{
					throw new ArgumentException(propertyName);
				}

				propertyAccess = Expression.MakeMemberAccess(parameter, property);
			}

			var orderByExpression = Expression.Lambda(propertyAccess, parameter);

			queryExpression = Expression.Call(typeof(Queryable),
				command,
				new[] { type, property.PropertyType },
				queryExpression,
				Expression.Quote(orderByExpression));

			return queryExpression;
		}

		private static PropertyInfo SearchProperty(Type type, string propertyName)
		{
			return type.GetProperties()
				.FirstOrDefault(item => string.Equals(item.Name, propertyName, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}