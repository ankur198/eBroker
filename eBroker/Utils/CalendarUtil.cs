using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker.Utils
{
	public static class CalendarUtil
	{
		public static bool IsBusinessHours(this DateTime dateTime)
		{
			var currentTime = dateTime;
			if (currentTime.DayOfWeek is not DayOfWeek.Saturday or DayOfWeek.Sunday && currentTime.Hour >= 9 && currentTime.Hour <= 15)
			{
				if (currentTime.Hour == 15 && currentTime.Minute > 0)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
