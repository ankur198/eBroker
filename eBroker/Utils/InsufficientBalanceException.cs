using System;

namespace eBroker.Utils
{
	public class InsufficientBalanceException : Exception
	{
		public InsufficientBalanceException()
		{
		}

		public InsufficientBalanceException(string message) : base(message)
		{
		}
	}
}
