using System;
using System.Linq;
using System.Threading.Tasks;
using Geo.QueryMapper.Test.Models;
using Xunit;

namespace Geo.QueryMapper.Test
{
	public class CompareTests : BaseQueryDtoMapperTest
	{
		private async Task CommonTest(OrganizationFilterDto filter, Func<OrganizationListDto, bool> func)
		{
			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			Assert.NotEmpty(result.Items);
			Assert.True(result.Items.All(func));
		}

		private DateTime? GetDate(int? day)
		{
			return day.HasValue ? (DateTime?) Organization.CreateDateConst.AddDays(day.Value) : null;
		}

	#region Int

		[Theory]
		[InlineData(2)]
		[InlineData(3)]
		public async Task LessIntTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				IdLess = id
			};

			await CommonTest(filter, e => e.Id < id);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task LessEqualIntTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				IdLessEqual = id
			};

			await CommonTest(filter, e => e.Id <= id);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task GreaterIntTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				IdGreater = id
			};

			await CommonTest(filter, e => e.Id > id);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GreaterEqualIntTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				IdGreaterEqual = id
			};

			await CommonTest(filter, e => e.Id >= id);
		}

	#endregion

	#region DateTime

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		public async Task LessDateTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				CreateDateLess = GetDate(id)
			};

			await CommonTest(filter, e => e.CreateDate < filter.CreateDateLess);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task LessEqualDateTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				CreateDateLessEqual = GetDate(id)
			};

			await CommonTest(filter, e => e.CreateDate <= filter.CreateDateLessEqual);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public async Task GreaterDateTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				CreateDateGreater = GetDate(id)
			};

			await CommonTest(filter, e => e.CreateDate > filter.CreateDateGreater);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(2)]
		public async Task GreaterEqualDateTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				CreateDateGreaterEqual = GetDate(id)
			};

			await CommonTest(filter, e => e.CreateDate >= filter.CreateDateGreaterEqual);
		}

	#endregion
	}
}