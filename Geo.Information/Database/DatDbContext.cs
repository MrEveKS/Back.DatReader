using System.Linq;
using Geo.Common.Constants;
using Geo.Common.Domain;
using Geo.DatReader;
using Geo.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Geo.Information.Database
{
	public sealed class DatDbContext : DbContext
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
			badData.InitializeAsync();

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