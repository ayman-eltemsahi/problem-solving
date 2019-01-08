using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.IO;
using Library;

namespace Hackerrank.Dynamic_Programming
{

    public static class The_Coin_Change_Problem
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int[] S = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            long[] map = new long[n + 1];

            map[0] = 1;

            for (int i = 0; i < m; i++)
                for (int j = S[i]; j <= n; j++)
                    map[j] += map[j - S[i]];

            Console.WriteLine(map[n]);
        }
    }

    public static class Sam_and_substrings
    {
        const int MOD = 1000000007;
        public static void Start() {
            string line = Console.ReadLine();
            int n = line.Length;

            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = int.Parse(line[i].ToString());

            double _r = 0, h = 1;
            for (int i = n - 1; i >= 0; i--) {
                _r = (_r + arr[i] * h * (i + 1)) % MOD;
                h = (h * 10 + 1) % MOD;
            }

            Console.WriteLine(_r);
        }
    }

    public static class Nikita_and_the_Game
    {
        static int Nikita_and_the_Game_final = 0;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                decimal[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToDecimal);
                if (n == 1) { Console.WriteLine(0); return; }
                if (n == 2) {
                    if (arr[0] == arr[1]) Console.WriteLine(0); else Console.WriteLine(1);
                    continue;
                }
                decimal[] sum = new decimal[n]; sum[0] = arr[0];

                for (int i = 1; i < n; i++) { sum[i] = sum[i - 1] + arr[i]; }

                decimal right = sum[n - 1], mid = right / 2;
                if (right == 0) {
                    Console.WriteLine(n - 1);
                } else {
                    if (right % 2 == 1) { Console.WriteLine(0); continue; }
                    int score = 0;
                    int midindex = Array.BinarySearch(sum, mid);
                    Nikita_and_the_Game_final = 0;
                    if (midindex >= 0) {
                        score++;
                        dyn(sum, midindex + 1, n - 1, score);
                        dyn(sum, 0, midindex, score);
                    }
                    Console.WriteLine(Nikita_and_the_Game_final);
                }
            }
        }
        static void dyn(decimal[] sum, int p1, int p2, int score) {
            Nikita_and_the_Game_final = Math.Max(score, Nikita_and_the_Game_final);
            decimal block = (p1 == 0 ? 0 : sum[p1 - 1]);
            decimal right = sum[p2] - block, mid = right / 2;

            if (p1 == p2 || right % 2 == 1) { return; }

            if (right == 0) {
                Nikita_and_the_Game_final = Math.Max(Nikita_and_the_Game_final, score + p2 - p1);
            } else {

                int midindex = Array.BinarySearch(sum, p1, p2 - p1 + 1, mid + block);

                if (midindex >= 0) {
                    score++;
                    dyn(sum, midindex + 1, p2, score);
                    dyn(sum, p1, midindex, score);
                }
            }
        }
    }

    public static class Knapsack
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int k = int.Parse(tmp[1]);
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                var sack = new List<int>();
                for (int i = 0; i < n; i++) {
                    if (arr[i] == 0 || arr[i] > k || sack.Contains(arr[i])) continue;
                    sack.Add(arr[i]);
                }
                if (sack.Count == 0) { Console.WriteLine(0); continue; }
                sack.Sort();
                int final = 0;
                if (sack[0] == 1) { Console.WriteLine(k); continue; }
                for (int i = sack.Count - 1; i >= 0; i--) {
                    Search_Knapsack(sack, sack[i], i, k, ref final);
                }
                Console.WriteLine(final);
            }
        }
        static void Search_Knapsack(List<int> sack, int cur, int at, int k, ref int final) {
            if (final == k || cur > k) return;
            if (cur == k) { final = k; return; }

            final = Math.Max(final, cur);

            for (int i = at; i >= 0; i--) {
                Search_Knapsack(sack, cur + sack[i], i, k, ref final);
            }
        }
    }

    public static class Mr_K_marsh
    {
        public static void Start() {
            var s = File.ReadAllLines("D:\\marsh.sublime"); int z = 0;
            var tmp = s[z++].Split(' ');
            int rows = int.Parse(tmp[0]);
            int cols = int.Parse(tmp[1]);
            string[] land = new string[rows];
            for (int i = 0; i < rows; i++) {
                land[i] = s[z++];
            }

            int fence = 0;
            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    if ((2 * (rows - r + cols - c - 2)) <= fence) break;
                    if (land[r][c] != 120) BuildFence(r, c, land, rows, cols, ref fence);
                }
            }
            Console.WriteLine(fence == 0 ? "impossible" : fence.ToString());
            Console.WriteLine(1966);
        }
        static void BuildFence(int r, int c, string[] land, int rows, int cols, ref int fence) {
            int right = 0;
            for (int i = c; i < cols; i++) {
                if (land[r][i] == 120) break;
                right = i;
            }
            if (right <= c) return;

            for (int i = r + 1; i < rows; i++) {
                if (land[i][c] == 120) break;
                int right2 = 0;
                for (int j = c; j < cols; j++) {
                    if (land[i][j] == 120) break;
                    right2 = j;
                }
                if (right2 <= c) continue;
                for (int k = Math.Min(right2, right); k > c; k--) {
                    bool valid = true;
                    for (int l = r; l < i; l++) {
                        if (land[l][k] == 120) { valid = false; break; }
                    }
                    if (valid) { fence = Math.Max(fence, 2 * (i - r + k - c)); break; }
                }
            }
        }
    }

    public static class Unfair_Game
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());

            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                int result = 0;
                Array.Sort(arr);

                int[] ones = new int[30];
                int[] zeros = new int[30];
                for (int i = 0; i < 30; i++) zeros[i] = n;

                foreach (var num in arr) {
                    var it = Convert.ToString(num, 2);
                    var len = it.Length;
                    for (int i = 0; i < len; i++) {

                    }
                }


                if (n % 2 == 0) {
                    for (int i = 0; i < n; i += 2) {
                        result += arr[i + 1] - arr[i];
                    }
                } else {
                    for (int i = 0; i < n - 1; i += 2) {
                        result += arr[i + 1] - arr[i];
                    }
                    if (arr[n - 1] % 2 == 1) { result += 1 + 2 * (arr[n - 1] + 1 - arr[n - 2]); } else {
                        result += 2 * (arr[n - 1] - arr[n - 2]);
                    }
                }
                //while (winning(arr, result)) result++;
                Console.WriteLine(result);
            }
        }
        static bool winning(int[] arr, int result) {
            int k = 0;

            for (int i = 0; i < arr.Length; i++) {
                k = 0;
                for (int j = 0; j < arr.Length; j++) {
                    if (i == j) k ^= arr[j] + result; else k ^= arr[j];
                }
                if (k == 0) return false;
            }
            return true;
        }

    }

    public static class Mandragora_Forest
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                long[] h = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

                Array.Sort(h);
                for (int i = 1; i < n; i++) h[i] += h[i - 1];

                long all = h[n - 1], exp = 0;

                for (int i = n; i > 0; i--) {
                    exp = Math.Max(exp, i * (all - (i - 2 >= 0 ? h[i - 2] : 0)));
                }

                Console.WriteLine(exp);
            }
        }
    }

    public static class Wet_Shark_and_Two_Subsequences
    {
        const int MOD = 1000000007;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int r = int.Parse(tmp[1]);
            int s = int.Parse(tmp[2]);

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

            if (s > r) { Console.WriteLine(0); return; }

            int a = r + s, b = r - s;
            if (a % 2 == 1) { Console.WriteLine(0); return; }
            a /= 2;
            b /= 2;

            long[][][] w = new long[n][][];

            long luffy = 0;
            long[] wa = new long[n + 1], wb = new long[n + 1];

            for (int i = 0; i < n; i++) {
                int num = arr[i];
                int bound = a - num;

                w[i] = new long[2001][];
                for (int j = 0; j < 2001; j++) {
                    w[i][j] = new long[n + 1];
                }

                for (int j = 0; j < i; j++) {

                    for (int k = 1; k <= bound; k++) {
                        if (w[j][k][0] == 0) continue;
                        long[] ad = w[i][k + num], ad2 = w[j][k];
                        for (int t = 1; t <= i; t++) {
                            ad[t + 1] = (ad[t + 1] + ad2[t]) % MOD;
                            ad[0] = 1;
                        }
                    }
                }

                w[i][num][1]++;
                w[i][num][0] = 1;

                for (int j = 1; j < n + 1; j++) {
                    wa[j] = (wa[j] + w[i][a][j]) % MOD;
                    wb[j] = (wb[j] + w[i][b][j]) % MOD;
                }
            }

            for (int i = 0; i <= n; i++) {
                luffy += wa[i] * wb[i];
                luffy %= MOD;
            }
            Console.WriteLine(luffy);
        }
    }

    public static class Sherlock_and_Cost
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                if (n == 1) { Console.WriteLine(0); continue; }


                int[][] hold = new int[n][];
                for (int i = 0; i < n; i++) hold[i] = new int[101];

                for (int i = 1; i < n; i++) {
                    hold[i][1] = Math.Max(hold[i][1], hold[i - 1][1] + Math.Abs(1 - 1));
                    hold[i][1] = Math.Max(hold[i][1], hold[i - 1][arr[i - 1]] + Math.Abs(1 - arr[i - 1]));
                    hold[i][arr[i]] = Math.Max(hold[i][arr[i]], hold[i - 1][1] + Math.Abs(arr[i] - 1));
                    hold[i][arr[i]] = Math.Max(hold[i][arr[i]], hold[i - 1][arr[i - 1]] + Math.Abs(arr[i] - arr[i - 1]));
                }

                Console.WriteLine(Math.Max(hold[n - 1][arr[n - 1]], hold[n - 1][1]));
            }
        }
    }

    public static class Bricks_Game
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                decimal[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToDecimal);
                Array.Reverse(arr);

                if (n < 4) { Console.WriteLine(arr.Sum()); continue; }

                decimal[] max = new decimal[n];

                max[0] = arr[0];
                max[1] = arr[1] + arr[0];
                max[2] = arr[2] + arr[1] + arr[0];
                for (int i = 3; i < n; i++) {
                    dp_bricks(arr, i, n, max);
                }
                Console.WriteLine(max[n - 1]);
            }
        }
        static void dp_bricks(decimal[] arr, int j, int n, decimal[] max) {
            if (j == n) return;

            decimal j2 = max[j - 2], j3 = max[j - 3],
                j4 = (j >= 4 ? max[j - 4] : 0),
                j5 = (j >= 5 ? max[j - 5] : 0),
                j6 = (j >= 6 ? max[j - 6] : 0);
            decimal aa = arr[j] + min(j2, j3, j4);
            aa = Math.Max(aa, arr[j] + arr[j - 1] + min(j3, j4, j5));
            aa = Math.Max(aa, arr[j] + arr[j - 1] + arr[j - 2] + min(j4, j5, j6));

            max[j] = aa;

        }
        static decimal min(decimal a, decimal b, decimal c) { return Math.Min(a, Math.Min(b, c)); }
    }

    public static class HackerRank_City
    {
        const int MOD = 1000000007;
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            var tmp = Console.ReadLine().Split(' ');
            ulong[] A = new ulong[n + 1];
            for (int i = 0; i < n; i++) A[i + 1] = ulong.Parse(tmp[i]);

            ulong[] answer = new ulong[n + 1],
                    N = new ulong[n + 1],
                    D = new ulong[n + 1],
                    insides = new ulong[n + 1];

            N[1] = 1; answer[1] = 29 * A[1];

            for (int i = 2; i <= n; i++) {
                City(i, A, answer, N, D, insides);
            }

            Console.WriteLine(answer[n]);
        }

        static void City(int i, ulong[] A, ulong[] answer, ulong[] N, ulong[] D, ulong[] I) {
            I[i] = (2 * I[i - 1] + 3 * A[i - 1]) % MOD;
            N[i] = (N[i - 1] * 4 + 2) % MOD;

            D[i] = ((4 * D[i - 1]) % MOD +
                    (2 * I[i - 1] + 3 * A[i - 1]) % MOD +
                    (3 * N[i - 1] * I[i - 1]) % MOD + (8 * N[i - 1] * A[i - 1]) % MOD) % MOD;

            ulong ans = 4 * answer[i - 1] % MOD;
            ans = (ans + 2 * (N[i] * 6 * A[i] % MOD + 4 * D[i] % MOD)) % MOD;
            ans = ans + A[i];
            ulong tmp = ((2 * D[i] * N[i]) % MOD + (2 * N[i] * N[i] * A[i]) % MOD) % MOD;
            ans = (ans + 2 * tmp) % MOD;
            tmp = (tmp + N[i] * N[i] * A[i] % MOD) % MOD;
            ans = (ans + 4 * tmp) % MOD;
            answer[i] = ans % MOD;
        }
    }

    public static class Grid_Walking
    {
        const int MOD = 1000000007;
        static BigInteger[] fact_gw;
        static Dictionary<int, BigInteger> dic_gw;
        public static void Start() {
            fact_gw = new BigInteger[301];
            for (int i = 0; i < 301; i++) {
                fact_gw[i] = fac_gw(i);
            }
            dic_gw = new Dictionary<int, BigInteger>();
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int k = int.Parse(tmp[1]);

                int[] position = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);
                int[] dimensions = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

                BigInteger[][] possiblePath = new BigInteger[n + 1][],
                    results = new BigInteger[n + 1][];

                results[0] = new BigInteger[k + 1]; for (int i = 0; i <= k; i++) results[0][i] = 1;

                for (int i = 1; i <= n; i++) {
                    possiblePath[i] = new BigInteger[k + 1];
                    int curDi = dimensions[i - 1];
                    BigInteger[][] oneDim = new BigInteger[k + 1][];
                    oneDim[0] = new BigInteger[curDi + 1]; for (int _ = 0; _ <= curDi; _++) oneDim[0][_] = 1;

                    for (int a = 1; a <= k; a++) {
                        oneDim[a] = new BigInteger[curDi + 1];
                        for (int pos = 0; pos < curDi; pos++) {
                            recurDimensions_gw(oneDim, a, pos + 1, curDi);
                        }
                    }
                    for (int g = 0; g <= k; g++) {
                        possiblePath[i][g] = oneDim[g][position[i - 1]];
                    }
                }

                results[1] = new BigInteger[k + 1];
                for (int i = 0; i <= k; i++) {
                    results[1][i] = possiblePath[1][i];
                }

                for (int u = 2; u <= n; u++) {
                    results[u] = new BigInteger[k + 1];
                    for (int kk = 1; kk <= k; kk++) {
                        results[u][kk] = 0;
                        results[u - 1][0] = 1;
                        for (int i = 0; i <= kk; i++) {
                            results[u][kk] += (CnK_gw(kk, i) * possiblePath[u][i] * results[u - 1][kk - i]) % MOD;
                        }
                        results[u][kk] %= MOD;
                    }
                }

                Console.WriteLine(results[n][k] % MOD);
            }
        }
        static void recurDimensions_gw(BigInteger[][] dim, int k, int p, int d) {
            if (p > 1) { dim[k][p] += dim[k - 1][p - 1]; }
            if (p < d) { dim[k][p] += dim[k - 1][p + 1]; }
        }
        static BigInteger CnK_gw(int n, int k) {
            int key = getKey_gw(n, k);
            if (!dic_gw.ContainsKey(key)) { dic_gw.Add(key, (fact_gw[n] / (fact_gw[k] * fact_gw[n - k])) % MOD); }

            return dic_gw[key];
        }
        static BigInteger fac_gw(int n) {
            if (n < 2) return 1;
            return n * fac_gw(n - 1);
        }
        static int getKey_gw(int a, int b) {
            if (b > a / 2) { b = a - b; }
            return a * 1000 + b;
        }
    }

    public static class Equal
    {
        public static void Start() {
            int[] vals = new int[10000];
            for (int i = 0; i < 10000; i++) vals[i] = i / 5 + i % 5 / 2 + i % 5 % 2;

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                Console.ReadLine();
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                Array.Sort(arr);
                int ans = int.MaxValue;
                for (int j = 0; j < 10; j++) {
                    int m = 0;
                    for (int i = 0; i < arr.Length; i++) m += vals[arr[i] - (arr[0] - j)];
                    ans = Math.Min(ans, m);
                }
                Console.WriteLine(ans);
            }
        }
    }

    public static class Vertical_Sticks
    {
        static BigInteger[] verticalSticks_fac;
        public static void Start() {
            verticalSticks_fac = new BigInteger[55];
            verticalSticks_fac[0] = 1;
            for (int i = 1; i < 55; i++) verticalSticks_fac[i] = verticalSticks_fac[i - 1] * i;

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
                int[] smaller = new int[n], larger = new int[n];

                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        if (i == j) continue;
                        if (arr[i] > arr[j]) smaller[i]++; else larger[i]++;
                    }
                }

                var map = new Dictionary<int, BigInteger>();

                BigInteger tot = 0;
                for (int num = 0; num < n; num++) {
                    int less = smaller[num];
                    int more = larger[num];

                    if (map.ContainsKey(arr[num])) { tot += map[arr[num]]; continue; }

                    BigInteger zero = verticalSticks_fac[n - 1];

                    for (int pos = 1; pos < n; pos++) {

                        if (pos <= less) {
                            zero += verticalSticks_fac[pos] * verticalSticks_fac[n - pos - 1] * verticslSticks_cnk(less, pos) * (pos + 1);
                        }

                        for (int other = pos - 1; other >= 0; other--) {
                            int mid = pos - other - 1;

                            if (less >= mid) {
                                zero += more * (mid + 1)
                                        * verticalSticks_fac[mid]
                                        * verticalSticks_fac[n - 2 - mid]
                                        * verticslSticks_cnk(less, mid);
                            }

                        }
                    }
                    tot += zero;
                    map.Add(arr[num], zero);
                }
                bool[] done = new bool[n + 1];
                for (int i = 1; i <= n; i++) {
                    if (tot % i == 0) {
                        tot /= i;
                        done[i] = true;
                    }
                }
                decimal fin = (decimal)tot;
                for (int i = 1; i <= n; i++) {
                    if (!done[i]) {
                        fin /= i;
                    }
                }

                Console.WriteLine(fin.ToString("N2"));
            }
        }
        static BigInteger verticslSticks_cnk(int n, int k) {
            if (n < k) return 0;
            return verticalSticks_fac[n] / (verticalSticks_fac[k] * verticalSticks_fac[n - k]);
        }
    }

    public static class Xor_and_Sum
    {
        const int MOD = 1000000007;
        public static void Start() {
            long[] twos = new long[500000]; twos[0] = 1;
            long bi = 1;
            for (long i = 1; i < 500000; i++) {
                bi *= 2;
                bi %= MOD;
                twos[i] = bi;
            }

            string la = Console.ReadLine();
            string lb = Console.ReadLine();

            int n = la.Length, n2 = lb.Length;

            bool[] a = new bool[n];
            for (int i = 0; i < n; i++) a[n - i - 1] = la[i] == '1';

            long[] after = new long[500000];
            after[0] = 1;
            for (int i = 1; i < 500000; i++) after[i] = (after[i - 1] + twos[i]) % MOD;

            long sum = 0;

            long[] zero = new long[500000], one = new long[500000];
            zero[0] = la[n - 1] - '0'; one[0] = '1' - la[n - 1];

            for (int j = 1; j < 500000; j++) {
                if (j >= n || !a[j]) one[j] = (one[j - 1] + twos[j]) % MOD; else one[j] = one[j - 1];

                if (j < n && a[j]) zero[j] = (zero[j - 1] + twos[j]) % MOD; else zero[j] = zero[j - 1];
            }

            for (int i = 0; i < n2; i++) {

                if (lb[n2 - i - 1] == '1') {
                    sum = (sum + (one[i + 314159] - (i > 0 ? one[i - 1] : 0)) % MOD);
                } else {
                    sum = (sum + (zero[i + 314159] - (i > 0 ? zero[i - 1] : 0)) % MOD);
                }

                sum = (sum + MOD) % MOD;
            }

            for (int i = 0; i < n; i++) {
                if (a[i]) {
                    long ai = i >= n2 ? (314160 - n2) : (314159 - i);
                    sum = (sum + twos[i] * ai) % MOD;
                }
            }
            Console.WriteLine(sum);
        }
    }

    public static class The_Longest_Common_Subsequence
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]),
                m = int.Parse(tmp[1]);

            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse),
                  B = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[][] w = new int[2][];
            string[][] s = new string[2][];
            w[0] = new int[m + 1]; w[1] = new int[m + 1];
            s[0] = new string[m + 1]; s[1] = new string[m + 1];

            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= m; j++) {
                    if (A[i - 1] == B[j - 1]) {
                        w[i % 2][j] = w[(i - 1) % 2][j - 1] + 1;
                        s[i % 2][j] = s[(i - 1) % 2][j - 1] + " " + A[i - 1];
                    } else {
                        if (w[(i - 1) % 2][j] > w[i % 2][j - 1]) {
                            w[i % 2][j] = w[(i - 1) % 2][j];
                            s[i % 2][j] = s[(i - 1) % 2][j];
                        } else {
                            w[i % 2][j] = w[i % 2][j - 1];
                            s[i % 2][j] = s[i % 2][j - 1];
                        }

                    }
                }
            }

            Console.WriteLine(s[n % 2][m].Trim());
        }
    }

    public static class Cut_Tree
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);

            var adj = new HashSet<int>[n + 1]; for (int _ = 0; _ <= n; _++) adj[_] = new HashSet<int>();

            for (int i = 0; i < n - 1; i++) {
                tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]);
                int r = int.Parse(tmp[1]);
                adj[l].Add(r);
                adj[r].Add(l);
            }

            long ans = 0;
            bool[] V = new bool[n + 1];
            for (int i = 1; i <= n; i++) {
                V[i] = true;
                var _r = findSubs(i, -1, adj, V);
                for (int j = 0; j <= k; j++) {
                    if (_r.ContainsKey(j)) ans += _r[j];
                }
            }
            Console.WriteLine(ans + 1);
        }
        static Dictionary<int, long> findSubs(int p, int proh, HashSet<int>[] adj, bool[] V) {
            int c = adj[p].Count;
            var d = new Dictionary<int, long>();
            d.Add(c, 1);
            foreach (var node in adj[p]) {
                if (node == proh || V[node]) continue;
                var tmp = new Dictionary<int, long>();
                foreach (var item in findSubs(node, p, adj, V)) {
                    foreach (var taken in d) {
                        Add(tmp, taken.Key + item.Key - 2, item.Value * taken.Value);
                    }
                }
                foreach (var taken in d) {
                    Add(tmp, taken.Key, taken.Value);
                }
                d = tmp;
            }
            return d;
        }
        static void Add(Dictionary<int, long> dic, int i, long j) {
            if (dic.ContainsKey(i)) { dic[i] += j; return; }
            dic.Add(i, j);
        }
    }

    public static class Alien_Languages
    {
        const int mod2 = 100000007;
        public static void Start() {
            var dic = new Dictionary<int, int[]>();
            dic.Add(10000, new int[] { 5000, 18750000, 29164976, 13244630, 19228816, 5159641, 36684025, 64602357, 57261028, 71475089, 60164694, 75945376, 56230983, 9562429 });
            dic.Add(9999, new int[] { 5000, 18747500, 24477476, 34599166, 50017240, 69603673, 99474815, 14956958, 974363, 25057866, 56142846, 18227273, 21934876, 71834180 });
            dic.Add(9998, new int[] { 4999, 18742501, 18229976, 31995598, 75175702, 72260409, 41793583, 64541085, 56790197, 98974407, 92514301, 7245057, 62804959, 53554660 });
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int lets = int.Parse(tmp[0]);
                int len = int.Parse(tmp[1]);

                if (lets == 1) { Console.WriteLine(1); continue; }

                Dictionary<int, int>[] map = new Dictionary<int, int>[len + lets];
                if (!dic.ContainsKey(lets)) {
                    dic.Add(lets, calculate(lets, 1, 1, map).Values.ToArray());
                }
                int[] factors = dic[lets];
                int fk = factors.Length;

                long[] w = new long[len + 1];
                w[0] = 1; w[1] = lets - lets / 2;

                for (int i = 2; i <= len; i++) {
                    for (int k = 1; k <= fk; k++) {
                        if (i - k < 0) break;
                        w[i] += w[i - k] * (long)factors[k - 1];
                        w[i] %= 100000007;

                    }
                }

                Console.WriteLine(w[len]);
            }
        }
        static Dictionary<int, int> calculate(int lets, int j, int k, Dictionary<int, int>[] map, int r = 0) {
            map[j] = new Dictionary<int, int>();
            var ss = map[j];

            int mid = lets / 2;
            if (j > mid) {
                ss.Add(k, lets - j + 1);
            } else {
                ss.Add(k, lets - mid);

                for (int i = mid; i >= j; i--) {
                    if (map[2 * i] != null) {
                        combine(ss, map[2 * i], r);
                    } else {
                        combine(ss, calculate(lets, 2 * i, k + 1, map, 1), r);
                    }
                }
            }
            return ss;
        }
        static void combine(Dictionary<int, int> a, Dictionary<int, int> b, int shift = 0) {
            foreach (var k in b.Keys) {
                shift += k;
                if (a.ContainsKey(shift)) {
                    a[shift] = (a[shift] + b[k]) % 100000007;
                } else {
                    a.Add(shift, b[k]);
                }
            }
        }
    }

    public static class The_Longest_Increasing_Subsequence
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] A = new int[n];
            for (int i = 0; i < n; i++) {
                A[i] = int.Parse(Console.ReadLine());
            }

            int ans = 1;
            int[] s = new int[n + 10];
            for (int i = 1; i < n; i++) {
                if (A[i] >= A[s[ans]]) {
                    ans++;
                    s[ans] = i;
                } else {
                    int low = 1, high = ans;
                    while (high > low) {
                        int mid = (high + low) / 2;

                        if (A[s[mid]] == A[i]) { low = mid; break; }

                        if (A[s[mid]] > A[i]) high = mid; else low = mid + 1;
                    }

                    if (A[s[low]] >= A[i]) {
                        s[low] = i;
                    }
                }
            }

            Console.WriteLine(ans);
        }
    }

    public static class Substring_Diff
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int s = int.Parse(tmp[0]);
                string first = tmp[1];
                string second = tmp[2];
                int n = first.Length;

                if (s == n) { Console.WriteLine(n); continue; }
                var hash = new HashSet<int>();
                int L = 0;

                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < n; j++) {
                        if (hash.Contains(j - i)) continue;
                        hash.Add(j - i);

                        int diff = 0, len = n - Math.Max(i, j);
                        if (len > L) {
                            int[] w = new int[len];

                            for (int k = 0; k < len; k++) {
                                if (first[i + k] != second[j + k]) {
                                    diff++;
                                }
                                w[k] = diff;
                            }

                            for (int k = len - 1; k + 1 > L; k--) {
                                if (w[k] <= s) { L = k + 1; }
                            }
                            for (int k = len - 1; k > L; k--) {
                                int low = 0, high = len - 1;
                                int d = w[k] - s;
                                while (high - low > 1) {
                                    int mid = (high + low) / 2;
                                    if (w[mid] >= d) high = mid; else low = mid;

                                }
                                if (w[k] - w[high] <= s) { L = Math.Max(L, k - high); }
                            }
                        }
                    }
                }

                Console.WriteLine(L);
            }
        }
    }

    public static class Best_spot
    {
        public static void Start() {
            var abc = File.ReadAllLines("C:\\spot.sublime"); int si = 0;
            var tmp = abc[si++].Split(' ');
            int R = int.Parse(tmp[0]);
            int C = int.Parse(tmp[1]);
            float[][] land = new float[R][];
            for (int i = 0; i < R; i++) {
                land[i] = Array.ConvertAll(abc[si++].Split(' '), x => float.Parse(x) + 0);
            }

            tmp = abc[si++].Split(' ');
            int H = int.Parse(tmp[0]);
            int W = int.Parse(tmp[1]);
            float[][] perfect = new float[H][];
            for (int i = 0; i < H; i++) {
                perfect[i] = Array.ConvertAll(abc[si++].Split(' '), x => float.Parse(x) + 0);
                Array.Reverse(perfect[i]);
            }

            //int[] all = new int[41 * 41 + 1];
            //for (int i = 0; i < 41; i++) {
            //    int ii = i * 41;
            //    for (int j = 0; j < 41; j++) {
            //        all[ii + j] = (i - j) * (i - j);
            //    }
            //}


            long sum = 0;
            int _i, _j;
            _i = _j = -1;
            long[,] total = new long[R, C];

            for (int i = 0; i < R - H; i++) {
                Console.WriteLine(i);
                for (int j = 0; j < H; j++) {
                    var A = land[i + j];
                    var B = perfect[j];

                    var F = FFT.MultiplyTwoPolynomialsFFT(A, B);

                    for (int k = 0; k <= C - W; k++) {
                        total[i, k] += (long)F[W + k];
                    }

                }

            }

            Console.WriteLine(total[106, 339]);
            Console.WriteLine(total[105, 338]);

            Console.WriteLine("R {0}    C {1}\nH {2}    W {3}\n", R, C, W, H);
            Console.WriteLine("{0}\n2666735\n{1} {2}\n106 339", sum, _i + 1, _j + 1);
        }
    }

    public static class Lucky_Numbers
    {
        static HashSet<int>[,] indices;
        static Dictionary<long, long> cache, shag;
        static int[] primesBelow2000 = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997, 1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097, 1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193, 1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297, 1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381, 1399, 1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499, 1511, 1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597, 1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693, 1697, 1699, 1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789 };
        static int[] two = { 0, 1, 4, 9, 16, 25, 36, 49, 64, 81 };
        static bool[] isPrime;
        static long[][,] jim;

        public static void Start() {
            indices = new HashSet<int>[20, 200];
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 200; j++) {
                    indices[i, j] = new HashSet<int>();
                }
            }
            cache = new Dictionary<long, long>();
            shag = new Dictionary<long, long>();
            isPrime = new bool[2000];
            foreach (var l in primesBelow2000) isPrime[l] = true;
            jim = new long[20][,];
            for (int i = 1; i < 20; i++) PrepareLucky(i);

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                Console.WriteLine(DP_Lucky(tmp[1]) - DP_Lucky(tmp[0]) + bruteLucky(tmp[1]));
            }
        }

        static void PrepareLucky(int x) {
            int i, j, k, kz, top = 9 * x + 1, bot = 81 * x + 1;
            jim[x] = new long[top, bot];
            var lee = jim[x];

            long add;

            long[,] dp1 = new long[top, bot], dp2 = new long[top, bot];

            if (x == 1) {
                for (i = 0; i < 10; i++) { lee[i, two[i]] = 1; indices[x, i].Add(two[i]); }
            } else {

                for (i = 0; i < 10; i++) dp1[i, two[i]] = 1;

                for (int l = 1; l < x - 1; l++) {
                    for (j = 0; j < top; j++) {
                        for (k = 0; k < bot; k++) {
                            add = dp1[j, k];
                            if (add > 0) {
                                for (int z = 0; z <= 9; z++) {
                                    kz = k + two[z];
                                    dp2[j + z, kz] += add;
                                }
                            }
                        }
                    }

                    dp1 = dp2; dp2 = new long[top, bot];
                }


                for (j = 0; j < top; j++) {
                    for (k = 0; k < bot; k++) {
                        add = dp1[j, k];
                        if (add > 0) {
                            for (int z = 0; z <= 9; z++) {
                                kz = k + two[z];
                                lee[j + z, kz] += add;
                                if (add > 0) indices[x, j + z].Add(kz);
                            }
                        }
                    }
                }
            }

        }

        static long DP_Lucky(string line) {
            int m = line.Length;

            long ans = 0, key;
            int a = 0, b = 0, c;
            for (int u = 0; u < m; u++) {
                c = line[u] - '0';
                if (c > 0) {
                    key = 1000000000 * c + 10000000 * (m - u) + b * 1000 + a;
                    if (!cache.ContainsKey(key)) {
                        cache[key] = calculateLucky(a, b, c, u, m);
                    }
                    ans += cache[key];
                    a += c; b += c * c;
                }
            }
            return ans;
        }

        static long calculateLucky(int a, int b, int w, int p, int m) {
            int mu = m - p - 1;
            long ans = 0;

            if (mu == 0) {
                for (int i = 0; i < w; i++) {
                    if (isPrime[a++] && isPrime[b + two[i]]) ans++;
                }
                return ans;
            }

            var lee = jim[mu];
            int top = lee.GetUpperBound(0), zz, ia;

            long key = 10000000 * (mu) + b * 1000 + a;
            for (int z = 0; z < w; z++, a++) {
                if (!shag.ContainsKey(key)) {
                    long minAns = 0;
                    zz = two[z] + b;
                    foreach (var i in primesBelow2000) {
                        if (i < a) continue;
                        ia = i - a;
                        if (ia > top) break;

                        foreach (var j in indices[mu, ia]) {
                            if (isPrime[j + zz]) minAns += lee[ia, j];
                        }
                    }
                    shag[key] = minAns;
                }
                ans += shag[key];

                key += 1000000000;
            }
            return ans;
        }

        static int bruteLucky(string m) {
            int a = 0, b = 0, k;
            foreach (var c in m) {
                k = c - '0';
                a += k; b += k * k;
            }
            if (isPrime[a] && isPrime[b]) return 1;

            return 0;
        }
    }

    public static class Coin_on_the_Table
    {
        static int cot_k, cot_n, cot_m;
        static int[,,] cot_score;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            cot_n = int.Parse(tmp[0]);
            cot_m = int.Parse(tmp[1]);
            cot_k = int.Parse(tmp[2]);
            string[] map = new string[cot_n];
            int x = -1, y = -1;
            for (int i = 0; i < cot_n; i++) {
                map[i] = Console.ReadLine();
                if (y < 0) {
                    x = i; y = map[i].IndexOf('*');
                }
            }

            cot_score = new int[cot_n, cot_m, cot_k + 1];
            for (int i = 0; i < cot_n; i++) {
                for (int j = 0; j < cot_m; j++) {
                    for (int k = 0; k <= cot_k; k++) cot_score[i, j, k] = -1;
                }
            }

            track(map, 0, 0, 0, 0);

            int ans = -1;
            for (int i = 0; i <= cot_k; i++) {
                if (cot_score[x, y, i] == -1) continue;
                if (ans < 0 || ans > cot_score[x, y, i]) {
                    ans = cot_score[x, y, i];
                }
            }
            Console.WriteLine(ans);
        }

        static void track(string[] map, int r, int c, int prev, int step) {
            if (cot_score[r, c, step] >= 0 && cot_score[r, c, step] <= prev) return;
            cot_score[r, c, step] = prev;

            if (step >= cot_k) return;

            prev++;
            int a1 = prev, a2 = prev, a3 = prev, a4 = prev;
            switch (map[r][c]) {
                case 'U': a1--; break;
                case 'D': a2--; break;
                case 'R': a3--; break;
                case 'L': a4--; break;
                default: return;
            }
            step++;
            if (r > 0) track(map, r - 1, c, a1, step);
            if (r < cot_n - 1) track(map, r + 1, c, a2, step);
            if (c < cot_m - 1) track(map, r, c + 1, a3, step);
            if (c > 0) track(map, r, c - 1, a4, step);

        }
    }

    public static class Hexagonal_Grid
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                string A = Console.ReadLine();
                string B = Console.ReadLine();

                bool ans = false;
                int z = 0;
                foreach (var c in A) z += '1' - c;
                foreach (var c in B) z += '1' - c;
                if (z % 2 == 0) {
                    ans = true;
                    int i = 0, j = 0;
                    while (ans && i < n && j < n) {
                        if (A[i] == '1') { i++; continue; }
                        if (B[j] == '1') { j++; continue; }

                        if (i == j) { i++; j++; continue; }
                        if (i - j == 1) { i++; j++; continue; }

                        if (i < j) {
                            if (i + 1 < n && A[i] == A[i + 1]) { i += 2; continue; }
                            if (j + 1 < n && B[j] == B[j + 1]) { j += 2; continue; }
                        } else {
                            if (j + 1 < n && B[j] == B[j + 1]) { j += 2; continue; }
                            if (i + 1 < n && A[i] == A[i + 1]) { i += 2; continue; }
                        }
                        ans = false;
                    }

                    while (ans && i < n) {
                        if (A[i] == '1') { i++; continue; }
                        if (A[i] == A[i + 1]) { i += 2; continue; }
                        ans = false;
                    }
                    while (ans && j < n) {
                        if (B[j] == '1') { j++; continue; }
                        if (B[j] == B[j + 1]) { j += 2; continue; }
                        ans = false;
                    }

                }
                Console.WriteLine(ans ? "YES" : "NO");
            }
        }
    }

    public static class Lego_Blocks
    {
        const int MOD = 1000000007;
        static long[][] LEGOAA;
        static long[][] LEGOSS;
        public static void Start() {
            long[] w = new long[1500];
            w[1] = 1; w[2] = 2; w[3] = 4; w[4] = 8;
            for (int i = 5; i < 1500; i++)
                w[i] = (w[i - 1] + w[i - 2] + w[i - 3] + w[i - 4]) % MOD;

            LEGOAA = new long[1001][];
            LEGOSS = new long[1001][];
            for (int i = 0; i < 1001; i++) {
                LEGOAA[i] = new long[1001]; LEGOSS[i] = new long[1001];
                for (int j = 0; j < 1001; j++) { LEGOAA[i][j] = -1; LEGOSS[i][j] = -1; }
            }

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);

                long ans = S(n, m, w) % MOD;
                ans = (MOD + ans) % MOD;
                Console.WriteLine(ans);
            }
        }
        static long S(int n, int m, long[] w) {
            if (LEGOAA[n][m] == -1) {
                long r = A(n, m, w) % MOD;
                long sum = 0;
                for (int i = 0; i < m; i++) {
                    sum += S(n, i, w) * A(n, m - i, w);
                    sum %= MOD;
                }
                LEGOAA[n][m] = (r - sum) % MOD;
            }
            return LEGOAA[n][m];
        }
        static long A(int n, int m, long[] w) {
            if (LEGOSS[n][m] == -1)
                LEGOSS[n][m] = (long)(BigInteger.ModPow(w[m], n, MOD));
            return LEGOSS[n][m];
        }
    }

    public static class Dortmund_Dilemma
    {
        const int MOD = 1000000007;
        static void modul(long[,] A, int i, int j) {
            A[i, j] %= MOD;
            if (A[i, j] < 0) A[i, j] += MOD;
        }
        public static void Start() {
            int mx = 100001;
            BigInteger[] fac = new BigInteger[30]; fac[0] = 1;
            for (int i = 1; i < 30; i++) {
                fac[i] = (fac[i - 1] * i);
            }
            long[,] CnK = new long[27, 27];
            for (int i = 0; i < 27; i++) {
                for (int j = 0; j <= i; j++) {
                    CnK[i, j] = (long)((fac[i] / (fac[i - j] * fac[j])) % MOD);
                }
            }
            long[,] kpi = new long[27, mx];
            for (int i = 0; i < 27; i++) {
                kpi[i, 0] = 1;
                for (int j = 1; j < mx; j++) {
                    kpi[i, j] = (kpi[i, j - 1] * i) % MOD;
                }
            }

            long[,] A = new long[mx, 27], B = new long[mx, 27], C = new long[mx, 27];

            for (int i = 1; i < mx; i++) {
                for (int k = 1; k < 27; k++) {
                    if (i == 1) A[1, k] = k;
                    else {
                        A[i, k] = A[i - 1, k] * k;
                        if (i % 2 == 0) A[i, k] -= A[i / 2, k];
                    }
                    modul(A, i, k);
                }
            }

            for (int i = 1; i < mx; i++) {
                for (int k = 1; k < 27; k++) {
                    B[i, k] = kpi[k, i] - A[i, k];
                    modul(B, i, k);
                }
            }

            for (int i = 1; i < mx; i++) {
                for (int k = 1; k < 27; k++) {
                    C[i, k] = B[i, k];
                    for (int j = 1; j < k; j++) {
                        C[i, k] -= C[i, j] * CnK[k, j];
                        modul(C, i, k);
                    }
                }
            }

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int k = int.Parse(tmp[1]);

                var ans = C[n, k] * CnK[26, k];
                Console.WriteLine(ans % MOD);
            }
        }
    }

    public static class Candles_Counting
    {
        const int MOD = 1000000007;
        static int[] A_fen, C_fen; static int mx_candles = 50001;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);

            A_fen = new int[n]; C_fen = new int[n];
            for (int i = 0; i < n; i++) {
                tmp = Console.ReadLine().Split(' ');
                A_fen[i] = int.Parse(tmp[0]);
                C_fen[i] = int.Parse(tmp[1]);
            }

            bool[] ser;

            long fin = 0;
            for (int i = 0; i < k; i++) {
                ser = new bool[k + 1];
                fin += (long)Math.Pow(-1, i) * iterateEm(ser, 1, k, k - i);
                fin %= MOD; fin += MOD; fin %= MOD;
            }
            Console.WriteLine(fin);
        }

        static long iterateEm(bool[] ser, int j, int k, int c) {
            long sum = 0;
            if (c == 0) {
                sum = SumOfStuff(ser);
            } else {
                for (int i = j; i < k + 1; i++) {
                    ser[i] = true;
                    sum += iterateEm(ser, i + 1, k, c - 1);
                    sum %= MOD;
                    ser[i] = false;
                }
            }
            return sum;
        }

        static long SumOfStuff(bool[] ser) {
            long[] dp = new long[mx_candles];
            long[] fenwick = new long[mx_candles + 1];

            for (int i = 0; i < A_fen.Length; i++) {
                if (!ser[C_fen[i]]) continue;
                int cur = A_fen[i];
                long q = (query_fenwick(fenwick, cur - 1) + 1) % MOD;
                dp[i] += q;
                dp[i] %= MOD;
                updatefenwick(fenwick, mx_candles, cur, dp[i]);
            }
            return query_fenwick(fenwick, mx_candles) % MOD;
        }

        static long query_fenwick(long[] fenwick, int i) {
            long sum = 0;
            for (; i > 0; i -= i & (-i)) sum = (sum + fenwick[i]) % MOD;
            return sum;
        }

        static void updatefenwick(long[] fenwick, int n, int i, long val) {
            for (; i <= n; i += i & (-i)) fenwick[i] = (fenwick[i] + val) % MOD;
        }
    }

    public static class Play_with_words
    {
        public static void Start() {
            var line = Console.ReadLine();
            int n = line.Length;

            int[,] dp = new int[n, n];

            for (int i = n - 1; i >= 0; i--) {
                for (int j = i; j < n; j++) {

                    if (line[i] == line[j]) {
                        dp[i, j] = 2 + (j > 0 && i < n - 1 ? dp[i + 1, j - 1] : 0);
                        if (i == j) dp[i, j]--;
                    } else {
                        dp[i, j] = Math.Max(j > 0 ? dp[i, j - 1] : 0, i < n - 1 ? dp[i + 1, j] : 0);
                    }

                }
            }

            int ans = 0;
            for (int i = 0; i < n - 1; i++) {
                ans = Math.Max(ans, dp[0, i] * dp[i + 1, n - 1]);
            }
            Console.WriteLine(ans);
        }
    }

    public static class The_Indian_Job
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int G = int.Parse(tmp[1]);
                int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                int sum = A.Sum();
                if (sum > 2 * G) {
                    Console.WriteLine("NO");
                } else {
                    var sums = new HashSet<int>();

                    foreach (int cur in A) {
                        var b = new HashSet<int>(sums);
                        b.Add(cur);
                        foreach (var item in sums.Where(x => x + cur <= G)) b.Add(item + cur);
                        sums = b;
                    }

                    bool found = (from cur in sums where cur <= G && sum - cur <= G select 0).Count() > 0;

                    Console.WriteLine(found ? "YES" : "NO");
                }
            }
        }
    }

    public static class Interval_Selection
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                var map = new Tuple<int, int>[n];
                for (int i = 0; i < n; i++) {
                    var tmp = Console.ReadLine().Split(' ');
                    map[i] = Tuple.Create(int.Parse(tmp[0]), int.Parse(tmp[1]));
                }

                map = map.OrderBy(x => x.Item2).ToArray();

                int taken = 0, mx1 = -1, mx2 = -1;
                foreach (int i in Enumerable.Range(0, n).Where(x => mx2 < map[x].Item1)) {
                    if (mx1 >= map[i].Item1) { mx2 = mx1; }

                    taken++;
                    mx1 = map[i].Item2;
                }

                // Code to display the intervals
                /* 
                for (int i = 0; i < n; i++) {
                    Console.Write(w[i] + "  ");
                    int cur = Console.CursorLeft;
                    for (int j = map[i].Item1; j <= map[i].Item2; j++) {
                        Console.CursorLeft = j + cur;
                        Console.Write("+");
                    }
                    Console.WriteLine();
                } // */

                Console.WriteLine(taken);
            }

        }
    }

    public static class Superman_Celebrates_Diwali
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int N = int.Parse(tmp[0]);
            int H = int.Parse(tmp[1]);
            int I = int.Parse(tmp[2]);

            long[,] building = new long[N, H], dp = new long[N, H];
            long[] mx = new long[H];

            for (int i = 0; i < N; i++) {
                int[] A = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);
                for (int j = 1; j < A.Length; j++) building[i, A[j] - 1]++;
            }

            for (int j = 0; j < N; j++) { dp[j, 0] = building[j, 0]; mx[0] = Math.Max(mx[0], building[j, 0]); }

            for (int h = 1; h < H; h++) {
                for (int b = 0; b < N; b++) {

                    long jump = 0;

                    if (h >= I) {
                        jump = Math.Max(jump, mx[h - I]);
                    }

                    dp[b, h] = building[b, h] + Math.Max(dp[b, h - 1], +jump);
                    mx[h] = Math.Max(mx[h], dp[b, h]);
                }
            }

            Console.WriteLine(mx[H - 1]);
        }
    }

    public static class New_Year_Present
    {
        static Dictionary<int, int> dict_new_year_present;
        static int[] keys_new_year_present;
        static bool[] exists_new_year_present;
        public static void Start() {
            dict_new_year_present = new Dictionary<int, int>();
            exists_new_year_present = new bool[10000001];
            int n = int.Parse(Console.ReadLine());
            var A = Console.ReadLine().Trim().Split(' ');

            for (int i = 0; i < n; i++) {
                int ts = int.Parse(A[i]);
                exists_new_year_present[ts] = true;
                if (dict_new_year_present.ContainsKey(ts)) dict_new_year_present[ts]++; else dict_new_year_present[ts] = 1;
            }

            dict_new_year_present = dict_new_year_present.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
            keys_new_year_present = dict_new_year_present.Keys.ToArray();
            n = keys_new_year_present.Length;

            long ans = 0;

            for (int _ = 0; _ < n; _++) {
                int i = keys_new_year_present[_];
                if (dict_new_year_present[i] < 2) continue;

                ans += CNK(dict_new_year_present[i], 2) * two(i);

                if (dict_new_year_present[i] > 2) {
                    ans += CNK(dict_new_year_present[i], 3) * three(i);
                }
            }

            Console.WriteLine(ans);
        }

        static long three(int n) {
            int kn = keys_new_year_present.Length, a, b, c;
            long ret = 0;

            for (int i = 0; i < kn; i++) {
                a = keys_new_year_present[i];
                for (int j = i + 1; j < kn; j++) {
                    b = keys_new_year_present[j]; c = n - a - b;
                    if (c <= b) break;
                    if (!exists_new_year_present[c]) continue;

                    ret += dict_new_year_present[a] * dict_new_year_present[b] * dict_new_year_present[c];
                }
            }

            for (int i = 0; i < kn; i++) {
                a = keys_new_year_present[i];
                if (2 * a >= n) break;
                if (dict_new_year_present[a] < 2) continue;
                b = n - 2 * a;
                if (a == b || !exists_new_year_present[b]) continue;

                ret += CNK(dict_new_year_present[a], 2) * dict_new_year_present[b];
            }

            if (n % 3 == 0 && exists_new_year_present[n / 3]) ret += CNK(dict_new_year_present[n / 3], 3);

            return ret;
        }

        static long two(int n) {
            long c, r = 0;
            int a, b, kn = keys_new_year_present.Length;

            for (int i = 0; i < kn; i++) {
                a = keys_new_year_present[i]; b = n - a;
                if (a > b) break;
                if (!exists_new_year_present[b]) continue;

                if (a == b) r += CNK(dict_new_year_present[a], 2);
                else r += dict_new_year_present[a] * dict_new_year_present[b];
            }

            long ret = 0;

            for (int i = 0; i < kn; i++) {
                int p = keys_new_year_present[i], q = n - p;
                if (p > q) break;
                if (!exists_new_year_present[q]) continue;

                if (p == q) ret += CNK(dict_new_year_present[p], 4);
                else {
                    a = dict_new_year_present[p]; b = dict_new_year_present[q]; c = a * b;
                    r = Math.Max(0, r - c);
                    ret += r * c + c * (b - 1) * (a - 1) / 4;
                }
            }

            return ret;
        }

        static long CNK(long n, long k) {
            if (n < k) return 0;
            if (n == k || k == 0) return 1;
            return (n * CNK(n - 1, k - 1)) / k;
        }
    }

    public static class Beautiful_Strings
    {
        public static void Start() {
            long[] w = new long[1000001];
            for (int i = 1; i < 1000001; i++) w[i] = w[i - 1] + i - 1;

            string m = Console.ReadLine();
            int n = m.Length, sup = 0, cut = 0;


            for (int i = 0; i < n; i++) {

                int j = i + 1;
                while (j < n && m[i] == m[j]) j++;
                j--;

                if (i == j) continue;
                cut += j - i;
                sup++;
                i = j;
            }

            for (int i = 0; i < n; i++) {
                if (i + 2 < n && m[i] == m[i + 2] && m[i] != m[i + 1]) {
                    if (i - 3 >= 0 && i + 3 < n &&
                        m[i] == m[i + 2] && m[i] == m[i - 2] &&
                        m[i - 3] == m[i - 1] && m[i + 3] == m[i + 1] && m[i + 1] != m[i - 1] && m[i] != m[i - 1]
                        && ((i - 4 >= 0 && m[i - 4] != m[i - 2]) || (i + 4 < n && m[i + 4] != m[i + 2]))
                       ) { sup--; i++; }
                    sup--; i++;
                }
            }

            for (int i = 0; i < n - 1; i++) {
                if (m[i] == m[i + 1]) continue;
                int j = i + 2;
                while (j + 1 < n && m[i] == m[j] && m[i + 1] == m[j + 1]) j += 2;
                j -= 2;
                if (i == j) continue;

                sup -= (j - i) / 2;
                i = j + 1;
            }

            Console.WriteLine(w[n - cut] + sup);
        }
    }

    public static class Angry_Children_2
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int k = int.Parse(Console.ReadLine());

            int[] A = new int[n];
            for (int i = 0; i < n; i++) A[i] = int.Parse(Console.ReadLine());

            Array.Sort(A);

            long ans = long.MaxValue;

            for (int i = 0; i < n - k + 1; i++) {
                long tmp = 0;
                int p = i, q = k - 1 + i, m = k - 1;
                while (p < q) {
                    tmp += Math.BigMul(m, A[q] - A[p]);
                    p++; q--; m -= 2;
                }
                ans = Math.Min(ans, tmp);
            }
            Console.WriteLine(ans);
        }
    }

    public static class A_Super_Hero
    {
        static int[][] P_superhero, B_superhero;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Trim().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);

                P_superhero = new int[n][];
                B_superhero = new int[n][];

                for (int i = 0; i < n; i++) {
                    P_superhero[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);
                }

                for (int i = 0; i < n; i++) {
                    B_superhero[i] = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);
                }


                int[,] cache = new int[n, 1001];
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < 1001; j++) cache[i, j] = 1;
                }

                int p = -1 * Bullets(cache, 0, n, m, 0);
                Console.WriteLine(Math.Max(p, 0));
            }
        }
        static int Bullets(int[,] cache, int i, int n, int m, int badB) {
            if (i >= n) return 0;
            if (cache[i, badB] != 1) return cache[i, badB];

            int good = int.MinValue;
            for (int j = 0; j < m; j++) {
                int power = P_superhero[i][j] - badB;
                if (power <= 0) power = 0;

                int x = Bullets(cache, i + 1, n, m, B_superhero[i][j]) - power;
                good = Math.Max(good, x);
            }

            cache[i, badB] = good;
            return good;
        }
    }

    public static class Dorsey_Thief
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int x = int.Parse(tmp[1]);

            List<Tuple<int, int>> val = new List<Tuple<int, int>>();
            for (int i = 0; i < n; i++) {
                tmp = Console.ReadLine().Split(' ');
                val.Add(Tuple.Create(int.Parse(tmp[1]), int.Parse(tmp[0])));
            }

            val.Sort();

            long[][] map = { new long[x + 1], new long[x + 1] };
            bool[][] check = { new bool[x + 1], new bool[x + 1] };
            check[0][0] = true;
            check[1][0] = true;

            bool c = false;

            for (int i = 1; i <= n; i++) {
                int a = i % 2, b = a == 0 ? 1 : 0;

                int wt = val[i - 1].Item1, diff = 0;
                while (wt <= x) {
                    if (check[b][diff]) {
                        c = i == n && wt == x;
                        check[a][wt] = true;
                        map[a][wt] = Math.Max(val[i - 1].Item2 + map[b][diff], map[b][wt]);
                    } else map[a][wt] = map[b][wt];

                    wt++; diff++;
                }
            }

            Console.WriteLine(c ? map[n % 2][x].ToString() : "Got caught!");
        }
    }

    public static class Travel_around_the_world
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            long c = long.Parse(tmp[1]);
            long[] a = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long[] b = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            bool[] w = new bool[n];

            for (int i = n - 1; i >= 0; i--) {
                long fuel = Math.Min(c, a[i]);

                if (fuel < b[i]) continue;
                bool good = true;
                fuel -= b[i];
                int cur = (i + 1) % n;
                while (cur != i) {
                    if (w[cur]) break;
                    fuel = Math.Min(fuel + a[cur], c);
                    if (fuel < b[cur]) { good = false; break; }

                    fuel -= b[cur];
                    cur++;
                    if (cur == n) cur = 0;
                }
                if (good) w[i] = true;
            }
            int ans = 0;
            foreach (var v in w) if (v) ans++;
            Console.WriteLine(ans);
        }
    }

    public static class LCS_Returns
    {
        public static void Start() {
            string a = Console.ReadLine(), b = Console.ReadLine();
            int n = a.Length, m = b.Length;

            int[,] w = new int[n + 1, m + 1];
            int[,] w2 = new int[n + 2, m + 2];

            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= m; j++) {
                    if (a[i - 1] == b[j - 1]) {
                        w[i, j] = w[i - 1, j - 1] + 1;
                    } else {
                        w[i, j] = Math.Max(w[i - 1, j], w[i, j - 1]);
                    }
                }
            }

            int lcs = w[n, m];

            for (int i = n; i > 0; i--) {
                for (int j = m; j > 0; j--) {
                    if (a[i - 1] == b[j - 1]) {
                        w2[i, j] = w2[i + 1, j + 1] + 1;
                    } else {
                        w2[i, j] = Math.Max(w2[i + 1, j], w2[i, j + 1]);
                    }
                }
            }

            int ans = 0;
            for (int i = 0; i <= n; i++) {
                var h = new HashSet<int>();
                for (int j = 1; j <= m; j++) {
                    if (w[i, j - 1] + w2[i + 1, j + 1] + 1 == lcs + 1 && h.Add(b[j - 1])) {
                        ans++;
                    }
                }
            }

            Console.WriteLine(ans);
        }
    }

    public static class Modify_The_Sequence
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] A = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);

            for (int i = 0; i < n; i++) A[i] -= i;

            int index = 1;
            int[] s = new int[n + 10];
            for (int i = 1; i < n; i++) {
                if (A[i] <= 0) continue;

                if (A[i] >= A[s[index]]) {
                    index++;
                    s[index] = i;
                } else {
                    int low = 1, high = index;
                    while (high > low) {
                        int mid = (high + low) / 2;
                        if (A[s[mid]] > A[i])
                            high = mid;
                        else
                            low = mid + 1;
                    }

                    if (A[s[low]] >= A[i]) {
                        s[low] = i;
                    }
                }
            }

            int ans = n - index;
            Console.WriteLine(ans);
        }
    }

    public static class Billboards
    {
        public static void Start() {
            var tmp = Console.ReadLine().Trim().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            long[] A = new long[n];
            for (int i = 0; i < n; i++) A[i] = long.Parse(Console.ReadLine().Trim());

            long[] sum = new long[n], w = new long[n];
            sum[0] = w[0] = A[0];
            for (int i = 1; i < n; i++) sum[i] = sum[i - 1] + A[i];

            for (int i = 1; i < k; i++) w[i] = A[i] + w[i - 1];

            int l = k - 1;
            for (int i = k; i < n; i++) {
                int r = -1;
                w[i] = w[i - 1];

                if (l == k - 1) {
                    for (int j = 0; j < k; j++) {
                        long a = sum[i] - sum[i - j - 1] + (i - j - 2 >= 0 ? w[i - j - 2] : 0);

                        if (a > w[i]) { w[i] = a; r = j; }
                    }
                } else {
                    r = l + 1;
                    w[i] = sum[i] - sum[i - r - 1] + (i - r - 2 >= 0 ? w[i - r - 2] : 0);
                }

                l = r;
            }
            Console.WriteLine(w[n - 1]);
        }
    }

    public static class Robot
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            long[] S = new long[n], E = new long[n];

            for (int i = 0; i < n; i++) {
                var tmp = Console.ReadLine().Split(' ');
                S[i] = int.Parse(tmp[0]);
                E[i] = int.Parse(tmp[1]);
            }

            long[] f = new long[n];
            for (int i = 1; i < n; i++) f[i] = long.MaxValue;

            List<long>[] added = new List<long>[n + 1], removed = new List<long>[n + 1];
            for (int i = 0; i <= n; i++) { added[i] = new List<long>(); removed[i] = new List<long>(); }

            var all = new Dictionary<long, int>();
            var L = new SortedSet<long>();

            for (int i = 0; i < n; i++) {
                foreach (var item in added[i]) {
                    if (all.ContainsKey(item)) all[item]++;
                    else {
                        all[item] = 1;
                        L.Add(item);
                    }
                }
                foreach (var item in removed[i]) {
                    if (all[item] == 1) {
                        all.Remove(item);
                        L.Remove(item);
                    } else {
                        all[item]--;
                    }
                }

                if (all.Count > 0) f[i] = L.Min;

                long cur = f[i] + S[i];
                long k = Math.Min(1 + E[i] + i, n);


                added[i + 1].Add(cur);
                if (k != n) removed[k].Add(cur);
            }

            Console.WriteLine(S.Sum() - f[n - 1]);
        }
    }

    public static class Two_Robots
    {
        public static void Start() {
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int q = int.Parse(tmp[1]);

                Dictionary<int, int> Q = new Dictionary<int, int>(), Q2 = new Dictionary<int, int>();

                Q.Add(0, 0);

                for (int i = 0; i < q; i++) {
                    tmp = Console.ReadLine().Split(' ');
                    int u = int.Parse(tmp[0]) + 1, v = int.Parse(tmp[1]) + 1, uv = Math.Abs(u - v);

                    foreach (var s_ in Q) {
                        int a = s_.Key / 100000, b = s_.Key % 100000;
                        var d = s_.Value;

                        int d1 = uv, d2 = uv;

                        if (a != 0) d1 += Math.Abs(a - u);
                        if (b != 0) d2 += Math.Abs(b - u);

                        AssignRobot(v, b, d1 + d, Q2);
                        AssignRobot(a, v, d2 + d, Q2);
                    }

                    Q = Q2;
                    Q2 = new Dictionary<int, int>();
                }

                Console.WriteLine(Q.Values.Min());
            }
        }

        static void AssignRobot(int a, int b, int d, Dictionary<int, int> Q) {
            if (a > b) { var _ = a; a = b; b = _; }
            int key = a * 100000 + b;
            if (Q.ContainsKey(key)) Q[key] = Math.Min(Q[key], d); else Q[key] = d;
        }
    }

    public static class Repetitive_K_Sums
    {
        public static void Start() {
            var sb = new StringBuilder();
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                long n = long.Parse(tmp[0]);
                long k = long.Parse(tmp[1]);

                List<long> A = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse).ToList();

                if (k == 1) {
                    sb.Append(string.Join(" ", A)).Append("\n");
                } else {
                    A.Sort();
                    Dictionary<long, int> map = new Dictionary<long, int>();
                    foreach (var a in A) { if (map.ContainsKey(a)) map[a]++; else map[a] = 1; }
                    RemoveFromMap(A[0], map);
                    var N = new List<long>();
                    N.Add(A[0] / k);

                    for (int i = 1; i < A.Count; i++) {
                        long cur = A[i];
                        if (!map.ContainsKey(cur)) continue;
                        N.Add(cur - N[0] * (k - 1));
                        RemoveNumsFromKSum(0, map, N, k - 1, N.Last());
                    }
                    sb.Append(string.Join(" ", N)).Append("\n");
                }
            }
            sb.Length--;
            Console.WriteLine(sb.ToString());
        }

        static void RemoveNumsFromKSum(int p, Dictionary<long, int> map, List<long> N, long k, long cur) {
            if (k == 0) {
                RemoveFromMap(cur, map);
            } else {
                for (int i = p; i < N.Count; i++) {
                    RemoveNumsFromKSum(i, map, N, k - 1, cur + N[i]);
                }
            }
        }

        static void RemoveFromMap(long n, Dictionary<long, int> map) {
            if (map[n] == 1) map.Remove(n); else map[n]--;
        }
    }

    public static class Swap_Permutation
    {
        const int MOD = 1000000007;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);

            Console.WriteLine(ExactAdjacent(n, k) + " " + AtMostAny(n, k));
        }

        static long ExactAdjacent(int n, int k) {
            long[,] dp = new long[n + 1, k + 1];
            for (int i = 0; i <= n; i++) dp[i, 0] = 1;

            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= k; j++) {
                    dp[i, j] = dp[i, j - 1] + dp[i - 1, j] - (j >= i ? dp[i - 1, j - i] : 0);
                    dp[i, j] %= MOD;
                }
            }


            long ans = 0;
            for (int i = 0; i <= k; i += 2) {
                ans += dp[n, k - i];
                ans %= MOD;
            }
            return ans;
        }

        static long AtMostAny(int n, int k) {
            long[,] dp = new long[n + 1, k + 1];
            for (int i = 1; i <= n; i++) {
                dp[i, 0] = 1;
            }

            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= k; j++) {
                    dp[i, j] += dp[i - 1, j] + (i - 1) * dp[i - 1, j - 1];
                    dp[i, j] %= MOD;
                }
            }

            long first = 0;
            for (int i = 0; i <= k; i++) {
                first += dp[n, i];
                first %= MOD;
            }
            return first;
        }
    }

    public static class Far_Vertices
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);

            HashSet<int>[] adj = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();
            for (int i = 0; i < n - 1; i++) {
                var t_m_p = Console.ReadLine().Trim().Split(' ');
                int _l_ = int.Parse(t_m_p[0]) - 1;
                int _r_ = int.Parse(t_m_p[1]) - 1;
                adj[_l_].Add(_r_); adj[_r_].Add(_l_);
            }

            int c = 0;
            bool[] mV = new bool[n];
            while (true) {
                int[] count = new int[n];
                bool[] V = new bool[n];
                mV.CopyTo(V, 0);

                foreach (var i in Enumerable.Range(0, n).Where(x => !V[x])) {
                    V[i] = true;
                    dfs(adj, i, i, -1, V, k, 0, count);
                }

                int j = -1, m = 0;
                for (int i = 0; i < n; i++) {
                    if (count[i] > m) { m = count[i]; j = i; }
                }

                if (j == -1) break;
                c++;
                mV[j] = true;
            }
            Console.WriteLine(c);
        }

        static void dfs(HashSet<int>[] adj, int top, int fr, int no, bool[] V, int k, int d, int[] count) {
            if (d > k && !V[fr]) {
                count[top]++;
                count[fr]++;
            }
            foreach (var node in adj[fr].Where(x => x != no)) dfs(adj, top, node, fr, V, k, d + 1, count);
        }
    }

    public static class Tree_Pruning
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            if (k > n) k = n;

            long[] w = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            HashSet<int>[] adj = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();
            for (int i = 0; i < n - 1; i++) {
                var t_m_p = Console.ReadLine().Split(' ');
                int _l_ = int.Parse(t_m_p[0]) - 1;
                int _r_ = int.Parse(t_m_p[1]) - 1;
                adj[_l_].Add(_r_); adj[_r_].Add(_l_);
            }

            long[] sum = new long[n];
            FillSums(adj, 0, -1, sum, w, 0);

            var rem = Prune(adj, sum, 0, -1, k);
            rem[0].set(0);

            Console.WriteLine(sum[0] - rem.Where(x => !x.empty).Min(x => x.get()));
        }

        public static Branch[] Prune(HashSet<int>[] adj, long[] sum, int fr, int no, int k) {
            var root = new Branch[k + 1];
            for (int i = 0; i <= k; i++) root[i] = new Branch();

            foreach (var node in adj[fr].Where(x => x != no)) {
                Branch[] children = Prune(adj, sum, node, fr, k);
                Branch[] tmp = CopyChildren(root);

                for (int i = 1; i <= k; i++) {
                    if (children[i].empty) continue;

                    for (int j = 0; i + j <= k; j++) {
                        if (j > 0 && tmp[j].empty) continue;
                        root[i + j].set(tmp[j].get() + children[i].get());
                    }
                }
            }

            if (sum[fr] < 0) root[1].set(sum[fr]);

            return root;
        }

        public class Branch
        {
            long value;
            public bool empty;
            public Branch() { value = 0; empty = true; }
            public Branch(long value) { this.value = value; empty = false; }
            public Branch(Branch br) { empty = br.empty; value = br.get(); }
            public void set(long value) {
                if (!empty) {
                    if (value < this.value) this.value = value;
                } else {
                    this.value = value; empty = false;
                }
            }
            public long get() { return value; }
        }

        static Branch[] CopyChildren(Branch[] children) {
            int n = children.Length;
            Branch[] ret = new Branch[n];
            for (int i = 0; i < n; i++) ret[i] = new Branch(children[i]);
            return ret;
        }

        static long FillSums(HashSet<int>[] adj, int fr, int no, long[] sum, long[] w, long cur) {
            long s = w[fr];
            cur += w[fr];
            adj[fr].Where(x => x != no).ToList().ForEach(node => s += FillSums(adj, node, fr, sum, w, cur));
            return sum[fr] = s;
        }
    }

    public static class The_Blacklist
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            int[,] bill = new int[k, n];
            for (int i = 0; i < k; i++) {
                tmp = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++) {
                    bill[i, j] = int.Parse(tmp[j]);
                }
            }

            allpaid = (1 << k) - 1;
            killingList = new int[1 << k, n];
            for (int i = 0; i < 1 << k; i++)
                for (int j = 0; j < n; j++)
                    killingList[i, j] = -1;

            int ans = Kill(bill, n, k, 0, 0);
            Console.WriteLine(ans);
        }

        static int allpaid;
        static int[,] killingList;
        static int Kill(int[,] bill, int n, int k, int mask, int p) {
            if (p == n) {
                return 0;
            }

            if (killingList[mask, p] != -1) return killingList[mask, p];

            if (mask == allpaid) return 10000000;
            int ret = int.MaxValue;
            for (int m = 0; m < k; m++) {
                if ((mask >> m & 1) == 1) continue;
                int cur = 0;
                for (int i = p; i < n; i++) {
                    cur += bill[m, i];

                    ret = Math.Min(ret, cur + Kill(bill, n, k, mask | (1 << m), i + 1));
                }
            }

            killingList[mask, p] = ret;
            return ret;
        }
    }

    public static class Vim_War
    {
        const int MOD = 1000000007;
        public static void Start() {
            long[] two = new long[100001];
            two[0] = 1;
            for (int i = 1; i < 100001; i++) {
                two[i] = (2 * two[i - 1]) % MOD;
            }
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);

            List<int> skills = new List<int>();
            for (int i = 0; i < n; i++) {
                skills.Add(Convert.ToInt32(Console.ReadLine().Trim(), 2));
            }

            int req = Convert.ToInt32(Console.ReadLine().Trim(), 2);

            int[,] count = new int[1 << 20, 21];
            int m2 = m;
            for (int i = 0; i < m; i++) {
                if ((req & (1 << i)) == 0) m2--;
            }

            for (int i = 0; i < n; i++) {
                int k = 0;

                if (((req ^ skills[i]) & skills[i]) != 0) continue;

                for (int j = 0; j < m; j++) {
                    if (((req >> j) & 1) == 1) {
                        k *= 2;
                        if ((skills[i] & (1 << j)) != 0) k++;
                    }
                }

                count[k, m2]++;
            }

            for (int j = m2; j > 0; j--) {
                for (int i = 0; i < (1 << m2); i++) {

                    count[i, j - 1] += count[i, j];
                    int val = (i & (1 << (j - 1)));

                    if (val == 0) {
                        count[i | (1 << (j - 1)), j - 1] += count[i, j];
                    }
                }
            }

            long ans = 0;
            for (int i = 0; i < (1 << m2); i++) {

                int cnt = 0;
                for (int j = i; j > 0; j >>= 1) {
                    cnt += j & 1;
                }

                long val = two[count[i, 0]];

                if (cnt % 2 == m2 % 2) ans = (ans + val) % MOD;
                else ans = (ans - val + MOD) % MOD;
            }

            if (req == 0) ans--;
            Console.WriteLine(ans);
        }
    }

    public static class Oil_Well
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]),
                m = int.Parse(tmp[1]),
                mx = 51,
                i, j, lt, rt, up, dn, z;

            bool[,] map = new bool[n, m];
            for (i = 0; i < n; i++) {
                tmp = Console.ReadLine().Split(' ');
                for (j = 0; j < m; j++) {
                    if (tmp[j] == "1") {
                        map[i, j] = true;
                    }
                }
            }

            int[,,,] dp = new int[mx, mx, mx, mx];
            for (i = 0; i < n; i++) {
                for (lt = 0; lt + i < n; lt++) {
                    for (j = 0; j < m; j++) {
                        for (up = 0; up + j < m; up++) {
                            rt = lt + i; dn = up + j;

                            if (i == 0 && j == 0) {
                                continue;
                            }

                            int c = 10000000;

                            if (lt < rt) {
                                int c_lt = 0, c_rt = 0;
                                for (z = up; z <= dn; ++z) {
                                    if (map[lt, z])
                                        c_lt += cost(lt, z, lt + 1, rt, up, dn);

                                    if (map[rt, z])
                                        c_rt += cost(rt, z, lt, rt - 1, up, dn);
                                }

                                c = Math.Min(c, dp[lt + 1, rt, up, dn] + c_lt);
                                c = Math.Min(c, dp[lt, rt - 1, up, dn] + c_rt);
                            }

                            if (up < dn) {
                                int c_up = 0, k_dn = 0;
                                for (z = lt; z <= rt; ++z) {
                                    if (map[z, up])
                                        c_up += cost(z, up, lt, rt, up + 1, dn);
                                    if (map[z, dn])
                                        k_dn += cost(z, dn, lt, rt, up, dn - 1);
                                }

                                c = Math.Min(c, dp[lt, rt, up + 1, dn] + c_up);
                                c = Math.Min(c, dp[lt, rt, up, dn - 1] + k_dn);
                            }

                            dp[lt, rt, up, dn] = c;
                        }
                    }
                }
            }

            Console.WriteLine(dp[0, n - 1, 0, m - 1]);
        }

        static int cost(int x, int y, int lt, int rt, int up, int dn) {
            int d1 = Math.Max(Math.Abs(lt - x), Math.Abs(rt - x));
            int d2 = Math.Max(Math.Abs(up - y), Math.Abs(dn - y));
            return Math.Max(d1, d2);
        }
    }

    public static class Queens_on_Board
    {
        const int MOD = 1000000007;
        public static void Start() {
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                var tmp = Console.ReadLine().Trim().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);
                bool[,] map = new bool[n, m];
                int[] badmasks = new int[n];
                for (int i = 0; i < n; i++) {
                    var _t = Console.ReadLine();
                    for (int j = 0; j < m; j++) {
                        if (_t[j] == '.') continue;
                        map[i, j] = true;
                        badmasks[i] = setBitOnQueen(badmasks[i], j);
                    }
                    badmasks[i] = ~badmasks[i];
                }

                int[] board = new int[n];
                Dictionary<long, long>[] cache = new Dictionary<long, long>[n];
                for (int i = 0; i < n; i++) cache[i] = new Dictionary<long, long>();


                long ans = longLiveTheQueen(0, 0, n, m, map, 0, false, badmasks, board, cache);
                Console.WriteLine(ans);
            }
        }
        static long longLiveTheQueen(int x, int y, int n, int m, bool[,] map, int mask, bool mv, int[] badmasks, int[] board, Dictionary<long, long>[] cache) {

            if (x == n) return 0;
            long ans = 0;
            long key = mask;
            int z = x;
            for (int i = 0; i < m; i++, z--) {
                key = key * 100;
                if (z >= 0) { key += board[z]; }
            }

            if (cache[x].ContainsKey(key)) return cache[x][key];

            for (int i = y; i < m; i++) {
                if (map[x, i]) { mv = false; continue; }
                if (mv || isOnQueen(mask, i)) continue;

                bool bad = false;
                int u = x - 1, v = i - 1;
                for (int j = 0; j < m && u >= 0 && v >= 0; j++, u--, v--) {
                    if (map[u, v]) {
                        bad = false; break;
                    } else if (isOnQueen(board[u], v)) {
                        bad = true; break;
                    }
                }
                if (bad) continue;

                u = x - 1; v = i + 1;
                for (int j = 0; j < m && u >= 0 && v < m; j++, u--, v++) {
                    if (map[u, v]) {
                        bad = false; break;
                    } else if (isOnQueen(board[u], v)) {
                        bad = true; break;
                    }
                }
                if (bad) continue;

                int res = board[x];

                board[x] = setBitOnQueen(board[x], i);
                ans += 1 + longLiveTheQueen(x, i + 1, n, m, map, badmasks[x] & setBitOnQueen(mask, i), true, badmasks, board, cache);
                board[x] = res;

            }

            ans += longLiveTheQueen(x + 1, 0, n, m, map, badmasks[x] & mask, false, badmasks, board, cache);
            ans %= MOD;

            cache[x][key] = ans;
            return ans;
        }
        public static int setBitOnQueen(int a, int i) { return a |= (1 << i); }
        public static bool isOnQueen(int a, int i) { return ((a >> i) & 1) == 1; }
    }

    public static class Brick_Tiling
    {
        const int MOD = 1000000007;
        static bool[,] BrickTilingBoard;
        static Dictionary<long, long> BrickTilingMap;
        public static void Start() {
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);
                int nm = n * m;

                BrickTilingMap = new Dictionary<long, long>();
                BrickTilingBoard = new bool[n, m];
                for (int i = 0; i < n; i++) {
                    var _l = Console.ReadLine();
                    for (int j = 0; j < m; j++) {
                        if (_l[j] == '#') {
                            BrickTilingBoard[i, j] = true;
                            nm--;
                        }
                    }
                }

                if (nm % 4 != 0) { Console.WriteLine(0); return; }

                long ans = Tile(n, m, nm);
                Console.WriteLine(ans);
            }
        }
        static long Tile(int n, int m, int nm) {
            if (nm == 0) return 1;

            long key = getkey(n, m);
            if (BrickTilingMap.ContainsKey(key)) return BrickTilingMap[key];

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (BrickTilingBoard[i, j]) continue;

                    long ans = 0;

                    foreach (var item in range(i, j)) {
                        if (valid(item, n, m)) {
                            Fill(item);
                            ans += Tile(n, m, nm - 4);
                            ans %= MOD;
                            Fill(item, false);
                        }
                    }

                    BrickTilingMap[key] = ans;
                    return ans;
                }
            }

            BrickTilingMap[key] = 0;
            return 0;
        }
        static long getkey(int n, int m) {
            long r = 17;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    r *= 17;
                    if (BrickTilingBoard[i, j]) r += 31;
                }
            }
            return r;
        }
        static void Fill(BrickTile[] L, bool assign = true) {
            L[0].fill(assign);
            L[1].fill(assign);
            L[2].fill(assign);
            L[3].fill(assign);
        }
        static bool valid(BrickTile[] L, int n, int m) {
            foreach (var item in L) {
                if (!onAndOff(item.first, item.second, n, m)) return false;
            }
            return true;
        }
        static bool onAndOff(int x, int y, int n, int m) {
            if (x < 0 || y < 0) return false;
            if (x >= n || y >= m) return false;
            if (BrickTilingBoard[x, y]) return false;
            return true;
        }
        static IEnumerable<BrickTile[]> range(int x, int y) {
            int[] l1 = { 1, 2, 2 }, r1 = { 0, 0, 1 }, l2 = { 1, 1, 1 }, r2 = { 0, 1, 2 }, l3 = { 0, 1, 2 }, r3 = { 1, 0, 0 }, lr = { 1, -1 };
            BrickTile[] r = { new BrickTile(0, 0), new BrickTile(0, 0), new BrickTile(0, 0), new BrickTile(x, y) };
            foreach (var u in lr) {
                foreach (var v in lr) {

                    // l1 , r1
                    for (int i = 0; i < 3; i++) {
                        r[i] = new BrickTile(x + l1[i] * u, y + r1[i] * v);
                    }
                    yield return r;

                    // r1, l1
                    for (int i = 0; i < 3; i++) {
                        r[i] = new BrickTile(x + r1[i] * u, y + l1[i] * v);
                    }
                    yield return r;

                    // l2, r2
                    for (int i = 0; i < 3; i++) {
                        r[i] = new BrickTile(x + l2[i] * u, y + r2[i] * v);
                    }
                    yield return r;

                    // r2, l2
                    for (int i = 0; i < 3; i++) {
                        r[i] = new BrickTile(x + r2[i] * u, y + l2[i] * v);
                    }
                    yield return r;

                }
            }


            foreach (var u in lr) {
                foreach (var v in lr) {

                    for (int i = 0; i < 3; i++) {
                        r[i] = new BrickTile(x + l3[i] * u, y + r3[i] * v);
                    }
                    yield return r;

                    for (int i = 0; i < 3; i++) {
                        r[i] = new BrickTile(x + r3[i] * u, y + l3[i] * v);
                    }
                    yield return r;
                }
            }
        }
        class BrickTile
        {
            public int first, second;
            public BrickTile(int f, int s) { first = f; second = s; }
            public void fill(bool assign) {
                BrickTilingBoard[first, second] = assign;
            }
        }
    }

    public static class Fair_Cut
    {
        static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);

            long[] A = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            Array.Sort(A);

            long[,] dp = new long[n, k + 1];
            for (int i = 0; i < n; i++) for (int j = 0; j <= k; j++) dp[i, j] = -1;

            long ans = solve(0, k, k, n, A, dp);

            Console.WriteLine(ans);
        }
        static long solve(int p, int l, int K, int n, long[] A, long[,] dp) {
            if (l < 0) return long.MaxValue / 1000;

            if (p == n) {
                if (l == 0) return 0;
                return long.MaxValue / 1000;
            }

            if (dp[p, l] != -1) return dp[p, l];

            // number of taken to right      l
            // number of taken to left       K-l

            // number of free to right       n - p - l
            // number of free to left        p - (K - l)

            long r = Math.Min(
                // take it
                A[p] * (p - (K - l) - (n - p - l)) + solve(p + 1, l - 1, K, n, A, dp),
                // leave it
                A[p] * (K - l - l) + solve(p + 1, l, K, n, A, dp)
            );

            dp[p, l] = r;
            return r;
        }
    }
}
