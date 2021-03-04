using Geo.Information.Database;
using Geo.Information.Test.ProjectQueryTest.Database;

namespace Geo.Information.Test.ProjectQueryTest
{
	public class BaseProjectQueryTest
	{
		protected readonly DatDbContext DbContext = TestDatDbContext.GetDbContext();
	}
}