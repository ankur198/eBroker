using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker.Utils
{
	public class CalenderUtil: ICalenderUtil
	{
		public static bool IsBusinessHours
		{
			get
			{
				var currentTime = DateTime.Now;
				if (currentTime.DayOfWeek is not DayOfWeek.Saturday or DayOfWeek.Sunday && currentTime.Hour >= 9 && currentTime.Hour <= 15)
				{
					return true;
				}
				return false;
			}
		}
	}
}
