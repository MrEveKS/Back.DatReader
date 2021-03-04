using Back.DatReader.Database;
using Back.DatReader.Test.ProjectQueryTest.Database;

namespace Back.DatReader.Test.ProjectQueryTest
{
	public class BaseProjectQueryTest
	{
		protected DatDbContext DbContext = TestDatDbContext.GetDbContext();
	}
}