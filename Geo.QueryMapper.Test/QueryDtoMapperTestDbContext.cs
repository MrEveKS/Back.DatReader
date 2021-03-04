using Geo.QueryMapper.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Geo.QueryMapper.Test
{
	public class QueryDtoMapperTestDbContext : DbContext
	{
		public QueryDtoMapperTestDbContext(DbContextOptions options) :
			base(options)
		{
		}

		public DbSet<Organization> Organizations { get; set; }

		public DbSet<OrganizationKind> OrganizationKinds { get; set; }

		public DbSet<Employee> Employees { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<OrganizationGroup>()
				.HasData(new OrganizationGroup
					{
						Id = 1,
						Name = "Finance"
					},
					new OrganizationGroup
					{
						Id = 2,
						Name = "IT"
					});

			modelBuilder.Entity<OrganizationKind>()
				.HasData(new OrganizationKind
					{
						Id = 1,
						Name = "Hardware",
						GroupId = 2
					},
					new OrganizationKind
					{
						Id = 2,
						Name = "Software",
						GroupId = 2
					},
					new OrganizationKind
					{
						Id = 100,
						Name = "Other"
					});

			modelBuilder.Entity<Organization>()
				.HasData(new Organization
					{
						Id = 1,
						Name = "Microsoft",
						Description = "Microsoft Corporation",
						KindId = 2,
						CreateDate = Organization.CreateDateConst,
						UpdateDate = Organization.CreateDateConst
					},
					new Organization
					{
						Id = 2,
						Name = "Oracle",
						KindId = 1,
						CreateDate = Organization.CreateDateConst.AddDays(1)
					},
					new Organization
					{
						Id = 3,
						Name = "Intel",
						KindId = 1,
						CreateDate = Organization.CreateDateConst.AddDays(2)
					},
					new Organization
					{
						Id = 100,
						Name = "-",
						KindId = 100,
						CreateDate = Organization.CreateDateConst
					});

			modelBuilder.Entity<Employee>()
				.HasData(new Employee
					{
						Id = 1,
						Name = "Ivanov",
						Description = "Ivan",
						OrganizationId = 1
					},
					new Employee
					{
						Id = 2,
						Name = "Andrey",
						OrganizationId = 1
					},
					new Employee
					{
						Id = 3,
						Name = "Sergey",
						OrganizationId = 2
					});
		}

		public static QueryDtoMapperTestDbContext GetDbContext()
		{
			var options = new DbContextOptionsBuilder<QueryDtoMapperTestDbContext>()
				.UseInMemoryDatabase("test_QueryDtoMapper")
				.Options;

			var dbContext = new QueryDtoMapperTestDbContext(options);
			dbContext.Database.EnsureCreated();

			return dbContext;
		}
	}
}