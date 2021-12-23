using Xunit;
using eBroker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using eBroker.Utils;
using eBroker.Test;
using eBroker.Models;

namespace eBroker.Services.Tests
{
	public class TraderServiceShould
	{
		private readonly TraderService sut;
		private readonly Equity equity = new Equity("SBIN", 10);

		public TraderServiceShould()
		{
			var dateTimeInBusinessHour = new DateTime(2021, 12, 22, 10, 0, 0);
			var dateTimeProviderInBusinessHour = Mock.Of<IDateTimeProvider>(_ => _.Now == dateTimeInBusinessHour);

			var dbContext = DbUtil.GetInMemoryDbContext();
			dbContext.Add(equity);
			dbContext.SaveChanges();

			sut = new TraderService(dbContext, dateTimeProviderInBusinessHour);
		}


		[Fact()]
		public void PerformAllActions()
		{
			var trader = sut.CreateTrader();
			trader = sut.GetTrader(trader.Id);
			sut.AddFund(new ViewModels.TraderAddFundVM(trader.Id, 100));
			sut.BuyEquity(new ViewModels.TraderPlaceOrderVM(trader.Id, equity.Id, 1));
			sut.SellEquity(new ViewModels.TraderPlaceOrderVM(trader.Id, equity.Id, 1));
		}
	}
}