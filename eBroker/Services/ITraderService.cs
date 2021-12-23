using eBroker.Models;
using eBroker.ViewModels;

namespace eBroker.Services
{
	public interface ITraderService
	{
		Trader CreateTrader();
		Trader GetTrader(int id);
		void AddFund(TraderAddFundVM traderAddFundVM);
		void BuyEquity(TraderPlaceOrderVM traderPlaceOrderVM);
		void SellEquity(TraderPlaceOrderVM traderPlaceOrderVM);
	}
}
