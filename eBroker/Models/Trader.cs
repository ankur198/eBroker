using eBroker.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eBroker.Models
{
	public class Trader
	{

		public int Id { get; set; }
		public double Balance { get; private set; }

		public virtual List<Holding> Holdings { get; } = new();

		public void BuyEquity(Equity equity, int quantity)
		{
			if (!ICalenderUtil.IsBusinessHours)
			{
				throw new OutOfBusinessHoursException();
			}

			var fundsRequired = equity.Price * quantity;

			if (Balance >= fundsRequired)
			{
				Balance -= fundsRequired;
				var holding = Holdings.FirstOrDefault(x => x.Equity.Id == equity.Id);
				var holdingQuantity = holding?.Quantity ?? 0;
				if (holding is not null)
				{
					Holdings.Remove(holding);
				}

				Holdings.Add(new Holding(equity, holdingQuantity + quantity));
			}
		}

		public void SellEquity(Equity equity, int quantity)
		{
			if (!ICalenderUtil.IsBusinessHours)
			{
				throw new OutOfBusinessHoursException();
			}

			var holding = Holdings.FirstOrDefault(x => x.Equity.Id == equity.Id);
			var holdingQuantity = holding?.Quantity ?? 0;
			if (holding != null && holdingQuantity >= quantity)
			{
				var value = holding.Equity.Price * quantity;
				var valueAfterDeduction = value - Math.Max(20, Math.Round(value * 0.05 / 100, 2));
				Balance += valueAfterDeduction;

				Holdings.Remove(holding);
				Holdings.Add(new Holding(equity, holdingQuantity - quantity));
			}
			else
			{
				throw new ArgumentException("Does not hold required qunatity of equity to sell");
			}
		}

		public double AddFund(double amount)
		{
			var amountAfterDeductions = amount;
			if (amount > 100000)
			{
				amountAfterDeductions = Math.Round(amount * (100 - 0.05) / 100, 2);
			}

			this.Balance = amountAfterDeductions;

			return amountAfterDeductions;
		}
	}
}
