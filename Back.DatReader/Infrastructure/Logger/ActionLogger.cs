using System;
using Serilog;

namespace Back.DatReader.Infrastructure.Logger
{
	public class ActionLogger : IActionLogger
	{
		private readonly ILogger _logger;

		public ActionLogger()
		{
			_logger = Log.Logger;
		}

		public void Information(string message)
		{
			_logger.Information("message: {MESSAGE}", message);
		}

		public void Fatal<TEntity>(Exception exception, string message)
		{
			_logger.Fatal(exception, "entity: {ENT}, message: {MSG}", typeof(TEntity), message);
		}
	}
}