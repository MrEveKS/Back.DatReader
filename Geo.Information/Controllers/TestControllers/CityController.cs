using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Common.Dto.UserLocation;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.UserLocationServices;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.TestControllers
{
	public class CityController : BaseController<UserLocationDto, UserLocationFilterDto>
	{
		public CityController(IUserLocationService service) : base(service)
		{
		}

		[HttpGet]
		public Task<IQueryResultDto<UserLocationDto>> Locations(string city, CancellationToken cancellationToken = default)
		{
			var filter = new QueryDto<UserLocationFilterDto>
			{
				Filter = new UserLocationFilterDto
					{ CityEqual = city }
			};

			return GetAll(filter, cancellationToken);
		}

		[NonAction]
		public override Task<IQueryResultDto<UserLocationDto>> GetAll(QueryDto<UserLocationFilterDto> filter,
																	CancellationToken cancellationToken = default)
		{
			return base.GetAll(filter, cancellationToken);
		}
	}
}