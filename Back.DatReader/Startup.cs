using System;
using Back.DatReader.Database;
using Back.DatReader.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Back.DatReader
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		private IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var isDevelop = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

			services.AddEntityFrameworkInMemoryDatabase();

			services.AddDbContext<DatDbContext>(options =>
				options.UseInMemoryDatabase(Configuration["DbContext:Name"]));

			services.AddSingleton(Configuration);

			if (isDevelop)
			{
				services.AddSwagger();
			}

			services.AddMvcCore()
				.AddApiExplorer();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var isDevelop = env.IsDevelopment();

			if (isDevelop)
			{
				app.UseDeveloperExceptionPage();
			}

			if (isDevelop)
			{
				app.UseSwaggerMiddleware();
			}

			if (!isDevelop)
			{
				app.UseHttpsRedirection();
			}

			app.UseSerilogRequestLogging();
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}