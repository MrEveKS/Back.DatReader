using System;
using System.IO.Compression;
using System.Linq;
using Back.DatReader.Constants;
using Back.DatReader.Database;
using Back.DatReader.Errors;
using Back.DatReader.Infrastructure.Logger;
using Back.DatReader.Middleware;
using Back.DatReader.Middleware.DbInitialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
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

			services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
			services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

			services.AddResponseCompression(options =>
			{
				options.EnableForHttps = true;
				options.Providers.Add<GzipCompressionProvider>();
				options.Providers.Add<BrotliCompressionProvider>();

				options.MimeTypes =
					ResponseCompressionDefaults.MimeTypes.Concat(new[]
					{
						"application/javascript",
						"application/json",
						"application/xml",
						"application/text",
						"text/css",
						"text/html",
						"text/json",
						"text/plain",
						"text/xml"
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
			} else
			{
				app.UseExceptionHandler("/error");
				app.UseHsts();
			}

			app.UseResponseCompression();

			switch (isDevelop)
			{
				case true:
					app.UseSwaggerMiddleware();

					break;
				case false:
					app.UseHttpsRedirection();

					break;
			}

			app.UseSerilogRequestLogging();
			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.Map("/error",
				ap => ap.Run(async context =>
				{
					context.Response.ContentType = "application/json";
					var response = JsonConvert.SerializeObject(new ErrorResponse("Exception", context.Response.StatusCode));

					await context.Response
						.WriteAsync(response)
						.ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT);
				}));

			app.UseStatusCodePages(async context =>
			{
				context.HttpContext.Response.ContentType = "application/json";
				var response = string.Empty;

				if (context.HttpContext.Response.StatusCode == 404)
				{
					response = JsonConvert.SerializeObject(new ErrorResponse("Page not found", 404));
				}

				await context.HttpContext.Response
					.WriteAsync(response)
					.ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT);
			});
		}
	}
}