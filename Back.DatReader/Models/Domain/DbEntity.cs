using System;
using System.ComponentModel.DataAnnotations;

namespace Back.DatReader.Models.Domain
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
