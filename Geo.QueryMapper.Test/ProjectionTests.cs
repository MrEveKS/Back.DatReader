using System.Threading.Tasks;
using Xunit;

namespace Geo.QueryMapper.Test
{
	public class ProjectionTests : BaseQueryDtoMapperTest
	{
		[Fact]
		public async Task CustomBindTest()
		{
			var mapper = GetQueryDtoMapper()
				.CustomizeProjection(e => e.CustomBindInt, e => 2010)
				.CustomizeProjection(e => e.CustomBindString, e => e.Kind.Name);

			var result = await GetResultAsync(mapper, null);

			Assert.NotEmpty(result.Items);
			Assert.Contains(result.Items, e => e.CustomBindInt == 2010);
			Assert.Contains(result.Items, e => e.CustomBindString != null);
		}
	}
}