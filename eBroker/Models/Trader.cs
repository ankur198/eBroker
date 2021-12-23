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

		public void BuyEquity(Equity equity, int quantity, IDateTimeProvider dateTimeProvider)
		{
			if (quantity < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(quantity));
			}

			if (!dateTimeProvider.Now.IsBusinessHours())
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

			else
			{
				throw new InsufficientBalanceException();
			}
		}

		public void SellEquity(Equity equity, int quantity, IDateTimeProvider dateTimeProvider)
		{
			if (quantity < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(quantity));
			}

			if (!dateTimeProvider.Now.IsBusinessHours())
			{
				throw new OutOfBusinessHoursException();
			}

			var holding = Holdings.FirstOrDefault(x => x.Equity.Id == equity.Id);
			var holdingQuantity = holding?.Quantity ?? 0;
			if (holding != null && holdingQuantity >= quantity)
			{
				var sellValue = equity.Price * quantity;
				var sellValueAfterDeduction = sellValue - Math.Max(20, Math.Round(sellValue * 0.05 / 100, 2));

				if (Balance + sellValueAfterDeduction < 0)
				{
					throw new InsufficientBalanceException();
				}

				Balance += sellValueAfterDeduction;

				Holdings.Remove(holding);
				if (holdingQuantity - quantity > 0)
				{
					Holdings.Add(new Holding(equity, holdingQuantity - quantity));
				}
			}
			else
			{
				throw new InsufficientBalanceException("Does not hold required qunatity of equity to sell");
			}
		}

		public double AddFund(double amount)
		{
			var amountAfterDeductions = amount;
			if (amount > 100000)
			{
				amountAfterDeductions = Math.Round(amount * (100 - 0.05) / 100, 2);
			}

			this.Balance += amountAfterDeductions;

			return amountAfterDeductions;
		}
	}
}
