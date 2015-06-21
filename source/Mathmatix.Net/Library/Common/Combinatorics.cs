using System;
using System.Collections.Generic;
using System.Linq;
using Mathmatix.Common.Algebra;

namespace Mathmatix.Common
{
    /// <summary>
    /// シーケンスの要素から順列・組合せを生成する
    /// </summary>
	public static class Combinatorics
    {
        /// <summary>
		/// 順列の場合の数
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static long PermutationCount(long n, long x)
		{
			long result = 1;
			for (var i = 0; i < x; i++)
			{
				result = result * (n - i);
			}
			return result;
		}

		/// <summary>
		/// 組合せの場合の数
		/// </summary>
		/// <param name="n"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		public static long CombinationCount(long n, long x)
		{
			return PermutationCount(n, x) / x.Factorial();
		}

		/// <summary>
		/// シーケンスの要素から順列を列挙する
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static IEnumerable<T[]> Permutations<T>(this IEnumerable<T> source, int length) where T: IEquatable<T>, IComparable<T>
		{
            var combinations = Combinations(source, length);

            foreach (var combination in combinations)
            {
                for (int i = 0; i < combination.Length; i++)
                {
                    foreach (var permutation in GetPermutations(combination, i))
                    {
                        yield return permutation;
                    }
                }
            }
		}

        private static IEnumerable<T[]> GetPermutations<T>(IEnumerable<T> source, int splitIndex)
        {
            var split = new
            {
                Left = source.Where((x, i) => i == splitIndex).ToArray(),
                Right = source.Where((x, i) => i != splitIndex).ToArray()
            };

            if (split.Right.Length == 2)
            {
                yield return new T[] { split.Left[0], split.Right[0], split.Right[1] };
                yield return new T[] { split.Left[0], split.Right[1], split.Right[0] };
            }
            else
            {
                for (int i = 0; i < split.Right.Length; i++)
                {
                    foreach (var item in GetPermutations(split.Right, i))
                    {
                        yield return split.Left.Concat(item).ToArray();
                    }
                }
            }
        }

		/// <summary>
		/// シーケンスの要素から組み合わせを列挙する
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static IEnumerable<T[]> Combinations<T>(this IEnumerable<T> source, int length) where T: IEquatable<T>, IComparable<T>
		{
			int sourceCount = source.Count();
			if (sourceCount < length)
			{
				throw new ArgumentException("'length' must be smaller than 'source.Count()'");
			}

			var flags = new bool[sourceCount];
			for (int i = 0; i < length; i++)
			{
				flags[i] = true;
			}

			var patterns = new List<bool[]>();
			var stack = new Stack<bool[]>();
			patterns.Add(flags);
			stack.Push(flags);

			while (stack.Any())
			{
				var flags1 = stack.Pop();
				for (int i = 0; i < flags1.Length - 1; i++)
				{
					if (!flags1[i] || flags1[i + 1]) continue;
					var flags2 = new bool[sourceCount];
					Array.Copy(flags1, flags2, flags1.Length);

					flags2[i] = flags1[i + 1];
					flags2[i + 1] = flags1[i];

					if (patterns.Any(x => x.SequenceEqual(flags2))) continue;
					patterns.Add(flags2);
					stack.Push(flags2);
				}
			}

			var results = new List<T[]>();
			foreach (var pattern in patterns)
			{
				var result = source.Where((x, i) => pattern[i]).ToArray();
				if (!results.Any(x => result.SequenceEqual(x)))
				{
					results.Add(result);
					yield return result;
				}
			}
		}
	}
}
