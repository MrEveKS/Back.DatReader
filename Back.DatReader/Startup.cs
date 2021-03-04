using System;
using System.IO.Compression;
using System.Linq;
using Back.DatReader.Constants;
using Back.DatReader.Database;
using Back.DatReader.Infrastructure.Logger;
using Back.DatReader.Middleware;
using Back.DatReader.Middleware.DbInitialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
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

			services.Configure<GzipCompressionProviderOptions>
				(options => options.Level = CompressionLevel.Optimal);
			services.Configure<BrotliCompressionProviderOptions>
				(options => options.Level = CompressionLevel.Optimal);

			services.AddResponseCompression(options =>
			{
				options.EnableForHttps = true;
				options.Providers.Add<GzipCompressionProvider>();
				options.Providers.Add<BrotliCompressionProvider>();
				options.MimeTypes =
					ResponseCompressionDefaults.MimeTypes.Concat(
						new[] {
							"application/javascript",
							"application/json",
							"application/xml",
							"application/text",
							"text/css",
							"text/html",
							"text/json",
							"text/plain",
							"text/xml",
						});
			});

			services.AddSingleton(Configuration);
			services.AddScoped<CoreDbContext, DatDbContext>();
			services.AddScoped<IActionLogger, ActionLogger>();
			services.AddEntityServices();

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

			app.UseResponseCompression();
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