using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Hackerrank.Greedy
{
    public static class Priyanka_and_Toys
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

            Array.Sort(arr);

            int ans = 1, j = arr[0];
            for (int i = 1; i < n; i++) {
                if (arr[i] - j > 4) {
                    ans++;
                    j = arr[i];
                }
            }
            Console.WriteLine(ans);
        }
    }

    public static class Luck_balance
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            List<int> imp = new List<int>();
            int luck = 0;
            for (int i = 0; i < n; i++) {
                tmp = Console.ReadLine().Split(' ');
                int L = int.Parse(tmp[0]);
                int T = int.Parse(tmp[1]);
                luck += L;
                if (T == 1) imp.Add(L);
            }
            imp.Sort();
            k = Math.Max(0, imp.Count - k);
            for (int i = 0; i < k; i++) luck -= 2 * imp[i];

            Console.WriteLine(luck);
        }
    }

    public static class Grid_Challenge
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {
                int n = int.Parse(Console.ReadLine());
                string[] map = new string[n];
                for (int i = 0; i < n; i++) {
                    map[i] = Console.ReadLine();
                }
                Console.WriteLine(grid_challange(n, map) ? "YES" : "NO");
            }

        }
        public static bool grid_challange(int n, string[] map) {
            for (int i = 0; i < n; i++) {
                map[i] = new string(map[i].OrderBy(x => x).ToArray());
            }

            for (int _i = 1; _i < n; _i++) {
                for (int j = 0; j < n; j++) {
                    if (map[_i][j] < map[_i - 1][j]) return false;
                }
            }

            for (int _i = 0; _i < n; _i++) {
                for (int j = 1; j < n; j++) {
                    if (map[_i][j] < map[_i][j - 1]) return false;
                }
            }

            return true;
        }
    }

    public static class Jim_and_the_Orders
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            Dictionary<int, int> map = new Dictionary<int, int>();

            for (int i = 1; i <= n; i++) {
                string[] tmp = Console.ReadLine().Split(' ');
                int ord = int.Parse(tmp[0]) + int.Parse(tmp[1]);
                map.Add(i, ord);
            }

            map = map.OrderBy(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

            int[] keys = map.Keys.ToArray();
            for (int i = 0; i < keys.Length; i++) {
                Console.Write(keys[i] + " ");
            }
            Console.WriteLine();
        }
    }

    public static class Beautiful_Pairs
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), x => int.Parse(x));
            int[] B = Array.ConvertAll(Console.ReadLine().Split(' '), x => int.Parse(x));

            int[] holdA = new int[3000];
            int[] holdB = new int[3000];

            for (int i = 0; i < n; i++) {
                holdA[A[i] - 1]++;
                holdB[B[i] - 1]++;
            }

            int r = 0; int diff = 0;
            for (int i = 0; i < 3000; i++) {

                r += Math.Min(holdA[i], holdB[i]);
                if (holdA[i] != holdB[i]) diff++;
            }

            if (diff > 1) r++; else if (diff == 0) r--;
            Console.WriteLine(r);
        }
    }

    public static class Greedy_Florist
    {
        public static void Start() {
            int N, K;
            string NK = Console.ReadLine();
            string[] NandK = NK.Split(new Char[] { ' ', '\t', '\n' });
            N = Convert.ToInt32(NandK[0]);
            K = Convert.ToInt32(NandK[1]);

            int[] C = new int[N];

            string numbers = Console.ReadLine();
            string[] split = numbers.Split(new Char[] { ' ', '\t', '\n' });

            int i = 0;

            foreach (string s in split) {
                if (s.Trim() != "") {
                    C[i++] = Convert.ToInt32(s);
                }
            }

            decimal result = 0;
            Array.Sort(C);
            Array.Reverse(C);

            var toBuy = (int)Math.Ceiling((double)N / (double)K);

            var inc = 1;
            var j = 0;
            while (j < C.Length) {

                for (int _j = 0; _j < K && j < C.Length; _j++) {
                    result += inc * C[j];
                    j++;
                }
                inc++;
            }
            Console.WriteLine(result);
        }
    }

    public static class Largest_Permutation
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            int[] arr = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), x => Convert.ToInt32(x));

            int[] ind = new int[n + 1];
            for (int i = 0; i < n; i++) {
                ind[arr[i]] = i;
            }

            for (int i = 0; i < n && k > 0; i++) {
                if (ind[n - i] == i) continue;
                arr[ind[n - i]] = arr[i];
                ind[arr[i]] = ind[n - i];

                arr[i] = n - i;
                ind[n - i] = ind[i];
                k--;
            }
            Console.WriteLine(string.Join(" ", arr));
        }
    }

    public static class Sherlock_and_MiniMax
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            arr = arr.Distinct().ToArray();
            Array.Sort(arr);
            n = arr.Length;
            var tmp = Console.ReadLine().Split(' ');
            int l = int.Parse(tmp[0]);
            int r = int.Parse(tmp[1]);

            int ans = -1; int max = int.MinValue;
            for (int i = 0; i < n - 1; i++) {
                int a = arr[i], b = arr[i + 1];
                int mid = a + (b - a) / 2;
                if (mid >= l && mid <= r) {
                    int tp = Math.Min(Math.Abs(arr[i] - mid), Math.Abs(arr[i + 1] - mid));
                    if (tp > max) {
                        max = tp;
                        ans = mid;
                    }
                } else if (l >= arr[i] && l <= arr[i + 1]) {
                    int tp = Math.Min(Math.Abs(arr[i] - l), Math.Abs(arr[i + 1] - l));
                    if (tp > max) {
                        max = tp;
                        ans = l;
                    }
                } else if (r >= arr[i] && r <= arr[i + 1]) {
                    int tp = Math.Min(Math.Abs(arr[i] - r), Math.Abs(arr[i + 1] - r));
                    if (tp > max) {
                        max = tp;
                        ans = r;
                    }
                    break;
                }
            }

            if (ans == -1) {
                if (l >= arr[n - 1]) {
                    max = Math.Abs(r - arr[n - 1]);
                    ans = r;
                } else if (r <= arr[0]) {
                    int ii = Math.Abs(arr[0] - l);
                    if (ii >= max) {
                        ans = l;
                    }
                }
            }

            if (l <= arr[0]) {
                int ii = Math.Abs(arr[0] - l);
                if (ii >= max) {
                    max = ii;
                    ans = l;
                }
            }
            if (r >= arr[n - 1]) {
                int ii = Math.Abs(arr[n - 1] - r);
                if (ii > max) {
                    max = ii;
                    ans = r;
                }
            }

            Console.WriteLine(ans);
        }
    }

    public static class Accessory_Collection
    {
        public static void Start() {
            var sb = new StringBuilder();
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int L = int.Parse(tmp[0]);  // number of accessories
                int A = int.Parse(tmp[1]);  // types of accessories
                int N = int.Parse(tmp[2]);  // size of choosen subset
                int D = int.Parse(tmp[3]);  // at least different items

                long ans = -1;
                if (D > A) {
                    ans = -1;
                } else if (D == 1) {
                    ans = Math.BigMul(L, A);
                } else {
                    for (int i = L; i >= 1; i--) {
                        long ter = sol(L, A, N, D, i);
                        if (ter > ans) {
                            ans = ter;
                        } else if (ans != -1) break;
                    }
                }
                sb.Append(ans == -1 ? "SAD" : ans.ToString()).Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }
        static long sol(long L, long A, long N, long D, long mid) {
            long p = A - D + 1;
            long top = mid + (N - 1 - mid * (D - 1));
            if (top < mid) return -1;

            long sum = mid;
            long gold = mid * p;
            for (long i = A; i > p; i--) { sum += top; gold += i * top; }

            long r = p + 1;
            while (sum > N && r < A) {
                sum += mid - top;
                gold += mid * r - top * r;
                r++;
            }

            if (L < sum) return -1;
            if (L == sum) return gold;

            L -= sum;
            r = p - 1;
            if (r * mid < L) return -1;

            long fix = L / mid, rem = L - fix * mid;
            return gold + mid * (q(r) - q(r - fix)) + rem * (r - fix);
        }
        static long q(long n) {
            return n * (n + 1) / 2;
        }
    }

    public static class Algorithmic_Crush
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);

            long[] w = new long[n + 1];

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]) - 1;
                int r = int.Parse(tmp[1]);
                int k = int.Parse(tmp[2]);

                w[l] += k;
                w[r] -= k;
            }

            long max = 0, x = 0;
            for (int i = 0; i < n; i++) {
                max = Math.Max(max, x += w[i]);
            }
            Console.WriteLine(max);
        }
    }

    public static class Cutting_Boards
    {
        const int MOD = 1000000007;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int y = int.Parse(tmp[0]);
                int x = int.Parse(tmp[1]);
                int[] Y = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                int[] X = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                int cutsX = 1, cutsY = 1;
                int[] keysY = getkeys(Y);
                int[] keysX = getkeys(X);
                long ans = 0;

                int i = 0, j = 0;
                while (i < x - 1 && j < y - 1) {
                    int a = X[keysX[i]], b = Y[keysY[j]];
                    if (a == b) {
                        if (x - i > y - j) {
                            ans += Math.BigMul(a, cutsY); cutsX++; i++;
                        } else {
                            ans += Math.BigMul(b, cutsX); cutsY++; j++;
                        }
                    } else if (a > b) {
                        ans += Math.BigMul(a, cutsY); cutsX++; i++;
                    } else {
                        ans += Math.BigMul(b, cutsX); cutsY++; j++;
                    }
                    ans %= MOD;
                }

                while (i < x - 1) {
                    ans += Math.BigMul(X[keysX[i]], cutsY); cutsX++; i++; ans %= MOD;
                }
                while (j < y - 1) {
                    ans += Math.BigMul(Y[keysY[j]], cutsX); cutsY++; j++; ans %= MOD;
                }
                Console.WriteLine(ans);
            }
        }
        static int[] getkeys(int[] Y) {
            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < Y.Length; i++) {
                map[i] = Y[i];
            }

            map = map.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return map.Keys.ToArray();
        }
    }

    public static class Chief_Hopper
    {
        const long INF = 1000000000000000L;
        public static void Start() {
            Console.ReadLine();
            int[] A = Array.ConvertAll(("0 " + Console.ReadLine()).Split(' '), int.Parse);

            BigInteger high = 100000000, low = 0;
            while (high - low > 1) {
                BigInteger mid = (high + low) / 2;
                if (!goodJump(A, mid)) low = mid; else high = mid;
            }
            Console.WriteLine(high);
        }
        static bool goodJump(int[] A, BigInteger energy) {
            for (int i = 1; i < A.Length; i++) {
                if (A[i] > energy) {
                    energy -= (A[i] - energy);
                } else {
                    energy += energy - A[i];
                }
                if (energy < 0) return false;
                if (energy > INF) return true;
            }
            return true;
        }
    }

    public static class Team_Formation
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Trim().Split(' ');
                int n = int.Parse(tmp[0]);
                if (n == 0) { Console.WriteLine(0); continue; }
                int[] A = new int[n];
                for (int i = 0; i < n; i++) A[i] = int.Parse(tmp[i + 1]);

                var map = new Dictionary<int, HashSet<int>>();
                Array.Sort(A);
                int[] teams = new int[n];

                int t = 0;
                for (int i = 0; i < n; i++) {
                    int cur = A[i], needed = cur - 1;
                    if (map.ContainsKey(needed) && map[needed].Count != 0) {
                        int min = -1, sz = int.MaxValue;
                        foreach (var tm in map[needed]) { if (teams[tm] < sz) { sz = teams[tm]; min = tm; } }
                        teams[min]++;
                        map[needed].Remove(min);
                        if (!map.ContainsKey(cur)) map[cur] = new HashSet<int>();
                        map[cur].Add(min);
                    } else {
                        if (!map.ContainsKey(cur)) map[cur] = new HashSet<int>();
                        map[cur].Add(t);
                        teams[t++] = 1;
                    }
                }
                int ans = int.MaxValue;
                for (int i = 0; i < t; i++) {
                    ans = Math.Min(ans, teams[i]);
                }
                Console.WriteLine(ans);
            }
        }
    }
}
