using System.Threading;
using System.Threading.Tasks;
using Back.DatReader.Controllers.BaseControllers;
using Back.DatReader.Models.Dto.CoordinateInformation;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;
using Back.DatReader.Services.CoordinateInformationServices;
using Microsoft.AspNetCore.Mvc;

namespace Back.DatReader.Controllers.TestControllers
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