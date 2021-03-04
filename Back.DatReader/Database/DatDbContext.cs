using System.Linq;
using Back.DatReader.Constants;
using Back.DatReader.Extensions;
using Back.DatReader.Models.Domain;
using File.DatReader;
using Microsoft.EntityFrameworkCore;

namespace Back.DatReader.Database
{
	public sealed class DatDbContext : CoreDbContext
	{
		public DatDbContext(DbContextOptions options) : base(options)
		{
			InitData();
		}

		public DbSet<Header> Headers { get; set; }

		public DbSet<CoordinateInformation> CoordinateInformations { get; set; }

		public DbSet<IpIntervalsInformation> IpIntervalsInformations { get; set; }

		private void InitData()
		{
			var badData = DatDbDataSingleton.Current;
			badData.InitializeAsync().ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT).GetAwaiter().GetResult();

			if (!Headers.Any())
			{
				AddRange(badData.Head.Map<Header>());
			}

			if (!IpIntervalsInformations.Any())
			{
				AddRange(badData.IpIntervalsInformations.Map<IpIntervalsInformation>());
			}

			if (!CoordinateInformations.Any())
			{
				AddRange(badData.CoordinateInformations.Map<CoordinateInformation>());
			}

			SaveChanges();
		}
	}
}