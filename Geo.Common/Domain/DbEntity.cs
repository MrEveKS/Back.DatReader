using System;

namespace Geo.Common.Domain
{
	public class DbEntity : ICloneable
	{
		public int Id { get; set; }

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}