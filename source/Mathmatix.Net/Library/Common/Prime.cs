using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable CheckNamespace
namespace Mathmatix.Common
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// 素数関連ライブラリ
	/// </summary>
	public static class Prime
	{
		#region public method

		/// <summary>
		/// 素数の一覧を取得する
		/// </summary>
		/// <param name="maxValue"></param>
		/// <returns></returns>
		public static IEnumerable<int> GetPrimes(int maxValue)
		{
			bool[] isPrime = new bool[maxValue];

			if (maxValue < 2)
			{
				yield break;
			}

			int sqrtN = (int)Math.Floor(Math.Sqrt(maxValue));
			int n;

			for (int x = 1; x <= sqrtN; ++x)
			{
				for (int y = 1; y <= sqrtN; ++y)
				{
					n = 4 * x * x + y * y;
					if (n <= maxValue && (n % 12 == 1 || n % 12 == 5))
					{
						isPrime[n - 1] = !isPrime[n - 1];
					}

					n = 3 * x * x + y * y;
					if (n <= maxValue && n % 12 == 7)
					{
						isPrime[n - 1] = !isPrime[n - 1];
					}

					n = 3 * x * x - y * y;
					if (x > y && n <= maxValue && n % 12 == 11)
					{
						isPrime[n - 1] = !isPrime[n - 1];
					}
				}
			}

			for (int i = 5; i <= sqrtN; ++i)
			{
				if (isPrime[i - 1])
				{
					for (int k = i * i; k <= maxValue; k += i * i)
					{
						isPrime[k - 1] = false;
					}
				}
			}

			if (maxValue >= 2)
			{
				isPrime[1] = true;
			}
			if (maxValue >= 3)
			{
				isPrime[2] = true;
			}

			for (int i = 0; i < isPrime.Length; i++)
			{
				if (isPrime[i])
				{
					yield return i + 1;
				}
			}
		}

		/// <summary>
		/// 整数を素因数分解する
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static PrimeFactors Factorize(int value)
		{
			switch (value)
			{
				case 1:
					return new PrimeFactors { Factors = new PrimeFactor[0], Sign = 1, Value = 1 };
				case 0:
					return new PrimeFactors { Factors = new PrimeFactor[0], Sign = 1, Value = 0, IsZero = true };
				case -1:
					return new PrimeFactors { Factors = new PrimeFactor[0], Sign = -1, Value = -1 };
			}

			var val = Math.Abs(value);
			var primes = GetPrimes(val);
			var enumerable = primes as int[] ?? primes.ToArray();
			if (enumerable.Max() == val)
			{
				return new PrimeFactors
				{
					Factors = new PrimeFactor[] { new PrimeFactor(val, 1) },
					Sign = Math.Sign(value),
					Value = value,
					IsPrime = true
				};
			}

			var v = val;
			var list = new List<PrimeFactor>();
			foreach (var prime in enumerable)
			{
				while (v % prime == 0 && v > 1)
				{
					var factor = list.FirstOrDefault(x => x.Prime == prime);
					if (factor == null)
					{
						factor = new PrimeFactor(prime, 0);
						list.Add(factor);
					}
					factor.Multiplier++;

					v = (int)(v / prime);
				}
			}

			var result = new PrimeFactors { Factors = list.ToArray(), Sign = Math.Sign(value), Value = value };
			return result;
		}

		#endregion
	}

	/// <summary>
	/// 素因数
	/// </summary>
	public class PrimeFactor
	{
		public PrimeFactor()
		{

		}

		public PrimeFactor(int prime, int multiplier)
		{
			this.Prime = prime;
			this.Multiplier = multiplier;
		}

		/// <summary>
		/// 素数
		/// </summary>
		public int Prime { get; set; }

		/// <summary>
		/// 乗数
		/// </summary>
		public int Multiplier { get; set; }

		#region method

		public override string ToString()
		{
			return string.Format("({0}^{1})", this.Prime, this.Multiplier);
		}

		#endregion
	}

	/// <summary>
	/// 整数の素因数の積と符合による表現
	/// </summary>
	public class PrimeFactors
	{
		/// <summary>
		/// 素因数
		/// </summary>
		public PrimeFactor[] Factors { get; set; }

		/// <summary>
		/// 符合
		/// </summary>
		public int Sign { get; set; }

		/// <summary>
		/// 元の整数値
		/// </summary>
		public int Value { get; set; }

		/// <summary>
		/// 0かどうか
		/// </summary>
		public bool IsZero { get; set; }

		/// <summary>
		/// 素数かどうか
		/// </summary>
		public bool IsPrime { get; set; }

		public override string ToString()
		{
			var value = new StringBuilder();

			value.AppendFormat("(Value:{0}, Sign:{1}, IsZero:{2}, IsPrime:{3} Factors:[", this.Value, this.Sign, this.IsZero, this.IsPrime);
			foreach (PrimeFactor t in this.Factors)
			{
				value.Append(t.ToString());
			}
			value.Append("])");

			return value.ToString();
		}
	}

	/// <summary>
	/// 有理数
	/// </summary>
	public class Rational
	{
		/// <summary>
		/// 分子
		/// </summary>
		public PrimeFactors Numerator { get; set; }

		/// <summary>
		/// 分母
		/// </summary>
		public PrimeFactors Denominator { get; set; }

		/// <summary>
		/// 符合
		/// </summary>
		public int Sign { get; set; }
	}
}
