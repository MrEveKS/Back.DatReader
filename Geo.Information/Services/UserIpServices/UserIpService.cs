using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Constants;
using Geo.Common.Domain;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.UserIp;
using Geo.Common.Dto.UserLocation;
using Geo.Common.Enums;
using Geo.QueryMapper;
using Microsoft.EntityFrameworkCore;

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

		public async Task<UserLocationDto> GetUserLocation(QueryDto<UserIpFilterDto> queryDto,
															CancellationToken cancellationToken = default)
		{
			var ipAddressUint = _addressConverterService.ConvertFromIpAddressToInteger(queryDto?.Filter?.IpAddress);

			if (!ipAddressUint.HasValue && queryDto?.Filter?.IpAddress != null)
			{
				return null;
			}

			queryDto ??= new QueryDto<UserIpFilterDto>();
			queryDto.Filter ??= new UserIpFilterDto();

			if (ipAddressUint.HasValue)
			{
				queryDto.Filter.IpFromGreaterEqual ??= ipAddressUint;

				queryDto.Order = new List<OrderDto>
					{ new() { Field = "IpFrom", Type = OrderType.Asc } };
			}

			var userIpDtoResult = await GetOneWithLocations(queryDto, cancellationToken)
				.ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT);

			if (!(ipAddressUint >= userIpDtoResult?.IpFrom && ipAddressUint <= userIpDtoResult.IpTo))
			{
				queryDto.Filter.IpFromGreaterEqual = null;
				queryDto.Filter.IpToLessEqual = userIpDtoResult?.IpTo - 1;

				queryDto.Order = new List<OrderDto>
					{ new() { Field = "IpTo", Type = OrderType.Desc } };

				userIpDtoResult = await GetOneWithLocations(queryDto, cancellationToken, true)
					.ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT);
			}

			if (!ipAddressUint.HasValue || ipAddressUint >= userIpDtoResult?.IpFrom && ipAddressUint <= userIpDtoResult.IpTo)
			{
				return userIpDtoResult.UserLocation;
			}

			return null;
		}

		private Task<UserIpDto> GetOneWithLocations(QueryDto<UserIpFilterDto> queryDto,
													CancellationToken cancellationToken = default, bool updateQuery = false)
		{
			var queryDtoMapper = QueryDtoMapper
				.QueryDto(queryDto, updateQuery);

			queryDtoMapper.Query = queryDtoMapper.Query.Include(x => x.UserLocation);

			return queryDtoMapper
				.CustomizeProjection(x => x.UserLocation,
					x => new UserLocationDto
					{
						Id = x.UserLocation.Id,
						City = x.UserLocation.City,
						Country = x.UserLocation.Country,
						Latitude = x.UserLocation.Latitude,
						Longitude = x.UserLocation.Longitude,
						Organization = x.UserLocation.Organization,
						Postal = x.UserLocation.Postal,
						Region = x.UserLocation.Region
					})
				.MapQueryOneAsync(cancellationToken: cancellationToken);
		}
	}
}