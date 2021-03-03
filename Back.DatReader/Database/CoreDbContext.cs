using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Back.DatReader.Database
{
    /// <summary>
    /// Base database context
    /// </summary>
    /// <remarks>
    /// All contexts must be inherited from it
    /// </remarks>
    public abstract class CoreDbContext : DbContext
    {
		protected CoreDbContext(DbContextOptions options) : base(options)
        {
		}
    }
}
