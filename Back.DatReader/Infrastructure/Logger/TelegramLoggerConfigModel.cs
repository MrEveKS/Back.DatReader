using System.Collections.Generic;
using Serilog.Events;

namespace Back.DatReader.Infrastructure.Logger
{
	public class TelegramLoggerConfigModel
	{
		public string BotId { get; set; }

		public string ChatId { get; set; }

		public LogEventLevel? LogEventLevel { get; set; }

		public List<string> ResponsibleDeveloperLogins { get; set; }
	}
}
