using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Geo.Information.Middleware
{
	/// <summary>
	/// Initialize swagger services
	/// </summary>
	public static class SwaggerMiddleware
	{
		/// <summary>
		/// Use swagger on Configure
		/// </summary>
		/// <param name="app"> </param>
		public static void UseSwaggerMiddleware(this IApplicationBuilder app)
		{
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
				c.RoutePrefix = "swagger";
			});
		}

		/// <summary>
		/// Add swagger in ConfigureServices
		/// </summary>
		/// <param name="services"> </param>
		public static void AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",
					new OpenApiInfo
					{
						Title = "Test API",
						Version = "v1",
						Contact = new OpenApiContact
						{
							Name = "Git Hub",
							Email = string.Empty
						}
					});

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				if (File.Exists(xmlPath))
				{
					c.IncludeXmlComments(xmlPath);
				}
			});
		}
	}
}