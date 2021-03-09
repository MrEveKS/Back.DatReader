using System;
using Geo.Common.Constants;
using Geo.Common.Domain;
using Geo.DatReader;
using Geo.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Geo.Information.Database
{
	public sealed class DatDbContext : DbContext
	{
		private readonly IDatDbData _datDbData;

		public DatDbContext(IDatDbData datDbData, DbContextOptions options) : base(options)
		{
			_datDbData = datDbData;
		}

		public DbSet<DatInfo> Headers { get; set; }

		public DbSet<UserLocation> CoordinateInformations { get; set; }

		public DbSet<UserIp> IpIntervalsInformations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			try
			{
				modelBuilder.Entity<UserIp>()
					.HasOne(p => p.UserLocation)
					.WithOne()
					.HasForeignKey<UserIp>(d => d.UserLocationId);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);

				throw;
			}


			_datDbData.InitializeAsync().ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT).GetAwaiter().GetResult();

			modelBuilder.Entity<DatInfo>()
				.HasData(_datDbData.DatInfo.Map<DatInfo>());

			modelBuilder.Entity<UserLocation>()
				.HasData(_datDbData.UserLocations.Map<UserLocation>());

			modelBuilder.Entity<UserIp>()
				.HasData(_datDbData.UserIps.Map<UserIp>());

			base.OnModelCreating(modelBuilder);
		}
	}
}