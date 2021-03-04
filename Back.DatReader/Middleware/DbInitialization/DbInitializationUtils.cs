using System;
using System.Diagnostics;
using Back.DatReader.Infrastructure.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Back.DatReader.Middleware.DbInitialization
{
	public static class DbInitializationUtils
	{
		/// <summary>
		/// Initialize Database
		/// </summary>
		/// <param name="app"></param>
		public static void InitializeDatabase<TContext>(this IApplicationBuilder app)
			where TContext : DbContext
		{
			var initialized = true;
			var timer = Stopwatch.StartNew();
			using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

			if (serviceScope == null)
			{
				return;
			}

			var provider = serviceScope.ServiceProvider;
			var logger = provider.GetService<IActionLogger>();

			try
			{
				var context = provider.GetService<TContext>();
				context?.Database.EnsureCreated();
			}
			catch (Exception ex)
			{
				initialized = false;
				logger?.Fatal<TContext>(ex, "Database initialize error");

				throw;
			}
			finally
			{
				var time = timer.Elapsed.TotalMilliseconds;
				var message = $"Database {(initialized ? "" : "not ")} initialized, time: {time: 0.0000} ms";
				logger?.Information(message);
			}
		}
	}
}