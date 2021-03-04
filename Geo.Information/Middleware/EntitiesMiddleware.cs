using Geo.Information.Services.CoordinateInformationServices;
using Geo.Information.Services.HeaderServices;
using Geo.Information.Services.IpIntervalsInformationServices;
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
			services.AddScoped<IHeaderService, HeaderService>();
			services.AddScoped<ICoordinateInformationService, CoordinateInformationService>();
			services.AddScoped<IIpIntervalsInformationService, IpIntervalsInformationService>();
		}
	}
}