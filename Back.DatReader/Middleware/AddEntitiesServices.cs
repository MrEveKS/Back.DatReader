using Back.DatReader.Database.QueryMapper;
using Back.DatReader.Services.CoordinateInformationServices;
using Back.DatReader.Services.HeaderServices;
using Back.DatReader.Services.IpIntervalsInformationServices;
using Microsoft.Extensions.DependencyInjection;

namespace Back.DatReader.Middleware
{
	public static class AddEntitiesServices
	{
		public static void AddEntityServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IQueryDtoMapper<,>), typeof(QueryDtoMapper<,>));
			services.AddScoped<IHeaderService, HeaderService>();
			services.AddScoped<ICoordinateInformationService, CoordinateInformationService>();
			services.AddScoped<IIpIntervalsInformationService, IpIntervalsInformationService>();
		}
	}
}
