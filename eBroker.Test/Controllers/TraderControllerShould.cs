using Xunit;
using eBroker.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eBroker.Controllers;
using eBroker.Models;
using eBroker.Services;
using FluentAssertions;
using Moq;
using eBroker.ViewModels;

namespace eBroker.Test.Controllers
{
	public class TraderControllerShould
	{
		private readonly Equity equity = new Equity("SBIN", 10);

		[Fact()]
		public void TraderController()
		{
			var mockEquityService = new Mock<ITraderService>();
			new TraderController(mockEquityService.Object);
		}

		[Fact()]
		public void AddFund()
		{
			var mockEquityService = new Mock<ITraderService>();
			var sut = new TraderController(mockEquityService.Object);
			var traderAddFundVM = new TraderAddFundVM(1, 100);

			sut.AddFund(traderAddFundVM);
			mockEquityService.Verify(x => x.AddFund(traderAddFundVM), Times.Once);
		}

		[Fact()]
		public void BuyEquity()
		{
			var mockEquityService = new Mock<ITraderService>();
			var sut = new TraderController(mockEquityService.Object);
			var traderPlaceOrderVM = new TraderPlaceOrderVM(1, "SBIN", 5);

			sut.BuyEquity(traderPlaceOrderVM);
			mockEquityService.Verify(x => x.BuyEquity(traderPlaceOrderVM), Times.Once);
		}

		[Fact()]
		public void CreateTrader()
		{
			var mockEquityService = new Mock<ITraderService>();
			var sut = new TraderController(mockEquityService.Object);

			sut.CreateTrader();
			mockEquityService.Verify(x => x.CreateTrader(), Times.Once);
		}

		[Fact()]
		public void GetTrader()
		{
			var mockEquityService = new Mock<ITraderService>();
			var sut = new TraderController(mockEquityService.Object);

			sut.GetTrader(1);
			mockEquityService.Verify(x => x.GetTrader(1), Times.Once);
		}

		[Fact()]
		public void SellEquity()
		{
			var mockEquityService = new Mock<ITraderService>();
			var sut = new TraderController(mockEquityService.Object);
			var traderPlaceOrderVM = new TraderPlaceOrderVM(1, "SBIN", 5);

			sut.SellEquity(traderPlaceOrderVM);
			mockEquityService.Verify(x => x.SellEquity(traderPlaceOrderVM), Times.Once);
		}
	}
}