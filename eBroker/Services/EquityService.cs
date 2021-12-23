using eBroker.DataAccess;
using eBroker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBroker.Services
{
	public class EquityService : IEquityService
	{
		private readonly DataContext dbContext;

		public EquityService(DataContext dbContext) => this.dbContext = dbContext;

		public Equity CreateEquity(Equity equity)
		{
			var equitySaved = dbContext.Add(equity);
			dbContext.SaveChanges();
			return equitySaved.Entity;
		}

		public List<Equity> GetAll() => dbContext.Equities.ToList();

		public Equity UpdatePrice(Equity equity)
		{
			dbContext.ChangeTracker.Clear();
			var equityFromDb = dbContext.Equities.AsNoTracking().FirstOrDefault(x => x.Id == equity.Id);
			if (equityFromDb != null)
			{
				var equityToSave = new Equity(equity.Id, equity.Price);
				var equityUpdated = dbContext.Update(equityToSave);
				dbContext.SaveChanges();
				return equityUpdated.Entity;
			}
			throw new KeyNotFoundException($"Equity with {nameof(equity.Id)} of {equity.Id} not found");
		}
	}
}
