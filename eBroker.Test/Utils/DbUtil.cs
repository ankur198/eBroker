using eBroker.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBroker.Test
{
	internal static class DbUtil
	{
		public static DataContext GetInMemoryDbContext() {
			var builder = new DbContextOptionsBuilder<DataContext>();
			builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
			return new DataContext(builder.Options);
		}
	}
}
