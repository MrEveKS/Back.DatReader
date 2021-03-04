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

		public IReadOnlyCollection<ICoordinateInformation> CoordinateInformations { get; private set; }

		public IHead Head { get; private set; }

		public IReadOnlyCollection<IIpIntervalsInformation> IpIntervalsInformations { get; private set; }

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

			Head = new Head(stream);
			var records = Head.Records;

			var ipIntervalsInformations = new IIpIntervalsInformation[records];
			var coordinateInformations = new ICoordinateInformation[records];

			for (var i = 0; i < records; i++)
			{
				ipIntervalsInformations[i] = new IpIntervalsInformation(stream);
			}

			for (var i = 0; i < records; i++)
			{
				coordinateInformations[i] = new CoordinateInformation(stream);
			}

			IpIntervalsInformations = new ConcurrentQueue<IIpIntervalsInformation>(ipIntervalsInformations);
			CoordinateInformations = new ConcurrentQueue<ICoordinateInformation>(coordinateInformations);
		}
	}
}