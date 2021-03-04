namespace Back.DatReader.Services
{
	public interface IIpAddressConverterService
	{
		/// <summary>
		/// Convert string ip address to uint
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns> null if not valid ip address </returns>
		uint? ConvertFromIpAddressToInteger(string ipAddress);

		/// <summary>
		/// Convert uint address to string
		/// </summary>
		/// <param name="ipAddress"></param>
		/// <returns></returns>
		string ConvertFromIntegerToIpAddress(uint ipAddress);
	}
}