using System;
using System.Collections.Generic;
using System.Linq;

namespace UMTProblem {
	class Program {

		static List<int> read() {
			List<int> A = new List<int>();

			// Expecting input of format:
			// 1 2 3 4 5 6 7 8
			string input = Console.ReadLine();

			foreach (string x in input.Split(' ')) {
				int val = int.Parse(x);
				A.Add(val);
			}
			return A;
		}

		static bool solve(List<int> A) {
			// It can be proven that if avg(B) == avg(C), then avg(B) == avg(A)
			// Problem boils down to finding B such that avg(B) == avg(A)
			int sumA = A.Sum();
			// existsB[s][i] = true if there are i elements from A that have sum s
			bool[,] existsB = new bool[sumA + 1, A.Count + 1];

			existsB[0,0] = true;

			int maxSum = 0;
			// Go through all elements of A to add them to B
			for(int i = 0; i < A.Count; ++i) {
				// Start from the largest sum (so far) to add the new number
				for(int s = maxSum; s >= 0; --s) {
					for(int j = i; j >= 0; --j) {
						// If there exists a sum s of j numbers, add A[i] to it
						if (existsB[s, j]) {
							existsB[s + A[i], j + 1] = true;
							maxSum = Math.Max(maxSum, s + A[i]);

							// If there are elements left to put in list C and
							// the average of A is equal to that of B, we have a solution
							if (j + 1 < A.Count && sumA * (j + 1) == (s + A[i]) * A.Count) 
								return true;
						}
					}
				}
			}
			return false;
		}

		static void Main(string[] args) {
			List<int> A = read();
			System.Console.Write(solve(A));
		}
	}
}
