using System;

namespace eBroker.Utils
{
	public interface IDateTimeProvider
	{
		DateTime Now { get; }
	}
}
