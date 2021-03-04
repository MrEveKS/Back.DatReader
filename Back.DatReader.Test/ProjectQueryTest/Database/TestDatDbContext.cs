using Back.DatReader.Database;
using Microsoft.EntityFrameworkCore;

namespace Back.DatReader.Test.ProjectQueryTest.Database
{
	public class TestDatDbContext
	{
		private static readonly object _lockObject = new object();
		
		public static DatDbContext GetDbContext()
		{
			DatDbContext dbContext;
			lock (_lockObject)
			{
				var options = new DbContextOptionsBuilder<DatDbContext>()
					.UseInMemoryDatabase("test_DatDbContext")
					.Options;

				dbContext = new DatDbContext(options);
				dbContext.Database.EnsureCreated();
			}

			return dbContext;
		}
	}
}