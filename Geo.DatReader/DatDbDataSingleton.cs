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
		private static bool _isInProgress;

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

		public void InitializeAsync()
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
			using var stream = _readerService.Read(filePath);

			Head = new Head(stream);
			var records = Head.Records;

			var ipIntervalsInformations = new IIpIntervalsInformation[records];
			var coordinateInformations = new ICoordinateInformation[records];

			for (var index = 0; index < records; index++)
			{
				ipIntervalsInformations[index] = new IpIntervalsInformation(stream);
			}

			for (var index = 0; index < records; index++)
			{
				coordinateInformations[index] = new CoordinateInformation(stream);
			}

			IpIntervalsInformations = new ConcurrentQueue<IIpIntervalsInformation>(ipIntervalsInformations);
			CoordinateInformations = new ConcurrentQueue<ICoordinateInformation>(coordinateInformations);
		}

		internal void ArrayInitialization()
		{
			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			using var stream = _readerService.ReadMemoryMapped(filePath);

			Head = new Head(stream);
			var records = Head.Records;

			var size = records * 2;
			var buffer = new byte[size];
			stream.Read(buffer, 0, size);
		}

		internal void ArrayInitialization2()
		{
			const byte ipSize = EntityConstants.IpSize;
			const byte coordinateSize = EntityConstants.CoordinateSize;

			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			using var stream = _readerService.Read(filePath);

			Head = new Head(stream);
			var records = Head.Records;

			var size = records * ipSize + records * coordinateSize;
			var buffer = new byte[size];

			stream.Read(buffer, 0, size);

			var ipIntervalsInformations = new IIpIntervalsInformation[records];
			var coordinateInformations = new ICoordinateInformation[records];

			for (int index = 0, skipIp = 0, skipCoordinate = records; index < records; index+=1, skipIp += ipSize, skipCoordinate += coordinateSize)
			{
				try
				{
					var ipBuffer = new byte[ipSize];
					var coordinateBuffer = new byte[coordinateSize];
					Array.Copy(buffer, skipIp, ipBuffer, 0, ipSize);
					Array.Copy(buffer, skipCoordinate, coordinateBuffer, 0, coordinateSize);
					ipIntervalsInformations[index] = new IpIntervalsInformation(ipBuffer);
					coordinateInformations[index] = new CoordinateInformation(coordinateBuffer);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);

					throw;
				}
			}

			IpIntervalsInformations = new ConcurrentQueue<IIpIntervalsInformation>(ipIntervalsInformations);
			CoordinateInformations = new ConcurrentQueue<ICoordinateInformation>(coordinateInformations);
		}

		internal void ArrayInitialization3()
		{
			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			using var stream = _readerService.ReadBuffer(filePath);

			Head = new Head(stream);
			var records = Head.Records;

			var size = records * 2;
			var buffer = new byte[size];

			stream.Read(buffer, 0, size);
		}

		internal void ArrayInitialization4()
		{
			const byte ipSize = IpIntervalsInformation.Size;
			const byte coordinateSize = CoordinateInformation.Size;

			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			using var stream = _readerService.Read(filePath);

			Head = new Head(stream);
			var records = Head.Records;

			var ipBuffer = new byte[records * ipSize];
			var coordinateBuffer = new byte[records * coordinateSize];

			var ipIntervalsInformations = new IIpIntervalsInformation[records];
			var coordinateInformations = new ICoordinateInformation[records];

			var ipMemoryStream = new MemoryStream(ipBuffer);
			var coordinateMemoryStream = new MemoryStream(coordinateBuffer);
			var ipBinaryReader = new BinaryReader(ipMemoryStream);
			var coordinateBinaryReader = new BinaryReader(coordinateMemoryStream);

			for (int index = 0, skipIp = 0, skipCoordinate = records; index < records; index += 1, skipIp += ipSize, skipCoordinate += coordinateSize)
			{
				ipIntervalsInformations[index] = new IpIntervalsInformation(ipBinaryReader.ReadBytes(ipSize));
				coordinateInformations[index] = new CoordinateInformation(coordinateBinaryReader.ReadBytes(coordinateSize));
			}

			IpIntervalsInformations = new ConcurrentQueue<IIpIntervalsInformation>(ipIntervalsInformations);
			CoordinateInformations = new ConcurrentQueue<ICoordinateInformation>(coordinateInformations);
		}

		public async Task InitializeAsync2()
		{
			lock (Locker)
			{
				if (_isInProgress && !SkipNoInitialize)
				{
					return;
				}

				_isInProgress = true;
			}

			await ArrayInitialization5();
		}

		internal async Task ArrayInitialization5()
		{
			var filePath = Path.Combine(AppContext.BaseDirectory, DataConstants.DAT_FILE_PATH);
			await using var stream = _readerService.Read(filePath);

			HeadInitialize(stream);
			BodyInitialize(stream);
		}

		internal void HeadInitialize(Stream stream)
		{
			Head = new Head(stream);
		}

		internal void BodyInitialize(Stream stream)
		{
			var body = BodyRead(stream);
			var records = Head.Records;
			const byte ipSize = IpIntervalsInformation.Size;
			const byte coordinateSize = CoordinateInformation.Size;

			var ipIntervalsInformations = new IIpIntervalsInformation[records];
			var coordinateInformations = new ICoordinateInformation[records];

			using var ipMemoryStream = new MemoryStream(body.ip);
			using var coordinateMemoryStream = new MemoryStream(body.coordinate);
			using var ipBinaryReader = new BinaryReader(ipMemoryStream);
			using var coordinateBinaryReader = new BinaryReader(coordinateMemoryStream);

			for (int index = 0, skipIp = 0, skipCoordinate = records; index < records; index += 1, skipIp += ipSize, skipCoordinate += coordinateSize)
			{
				ipIntervalsInformations[index] = new IpIntervalsInformation(ipBinaryReader.ReadBytes(ipSize));
				coordinateInformations[index] = new CoordinateInformation(coordinateBinaryReader.ReadBytes(coordinateSize));
			}

			IpIntervalsInformations = new ConcurrentQueue<IIpIntervalsInformation>(ipIntervalsInformations);
			CoordinateInformations = new ConcurrentQueue<ICoordinateInformation>(coordinateInformations);
		}

		internal (byte[] ip, byte[] coordinate) BodyRead(Stream stream)
		{
			var records = Head.Records;
			var ipSize = IpIntervalsInformation.Size * records;
			var coordinateSize = CoordinateInformation.Size * records;

			var ipBuffer = new byte[ipSize];
			var coordinateBuffer = new byte[coordinateSize];

			stream.Read(ipBuffer, 0, ipSize);
			stream.Read(coordinateBuffer, 0, coordinateSize);

			return (ipBuffer, coordinateBuffer);
		}
	}
}