using System;
using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Domain;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Common.Dto.UserIp;
using Geo.QueryMapper;

namespace Geo.Information.Services.UserIpServices
{
	public class UserIpService : BaseApiService<UserIp, UserIpDto,
									UserIpFilterDto>, IUserIpService
	{
		private readonly IIpAddressConverterService _addressConverterService;

		public UserIpService(IQueryDtoMapper<UserIp, UserIpDto> queryDtoMapper,
							IIpAddressConverterService addressConverterService) : base(queryDtoMapper)
		{
			_addressConverterService = addressConverterService;
		}

		public override Task<IQueryResultDto<UserIpDto>> GetAll(QueryDto<UserIpFilterDto> queryDto,
																CancellationToken cancellationToken = default)
		{
			var ipAddressUint = _addressConverterService.ConvertFromIpAddressToInteger(queryDto.Filter?.IpAddress);

			if (!ipAddressUint.HasValue)
			{
				return base.GetAll(queryDto, cancellationToken);
			}

			queryDto.Filter ??= new UserIpFilterDto();
			queryDto.Filter.IpFromGreaterEqual ??= ipAddressUint;
			queryDto.Filter.IpToLessEqual ??= ipAddressUint;

			return base.GetAll(queryDto, cancellationToken);
		}
	}
}