namespace Mathmatix.Common.Random
{
	/// <summary>
	/// カイ2乗分布乱数クラス
	/// </summary>
	public class Chi2Random
	{
		private static readonly object LockObj = new object();
		private static readonly NormalRandom NormalRnd = new NormalRandom();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="freedomDegree">自由度</param>
		/// <returns></returns>
		public double NextDouble(int freedomDegree)
		{
			lock (LockObj)
			{
				double s = 0;
				for (var i = 0; i < freedomDegree; i++)
				{
					double r = NormalRnd.NextDouble();
					s += r * r;
				}

				return s;
			}
		}
	}
}
