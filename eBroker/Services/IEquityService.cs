using eBroker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker.Services
{
	public interface IEquityService
	{
		List<Equity> GetAll();
		Equity CreateEquity(Equity equity);
		Equity UpdatePrice(Equity equity);
	}
}
