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

			await DeserializationAsync();
		}

		internal async Task DeserializationAsync()
		{
			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			await using var stream = _readerService.Read(filePath);

			HeadInitialize(stream);
			await BodyInitialize(stream);
		}

		internal void HeadInitialize(Stream stream)
		{
			Head = new Head(stream);
		}

		internal async Task BodyInitialize(Stream stream)
		{
			var body = BodyRead(stream);
			var records = Head.Records;
			const byte ipSize = IpIntervalsInformation.Size;
			const byte coordinateSize = CoordinateInformation.Size;

			var ipIntervalsInformations = new IIpIntervalsInformation[records];
			var coordinateInformations = new ICoordinateInformation[records];

			await using var ipMemoryStream = new MemoryStream(body.IdBuffer);
			await using var coordinateMemoryStream = new MemoryStream(body.CoordinateBuffer);
			using var ipBinaryReader = new BinaryReader(ipMemoryStream);
			using var coordinateBinaryReader = new BinaryReader(coordinateMemoryStream);

			for (var index = 0; index < records; index += 1)
			{
				ipIntervalsInformations[index] = new IpIntervalsInformation(ipBinaryReader.ReadBytes(ipSize));
				coordinateInformations[index] = new CoordinateInformation(coordinateBinaryReader.ReadBytes(coordinateSize));
			}

			IpIntervalsInformations = new ConcurrentQueue<IIpIntervalsInformation>(ipIntervalsInformations);
			CoordinateInformations = new ConcurrentQueue<ICoordinateInformation>(coordinateInformations);
		}

		internal DatBody BodyRead(Stream stream)
		{
			var records = Head.Records;
			var ipSize = IpIntervalsInformation.Size * records;
			var coordinateSize = CoordinateInformation.Size * records;

			var ipBuffer = new byte[ipSize];
			var coordinateBuffer = new byte[coordinateSize];

			stream.Read(ipBuffer, 0, ipSize);
			stream.Read(coordinateBuffer, 0, coordinateSize);

			return new DatBody(ipBuffer, coordinateBuffer);
		}
	}
}