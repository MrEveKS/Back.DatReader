using Geo.Information.Services.HeaderServices;
using Geo.Information.Services.UserIpServices;
using Geo.Information.Services.UserLocationServices;
using Geo.QueryMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Geo.Information.Middleware
{
	public static class EntitiesMiddleware
	{
		/// <summary>
		/// Add services for entities
		/// </summary>
		/// <param name="services"> </param>
		public static void AddEntityServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IQueryDtoMapper<,>), typeof(QueryDtoMapper<,>));
			services.AddScoped<IDatInfoService, DatInfoService>();
			services.AddScoped<IUserLocationService, UserLocationService>();
			services.AddScoped<IUserIpService, UserIpService>();
		}
	}
}