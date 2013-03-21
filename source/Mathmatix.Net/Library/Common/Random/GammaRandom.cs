using System;

namespace Mathmatix.Common.Random
{
	public class GammaRandom
	{
		private static readonly object LockObject = new object();
		private static readonly MtRandom MtRnd = new MtRandom();

		public double NextDouble(double a)
		{
			lock (LockObject)
			{
				double x;
				if (a > 1)
				{
					double t = Math.Sqrt(2 * a - 1);
					double u, y;

					do
					{
						do
						{
							do
							{
								x = 1 - MtRnd.NextDouble();
								y = 2 * MtRnd.NextDouble() - 1;
							} while (x * x + y * y > 1);

							y /= x;
							x = t * y + a - 1;
						} while (x <= 0);

						u = (a - 1) * Math.Log(x / (a - 1)) - t * y;
					} while (u < -50 || MtRnd.NextDouble() > (1 + y * y) * Math.Exp(u));
				}
				else
				{
					double t = Math.E / (a + Math.E);
					double y;

					do
					{
						if (MtRnd.NextDouble() < t)
						{
							x = Math.Pow(MtRnd.NextDouble(), 1 / a);
							y = Math.Exp(-x);
						}
						else
						{
							x = 1 - Math.Log(MtRnd.NextDouble());
							y = Math.Pow(x, a - 1);
						}
					} while (MtRnd.NextDouble() >= y);
				}

				return x;
			}
		}
	}
}
