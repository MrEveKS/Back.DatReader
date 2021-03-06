using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Common.Dto.UserIp;
using Geo.Information.Controllers.BaseControllers;
using Geo.Information.Services.UserIpServices;
using Microsoft.AspNetCore.Mvc;

namespace Geo.Information.Controllers.TestControllers
{
	public class IpController : BaseSpaController<UserIpDto, UserIpFilterDto>
	{
		public IpController(IUserIpService service) : base(service)
		{
		}

		[HttpGet]
		public Task<IQueryResultDto<UserIpDto>> Location(string ip, CancellationToken cancellationToken = default)
		{
			var filter = new QueryDto<UserIpFilterDto>
			{
				Filter = new UserIpFilterDto
					{ IpAddress = ip },
				WithCount = true
			};

			return GetAll(filter, cancellationToken);
		}

		[NonAction]
		public override Task<IQueryResultDto<UserIpDto>> GetAll(QueryDto<UserIpFilterDto> filter,
																CancellationToken cancellationToken = default)
		{
			return base.GetAll(filter, cancellationToken);
		}
	}
}