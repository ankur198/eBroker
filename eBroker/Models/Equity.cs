using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eBroker.Models
{
	public record Equity
	{
		public Equity(string id, double price)
		{
			Id = id ?? throw new ValidationException($"{nameof(Id)} cannot be null");
			Price = price >= 0 ? price : throw new ValidationException($"{Price} cannot be less than 0");

			if (id.Length < 3)
			{
				throw new ValidationException($"{nameof(Id)} should have atleast 3 character");
			}
		}

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public string Id { get; init; }

		[Range(0, double.MaxValue)]
		public double Price { get; init; }
	}
}