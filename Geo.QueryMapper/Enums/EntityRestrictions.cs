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
		Contains
	}
}