using System;

namespace Mathmatix.Common
{
	public static class MathExtensions
	{
		#region static method

		#endregion

		#region extention method

		/// <summary>
		/// 階乗
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int Factorial(this int value)
		{
			if (value < 0)
			{
				throw new ArgumentException("1未満の値は指定できません");
			}
			if (value == 0 || value == 1)
			{
				return 1;
			}
			return value * Factorial(value - 1);
		}

		/// <summary>
		/// 階乗
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static long Factorial(this long value)
		{
			if (value < 0L)
			{
				throw new ArgumentException("1未満の値は指定できません");
			}
			if (value == 0L || value == 1L)
			{
				return 1L;
			}
			return value * Factorial(value - 1L);
		}

		private const double Tolerance = 1e10d;

		public static bool IsZero(double value)
		{
			return (Math.Abs(value) < Tolerance);
		}

		#endregion
	}
}
