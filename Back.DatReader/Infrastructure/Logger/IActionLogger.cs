using System;

namespace Back.DatReader.Infrastructure.Logger
{
	public interface IActionLogger
	{
		void Information(string message);

		void Fatal<TEntity>(Exception exception, string message);
	}
}