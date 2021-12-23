using eBroker.DataAccess;
using eBroker.Models;
using eBroker.Utils;
using eBroker.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eBroker.Services
{
	public class TraderService : ITraderService
	{
		private readonly DataContext dbContext;
		private readonly IDateTimeProvider dateTimeProvider;

		public TraderService(DataContext dbContext, IDateTimeProvider dateTimeProvider)
		{
			this.dbContext = dbContext;
			this.dateTimeProvider = dateTimeProvider;
		}

		public void AddFund(TraderAddFundVM traderAddFundVM)
		{
			var trader = dbContext.Traders.First(x => x.Id == traderAddFundVM.traderId);
			trader.AddFund(traderAddFundVM.balance);
			dbContext.SaveChanges();
		}

		public void BuyEquity(TraderPlaceOrderVM traderPlaceOrderVM)
		{
			var trader = dbContext.Traders.Include(x => x.Holdings).First(x => x.Id == traderPlaceOrderVM.traderId);
			var equity = dbContext.Equities.First(x => x.Id == traderPlaceOrderVM.equityId);
			trader.BuyEquity(equity, traderPlaceOrderVM.quantity, dateTimeProvider);
			dbContext.SaveChanges();
		}

		public Trader CreateTrader()
		{
			var trader = dbContext.Add(new Trader());
			dbContext.SaveChanges();
			return trader.Entity;
		}

		public Trader GetTrader(int id)
		{
			return dbContext.Traders.Include(x => x.Holdings).First(x => x.Id == id);
		}

		public void SellEquity(TraderPlaceOrderVM traderPlaceOrderVM)
		{
			var trader = dbContext.Traders.Include(x => x.Holdings).First(x => x.Id == traderPlaceOrderVM.traderId);
			var equity = dbContext.Equities.First(x => x.Id == traderPlaceOrderVM.equityId);
			trader.SellEquity(equity, traderPlaceOrderVM.quantity, dateTimeProvider);
			dbContext.SaveChanges();
		}
	}
}
