namespace Geo.QueryMapper.Enums
{
	/// <summary>
	/// Restrictions applied to filter fields
	/// </summary>
	internal enum EntityRestrictions
	{
		/// <summary>
		/// Equal
		/// </summary>
		Equal,

		/// <summary>
		/// Search for an occurrence of a string field like ' % ' + value +'%'
		/// </summary>
		Contains,

		/// <summary>
		/// Greater
		/// </summary>
		Greater,

		/// <summary>
		/// Greater or Equal
		/// </summary>
		GreaterEqual,

		/// <summary>
		/// Less
		/// </summary>
		Less,

		/// <summary>
		/// Less or Equal
		/// </summary>
		LessEqual
	}
}