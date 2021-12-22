using System;
using System.ComponentModel.DataAnnotations;

namespace eBroker.Models
{
	public record Holding
	{
		public Holding(Equity equity, int quantity)
		{
			Equity = equity ?? throw new ValidationException($"{nameof(Equity)} cannot be null");
			Quantity = quantity > 0 ? quantity : throw new ValidationException($"{nameof(Quantity)} must be a positive number");
		}

		public Holding(int id, Equity equity, int quantity) : this(equity, quantity)
		{
			Id = id;
		}

		private Holding()
		{ }

		public int Id { get; init; }
		public virtual Equity Equity { get; init; }
		public int Quantity { get; init; }
	}
}