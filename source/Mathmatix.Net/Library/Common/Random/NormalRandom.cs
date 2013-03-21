using System;

namespace Mathmatix.Common.Random
{
	/// <summary>
	/// 正規分布乱数クラス
	/// </summary>
	public class NormalRandom
	{
		private static readonly object LockObj = new object();
		private static readonly MtRandom MtRnd = new MtRandom();

		private bool _sw = true;
		private double _t = 0;
		private double _u = 0;

		/// <summary>
		/// 平均0,分散1の正規乱数を返す
		/// </summary>
		/// <returns></returns>
		public double NextDouble()
		{
			lock (LockObj)
			{
				if (_sw)
				{
					_sw = false;

					_t = Math.Sqrt(-2 * Math.Log(1 - MtRnd.NextDouble()));
					_u = 2 * Math.PI * MtRnd.NextDouble();

					return _t * Math.Cos(_u);
				}
				else
				{
					_sw = true;

					return _t * Math.Sin(_u);
				}
			}
		}
	}
}
