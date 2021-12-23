using eBroker.Models;
using eBroker.Services;
using eBroker.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eBroker.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TraderController : ControllerBase, ITraderService
	{
		private readonly ITraderService traderService;

		public TraderController(ITraderService traderService)
		{
			this.traderService = traderService;
		}

		[HttpPost("/addFund")]
		public void AddFund([FromBody] TraderAddFundVM traderAddFundVM)
		{
			traderService.AddFund(traderAddFundVM);
		}

		[HttpPost("/buy")]
		public void BuyEquity([FromBody] TraderPlaceOrderVM traderPlaceOrderVM)
		{
			traderService.BuyEquity(traderPlaceOrderVM);
		}

		[HttpPost("/createTrader")]
		public Trader CreateTrader()
		{
			return traderService.CreateTrader();
		}

		[HttpGet("/get/{id}")]
		public Trader GetTrader(int id)
		{
			return traderService.GetTrader(id);
		}

		[HttpPost("/sell")]
		public void SellEquity([FromBody] TraderPlaceOrderVM traderPlaceOrderVM)
		{
			traderService.SellEquity(traderPlaceOrderVM);
		}
	}
}
