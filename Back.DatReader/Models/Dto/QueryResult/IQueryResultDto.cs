using System.Collections.Generic;

namespace Back.DatReader.Models.Dto.QueryResult
{
	/// <summary>
	/// Query result
	/// </summary>
	/// <typeparam name="TResult">Type of DTO returned by the request</typeparam>
	public interface IQueryResultDto<TResult>
		where TResult : class
	{
		/// <summary>
		/// Records returned by the request
		/// </summary>
		IList<TResult> Items { get; set; }

		/// <summary>
		/// The number of records that meet the request restrictions.
		/// Set if the request specifies whether to return the number of records.
		/// </summary>
		long? Count { get; set; }
	}
}
