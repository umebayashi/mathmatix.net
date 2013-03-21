using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mathmatix.Common.Analysis
{
	[TestClass]
	public class BetaFunctionTest
	{
		[TestMethod]
		public void TestBeta()
		{
			foreach (var x in Enumerable.Range(10, 21))
			{
				foreach (var y in Enumerable.Range(10, 21))
				{
					Console.WriteLine("Beta(x = {0}, y = {1}) = {2}", x, y, BetaFunction.Beta(x / 10.0, y / 10.0));
				}
			}
		}

		[TestMethod]
		public void TestP_F()
		{
			var p = BetaFunction.P_F(0.2761709627258, 10, 12);
			Console.WriteLine(p);
		}
	}
}
