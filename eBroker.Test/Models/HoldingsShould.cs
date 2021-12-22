using eBroker.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eBroker.Test.Models
{
	public class HoldingsShould
	{

		private readonly Equity equity = new Equity("SBIN", 480);

		[Theory]
		[InlineData(1)]
		[InlineData(100)]
		[InlineData(0, false)]
		[InlineData(-1, false)]
		public void HaveValidQuantity(int quantity, bool isValid = true)
		{
			if (!isValid)
			{
				Assert.Throws<ValidationException>(() => new Holding(equity, quantity));
			}
			else
			{
				var holding = new Holding(equity, quantity);
				
				holding.Equity.Should().Be(equity);
				holding.Quantity.Should().Be(quantity);
				holding.Id.Should().Be(default);
			}
		}

		[Fact]
		public void HaveNotNullEquity()
		{
			Assert.Throws<ValidationException>(() => new Holding(null, 1));
		}
	}
}
