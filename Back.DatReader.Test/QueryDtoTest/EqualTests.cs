using System;
using System.Linq;
using System.Threading.Tasks;
using Back.DatReader.Test.QueryDtoTest.Models;
using Xunit;

namespace Back.DatReader.Test.QueryDtoTest
{
	public class EqualTests : BaseQueryDtoMapperTest
	{
		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(10)]
		public async Task WithoutEqualIntTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				Id = id
			};

			await CommonEqualIntTest(filter, id);
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(10)]
		public async Task EqualIntTest(int? id)
		{
			var filter = new OrganizationFilterDto
			{
				IdEqual = id
			};

			await CommonEqualIntTest(filter, id);
		}

		private async Task CommonEqualIntTest(OrganizationFilterDto filter, int? id)
		{
			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			if (id == 10)
			{
				Assert.Empty(result.Items);
			} else
			{
				Assert.NotEmpty(result.Items);
				Assert.True(result.Items.All(e => e.Id == id));
			}
		}

		[Theory]
		[InlineData("Microsoft")]
		[InlineData("Oracle")]
		[InlineData("o")]
		public async Task WithoutEqualStringTest(string name)
		{
			var filter = new OrganizationFilterDto
			{
				Name = name
			};

			await CommonEqualStringTest(filter, name);
		}

		[Theory]
		[InlineData("Microsoft")]
		[InlineData("Oracle")]
		[InlineData("o")]
		public async Task EqualStringTest(string name)
		{
			var filter = new OrganizationFilterDto
			{
				NameEqual = name
			};

			await CommonEqualStringTest(filter, name);
		}

		private async Task CommonEqualStringTest(OrganizationFilterDto filter, string name)
		{
			var mapper = GetQueryDtoMapper();
			var result = await GetResultAsync(mapper, filter);

			if (name == "o")
			{
				Assert.Empty(result.Items);
			} else
			{
				Assert.NotEmpty(result.Items);
				Assert.True(result.Items.All(e => e.Name == name));
			}
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(10)]
		public async Task WithoutEqualDateTest(int? day)
		{
			var filter = new OrganizationFilterDto
			{
				CreateDate = GetDate(day)
			};

			await CommonEqualDateTest(filter, filter.CreateDate);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		[InlineData(10)]
		public async Task EqualDateTest(int? day)
		{
			var filter = new OrganizationFilterDto
			{
				CreateDateEqual = GetDate(day)
			};

			await CommonEqualDateTest(filter, filter.CreateDateEqual);
		}

		private DateTime? GetDate(int? day)
		{
			return day.HasValue ? (DateTime?) Organization.CreateDateConst.AddDays(day.Value) : null;
		}

		private async Task CommonEqualDateTest(OrganizationFilterDto filter, DateTime? date)
		{
			var mapper = GetQueryDtoMapper();

			var result = await GetResultAsync(mapper, filter);

			if (date == Organization.CreateDateConst.AddDays(10))
			{
				Assert.Empty(result.Items);
			} else
			{
				Assert.NotEmpty(result.Items);
				Assert.True(result.Items.All(e => e.CreateDate == date));
			}
		}
	}
}