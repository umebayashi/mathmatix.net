﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mathmatix.Common.Analysis
{
	[TestClass]
	public class GammaFunctionTest
	{
		[TestMethod]
		public void TestGamma()
		{
			for (double x = 0.5; x <= 5; x += 0.5)
			{
				var gamma = GammaFunction.Gamma(x);
				Console.WriteLine("Gamma({0}) = {1}", x, gamma);
			}
		}

		[TestMethod]
		public void TestP_Normal()
		{
			//var p_normal = GammaFunction.P_Normal(-2.326347874);
			var pNormal = GammaFunction.P_Normal(1.51);
			Console.WriteLine(pNormal);
		}
	}
}
