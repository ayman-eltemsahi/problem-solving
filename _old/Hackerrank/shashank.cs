using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
class Solutionw {
    const int MOD = 1000000007;
    static string[] Q;
    static List<int> start, end;
    static string all;
    static int[] level;
    static long[,] w;
    static bool[,] wb;
    static List<int>[,] big;
    static void Maidn(String[] args) {
        Console.SetIn(new StreamReader("input"));
        int _tc_ = int.Parse(Console.ReadLine());
        while (_tc_-- > 0) {
            int n = int.Parse(Console.ReadLine());
            Q = new string[n];
            start = new List<int>();
            end = new List<int>();

            bool[] V = new bool[2000];

            V[0] = true;
            StringBuilder sb = new StringBuilder();
            all = "";
            for (int i = 0; i < n; i++) {
                Q[i] = Console.ReadLine().Trim();
                sb.Append(Q[i]);
                if (i == 0) {
                    start.Add(0);
                    end.Add(Q[i].Length - 1);
                } else {
                    V[end.Last() + 1] = true;
                    start.Add(end.Last() + 1);
                    end.Add(start.Last() + Q[i].Length - 1);
                }
            }

            big = new List<int>[n, 26];
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < 26; j++) big[i, j] = new List<int>();
            }

            all = sb.ToString();
            n = all.Length;
            w = new long[n, n];
            wb = new bool[n, n];
            level = new int[n];
            big[0, all[0] - 'a'].Add(0);
            for (int i = 1; i < n; i++) {
                level[i] = level[i - 1];
                if (V[i]) level[i]++;

                big[level[i], all[i] - 'a'].Add(i);

            }


            long ans = 0;
            int s = 0, e = all.Length - 1, l = level.Last();
            for (int i = s; i <= e && level[i] == 0; i++) {
                foreach (var j in big[l, all[i] - 'a'].Where(x => x >= i)) {

                    ans += solve(i + 1, j - 1, 0, l);

                    if (level[j] - level[i] < 2) ans++;

                    ans %= MOD;
                }
            }

            Console.WriteLine(ans);
            Console.WriteLine(189128066);
        }
    }

    static long solve(int s, int e, int l1, int l2) {
        if (e < s) return 0;
        if (e == s) return 1;
        if (wb[s, e]) return w[s, e];

        long r = 0;

        for (int i = s; i <= e && level[i] - l1 < 2; i++) {
            foreach (var j in big[l2, all[i] - 'a'].Where(x => x >= i && x <= e)) {

                r += solve(i + 1, j - 1, level[i], level[j]);


                if (level[j] - level[i] < 2) r++;

                r %= MOD;
            }
        }

        if (l2 > 0) {
            l2--;
            for (int i = s; i <= e && level[i] - l1 < 2; i++) {
                foreach (var j in big[l2, all[i] - 'a'].Where(x => x >= i && x <= e)) {

                    r += solve(i + 1, j - 1, level[i], level[j]);

                    if (level[j] - level[i] < 2) r++;

                    r %= MOD;
                }
            }
        }


        wb[s, e] = true;
        w[s, e] = r;
        return r;
    }

}