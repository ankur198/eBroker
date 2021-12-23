using eBroker.Utils;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eBroker.Test.Utils
{
	public class CalendarUtilShould
	{
		[Theory]
		[InlineData(DayOfWeek.Monday, 12, 20, true)]
		[InlineData(DayOfWeek.Monday, 15, 20, false)]
		[InlineData(DayOfWeek.Friday, 15, 0, true)]
		[InlineData(DayOfWeek.Monday, 8, 20, false)]
		[InlineData(DayOfWeek.Saturday, 10, 20, false)]
		public void SayItIsBusinessHourBetweenMonToFriBetween9To15(DayOfWeek dayOfWeek, int hour, int minute, bool expectedOutput)
		{
			// Arrange
			var dateTime = DateTime.Now;
			while (dateTime.Minute != minute)
			{
				dateTime = dateTime.AddMinutes(1);
			}

			while (dateTime.Hour != hour)
			{
				dateTime = dateTime.AddHours(1);
			}

			while (dateTime.DayOfWeek != dayOfWeek)
			{
				dateTime = dateTime.AddDays(1);
			}

			// Act
			var isBusinessHour = CalendarUtil.IsBusinessHours(dateTime);

			// Assert
			isBusinessHour.Should().Be(expectedOutput);
		}
	}
}
