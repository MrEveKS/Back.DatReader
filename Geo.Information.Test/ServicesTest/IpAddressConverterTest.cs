using Geo.Information.Services;
using Xunit;

namespace Geo.Information.Test.ServicesTest
{
	public class IpAddressConverterTest
	{
		private readonly IpAddressConverterService _service;

		public IpAddressConverterTest()
		{
			_service = new IpAddressConverterService();
		}


		[Theory]
		[InlineData((uint) 0xffffffff)]
		[InlineData(100000)]
		public void ConvertFromIpAddressToInteger_Test(uint ipAddress)
		{
			var ipAddressString = _service.ConvertFromIntegerToIpAddress(ipAddress);
			Assert.NotEmpty(ipAddressString);
		}

		[Theory]
		[InlineData("256.0.0.0")]
		[InlineData("0.0.0.0.0")]
		[InlineData("0.0.5_.0")]
		[InlineData("val")]
		public void ConvertFromIpAddressToInteger_Null_Test(string ipAddressString)
		{
			var ipAddress = _service.ConvertFromIpAddressToInteger(ipAddressString);
			Assert.Null(ipAddress);
		}

		[Theory]
		[InlineData("255.0.255.0")]
		[InlineData("127.0.0.1")]
		[InlineData("198.162.0.1")]
		[InlineData("33.0.0.0")]
		public void ConvertFromIpAddressToInteger_NotNull_Test(string ipAddressString)
		{
			var ipAddress = _service.ConvertFromIpAddressToInteger(ipAddressString);
			Assert.NotNull(ipAddress);
		}

	}
}
