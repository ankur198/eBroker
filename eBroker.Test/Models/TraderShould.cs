using eBroker.Models;
using eBroker.Utils;
using FluentAssertions;
using Moq;
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
		IDateTimeProvider dateTimeProviderInBusinessHour;
		IDateTimeProvider dateTimeProviderOutsideBusinessHour;

		public TraderShould()
		{
			var dateTimeInBusinessHour = new DateTime(2021, 12, 22, 10, 0, 0);
			dateTimeProviderInBusinessHour = Mock.Of<IDateTimeProvider>(_ => _.Now == dateTimeInBusinessHour);

			var dateTimeOutsideBusinessHour = new DateTime(2021, 12, 25, 10, 0, 0);
			dateTimeProviderOutsideBusinessHour = Mock.Of<IDateTimeProvider>(_ => _.Now == dateTimeOutsideBusinessHour);
		}

		[Fact]
		public void Have0BalanceInitially()
		{
			var sut = new Trader();

			sut.Balance.Should().Be(0);
			sut.Id.Should().Be(0);
		}

		[Fact]
		public void AbleToAddFundsMultipleTime()
		{
			var sut = new Trader();
			sut.AddFund(100).Should().Be(100);
			sut.AddFund(100).Should().Be(100);
			sut.Balance.Should().Be(200);
		}

		[Theory]
		[InlineData(10, 10)]
		[InlineData(100001, 99951)]
		[InlineData(100000, 100000)]
		public void DeductCharges(int amountToAdd, int amountAfterCharges)
		{
			var sut = new Trader();
			sut.AddFund(amountToAdd).Should().Be(amountAfterCharges);

			sut.Balance.Should().Be(amountAfterCharges);
		}

		[Theory]
		[InlineData(10, 2, 20, true)]
		[InlineData(11, 2, 20, false)]
		[InlineData(10, 1, 20, true)]
		public void BuyEquityWithBalance(int price, int quantity, int balance, bool shouldBeAbleToBuy)
		{
			// Arrange
			var equity = new Equity("SBIN", price);
			var sut = new Trader();
			sut.AddFund(balance);

			// Act
			var action = new Action(() => sut.BuyEquity(equity, quantity, dateTimeProviderInBusinessHour));

			if (shouldBeAbleToBuy)
			{
				action();

				// Assert
				sut.Holdings.Should().HaveCount(1);
				sut.Holdings[0].Equity.Should().Be(equity);
				sut.Holdings[0].Quantity.Should().Be(quantity);
			}

			else
			{
				Assert.Throws<InsufficientBalanceException>(action);
				sut.Holdings.Should().HaveCount(0);
			}
		}

		[Fact]
		public void NotAllowBuyingEquityOutsideWorkingHours()
		{
			var equity = new Equity("SBIN", 10);
			var sut = new Trader();
			sut.AddFund(20);

			Assert.Throws<OutOfBusinessHoursException>(() => sut.BuyEquity(equity, 1, dateTimeProviderOutsideBusinessHour));
		}

		[Fact]
		public void NotAllowBuyingEquityWithNotPositiveQuantity()
		{
			var equity = new Equity("SBIN", 10);
			var sut = new Trader();
			sut.AddFund(20);

			Assert.Throws<ArgumentOutOfRangeException>(() => sut.BuyEquity(equity, 0, dateTimeProviderOutsideBusinessHour));
		}

		[Fact]
		public void AllowBuySameEquity()
		{
			var equity = new Equity("SBIN", 10);
			var sut = new Trader();
			sut.AddFund(20);

			sut.BuyEquity(equity, 1, dateTimeProviderInBusinessHour);
			sut.BuyEquity(equity, 1, dateTimeProviderInBusinessHour);

			sut.Holdings.Should().HaveCount(1);
			sut.Holdings[0].Quantity.Should().Be(2);
		}

		[Theory]
		[InlineData(1000, 980)]
		[InlineData(100000, 99950)]
		public void SellEquityWithBalance(int price, double expectedBalance)
		{
			var equity = new Equity("SBIN", 1);
			var sut = new Trader();
			sut.AddFund(1);
			sut.BuyEquity(equity, 1, dateTimeProviderInBusinessHour);

			sut.Balance.Should().Be(0);
			sut.Holdings.Should().HaveCount(1);

			equity = new Equity("SBIN", price);
			sut.SellEquity(equity, 1, dateTimeProviderInBusinessHour);

			sut.Balance.Should().Be(expectedBalance);
		}

		[Fact]
		public void NotAllowSellingEquityIfBalanceWillBecomeLessThanZero()
		{
			var equity = new Equity("SBIN", 1);
			var sut = new Trader();
			sut.AddFund(1);
			sut.BuyEquity(equity, 1, dateTimeProviderInBusinessHour);

			sut.Balance.Should().Be(0);
			sut.Holdings.Should().HaveCount(1);

			Assert.Throws<InsufficientBalanceException>(() => sut.SellEquity(equity, 1, dateTimeProviderInBusinessHour));
		}

		[Fact]
		public void NotAllowSellingEquityIfNotBought()
		{
			var equity = new Equity("SBIN", 1);
			var sut = new Trader();

			sut.Holdings.Should().HaveCount(0);

			Assert.Throws<InsufficientBalanceException>(() => sut.SellEquity(equity, 1, dateTimeProviderInBusinessHour));
		}

		[Fact]
		public void NotAllowSellingEquityIfNotHaveEnoughQuantity()
		{
			var equity = new Equity("SBIN", 1);
			var sut = new Trader();
			sut.AddFund(1);
			sut.BuyEquity(equity, 1, dateTimeProviderInBusinessHour);

			sut.Holdings.Should().HaveCount(1);

			Assert.Throws<InsufficientBalanceException>(() => sut.SellEquity(equity, 2, dateTimeProviderInBusinessHour));
		}

		[Fact]
		public void KeepInHoldingAfterSellingEquityIfHaveExtraQuantity()
		{
			var equity = new Equity("SBIN", 1);
			var sut = new Trader();
			sut.AddFund(100);
			sut.BuyEquity(equity, 2, dateTimeProviderInBusinessHour);

			sut.Holdings.Should().HaveCount(1);
			sut.Holdings[0].Quantity.Should().Be(2);

			sut.SellEquity(equity, 1, dateTimeProviderInBusinessHour);

			sut.Holdings.Should().HaveCount(1);
			sut.Holdings[0].Quantity.Should().Be(1);
		}

		[Fact]
		public void NotAllowSellingEquityOutsideTradingHours()
		{
			var equity = new Equity("SBIN", 1);
			var sut = new Trader();
			sut.AddFund(100);
			sut.BuyEquity(equity, 2, dateTimeProviderInBusinessHour);

			sut.Holdings.Should().HaveCount(1);
			sut.Holdings[0].Quantity.Should().Be(2);

			Assert.Throws<OutOfBusinessHoursException>(() => sut.SellEquity(equity, 1, dateTimeProviderOutsideBusinessHour));

			sut.Holdings.Should().HaveCount(1);
			sut.Holdings[0].Quantity.Should().Be(2);
		}

		[Fact]
		public void NotAllowSellingEquityWithNotPositiveQuantity()
		{
			var equity = new Equity("SBIN", 1);
			var sut = new Trader();
			sut.AddFund(100);
			sut.BuyEquity(equity, 2, dateTimeProviderInBusinessHour);

			sut.Holdings.Should().HaveCount(1);
			sut.Holdings[0].Quantity.Should().Be(2);

			Assert.Throws<ArgumentOutOfRangeException>(() => sut.SellEquity(equity, 0, dateTimeProviderOutsideBusinessHour));

			sut.Holdings.Should().HaveCount(1);
			sut.Holdings[0].Quantity.Should().Be(2);
		}
	}
}
