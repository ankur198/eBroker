using eBroker.Models;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace eBroker.Test.Models
{
	public class EquitiesShould
	{
		[Theory]
		[InlineData(10, true)]
		[InlineData(0, true)]
		[InlineData(-1, false)]
		public void NotHaveNegativePrice(int price, bool isValid)
		{
			if (!isValid)
			{
				Assert.Throws<ValidationException>(() => new Equity("SBIN", price));
			}
			else
			{
				var equity = new Equity("SBIN", price);
				equity.Price.Should().Be(price);
			}
		}

		[Theory]
		[InlineData("SBIN", true)]
		[InlineData("JKCEMENT", true)]
		[InlineData("IT", false)]
		[InlineData(null, false)]
		public void HaveAtleast3CharLongId(string id, bool isValid)
		{
			if (!isValid)
			{
				Assert.Throws<ValidationException>(() => new Equity(id, 10));
			}
			else
			{
				var equity = new Equity(id, 10);
				equity.Id.Should().Be(id);
			}
		}
	}
}
