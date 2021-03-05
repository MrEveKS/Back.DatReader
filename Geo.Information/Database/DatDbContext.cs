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
			// InitData();
		}

		public DbSet<DatInfo> Headers { get; set; }

		public DbSet<UserLocation> CoordinateInformations { get; set; }

		public DbSet<UserIp> IpIntervalsInformations { get; set; }

		private void InitData()
		{
			var badData = DatDbDataSingleton.Current;
			badData.InitializeAsync().ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT).GetAwaiter().GetResult();

			if (!Headers.Any())
			{
				AddRange(badData.DatInfo.Map<DatInfo>());
			}

			if (!IpIntervalsInformations.Any())
			{
				AddRange(badData.UserIps.Map<UserIp>());
			}

			if (!CoordinateInformations.Any())
			{
				AddRange(badData.UserLocations.Map<UserLocation>());
			}

			SaveChanges();
		}
	}
}