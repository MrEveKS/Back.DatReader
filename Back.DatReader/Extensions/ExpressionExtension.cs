using System.Linq.Expressions;

namespace Back.DatReader.Extensions
{
	public static class ExpressionExtension
	{
		/// <summary>
		/// Convert one expression to a Nullable type if the other expression has a Nullable type
		/// </summary>
		/// <param name="e1"> expression </param>
		/// <param name="e2"> expression </param>
		public static void ConvertToCommonNullable(ref Expression e1, ref Expression e2)
		{
			if (e1.Type.IsNullableType() && !e2.Type.IsNullableType())
			{
				e2 = Expression.Convert(e2, e1.Type);
			} else if (!e1.Type.IsNullableType() && e2.Type.IsNullableType())
			{
				e1 = Expression.Convert(e1, e2.Type);
			}
		}
	}
}
