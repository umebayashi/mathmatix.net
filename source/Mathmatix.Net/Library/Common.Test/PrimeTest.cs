using System;
using System.Linq;
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
			var primes = Prime.GetPrimes(100000);

			int index = 0;
			foreach (var prime in primes)
			{
				Console.WriteLine("Prime({0}) :  {1}", ++index, prime);
			}
		}

		[TestMethod]
		public void TestFactorize()
		{
			for (var i = 2; i <= 10000; i++)
			{
				var factors = Prime.Factorize(i);
				Console.WriteLine(factors.ToString());
			}
		}

		[TestMethod]
		public void TestFactorize2()
		{
			var factors = Enumerable.Range(1, 10000)
				.Select(x => Prime.Factorize(x))
				.Where(x => x.Factors.Count() == 2 && x.Factors.All(p => p.Multiplier == 1));

			foreach (var factor in factors)
			{
				Console.WriteLine(factor);
			}
		}
	}
}
