using Geo.DatReader;
using Geo.Information.Database;
using Microsoft.EntityFrameworkCore;

namespace Geo.Information.Test.Database
{
	public class TestDatDbContext
	{
		private static readonly object LockObject = new();

		public static DatDbContext GetDbContext()
		{
			DatDbContext dbContext;

			lock (LockObject)
			{
				var options = new DbContextOptionsBuilder<DatDbContext>()
					.UseInMemoryDatabase("test_DatDbContext")
					.Options;

				dbContext = new DatDbContext(new DatDbData(), options);
				dbContext.Database.EnsureCreated();
			}

			return dbContext;
		}

	}
}