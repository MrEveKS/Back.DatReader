using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DatDbData.Database
{
    /// <summary>
    /// Base database context
    /// </summary>
    /// <remarks>
    /// All contexts must be inherited from it
    /// </remarks>
    public class CoreDbContext : DbContext
    {
        public CoreDbContext(DbContextOptions options) : base(options)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options2 = new DbContextOptionsBuilder<CoreDbContext>()
                .UseInMemoryDatabase("test_SupplierService")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var dbContext = new CoreDbContext(options);

            dbContext.Database.EnsureCreated();
        }
    }
}
