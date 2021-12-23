using eBroker.Models;
using eBroker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eBroker.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EquityController : ControllerBase, IEquityService
	{
		private readonly IEquityService equityService;

		public EquityController(IEquityService equityService)
		{
			this.equityService = equityService;
		}

		[HttpPost("/create")]
		public Equity CreateEquity([FromBody] Equity equity)
		{
			return equityService.CreateEquity(equity);
		}

		[HttpGet("/all")]
		public List<Equity> GetAll()
		{
			return equityService.GetAll();
		}

		[HttpPost("/update")]
		public Equity UpdatePrice([FromBody] Equity equity)
		{
			return equityService.UpdatePrice(equity);
		}
	}
}
