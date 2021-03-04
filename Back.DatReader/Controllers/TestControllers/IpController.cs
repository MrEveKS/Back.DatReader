using System.Threading;
using System.Threading.Tasks;
using Back.DatReader.Controllers.BaseControllers;
using Back.DatReader.Models.Dto.IpIntervalsInformation;
using Back.DatReader.Models.Dto.Query;
using Back.DatReader.Models.Dto.QueryResult;
using Back.DatReader.Services.IpIntervalsInformationServices;
using Microsoft.AspNetCore.Mvc;

namespace Back.DatReader.Controllers.TestControllers
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
					{ UserIp = ip }
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