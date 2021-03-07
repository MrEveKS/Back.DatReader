using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Geo.DatReader.Constants;
using Geo.DatReader.Models;
using Geo.DatReader.Services;

[assembly: InternalsVisibleTo("Geo.DatReader.Test")]

namespace Geo.DatReader
{
	public class DatDbData : IDisposable, IDatDbData
	{
		private readonly IFileReaderService _readerService;

		private bool _disposedValue;

		public DatDbData()
		{
			_readerService = new FileReaderService();
		}

		/// <summary>
		/// Initialize again for testing
		/// </summary>
		/// <returns> test property </returns>
		internal bool SkipNoInitialize { private get; set; }

		public IReadOnlyCollection<IUserLocation> UserLocations { get; private set; }

		public IDatInfo DatInfo { get; private set; }

		public IReadOnlyCollection<IUserIp> UserIps { get; private set; }

		public async Task InitializeAsync()
		{
			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			await using var stream = _readerService.Read(filePath);

			DatInfo = new DatInfo(stream, 1);
			var records = DatInfo.Records;

			var ipIntervalsInformations = new IUserIp[records];
			var coordinateInformations = new IUserLocation[records];

			for (var index = 0; index < records; index++)
			{
				ipIntervalsInformations[index] = new UserIp(stream, index + 1);
			}

			for (var index = 0; index < records; index++)
			{
				coordinateInformations[index] = new UserLocation(stream, index + 1);
			}

			UserIps = new ConcurrentQueue<IUserIp>(ipIntervalsInformations);
			UserLocations = new ConcurrentQueue<IUserLocation>(coordinateInformations);
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (_disposedValue)
			{
				return;
			}

			if (disposing)
			{
				UserLocations = null;
				DatInfo = null;
				UserIps = null;

				GC.Collect(GC.MaxGeneration);
			}

			_disposedValue = true;
		}

		~DatDbData()
		{
			Dispose(false);
		}
	}
}