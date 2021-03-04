using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Geo.DatReader.Constants;
using Geo.DatReader.Models;
using Geo.DatReader.Services;

[assembly: InternalsVisibleTo("Geo.DatReader.Test")]

namespace Geo.DatReader
{
	public class DatDbDataSingleton
	{
		private static volatile bool _isInProgress;

		private static readonly object Locker = new();

		private static readonly Lazy<DatDbDataSingleton> LazyInstance =
			new(() => new DatDbDataSingleton(), LazyThreadSafetyMode.ExecutionAndPublication);

		private readonly IFileReaderService _readerService;

		private DatDbDataSingleton()
		{
			_readerService = new FileReaderService();
		}

		/// <summary>
		/// Initialize again for testing
		/// </summary>
		/// <returns> test property </returns>
		internal bool SkipNoInitialize { private get; set; }

		public static DatDbDataSingleton Current => LazyInstance.Value;

		public IReadOnlyCollection<IUserLocation> UserLocations { get; private set; }

		public IDatInfo DatInfo { get; private set; }

		public IReadOnlyCollection<IUserIp> UserIps { get; private set; }

		public async Task InitializeAsync()
		{
			lock (Locker)
			{
				if (_isInProgress && !SkipNoInitialize)
				{
					return;
				}

				_isInProgress = true;
			}

			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			await using var stream = _readerService.Read(filePath);

			DatInfo = new DatInfo(stream);
			var records = DatInfo.Records;

			var ipIntervalsInformations = new IUserIp[records];
			var coordinateInformations = new IUserLocation[records];

			for (var i = 0; i < records; i++)
			{
				ipIntervalsInformations[i] = new UserIp(stream);
			}

			for (var i = 0; i < records; i++)
			{
				coordinateInformations[i] = new UserLocation(stream);
			}

			UserIps = new ConcurrentQueue<IUserIp>(ipIntervalsInformations);
			UserLocations = new ConcurrentQueue<IUserLocation>(coordinateInformations);
		}
	}
}