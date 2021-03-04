using System;
using System.Reflection;

namespace Back.DatReader.Extensions
{
	public static class TypeExtension
	{
		/// <summary>
		/// Is the type Nullable
		/// </summary>
		/// <param name="t"> </param>
		/// <returns> true if the type is Nullable </returns>
		public static bool IsNullableType(this Type t)
		{
			return typeof(string) == t
					|| (t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
		}
	}
}
