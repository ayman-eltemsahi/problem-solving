using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

class Solution231
{
    static void Main2() {

    }

    public static class Maximal_AND_Subsequences
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);

            long[] arr = new long[n];
            for (int i = 0; i < n; i++) arr[i] = long.Parse(Console.ReadLine());
            //arr = Enumerable.Range(0, n).Select(x => (long)x).ToArray();
            var root = new trie();
            for (int i = 0; i < n; i++) root.insert(arr[i], 5);

            var ans = root.search(k, 5);
            Console.WriteLine(ans.Item1);
            Console.WriteLine(CNK(ans.Item2, k));
        }

        class trie
        {
            int count;
            trie zero, one;
            public void insert(long num, int i) {
                if (i == 0) return;
                i--;
                long b = (num >> i) & 1;
                count++;
                if (b == 1) {
                    if (one == null) one = new trie();
                    one.insert(num, i);
                } else {
                    if (zero == null) zero = new trie();
                    zero.insert(num, i);
                }
            }

            public Tuple<long, int> search(int k, int x) {
                if (one == null && zero == null) return Tuple.Create(0L, -1);

                if (one != null && one.count >= k) {
                    long r = (1L << (x - 1));
                    var r2 = one.search(k, x - 1);
                    r += r2.Item1;
                    if (r2.Item2 == -1) return Tuple.Create(r, one.count);
                    return Tuple.Create(r, r2.Item2);
                }
                if (one == null) one = new trie();
                one.merge(zero);
                return one.search(k, x - 1);
            }

            private void merge(trie t) {
                if (t == null) return;
                count += t.count;
                if (t.one != null) {
                    if (one == null) one = new trie();
                    one.merge(t.one);
                }
                if (t.zero != null) {
                    if (zero == null) zero = new trie();
                    zero.merge(t.zero);
                }
            }

        }

        const Int32 MOD = 1000000007;
        static long CNK(int n, int k) {
            if (n < k) return 0;
            if (n == k || k == 0) return 1;
            long h = n * CNK(n - 1, k - 1);
            h %= MOD;
            return h * (modInverse(k, MOD)) % MOD;
        }


        public static Int32 modInverse(Int32 a, Int32 m) {
            Int32 t, q, x0 = 0, x1 = 1;

            while (a > 1) {
                q = a / m; t = m;
                m = a % m; a = t; t = x0;
                x0 = x1 - q * x0; x1 = t;
            }

            x1 %= MOD;
            if (x1 < 0) return x1 + MOD;
            return x1;
        }
    }

    public static class Zigzag_Array
    {
        static Dictionary<string, int> map = new Dictionary<string, int>();
        public static int[] splice(int[] a, int p) {
            int n = a.Length - 1;
            int[] b = new int[n];
            for (int i = 0; i < p; i++) b[i] = a[i];
            for (int i = p + 1; i <= n; i++) b[i - 1] = a[i];
            return b;
        }
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int ans = solve(arr);
            Console.WriteLine(ans);
        }

        private static int solve(int[] arr) {
            var key = string.Join(" ", arr);
            if (map.ContainsKey(key)) return map[key];

            int n = arr.Length - 2;
            for (int i = 0; i < n; i++) {
                if (bad(arr[i], arr[i + 1], arr[i + 2])) {
                    map[key] = 1 + Math.Min(solve(splice(arr, i)), Math.Min(solve(splice(arr, i + 1)),
                        solve(splice(arr, i + 2))));
                    return map[key];
                }
            }
            map[key] = 0;
            return 0;
        }
        public static bool bad(int a, int b, int c) {
            return (a > b && b > c) || (a < b && b < c);
        }
    }

    public static class Reward_Points
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int a = int.Parse(tmp[0]);
            int b = int.Parse(tmp[1]);
            int c = int.Parse(tmp[2]);

            if (a > 10) a = 10;
            if (b > 10) b = 10;
            if (c > 10) c = 10;
            Console.WriteLine(10 * (a + b + c));
        }
    }

}