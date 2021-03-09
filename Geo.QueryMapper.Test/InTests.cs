using System.Linq;
using System.Threading.Tasks;
using Geo.QueryMapper.Test.Models;
using Xunit;

namespace Geo.QueryMapper.Test
{
	public class InTests : BaseQueryDtoMapperTest
	{
		[Fact]
		public async Task InIntNotEmptyTest()
		{
			var filter = new OrganizationFilterDto
			{
				IdIn = new[] { 1, 3 }
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotEmpty(result.Items);
			Assert.Equal(2, result.Count);
			Assert.True(result.Items.All(e => filter.IdIn.Any(x => x == e.Id)));
		}

		[Fact]
		public async Task InIntEmptyTest()
		{
			var filter = new OrganizationFilterDto
			{
				IdIn = new[] { 4 }
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.Empty(result.Items);
		}

		[Fact]
		public async Task InIntListTest()
		{
			var filter = new OrganizationFilterDto
			{
				KindIdIn = new int?[] { 1, 3 }
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotEmpty(result.Items);
			Assert.Equal(2, result.Count);
			Assert.True(result.Items.All(e => filter.KindIdIn.Any(x => x == e.KindId)));
		}

		[Fact]
		public async Task InIntList2Test()
		{
			var filter = new OrganizationFilterDto
			{
				KindGroupIdIn = new int?[] { 2 }
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotEmpty(result.Items);
			Assert.Equal(3, result.Count);
			Assert.True(result.Items.All(e => filter.KindGroupIdIn.Any(x => x == e.KindGroupId)));

			filter = new OrganizationFilterDto
			{
				KindGroupIdIn = new int?[] { 1 }
			};

			mapper = GetQueryDtoMapper();
			result = await GetResultAsync(mapper, filter);

			Assert.Empty(result.Items);
		}

		[Fact]
		public async Task InStringNotEmptyTest()
		{
			var filter = new OrganizationFilterDto
			{
				NameIn = new[] { "Oracle" }
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotEmpty(result.Items);
			Assert.Equal(1, result.Count);
			Assert.True(result.Items.All(e => filter.NameIn.Any(x => x == e.Name)));
		}

		[Fact]
		public async Task InStringListNotEmptyTest()
		{
			var filter = new OrganizationFilterDto
			{
				KindGroupNameIn = new[] { "IT" }
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotEmpty(result.Items);
			Assert.Equal(3, result.Count);
			Assert.True(result.Items.All(e => filter.KindGroupNameIn.Any(x => x == e.KindGroupName)));
		}

		[Fact]
		public async Task InStringListEmptyTest()
		{
			var filter = new OrganizationFilterDto
			{
				KindGroupNameIn = new[] { "Finance" }
			};

			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.Empty(result.Items);
		}
	}
}