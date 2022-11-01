using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam {
    class R1P3 {
        static StringBuilder sb = new StringBuilder();
        public static void Main32() {
            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++) {
                sb.AppendLine($"Case #{i}: {solve()}");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }
        static int[,] dp;
        static int[] W;
        static string solve() {
            int N = int.Parse(Console.ReadLine());
            W = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            dp = new int[N, 9000];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < 9000; j++) dp[i, j] = -1;
            return getcount(N - 1, 9000 - 1).ToString();
        }

        static int getcount(int p, int max) {
            if (p < 0 || max < 1) return 0;
            if (p == 0) return W[0] <= max ? 1 : 0;
            if (dp[p, max] == -1) {

                int with = 0;
                int max2 = max - W[p];
                if (max2 >= 0) {
                    if (W[p] * 6 < max2) max2 = W[p] * 6;
                    with = 1 + getcount(p - 1, max2);
                }
                int without = getcount(p - 1, max);

                dp[p, max] = Math.Max(with, without);
            }
            return dp[p, max];
        }
    }
}