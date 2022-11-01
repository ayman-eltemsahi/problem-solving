using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading;

namespace Hackerrank.Search
{
    public static class Count_Luck
    {
        static int final;
        static bool searcing;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int rows = int.Parse(tmp[0]);
                int cols = int.Parse(tmp[1]);

                int MR = -1, MC = -1;
                string[] map = new string[rows];
                for (int i = 0; i < rows; i++) {
                    map[i] = Console.ReadLine();
                    if (map[i].IndexOf('M') > -1) { MR = i; MC = map[i].IndexOf('M'); }
                }
                int lucky = int.Parse(Console.ReadLine());

                HashSet<int> visited = new HashSet<int>();
                for (int r = 0; r < rows; r++) {
                    for (int c = 0; c < cols; c++) {
                        if (map[r][c] == 'X') { visited.Add(r * 100 + c); }
                    }
                }
                visited.Add(MR * 100 + MC);
                final = 0;
                searcing = true;
                Search(MR, MC, 0, map, visited);

                Console.WriteLine(final == lucky ? "Impressed" : "Oops!");
            }
        }
        static void Search(int r, int c, int cur, string[] map, HashSet<int> visited) {
            if (!searcing) return;

            if (map[r][c] == '*') {
                final = cur;
                searcing = false;
                return;
            }

            int wave = 0;
            if (r - 1 >= 0 && !visited.Contains((r - 1) * 100 + c)) wave++;
            if (r + 1 < map.Length && !visited.Contains((r + 1) * 100 + c)) wave++;
            if (c - 1 >= 0 && !visited.Contains(100 * r + c - 1)) wave++;
            if (c + 1 < map[0].Length && !visited.Contains(100 * r + c + 1)) wave++;

            wave = wave > 1 ? 1 : 0;

            if (r - 1 >= 0 && !visited.Contains((r - 1) * 100 + c)) {
                var nv = new HashSet<int>(visited);
                nv.Add(r * 100 + c);
                Search(r - 1, c, cur + wave, map, nv);
            }

            if (r + 1 < map.Length && !visited.Contains((r + 1) * 100 + c)) {
                var nv = new HashSet<int>(visited);
                nv.Add(r * 100 + c);
                Search(r + 1, c, cur + wave, map, nv);
            }

            if (c - 1 >= 0 && !visited.Contains(100 * r + c - 1)) {
                var nv = new HashSet<int>(visited);
                nv.Add(r * 100 + c);
                Search(r, c - 1, cur + wave, map, nv);
            }

            if (c + 1 < map[0].Length && !visited.Contains(100 * r + c + 1)) {
                var nv = new HashSet<int>(visited);
                nv.Add(r * 100 + c);
                Search(r, c + 1, cur + wave, map, nv);
            }

        }
    }

    public static class Similar_Pair
    {
        public static void Start() {
            // to avoid stack overflow, assign the maxStackSize manually to allocate more data
            Thread T = new Thread(similar2, 100000000);
            T.Start();
        }
        static void similar2() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int t = int.Parse(tmp[1]);

            int[] fenwick = new int[n + 1];
            HashSet<int>[] children = new HashSet<int>[n + 1];
            for (int i = 0; i < n + 1; i++) { children[i] = new HashSet<int>(); }
            bool[] rootarr = new bool[n + 1];

            for (int i = 0; i <= n; i++) {
                rootarr[i] = true;
            }

            for (int i = 0; i < n - 1; i++) {
                tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]);
                int r = int.Parse(tmp[1]);
                children[l].Add(r);
                rootarr[r] = false;
            }

            int root = -1;
            for (int i = 1; i <= n; i++) { if (rootarr[i]) { root = i; rootarr = null; break; } }

            decimal ans = 0;
            DFS_search(root, fenwick, children, n, t, ref ans);
            Console.WriteLine(ans);
        }
        static void DFS_search(int node, int[] fenwick, HashSet<int>[] children, int n, int t, ref decimal ans) {
            ans += sum_fenwick(fenwick, Math.Max(1, node - t), Math.Min(n, node + t));

            // update fenwick tree so that it has 1 on this node that we passed by
            update_fenwick(fenwick, n, node, 1);
            foreach (var d in children[node]) DFS_search(d, fenwick, children, n, t, ref ans);
            // remove the one from fenwick tree as we finished this node and are moving on to others
            update_fenwick(fenwick, n, node, -1);
        }
        static decimal sum_fenwick(int[] arr, int i, int j) {
            decimal sum = 0;
            while (j > 0) {
                sum += arr[j];
                j -= (j & (j * -1));
            }
            i--;
            while (i > 0) {
                sum -= arr[i];
                i -= (i & (i * -1));
            }
            return sum;
        }
        static void update_fenwick(int[] arr, int n, int i, int diff) {
            while (i <= n) {
                arr[i] += diff;
                i += (i & (i * -1));
            }
        }

    }

    public static class Absolute_Element_Sums
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

            int q = int.Parse(Console.ReadLine());
            decimal[] queries = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToDecimal);

            for (int i = 1; i < q; i++) {
                queries[i] += queries[i - 1];
            }

            int[] numbers = new int[4001];
            for (int i = 0; i < n; i++) {
                numbers[arr[i] + 2000]++;
            }
            int negs = 0, poss = 0;
            decimal negsum = 0, possum = 0;
            for (int i = 0; i < 2000; i++) { negsum -= numbers[i] * (i - 2000); negs += numbers[i]; }
            for (int i = 2000; i < 4001; i++) { possum += numbers[i] * (i - 2000); poss += numbers[i]; }

            foreach (var x in queries) {
                decimal sum = 0;
                if (x < 0) {
                    sum += negsum - x * negs;
                    for (int i = 2000; i < 4001; i++) {
                        sum += (numbers[i] * Math.Abs((i - 2000) + x));
                    }
                } else if (x >= 0) {
                    sum += possum + x * poss;
                    for (int i = 0; i < 2000; i++) {
                        sum += (numbers[i] * Math.Abs((i - 2000) + x));
                    }
                }
            }
        }
    }

    public static class Bike_Racers
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int k = int.Parse(tmp[2]);

            Tuple<long, long>[] Bikers = new Tuple<long, long>[n],
                Bikes = new Tuple<long, long>[m];

            for (int i = 0; i < n; i++) {
                tmp = Console.ReadLine().Split(' ');
                Bikers[i] = Tuple.Create(long.Parse(tmp[0]), long.Parse(tmp[1]));
            }
            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                Bikes[i] = Tuple.Create(long.Parse(tmp[0]), long.Parse(tmp[1]));
            }

            long[][] w = new long[n][]; for (int i = 0; i < n; i++) w[i] = new long[m];
            long[] times = new long[n * m]; int __ = 0;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    long x = (Bikers[i].Item1 - Bikes[j].Item1);
                    long y = (Bikers[i].Item2 - Bikes[j].Item2);

                    w[i][j] = x * x + y * y;
                    times[__++] = w[i][j];
                }
            }

            Array.Sort(times);

            long topMax = long.MaxValue;

            int high = n * m - 1, low = 0;
            while (high - low > 1) {
                int mid = (high + low) / 2;
                long tested = times[mid];

                bool good = testBikes(n, m, k, w, tested);

                if (good) { topMax = Math.Min(tested, topMax); high = mid; } else { low = mid; }
            }

            Console.WriteLine(topMax);
        }
        static bool testBikes(int n, int m, int k, long[][] w, long tested) {
            bool[][] usopp = new bool[n][];
            for (int i = 0; i < n; i++) usopp[i] = new bool[m];

            for (int i = 0; i < n; i++)
                for (int z = 0; z < m; z++)
                    usopp[i][z] = w[i][z] <= tested;


            int[] match = new int[m];
            int robin = 0;
            for (int i = 0; i < m; i++) match[i] = -1;

            for (int i = 0; i < n; i++) {
                bool[] seen = new bool[m];
                if (bpm(usopp, i, seen, match)) robin++;
            }

            return robin >= k;
        }
        static bool bpm(bool[][] usopp, int j, bool[] seen, int[] match) {
            int n = usopp.Length, m = usopp[0].Length;

            for (int i = 0; i < m; i++) {
                if (usopp[j][i] && !seen[i]) {

                    seen[i] = true;

                    if (match[i] < 0 || bpm(usopp, match[i], seen, match)) {
                        match[i] = j;
                        return true;
                    }

                }
            }
            return false;
        }
    }

    public static class _Beautiful_Quadruples
    {
        public static void Beautiful_Quadruples() {
            // Method used: Meet in the middle
            // https://www.hackerrank.com/challenges/xor-quadruples/editorial

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            Array.Sort(arr);
            int A = arr[0], B = arr[1], C = arr[2], D = arr[3];

            int mx1 = 3001, mx2 = 4500;

            long ans = 0;
            long[,] AB = new long[mx1, mx2];
            long[] all = new long[mx1];

            for (int a = 1; a <= A; a++) {
                for (int b = a; b <= B; b++) {
                    AB[b, a ^ b]++;
                    all[b]++;
                }
            }

            for (int i = 1; i < mx1; i++) {
                all[i] += all[i - 1];

                for (int j = 0; j < mx2; j++) AB[i, j] += AB[i - 1, j];
            }

            for (int c = 1; c <= C; c++) {
                for (int d = c; d <= D; d++) {
                    ans += all[c] - AB[c, c ^ d];
                }
            }

            Console.WriteLine(ans);
        }
    }

    public static class Gena_Playing_Hanoi
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] rr = { 0, 1, 3, 5, 9, 13, 17, 25, 33, 41, 49 };
            if (arr.Distinct().Count() == 1) { Console.WriteLine(arr[0] == 1 ? 0 : rr[n]); return; }

            string key = "".PadLeft(n, '0');
            string k = "";

            for (int i = 0; i < n; i++) k += arr[i] - 1;
            char[] G = { '0', '1', '2', '3' };
            Dictionary<string, int> mapH = new Dictionary<string, int>() { { key, 0 } };

            bool found = false;
            bool[] v = new bool[4];
            Queue<string> Q = new Queue<string>();
            Q.Enqueue(key);
            while (!found && Q.Count > 0) {
                v[0] = false; v[1] = false; v[2] = false; v[3] = false;
                var from = Q.Dequeue();
                int m = 1 + mapH[from];

                var sb = new StringBuilder(from);
                for (int i = 0; i < n; i++) {
                    int cur = from[i] - '0';

                    if (!v[cur]) {
                        v[cur] = true;
                        for (int j = 0; j < 4; j++) {
                            if (v[j]) continue;
                            var t = sb[i];
                            sb[i] = G[j];
                            string to = sb.ToString();
                            sb[i] = t;

                            if (!mapH.ContainsKey(to)) {
                                mapH[to] = m;
                                found = to == k;
                                Q.Enqueue(to);
                            }
                        }
                    }
                }
            }

            Console.WriteLine(mapH[k]);
        }
    }

    public static class King_Richards_Knights
    {
        public static void Start() {
            StringBuilder sb = new StringBuilder();
            int n = int.Parse(Console.ReadLine());
            int s = int.Parse(Console.ReadLine());

            long[] a = new long[s], b = new long[s], d = new long[s];
            for (int i = 0; i < s; i++) {
                var tmp = Console.ReadLine().Split(' ');
                a[i] = long.Parse(tmp[0]) - 1;
                b[i] = long.Parse(tmp[1]) - 1;
                d[i] = long.Parse(tmp[2]);
            }

            long[] eOdd = new long[s], eEven = new long[s], fOdd = new long[s], fEven = new long[s];

            for (int i = 1; i < s; i += 2) {
                eOdd[i] = (a[i] + b[i] + d[i]) - (i > 1 ? eOdd[i - 2] : 0);
                if (i < s - 1) eOdd[i + 1] = eOdd[i];

                fOdd[i] = (a[i] - b[i]) - (i > 1 ? fOdd[i - 2] : 0);
                if (i < s - 1) fOdd[i + 1] = fOdd[i];
            }
            for (int i = 0; i < s; i += 2) {
                eEven[i] = (a[i] + b[i] + d[i]) - (i > 1 ? eEven[i - 2] : 0);
                if (i < s - 1) eEven[i + 1] = eEven[i];

                fEven[i] = (a[i] - b[i]) - (i > 1 ? fEven[i - 2] : 0);
                if (i < s - 1) fEven[i + 1] = fEven[i];
            }

            int q = int.Parse(Console.ReadLine());
            while (q-- > 0) {
                long t = long.Parse(Console.ReadLine());
                long i = t / n, j = t % n, II = i, JJ = j;

                long aa = 0, bb = 0;
                int mid;
                int low = 0, high = s - 1;
                if (Inside(a, b, d, II, JJ, 0)) {
                    EvaluatePosition(eOdd, eEven, fOdd, fEven, II, JJ, 0, out aa, out bb);
                    if (Inside(a, b, d, aa, bb, 0)) { i = aa; j = bb; }

                    while (high - low > 1) {
                        mid = (low + high) / 2;
                        aa = bb = 0;
                        EvaluatePosition(eOdd, eEven, fOdd, fEven, II, JJ, mid, out aa, out bb);
                        if (Inside(a, b, d, aa, bb, mid)) {
                            low = mid; i = aa; j = bb;
                        } else high = mid;
                    }
                }

                i++; j++;
                sb.Append(i).Append(" ").Append(j).Append("\n");
            }
            Console.WriteLine(sb.ToString());
        }
        static void EvaluatePosition(long[] eOdd, long[] eEven, long[] fOdd, long[] fEven, long i, long j, int p, out long a, out long b) {
            if (p % 2 == 0) {
                if (p % 4 == 0) {
                    a = eOdd[p] + fEven[p] + j; b = eEven[p] - fOdd[p] - i;
                } else {
                    a = eOdd[p] + fEven[p] - j; b = eEven[p] - fOdd[p] + i;
                }
            } else {
                if ((1 + p) % 4 == 0) {
                    a = eEven[p] + fOdd[p] + i; b = eOdd[p] - fEven[p] + j;
                } else {
                    a = eEven[p] + fOdd[p] - i; b = eOdd[p] - fEven[p] - j;
                }
            }
        }
        static bool Inside(long[] a, long[] b, long[] d, long i, long j, int p) {
            return i >= a[p] && j >= b[p] && i <= a[p] + d[p] && j <= b[p] + d[p];
        }
    }

    public static class Short_Palindrome
    {
        const int MOD = 1000000007;
        public static void Start() {
            string line = Console.ReadLine();
            int n = line.Length;

            if (n < 4) { Console.WriteLine(0); return; }

            long ans = 0, top, toptop;
            for (int mainChar = 0; mainChar < 26; mainChar++) {
                char c = (char)('a' + mainChar);

                for (int searchedChar = 0; searchedChar < 26; searchedChar++) {
                    if (mainChar == searchedChar) continue;
                    char ser = (char)('a' + searchedChar);

                    long[] left = new long[n];

                    top = 0;
                    for (int i = 0; i < n; i++) {
                        if (line[i] == c) {
                            top++;
                        } else if (line[i] == ser) {
                            left[i] = top;
                        }
                    }

                    top = 0; toptop = 0;
                    for (int i = n - 1; i >= 0; i--) {
                        if (line[i] == c) {
                            top++;
                        } else if (line[i] == ser) {
                            ans += (left[i] * toptop);
                            ans %= MOD;
                            toptop += top;
                        }
                    }
                }
            }

            int[] four = new int[26];
            for (int i = 0; i < n; i++) four[line[i] - 'a']++;

            for (int i = 0; i < 26; i++) {
                ans += (long)(CNK_SP(four[i], 4) % MOD);
                ans %= MOD;
            }

            Console.WriteLine(ans);
        }

        static BigInteger CNK_SP(long n, long k) {
            if (n < k) return 0;
            if (n == k || k == 0) return 1;
            return BigInteger.Multiply(n, CNK_SP(n - 1, k - 1)) / k;
        }
    }

    public static class Minimum_Loss
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            long[] p = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            long ans = long.MaxValue;
            Node h = new Node(p[0]);
            for (int i = 1; i < n; i++) {
                long l = h.getMinLarger(p[i]);
                ans = Math.Min(ans, l - p[i]);
                h.insert(p[i]);
            }
            Console.WriteLine(ans);
        }
        class Node
        {
            public long val;
            public long max;
            public Node left, right;
            public Node(long v) { val = v; max = v; }
            public void insert(long v) {
                if (v > val) {
                    if (right == null) right = new Node(v); else right.insert(v);
                    if (v > max) max = v;
                } else {
                    if (left == null) left = new Node(v); else left.insert(v);
                }
            }
            public long getMinLarger(long v) {
                if (left == null && right == null) return val > v ? val : long.MaxValue;
                if (left != null && left.max > v) {
                    return left.getMinLarger(v);
                }
                if (val > v) return val;
                if (right != null) return right.getMinLarger(v);
                return long.MaxValue;
            }
        }
    }

    public static class Making_Candies
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            BigInteger m = BigInteger.Parse(tmp[0]);    // machines
            BigInteger w = BigInteger.Parse(tmp[1]);    // workers
            BigInteger p = BigInteger.Parse(tmp[2]);    // cost
            BigInteger n = BigInteger.Parse(tmp[3]);    // required candies

            Console.WriteLine(solve(m, w, p, n));
        }
        private static BigInteger solve(BigInteger m, BigInteger w, BigInteger p, BigInteger n) {
            BigInteger passes = 0;
            BigInteger limit = 100000000000000L;
            BigInteger candies = 0;

            /*
             * If you invest, invest everything you have and as early as possible
             */

            while (candies + m * w < n && passes < limit) {
                passes++;
                candies += m * w;
                BigInteger lmt = passes + ceil(n - candies, m * w);
                if (lmt < limit) limit = lmt;

                if (candies < p) {
                    lmt = (p - candies) / m / w;
                    passes += lmt;
                    candies += m * w * lmt;
                }
                update(ref m, ref w, ref candies, p);

            }
            passes++;

            return passes < limit ? passes : limit;
        }
        static void update(ref BigInteger m, ref BigInteger w, ref BigInteger candies, BigInteger p) {
            BigInteger c = candies / p;
            if (m + c <= w) {
                m += c;
            } else if (w + c <= m) {
                w += c;
            } else {
                BigInteger tot = m + w + c;
                w = tot / 2;
                m = tot - w;
            }
            candies -= p * c;
        }

        static BigInteger ceil(BigInteger x, BigInteger y) {
            if (x % y == 0) return x / y;
            return 1 + x / y;
        }
    }
}
