using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using File.DatReader;
using File.DatReader.Constants;
using File.DatReader.Services;

[assembly: InternalsVisibleTo("File.DatReader.Test")]

namespace File.DatReader
{
	public class DatDbDataSingleton
	{
		private static bool _isInProgress;

		private static readonly object _locker = new();

		private static readonly Lazy<DatDbDataSingleton> LazyInstance =
			new(() => new DatDbDataSingleton(), LazyThreadSafetyMode.ExecutionAndPublication);

		private readonly IBatReaderService _readerService;

		private DatDbDataSingleton()
		{
			_readerService = new BatReaderService();
		}

		internal bool SkipLock { private get; set; }

		public static DatDbDataSingleton Current => LazyInstance.Value;

		public IReadOnlyCollection<ICoordinateInformation> CoordinateInformations { get; private set; }

		public IHead Head { get; private set; }

		public IReadOnlyCollection<IIpIntervalsInformation> IpIntervalsInformation { get; private set; }

		public async Task InitializeAsync()
		{
			lock (_locker)
			{
				if (_isInProgress && !SkipLock)
				{
					return;
				}

				_isInProgress = true;
			}

			var filePath = Path.Combine(Directory.GetCurrentDirectory(), DataConstatns.BatFilePath);
			await using var stream = _readerService.ReadAsync(filePath);

			Head = new Head(stream);
			var records = Head.Records;

			var ipIntervalsInformation = new IIpIntervalsInformation[records];
			var coordinateInformations = new ICoordinateInformation[records];

			for (var i = 0; i < records; i++)
			{
				ipIntervalsInformation[i] = new IpIntervalsInformation(stream);
			}

			for (var i = 0; i < records; i++)
			{
				coordinateInformations[i] = new CoordinateInformation(stream);
			}

			IpIntervalsInformation = new ConcurrentQueue<IIpIntervalsInformation>(ipIntervalsInformation);
			CoordinateInformations = new ConcurrentQueue<ICoordinateInformation>(coordinateInformations);
		}
	}
}