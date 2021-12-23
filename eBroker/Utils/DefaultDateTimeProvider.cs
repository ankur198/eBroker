using System;

namespace eBroker.Utils
{
	public class DefaultDateTimeProvider : IDateTimeProvider
	{
		public DateTime Now => DateTime.Now;
	}
}
