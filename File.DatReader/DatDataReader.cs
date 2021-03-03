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

		public DatDataReader()
		{
			_readerService = new BatReaderService();
		}

		public async Task DeserializeAsync()
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), DataConstatns.BatFilePath);
			await using var stream = _readerService.ReadAsync(filePath);

			var head = new Head(stream);

            var records = head.Records;
            var informations = new IpIntervalsInformation[records];
            var coordinateInformations = new CoordinateInformation[records];

            for (var i = 0; i < records; i++)
            {
                informations[i] = new IpIntervalsInformation(stream);
            }

            for (var i = 0; i < records; i++)
            {
                coordinateInformations[i] = new CoordinateInformation(stream);
            }
        }
    }
}
