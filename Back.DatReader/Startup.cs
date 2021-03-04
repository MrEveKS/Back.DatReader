using System;
using Back.DatReader.Controllers;
using Back.DatReader.Database;
using Back.DatReader.Infrastructure.Logger;
using Back.DatReader.Middleware;
using Back.DatReader.Middleware.DbInitialization;
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
			var isDevelop = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == BuildConstants.DEVELOPMENT;

			services.AddDbContext<DatDbContext>(options =>
				options.UseInMemoryDatabase(Configuration["DbContext:Name"]));

			services.AddSingleton(Configuration);
			services.AddScoped<IActionLogger, ActionLogger>();

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
			app.InitializeDatabase<DatDbContext>();

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