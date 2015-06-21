using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mathmatix.Common
{
	/// <summary>
	/// CombinatoricsTest の概要の説明
	/// </summary>
	[TestClass]
	public class CombinatoricsTest
	{
		public CombinatoricsTest()
		{
			//
			// TODO: コンストラクター ロジックをここに追加します
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///現在のテストの実行についての情報および機能を
		///提供するテスト コンテキストを取得または設定します。
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region 追加のテスト属性
		//
		// テストを作成する際には、次の追加属性を使用できます:
		//
		// クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// 各テストを実行する前に、TestInitialize を使用してコードを実行してください
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// 各テストを実行した後に、TestCleanup を使用してコードを実行してください
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void TestPermutationCount()
		{
			var result = Combinatorics.PermutationCount(10, 5);
			Assert.AreEqual<long>(30240, result);
		}

		[TestMethod]
		public void TestCombinationCount()
		{
			var result = Combinatorics.CombinationCount(5, 3);
			Assert.AreEqual<long>(10, result);
		}

		[TestMethod]
		public void TestCombinations()
		{
			//var source = new char[] { 'A', 'A', 'B', 'C', 'D', 'D', 'E' };
            var source = Enumerable.Range(1, 10);
			var combinations = source.Combinations(4);

			int count = 0;
			foreach (var combination in combinations)
			{
				WriteArray<int>(combination, ++count);
			}

			Console.WriteLine();
			Console.Write("組合せ数:{0}", count);
		}

        [TestMethod]
        public void TestPermutations()
        {
            var source = Enumerable.Range(1, 9);
            var permutations = source.Permutations(source.Count());

            int count = 0;
            foreach (var permutation in permutations)
            {
                WriteArray(permutation, ++count);
            }
        }

		private void WriteArray<T>(T[] array, int count)
		{
			var result = new StringBuilder();
			result.AppendFormat("({0})[ ", count.ToString("000"));
			for (int i = 0; i < array.Length; i++)
			{
				result.Append(array[i]);
				if (i < array.Length - 1)
				{
					result.Append(", ");
				}
			}
			result.Append(" ]");

			Console.WriteLine(result.ToString());
		}
	}
}
