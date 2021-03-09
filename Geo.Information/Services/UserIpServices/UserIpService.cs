using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Geo.Common.Constants;
using Geo.Common.Domain;
using Geo.Common.Dto.Query;
using Geo.Common.Dto.QueryResult;
using Geo.Common.Dto.UserIp;
using Geo.Common.Dto.UserLocation;
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

		public override Task<IQueryResultDto<UserIpDto>> GetAll(QueryDto<UserIpFilterDto> queryDto,
																CancellationToken cancellationToken = default)
		{
			var ipAddressUint = _addressConverterService.ConvertFromIpAddressToInteger(queryDto?.Filter?.IpAddress);

			if (!ipAddressUint.HasValue)
			{
				return GetAllWithLocations(queryDto, cancellationToken);
			}

			queryDto ??= new QueryDto<UserIpFilterDto>();
			queryDto.Filter ??= new UserIpFilterDto();
			queryDto.Filter.IpAddress = null;
			queryDto.Filter.IpFromGreaterEqual ??= ipAddressUint;
			queryDto.Filter.IpToLessEqual ??= ipAddressUint;

			return GetAllWithLocations(queryDto, cancellationToken);
		}

		public async Task<IQueryResultDto<UserLocationDto>> GetUserLocation(QueryDto<UserIpFilterDto> queryDto,
																			CancellationToken cancellationToken = default)
		{
			var userIpDtoResult = await GetAll(queryDto, cancellationToken)
				.ConfigureAwait(AsyncConstants.CONTINUE_ON_CAPTURED_CONTEXT);

			if (userIpDtoResult?.Items == null || userIpDtoResult.Items.Count == 0)
			{
				return new QueryResultDto<UserLocationDto>
					{ Items = new List<UserLocationDto>(0), Count = 0 };
			}

			var locations = userIpDtoResult.Items.Select(v => v.UserLocation).ToList();

			return new QueryResultDto<UserLocationDto>
			{
				Count = userIpDtoResult.Count,
				Items = locations
			};
		}

		private Task<IQueryResultDto<UserIpDto>> GetAllWithLocations(QueryDto<UserIpFilterDto> queryDto,
																	CancellationToken cancellationToken = default)
		{
			var queryDtoMapper = _queryDtoMapper
				.QueryDto(queryDto);

			queryDtoMapper.Query = queryDtoMapper.Query.Include(x => x.UserLocation);

			return queryDtoMapper
				.CustomizeProjection(x => x.UserLocation, x => new UserLocationDto()
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
				.MapQueryAsync(true, cancellationToken: cancellationToken);
		}
	}
}