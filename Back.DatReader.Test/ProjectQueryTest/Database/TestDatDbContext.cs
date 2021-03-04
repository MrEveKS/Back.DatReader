using Back.DatReader.Database;
using Microsoft.EntityFrameworkCore;

namespace Back.DatReader.Test.ProjectQueryTest.Database
{
	public class TestDatDbContext : DatDbContext
	{
		private static readonly object _lockObject = new object();

		public TestDatDbContext(DbContextOptions options) : base(options)
		{
		}

		public static TestDatDbContext GetDbContext()
		{
			TestDatDbContext dbContext;
			lock (_lockObject)
			{
				var options = new DbContextOptionsBuilder<TestDatDbContext>()
					.UseInMemoryDatabase("test_DatDbContext")
					.Options;

				dbContext = new TestDatDbContext(options);
				dbContext.Database.EnsureCreated();
			}

			return dbContext;
		}
	}
}