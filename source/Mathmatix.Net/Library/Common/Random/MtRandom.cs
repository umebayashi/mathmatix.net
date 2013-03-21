using System;
using System.Collections.Generic;
using System.Linq;

namespace Mathmatix.Common.Random
{
	/// <summary>
	/// メルセンヌ・ツイスタ乱数クラス
	/// </summary>
	public class MtRandom
	{
		#region const

		private static readonly object LockObj = new object();

		private const int N = 624;

		private const int M = 397;

		private const uint MatrixA = 0x9908b0dfU;

		private const uint UpperMask = 0x80000000U;

		private const uint LowerMask = 0x7fffffffU;

		#endregion

		#region constructor

		public MtRandom() : this(new int[] { 
			DateTime.Now.Year,
			DateTime.Now.Month,
			DateTime.Now.Day,
			DateTime.Now.Hour, 
			DateTime.Now.Minute, 
			DateTime.Now.Second, 
			DateTime.Now.Millisecond })
		{
		}

		public MtRandom(IEnumerable<int> initKey)
		{
			var init = initKey.Select(x => (uint)x).ToArray();
			InitByArray(init, init.Length);
		}

		#endregion

		#region field

		private readonly uint[] _mt = new uint[N];

		private uint _mti = N + 1;

		#endregion

		#region method

		private void InitGenRand(uint s)
		{
			_mt[0] = s & 0xffffffff;
			for (_mti = 1; _mti < N; _mti++)
			{
				_mt[_mti] = (1812433253U * (_mt[_mti - 1] ^ (_mt[_mti - 1] >> 30)) + _mti);
				_mt[_mti] &= 0xffffffffU;
			}
		}

		private void InitByArray(uint[] initKey, int keyLength)
		{
			InitGenRand(19650218U);

			int i = 1;
			int j = 0;
			for (int k = (N > keyLength ? N : keyLength); k > 0; k--)
			{
				_mt[i] =
					(_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 30)) * 1664525U)) +
					initKey[j] +
					(uint)j;
				_mt[i] &= 0xffffffffU;
				i++;
				j++;
				if (i >= N)
				{
					_mt[0] = _mt[N - 1];
					i = 1;
				}
				if (j >= keyLength)
				{
					j = 0;
				}
			}
			for (int k = N - 1; k > 0; k--)
			{
				_mt[i] = (_mt[i] ^ ((_mt[i - 1] ^ (_mt[i - 1] >> 30)) * 1566083941U)) - 1;
				_mt[i] &= 0xffffffffU;
				i++;
				if (i >= N)
				{
					_mt[0] = _mt[N - 1];
					i = 1;
				}
			}

			_mt[0] = 0x80000000U;
		}

		private uint GenRandInt32()
		{
			uint y;
			var mag01 = new uint[] { 0x0U, MatrixA };

			if (_mti >= N)
			{
				if (_mti == N + 1)
				{
					InitGenRand(5489U);
				}
				for (int kk = 0; kk < N - M; kk++)
				{
					y = (_mt[kk] & UpperMask) | (_mt[kk + 1] & LowerMask);
					_mt[kk] = _mt[kk + M] ^ (y >> 1) ^ mag01[y & 0x1U];
				}
				for (int kk = N - M; kk < N - 1; kk++)
				{
					y = (_mt[kk] & UpperMask) | (_mt[kk + 1] & LowerMask);
					_mt[kk] = _mt[kk + (M - N)] ^ (y >> 1) ^ mag01[y & 0x1U];
				}
				y = (_mt[N - 1] & UpperMask) | (_mt[0] & LowerMask);
				_mt[N - 1] = _mt[M - 1] ^ (y >> 1) ^ mag01[y & 0x1U];

				_mti = 0;
			}

			y = _mt[_mti++];

			y ^= (y >> 11);
			y ^= (y << 7) & 0x9d2c5680U;
			y ^= (y << 15) & 0xefc60000U;
			y ^= (y >> 18);

			return y;
		}

		private int GenRandInt31()
		{
			return (int)(GenRandInt32() >> 1);
		}

		private double GenRandReal1()
		{
			return GenRandInt32() * (1.0 / 4294967295.0);
		}

		#endregion

		#region public method

		/// <summary>
		/// 0以上の乱数を返す
		/// </summary>
		/// <returns></returns>
		public int Next()
		{
			return GenRandInt31();
		}

		/// <summary>
		/// 指定した最大値より小さい0以上の乱数を返す
		/// </summary>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		public int Next(int maxValue)
		{
			lock (LockObj)
			{
				var value = GenRandInt31() % maxValue;
				return value;
			}
		}

		/// <summary>
		/// 指定した範囲内の乱数を返す
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		public int Next(int minValue, int maxValue)
		{
			lock (LockObj)
			{
				var value = (GenRandInt31() % (maxValue - minValue + 1)) + minValue;
				return value;
			}
		}

		/// <summary>
		/// 0.0と1.0の間の乱数を返す
		/// </summary>
		/// <returns></returns>
		public double NextDouble()
		{
			lock (LockObj)
			{
				return GenRandReal1();
			}
		}

		#endregion
	}
}
