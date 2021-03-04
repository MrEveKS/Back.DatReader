using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto.IpIntervalsInformation;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.IpIntervalsInformationServices;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.TestControllers
{
	public class IpController : BaseController<IpIntervalsInformationDto, IpIntervalsInformationFilterDto>
	{
		public IpController(IIpIntervalsInformationService service) : base(service)
		{
		}

		[HttpGet]
		public Task<IQueryResultDto<IpIntervalsInformationDto>> Location(string ip, CancellationToken cancellationToken = default)
		{
			var filter = new QueryDto<IpIntervalsInformationFilterDto>
			{
				Filter = new IpIntervalsInformationFilterDto
					{ IpAddress = ip }
			};

			return GetAll(filter, cancellationToken);
		}

		[NonAction]
		public override Task<IQueryResultDto<IpIntervalsInformationDto>> GetAll(QueryDto<IpIntervalsInformationFilterDto> filter,
																				CancellationToken cancellationToken = default)
		{
			return base.GetAll(filter, cancellationToken);
		}
	}
}