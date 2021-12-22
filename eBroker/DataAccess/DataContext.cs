using eBroker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace eBroker.DataAccess
{
	public class DataContext: DbContext
	{
		public DataContext(DbContextOptions<DataContext> options): base(options)
		{
		}

		public DbSet<Equity> Equities { get; set; }
		public DbSet<Trader> Traders { get; set; }
		public DbSet<Holding> Holdings { get; set; }
	}
}
