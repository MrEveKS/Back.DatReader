using Back.DatReader.Constants;
using Back.DatReader.Models;
using File.DatReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Back.DatReader.Database
{
	public class DatDbContext : CoreDbContext
	{
		public DatDbContext(IWebHostEnvironment env, DbContextOptions options) : base(options)
		{
			Database.EnsureCreated();
		}

		public DbSet<Header> Headers { get; set; }

		public DbSet<CoordinateInformation> CoordinateInformations { get; set; }

		public DbSet<IpIntervalsInformation> IpIntervalsInformations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var badData = DatDbDataSingleton.Current;

			badData.InitializeAsync().ConfigureAwait(AsyncConstants.ContinueOnCapturedContext).GetAwaiter().GetResult();

			modelBuilder.Entity<Header>()
				.HasData(badData.Head);

			modelBuilder.Entity<CoordinateInformation>()
				.HasData(badData.CoordinateInformations);

			modelBuilder.Entity<IpIntervalsInformation>()
				.HasData(badData.IpIntervalsInformations);

			base.OnModelCreating(modelBuilder);
		}
	}
}