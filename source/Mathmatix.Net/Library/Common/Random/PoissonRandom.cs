using System;
using System.Collections.Generic;

namespace Mathmatix.Common.Random
{
	public class PoissonRandom
	{
		#region constructor

		public PoissonRandom(double lambda)
		{
			_random = new MtRandom();
			_lambda = lambda;
		}
		
		public PoissonRandom(IEnumerable<int> initKey, double lambda)
		{
			_random = new MtRandom(initKey);
			_lambda = lambda;
		}

		#endregion

		#region field

		private readonly MtRandom _random;

		private readonly double _lambda;

		#endregion

		#region method

		public int Next()
		{
			var tmpLambda = Math.Exp(_lambda) * _random.NextDouble();
			var k = 0;
			while (tmpLambda > 1)
			{
				tmpLambda *= _random.NextDouble();
				k++;
			}

			return k;
		}

		#endregion
	}
}
