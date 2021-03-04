using System.Collections.Generic;

namespace Back.DatReader.Models.Dto.QueryResult
{
	/// <inheritdoc />
	public class QueryResultDto<TResult> : IQueryResultDto<TResult>
		where TResult : class
	{
		/// <inheritdoc />
		public IList<TResult> Items { get; set; }

		/// <inheritdoc />
		public long? Count { get; set; }
	}
}
