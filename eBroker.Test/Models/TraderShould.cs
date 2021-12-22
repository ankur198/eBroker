using eBroker.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eBroker.Test.Models
{
	public class TraderShould
	{
		[Fact]
		public void Have0BalanceInitially()
		{
			var sut = new Trader();

			sut.Balance.Should().Be(0);
		}

		[Theory]
		[InlineData(10,10)]
		[InlineData(100001, 99951)]
		[InlineData(100000, 100000)]
		public void DeductCharges(int amountToAdd, int amountAfterCharges)
		{
			var sut = new Trader();
			sut.AddFund(amountToAdd).Should().Be(amountAfterCharges);

			sut.Balance.Should().Be(amountAfterCharges);
		}
	}
}
