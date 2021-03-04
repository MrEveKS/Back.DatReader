using System.Collections.Generic;

namespace Geo.DatReader.Models
{
	internal readonly struct DatBody
	{
		public byte[] IdBuffer { get; init; }
		public byte[] CoordinateBuffer { get; init; }

		public DatBody(byte[] idBuffer, byte[] coordinateBuffer)
		{
			IdBuffer = idBuffer;
			CoordinateBuffer = coordinateBuffer;
		}
	}
}
