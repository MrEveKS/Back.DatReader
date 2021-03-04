using System.Collections.Generic;
using System.Linq;

namespace Geo.Utility.Extensions
{
	public static class ObjectMapperExtension
	{
		/// <summary>
		/// Map entity to entity
		/// </summary>
		/// <typeparam name="TEntityTo"> </typeparam>
		/// <param name="objectFrom"> </param>
		/// <returns> </returns>
		public static TEntityTo Map<TEntityTo>(this object objectFrom)
			where TEntityTo : new()
		{
			var objectTo = new TEntityTo();
			var toProperties = objectTo.GetType().GetProperties();
			var fromProperties = objectFrom.GetType().GetProperties();

			toProperties.ToList()
				.ForEach(o =>
				{
					var fromProperty = fromProperties.FirstOrDefault(x => x.Name == o.Name && x.PropertyType == o.PropertyType);

					if (fromProperty != null)
					{
						o.SetValue(objectTo, fromProperty.GetValue(objectFrom));
					}
				});

			return objectTo;
		}

		/// <summary>
		/// Map collection to collection
		/// </summary>
		/// <typeparam name="TEntityTo"> </typeparam>
		/// <param name="collectionFrom"> </param>
		/// <returns> </returns>
		public static IEnumerable<TEntityTo> Map<TEntityTo>(this IEnumerable<object> collectionFrom)
			where TEntityTo : new()

		{
			return collectionFrom.Select(c => c.Map<TEntityTo>());
		}
	}
}