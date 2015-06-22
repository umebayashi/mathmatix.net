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
        /// シーケンスの要素から組み合わせを列挙する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static IEnumerable<T[]> Combinations<T>(this IEnumerable<T> source, int length) where T : IEquatable<T>, IComparable<T>
        {
            var sourceCount = source.Count();

            if (sourceCount < length)
            {
                throw new ArgumentException("'length' must be smaller than 'source.Count()'");
            }
            else if (sourceCount == length)
            {
                yield return source.ToArray();
            }
            else
            {
                if (length == 1)
                {
                    foreach (var item in source)
                    {
                        yield return new T[] { item };
                    }
                }
                else
                {
                    var split = new
                    {
                        Left = source.Where((x, i) => i == 0).ToArray(),
                        Right = source.Where((x, i) => i > 0).ToArray()
                    };

                    foreach (var item in split.Right.Combinations(length - 1).ToArray())
                    {
                        yield return split.Left.Concat(item).ToArray();
                    }

                    foreach (var result in split.Right.Combinations(length).ToArray())
                    {
                        yield return result;
                    }
                }
            }
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
            var combinations = source.Combinations(length);

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
    }
}
