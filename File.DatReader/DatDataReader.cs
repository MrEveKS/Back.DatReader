using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using File.DatReader.Constants;
using File.DatReader.Services;

[assembly: InternalsVisibleTo("File.DatReader.Test")]

namespace DatReader
{
	public class DatDataReader
	{
		private readonly IBatReaderService _readerService;

		private ICoordinateInformation[] _coordinateInformations;

		private IHead _head;

		private IIpIntervalsInformation[] _ipIntervalsInformations;

		public DatDataReader()
		{
			_readerService = new BatReaderService();
		}

		public async Task InitializeAsync()
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), DataConstatns.BatFilePath);
			await using var stream = _readerService.ReadAsync(filePath);

			_head = new Head(stream);
			var records = _head.Records;

			_ipIntervalsInformations = new IIpIntervalsInformation[records];
			_coordinateInformations = new ICoordinateInformation[records];

			for (var i = 0; i < records; i++)
			{
				_ipIntervalsInformations[i] = new IpIntervalsInformation(stream);
			}

			for (var i = 0; i < records; i++)
			{
				_coordinateInformations[i] = new CoordinateInformation(stream);
			}
		}
	}
}