using System;
using System.Collections.Generic;
using System.Linq;
using Mathmatix.Common.Algebra;

namespace Mathmatix.Common
{
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
			return null;
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

	public abstract class CombinatoricsGenerator<T> where T: IEquatable<T>, IComparable<T>
	{
		protected CombinatoricsGenerator()
		{
			Result = new List<T[]>();
		}

		protected List<T[]> Result { get; set; }

		public abstract IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false);

		protected void SortResult()
		{
			Result.Sort((x, y) =>
			{
				for (int i = 0; i < x.Length; i++)
				{
					if (x[i].Equals(y[i])) continue;
					return x[i].CompareTo(y[i]);
				}

				return 0;
			});
		}
	}

	public class CombinationGenerator<T> : CombinatoricsGenerator<T> where T : IEquatable<T>, IComparable<T>
	{
		private int SourceLength { get; set; }
		private int TargetLength { get; set; }
		private List<bool[]> Patterns { get; set; }

		/// <summary>
		/// 要素の組み合わせの一覧を作成する
		/// </summary>
		/// <param name="source"></param>
		/// <param name="length"></param>
		/// <param name="allowDuplicate"></param>
		/// <exception cref="NotImplementedException"></exception>
		/// <returns></returns>
		public override IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			var enumerable = source as T[] ?? source.ToArray();
			SourceLength = enumerable.Count();
			TargetLength = length;
			Patterns = new List<bool[]>();

			if (allowDuplicate)
			{
				throw new NotImplementedException();
			}

			var flags = new bool[SourceLength];
			for (var i = 0; i < TargetLength; i++)
			{
				flags[i] = true;
			}
			EnumPatterns(flags);

			foreach (var pattern in Patterns)
			{
				var pattern1 = pattern;
				Result.Add(enumerable.Where((x, i) => pattern1[i]).ToArray());
			}

			SortResult();

			return Result;
		}

		private void EnumPatterns(bool[] flags)
		{
			Patterns.Add(flags);

			var stack = new Stack<bool[]>();
			stack.Push(flags);

			while (stack.Any())
			{
				var flags1 = stack.Pop();
				for (int i = 0; i < flags1.Length - 1; i++)
				{
					if (!flags1[i] || flags1[i + 1]) continue;
					var flags2 = new bool[SourceLength];
					Array.Copy(flags1, flags2, flags1.Length);

					flags2[i] = flags1[i + 1];
					flags2[i + 1] = flags1[i];

					//if (!Patterns.Exists(x => x.SequenceEqual(flags2)))
					if (Patterns.Any(x => x.SequenceEqual(flags2))) continue;
					Patterns.Add(flags2);
					stack.Push(flags2);
				}
			}
		}
	}

	/// <summary>
	/// 要素の順列の一覧を作成する
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class PermutationGenerator<T> : CombinatoricsGenerator<T> where T : IEquatable<T>, IComparable<T>
	{
		public override IEnumerable<T[]> Generate(IEnumerable<T> source, int length, bool allowDuplicate = false)
		{
			// 組み合わせの一覧を取得する
			var genCombination = new CombinationGenerator<T>();
			var combinations = genCombination.Generate(source, length, allowDuplicate).ToArray();

			// 置換群の一覧を取得する(組み合わせの並べ替えに使用)
			var permutationGroups = PermutationGroup.Calculate(length).ToArray();

			if (allowDuplicate)
			{
				throw new NotImplementedException();
			}
			foreach (var combination in combinations)
			{
				foreach (var permutationGroup in permutationGroups)
				{
					var copy = new T[combination.Length];

					for (int i = 0; i < copy.Length; i++)
					{
						copy[i] = combination[permutationGroup.Vector[i]];
					}

					Result.Add(copy);
				}
			}

			SortResult();

			return Result;
		}
	}
}
