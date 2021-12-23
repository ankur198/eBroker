using eBroker.Models;
using eBroker.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace eBroker.Test.Services
{
	public class EquityServiceShould
	{
		private readonly EquityService sut;

		public EquityServiceShould()
		{
			var dbContext = DbUtil.GetInMemoryDbContext();
			sut = new EquityService(dbContext);
		}

		[Fact]
		public void HaveNoEquityInDBByDefault()
		{
			var equities = sut.GetAll();
			equities.Should().HaveCount(0, "it is a fresh db");
		}

		[Fact]
		public void SaveNewEquity()
		{
			sut.CreateEquity(new Equity("SBIN", 40));
			sut.GetAll().Should().HaveCount(1);
		}

		[Fact]
		public void ThrowExceptionWhenSavingNull()
		{
			Assert.Throws<ArgumentNullException>(() => sut.CreateEquity(null));
			sut.GetAll().Should().HaveCount(0);
		}

		[Fact]
		public void ThrowExceptionWhenSavingEquityWithSameId()
		{
			sut.CreateEquity(new Equity("SBIN", 10));
			sut.GetAll().Should().HaveCount(1);

			Assert.Throws<InvalidOperationException>(() => sut.CreateEquity(new Equity("SBIN", 20)));
			sut.GetAll().Should().HaveCount(1);

			sut.GetAll()[0].Price.Should().Be(10);

		}

		[Fact]
		public void SaveEquityWithDifferentId()
		{
			sut.CreateEquity(new Equity("SBIN", 10));
			sut.GetAll().Should().HaveCount(1);

			sut.CreateEquity(new Equity("TCS", 20));
			sut.GetAll().Should().HaveCount(2);

			sut.GetAll()[0].Price.Should().Be(10);
			sut.GetAll()[1].Price.Should().Be(20);

		}

		[Fact]
		public void UpdatePrice()
		{
			sut.CreateEquity(new Equity("SBIN", 10));
			sut.GetAll().Should().HaveCount(1);
			sut.GetAll()[0].Price.Should().Be(10);

			sut.UpdatePrice(new Equity("SBIN", 20));
			sut.GetAll().Should().HaveCount(1);
			sut.GetAll()[0].Price.Should().Be(20);

			Assert.Throws<KeyNotFoundException>(() => sut.UpdatePrice(new Equity("ICICI", 20)));
		}
	}
}
