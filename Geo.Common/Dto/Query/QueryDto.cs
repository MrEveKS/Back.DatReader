namespace Geo.Common.Dto.Query
{
	/// <summary>
	/// Request to data
	/// </summary>
	public class QueryDto<TFilter> : QueryDtoBase
		where TFilter : class
	{
		/// <summary>
		/// Filter
		/// </summary>
		public TFilter Filter { get; set; }
	}
}