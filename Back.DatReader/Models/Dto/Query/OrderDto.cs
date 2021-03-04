namespace Back.DatReader.Models.Dto.Query
{
	/// <summary>
	/// The sort criteria
	/// </summary>
	public class OrderDto
	{
		/// <summary>
		/// Path to the field
		/// </summary>
		public string Field { get; set; }

		/// <summary>
		/// Sort order type
		/// </summary>
		public OrderType Type { get; set; }
	}
}
