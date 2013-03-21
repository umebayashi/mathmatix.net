namespace Mathmatix.Common.Random
{
	/// <summary>
	/// F分布乱数生成クラス
	/// </summary>
	public class FRandom
	{
		private static readonly object LockObj = new object();
		private static readonly Chi2Random Chi2Rnd = new Chi2Random();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="freedomDegree1"></param>
		/// <param name="freedomDegree2"></param>
		/// <returns></returns>
		public double NextDouble(int freedomDegree1, int freedomDegree2)
		{
			lock (LockObj)
			{
				var r1 = Chi2Rnd.NextDouble(freedomDegree1);
				var r2 = Chi2Rnd.NextDouble(freedomDegree2);

				return (r1 * freedomDegree2) / (r2 * freedomDegree1);
			}
		}
	}
}
