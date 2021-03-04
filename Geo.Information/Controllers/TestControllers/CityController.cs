using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto.CoordinateInformation;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.CoordinateInformationServices;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.TestControllers
{
	public class CityController : BaseController<CoordinateInformationDto, CoordinateInformationFilterDto>
	{
		public CityController(ICoordinateInformationService service) : base(service)
		{
		}

		[HttpGet]
		public Task<IQueryResultDto<CoordinateInformationDto>> Locations(string city, CancellationToken cancellationToken = default)
		{
			var filter = new QueryDto<CoordinateInformationFilterDto>
			{
				Filter = new CoordinateInformationFilterDto
					{ CityEqual = city }
			};

			return GetAll(filter, cancellationToken);
		}

		[NonAction]
		public override Task<IQueryResultDto<CoordinateInformationDto>> GetAll(QueryDto<CoordinateInformationFilterDto> filter,
																				CancellationToken cancellationToken = default)
		{
			return base.GetAll(filter, cancellationToken);
		}
	}
}