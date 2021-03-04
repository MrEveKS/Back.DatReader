namespace Back.DatReader.Database.QueryMapper
{
	/// <summary>
	/// Restrictions applied to filter fields
	/// </summary>
	public enum EntityRestrictions
	{
		/// <summary>
		/// Equal
		/// </summary>
		Equal,
		/// <summary>
		/// Search for an occurrence of a string field like ' % ' + value +'%'
		/// </summary>
		Contains,
	}
}
