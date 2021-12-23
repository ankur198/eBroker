using eBroker.Controllers;
using eBroker.Models;
using eBroker.Services;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace eBroker.Test.Controllers
{
	public class EquityControllerShould
	{
		private readonly Equity equity = new Equity("SBIN", 10);

		[Fact]
		public void CreateEquity()
		{
			var mockEquityService = new Mock<IEquityService>();
			var sut = new EquityController(mockEquityService.Object);
			var returns = sut.CreateEquity(equity);

			mockEquityService.Verify(x => x.CreateEquity(equity), Times.Once);
		}

		[Fact]
		public void GetAll()
		{
			var mockEquityService = new Mock<IEquityService>();
			var sut = new EquityController(mockEquityService.Object);

			sut.GetAll();
			mockEquityService.Verify(x => x.GetAll(), Times.Once);
		}

		[Fact]
		public void UpdatePrice()
		{
			var mockEquityService = new Mock<IEquityService>();
			var updatedEquity = new Equity("SBIN", 20);
			var sut = new EquityController(mockEquityService.Object);

			sut.UpdatePrice(equity);
			mockEquityService.Verify(x => x.UpdatePrice(equity), Times.Once);
		}
	}
}
