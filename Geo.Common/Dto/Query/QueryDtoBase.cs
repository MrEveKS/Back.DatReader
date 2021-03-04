using System.Collections.Generic;

namespace Geo.Common.Dto.Query
{
	public class QueryDtoBase
	{
		/// <summary>
		/// Return the specified number of entries
		/// </summary>
		public int? Take { get; set; }

		/// <summary>
		/// Skip the specified number of entries
		/// </summary>
		public int? Skip { get; set; }

		/// <summary>
		/// Sort
		/// </summary>
		public List<OrderDto> Order { get; set; }

		/// <summary>
		/// Return the number of records that meet the request limits?
		/// </summary>
		public bool? WithCount { get; set; }
	}
}