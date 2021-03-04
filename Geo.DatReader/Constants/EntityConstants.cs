namespace Geo.DatReader.Constants
{
	internal static class EntityConstants
	{
	#region Head

		public const byte VersionSize = 4;

		public const byte NameSize = 32;

		public const byte TimestampSize = 8;

		public const byte RecordsSize = 4;

		public const byte OffsetRangesSize = 4;

		public const byte OffsetCitiesSize = 4;

		public const byte OffsetLocationsSize = 4;

		public const byte HeadSize = VersionSize
									+ NameSize
									+ TimestampSize
									+ RecordsSize
									+ OffsetRangesSize
									+ OffsetCitiesSize
									+ OffsetLocationsSize;

	#endregion

	#region IpIntervalsInformation

		public const byte IpFromSize = 4;

		public const byte IpToSize = 4;

		public const byte LocationIndexSize = 4;

		public const byte IpSize = IpFromSize + IpToSize + LocationIndexSize;

	#endregion

	#region CoordinateInformation

		public const byte CountrySize = 8;

		public const byte RegionSize = 12;

		public const byte PostalSize = 12;

		public const byte CitySize = 24;

		public const byte OrganizationSize = 32;

		public const byte LatitudeSize = 4;

		public const byte LongitudeSize = 4;

		public const byte CoordinateSize =
			CountrySize + RegionSize + PostalSize + CitySize + OrganizationSize + LatitudeSize + LongitudeSize;

	#endregion
	}
}