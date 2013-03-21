﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CheckNamespace
namespace Mathmatix.Common
// ReSharper restore CheckNamespace
{
	[TestClass]
	public class PrimeTest
	{
		[TestMethod]
		public void TestGetPrime()
		{
			var primes = Prime.GetPrimes(1000);

			int index = 0;
			foreach (var prime in primes)
			{
				Console.WriteLine("Prime({0}) :  {1}", index++, prime);
			}
		}

		[TestMethod]
		public void TestFactorize()
		{
			Prime.GetPrimes(1000);

			for (var i = 2; i <= 1000; i++)
			{
				var factors = Prime.Factorize(i);
				Console.WriteLine(factors.ToString());
			}
		}
	}
}
