using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hackerrank.Game_Theory
{
    public static class Game_of_Stones
    {
        static void Start() {
            int t_c = int.Parse(Console.ReadLine());
            List<int> good = new List<int> { 2, 3, 4, 5, 6, 9, 10 }, bad = new List<int> { 1, 7, 8 };

            for (int i = 11; i < 101; i++) {
                if (bad.Contains(i - 2) || bad.Contains(i - 3) || bad.Contains(i - 5)) good.Add(i); else bad.Add(i);
            }
            for (int i_tc = 0; i_tc < t_c; i_tc++) {
                Console.WriteLine(good.Contains(int.Parse(Console.ReadLine())) ? "First" : "Second");
            }
        }
    }

    public static class Tower_Breakers
    {
        static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                string[] tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);

                if (m == 1) { Console.WriteLine(2); continue; }
                if (n == 1) { Console.WriteLine(1); continue; }


                Console.WriteLine(n % 2 == 0 ? 2 : 1);
            }
        }
    }

    public static class A_Chessboard_Game
    {
        static void Start() {
            List<int> good = new List<int> { };
            List<int> bad = new List<int> { 101 };
            bool check = true;
            while (check) {
                check = false;
                for (int i = 1; i < 16; i++) {
                    for (int j = 1; j < 16; j++) {
                        int key = i * 100 + j;
                        if (bad.Contains(key) || good.Contains(key)) continue;

                        int t1 = -1, t2 = -1, t3 = -1, t4 = -1;

                        if (i - 2 > 0 && j + 1 < 16) { t1 = (i - 2) * 100 + (j + 1); if (bad.Contains(t1)) { good.Add(key); check = true; continue; } }
                        if (i - 2 > 0 && j - 1 > 0) { t2 = (i - 2) * 100 + (j - 1); if (bad.Contains(t2)) { good.Add(key); check = true; continue; } }
                        if (i + 1 < 16 && j - 2 > 0) { t3 = (i + 1) * 100 + (j - 2); if (bad.Contains(t3)) { good.Add(key); check = true; continue; } }
                        if (i - 1 > 0 && j - 2 > 0) { t4 = (i - 1) * 100 + (j - 2); if (bad.Contains(t4)) { good.Add(key); check = true; continue; } }

                        if ((t1 == -1 || good.Contains(t1)) &&
                            (t2 == -1 || good.Contains(t2)) &&
                            (t3 == -1 || good.Contains(t3)) &&
                            (t4 == -1 || good.Contains(t4))) { bad.Add(key); }
                    }
                }
            }

            int tc = int.Parse(Console.ReadLine());
            for (int i_tc = 0; i_tc < tc; i_tc++) {
                string[] tmp = Console.ReadLine().Split(' ');
                Console.WriteLine(bad.Contains(100 * int.Parse(tmp[0]) + int.Parse(tmp[1])) ? "Second" : "First");

            }
        }
    }

    public static class Nim_Game
    {
        static void Start() {
            string A = "First", B = "Second";
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] piles = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

                if (n == 1) { Console.WriteLine(A); } else {
                    int k = 0;
                    for (int i = 0; i < n; i++) k ^= piles[i];
                    Console.WriteLine(k != 0 ? A : B);
                }
            }
        }
    }

    public static class Misère_Nim
    {
        static void Start() {
            string A = "First", B = "Second";
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] piles = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

                if (n == 1) { Console.WriteLine(piles[0] == 1 ? B : A); } else {
                    int k = 0; bool m = false;
                    for (int i = 0; i < n; i++) { k ^= piles[i]; m = m || piles[i] != 1; }

                    if (!m) Console.WriteLine(n % 2 == 1 ? B : A);
                    else {
                        Console.WriteLine(k == 0 ? B : A);
                    }
                }
            }
        }
    }

    public static class Nimble_Game
    {
        static void Start() {
            // Nimble is a disguised form of Nim. 
            // Consider each chipa Nim pile with the number of objects in the pile equal to the chip's position on the tape
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] boxes = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

                if (n == 1) { Console.WriteLine("Second"); } else if (n == 2) { Console.WriteLine(boxes[1] % 2 == 1 ? "First" : "Second"); } else {
                    int x = 0;
                    for (int i = 1; i < n; i++) { if (boxes[i] % 2 == 1) x ^= i; }

                    Console.WriteLine(x == 0 ? "Second" : "First");
                }
            }
        }
    }

    public static class Poker_Nim
    {
        static void Start() {
            // it's the same as nim because anything i put, he can remove thus cancelling eachother;
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                string[] tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int[] piles = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

                int x = 0;
                for (int i = 0; i < n; i++) x ^= piles[i];

                Console.WriteLine(x == 0 ? "Second" : "First");
            }
        }
    }

    public static class Tower_Breakers_Revisited
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {
                int n = int.Parse(Console.ReadLine());
                int[] towers = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
                if (n == 1) { Console.WriteLine(towers[0] > 1 ? 1 : 2); } else {
                    int[] piles = new int[n];
                    for (int i = 0; i < n; i++) { piles[i] = divisors(towers[i]) - 1; }

                    int x = 0;
                    foreach (var p in piles) x ^= p;

                    Console.WriteLine(x == 0 ? 2 : 1);
                }
            }
        }
        static int divisors(int n) {
            int div = 1;
            int i = 1;
            while (i < n) {
                i++;
                if (n % i == 0) {
                    n = n / i;
                    div++;
                    i = 1;
                }
            }
            return div;
        }
    }

    public static class Tower_Breakers_Again
    {
        public static void Start() {
            int[] gr = new int[100001];
            grundy(gr);
            int tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {
                int n = int.Parse(Console.ReadLine());
                int[] towers = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
                if (n == 1) { Console.WriteLine(towers[0] > 1 ? "1" : "2"); } else {

                    int xs = 0;
                    for (int i = 0; i < n; i++) { xs ^= gr[towers[i]]; }

                    Console.WriteLine(xs == 0 ? 2 : 1);

                }
            }
        }
        static void grundy(int[] gr) {
            gr[0] = 0;
            gr[1] = 0;

            var gruns = new HashSet<int>();

            for (int i = 2; i < gr.Length; i++) {

                gruns.Clear();

                int p = (int)Math.Sqrt(i);
                for (int j = 1; j <= p; j++) {
                    if (i % j == 0) {
                        // divided in towers of length i/j , if j is odd, the final xor will be i/j
                        // 18 tower to 3 towers of length 6, the final xor will be 6
                        if (j % 2 != 0) gruns.Add(gr[i / j]);

                        // divided in towers of length j , if i/j is odd, the final xor will be j
                        // 18 tower to 6 towers of length 3, the final xor will be 3
                        if ((i / j) % 2 == 1) { gruns.Add(gr[j]); }
                    }
                }
                int g = -1;
                for (int j = 0; j < 1000000; j++) {
                    if (!gruns.Contains(j)) { g = j; break; }
                }
                gr[i] = g;
            }

        }
    }

    public static class A_Chessboard_Game_Again
    {
        public static void Start() {
            Dictionary<int, int> grs = new Dictionary<int, int> { { 101, 0 }, { 102, 0 }, { 201, 0 }, { 202, 0 } };

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int k = 0;
                while (n-- > 0) {
                    string[] tmp = Console.ReadLine().Split(' ');
                    int x = int.Parse(tmp[0]), y = int.Parse(tmp[1]);
                    k ^= findGrundy(grs, x, y);
                }
                Console.WriteLine(k == 0 ? "Second" : "First");
            }
        }
        static int findGrundy(Dictionary<int, int> grs, int x, int y) {
            if (x <= 0 || y <= 0 || x > 15 || y > 15) return -1;

            int key = 100 * x + y;
            if (grs.ContainsKey(key)) return grs[key];

            HashSet<int> hash = new HashSet<int>();
            hash.Add(findGrundy(grs, x - 2, y + 1));
            hash.Add(findGrundy(grs, x - 2, y - 1));
            hash.Add(findGrundy(grs, x + 1, y - 2));
            hash.Add(findGrundy(grs, x - 1, y - 2));

            int ret = 0;
            while (hash.Contains(ret)) ret++;
            grs.Add(key, ret);

            return ret;
        }
    }

    public static class Digits_Square_Board
    {
        static int[,,,] sgrundy;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                sgrundy = new int[n, n, n, n];
                bool[,] map = new bool[n, n];
                for (int i = 0; i < n; i++) {
                    var tmp = Console.ReadLine().Split(' ');
                    for (int inj = 0; inj < n; inj++) {
                        map[i, inj] = !"2357".Contains(tmp[inj]);
                    }
                }

                for (int a = 0; a < n; a++) {
                    for (int b = 0; b < n; b++) {

                        for (int i = 0; i < n; i++) {
                            for (int j = 0; j < n; j++) {
                                if (a == 0 && b == 0) continue;
                                if (i + a >= n || j + b >= n) break;

                                CutIt(map, i, i + a, j, j + b);

                            }
                        }
                    }
                }

                Console.WriteLine(sgrundy[0, n - 1, 0, n - 1] == 0 ? "Second" : "First");
            }
        }
        static void CutIt(bool[,] map, int x1, int x2, int y1, int y2) {
            if (noNonPrimes(x1, x2, y1, y2, map)) return;

            bool[] h = new bool[1000];

            for (int i = x1; i < x2; i++) {
                h[sgrundy[x1, i, y1, y2] ^ sgrundy[i + 1, x2, y1, y2]] = true;
            }

            for (int j = y1; j < y2; j++) {
                h[sgrundy[x1, x2, y1, j] ^ sgrundy[x1, x2, j + 1, y2]] = true;
            }

            int g = 0;
            while (h[g]) g++;
            sgrundy[x1, x2, y1, y2] = g;
        }
        static bool noNonPrimes(int x1, int x2, int y1, int y2, bool[,] map) {
            for (int i = x1; i <= x2; i++) {
                for (int j = y1; j <= y2; j++) {
                    if (map[i, j]) return false;
                }
            }
            return true;
        }
    }

    public static class Fun_Game
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {
                try {
                    int n = int.Parse(Console.ReadLine());
                    int[] a = new int[n], b = new int[n];
                    bool[] taken = new bool[n];

                    string[] tmp1 = Console.ReadLine().Split(' '), tmp2 = Console.ReadLine().Split(' ');
                    for (int q = 0; q < n; q++) { a[q] = int.Parse(tmp1[q]); b[q] = int.Parse(tmp2[q]); }
                    //int move  = getnextmove(a, b, taken, true, int c);
                    bool fturn = false;
                    int c = 0; int p1 = 0, p2 = 0;
                    int sa = a.Sum(), sb = b.Sum();
                    while (c < n) {
                        fturn = !fturn;
                        if (fturn) {
                            int j = indexMax(a, b, taken);

                            taken[j] = true;
                            p1 += a[j];
                            sa -= a[j]; sb -= b[j];
                        } else {
                            int j = indexMax(b, a, taken);

                            taken[j] = true;
                            p2 += b[j];
                            sa -= a[j]; sb -= b[j];
                        }
                        c++;
                    }

                    string print = "";
                    if (p1 == p2) { print = "Tie"; } else if (p1 > p2) { print = "First"; } else { print = "Second"; }
                    Console.WriteLine(print);
                } catch (Exception e) { }
            }

        }
        static int indexMax(int[] a, int[] b, bool[] v) {
            int r = -1;
            for (int i = 0; i < a.Length; i++) {
                if (v[i]) continue;
                if (r == -1) { r = i; continue; }

                if (a[i] + b[i] > a[r] + b[r]) r = i;
            }
            return r;
        }
    }

    public static class Powers_Game
    {
        static void Start() {
            int tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {
                int n = int.Parse(Console.ReadLine());
                if (n < 6) { Console.WriteLine("First"); }
                if (n % 4 == 0) {
                    Console.WriteLine("Second");
                } else { Console.WriteLine("First"); }
            }
        }
    }

    public static class Tower_Breakers_final
    {
        public static void Start() {
            var highestInThatNum = new List<decimal> { 1, 1, 1, 1, 2, 3, 4, 5, 7, 11, 16, 22, 30 };
            for (int i = 13; i < 150; i++) {
                int sq = (int)Math.Sqrt(i);
                decimal hinge = 0;
                for (int j = 1; j <= sq; j++) {
                    hinge += highestInThatNum[i - j * j];
                }
                highestInThatNum.Add(hinge);
            }
            var tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {

                decimal n = Convert.ToDecimal(Console.ReadLine());
                for (int i = 0; i < highestInThatNum.Count; i++) {
                    if (highestInThatNum[i] >= n) { Console.WriteLine(i); break; }
                }
            }

        }
    }

    public static class Deforestation
    {
        public static void Start() {
            string A = "Alice", B = "Bob";
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                HashSet<int>[] adj = new HashSet<int>[n + 1];
                for (int i = 0; i <= n; i++) { adj[i] = new HashSet<int>(); }

                for (int i = 0; i < n - 1; i++) {
                    string[] tmp = Console.ReadLine().Split(' ');
                    int l = int.Parse(tmp[0]), r = int.Parse(tmp[1]);
                    adj[l].Add(r); adj[r].Add(l);
                }

                int xor = dfs_deforestation(adj, 1, 0);

                Console.WriteLine(xor == 0 ? B : A);
            }
        }
        static int dfs_deforestation(HashSet<int>[] adj, int n, int proh) {
            int xor = 0;
            foreach (var node in adj[n]) {
                if (node == proh) continue;
                xor ^= (1 + dfs_deforestation(adj, node, n));
            }
            return xor;
        }
    }

    public static class A_stones_game
    {
        public static void Start() {
            // x ^ 28
            List<long> ind = new List<long> { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768 };
            List<long> vals = new List<long> { 2, 7, 8, 4, 5, 127, 128, 121, 125, 97, 113, 64, 65, 32767, 32768 };

            long before = 32768;
            for (int i = 0; i < 25; i++) {
                ind.Add(before * 2);
                ind.Add(before * 4);

                vals.Add(before * 4 - 1);
                vals.Add(before * 4);
                before *= 4;
            }

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                if ((n - 3) % 4 == 0) {
                    Console.WriteLine(0);
                    continue;
                } else if ((n - 5) % 4 == 0) {
                    Console.WriteLine(1);
                    continue;
                } else if ((n - 6) % 4 == 0) {
                    n -= 2;
                }
                n /= 4;
                for (int i = ind.Count - 1; i >= 0; i--) {
                    if (ind[i] <= n) {
                        Console.WriteLine(vals[i]);
                        break;
                    }
                }
            }
        }
        public static void Solve() {
            int[] ind = { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096, 8192, 16384, 32768, 65536, 131072, 262144, 524288, 1048576, 2097152, 4194304, 8388608, 16777216, 33554432, 67108864, 134217728, 268435456 };
            int[] vals = { 2, 7, 8, 4, 5, 127, 128, 121, 125, 97, 113, 64, 65, 32767, 32768, 32761, 32765, 32737, 32753, 32641, 32705, 32257, 32513, 30721, 31745, 24577, 28673, 16384, 16385 };

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                if (n == 1 || n == 2) {
                    Console.WriteLine(1);
                    continue;
                } else if ((n - 3) % 4 == 0 || (n - 5) % 4 == 0) {
                    Console.WriteLine(1);
                    continue;
                } else if ((n - 6) % 4 == 0) {
                    n -= 2;
                }
                n /= 4;
                for (int i = 28; i >= 0; i--) {
                    if (ind[i] <= n) {
                        Console.WriteLine(vals[i]);
                        break;
                    }
                }
            }
        }

    }

    public static class Chocolate_in_Box
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            if (n == 1) { Console.WriteLine(1); } else {
                long xor = 0;
                for (int i = 0; i < n; i++) xor ^= arr[i];

                if (xor == 0) { Console.WriteLine(0); } else {
                    int ans = 0;
                    for (int i = 0; i < n; i++) {
                        long xx = xor ^ arr[i];
                        if (xx <= arr[i]) ans++;
                    }
                    Console.WriteLine(ans);
                }
            }
        }
    }

    public static class Manasa_and_Prime_game
    {
        public static void Start() {
            int[] gr = { 0, 0, 1, 1, 2, 2, 3, 3, 4 };
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                long[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

                long xor = 0;
                for (int i = 0; i < n; i++) xor ^= gr[arr[i] % 9];

                Console.WriteLine(xor == 0 ? "Sandy" : "Manasa");
            }
        }
    }

    public static class Vertical_Rooks
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] A = new int[n], B = new int[n];
                for (int i = 0; i < n; i++) A[i] = int.Parse(Console.ReadLine());
                for (int i = 0; i < n; i++) B[i] = int.Parse(Console.ReadLine());

                int xor = 0;
                for (int i = 0; i < n; i++) xor ^= Math.Abs(A[i] - B[i]) - 1;

                Console.WriteLine(xor == 0 ? "player-1" : "player-2");
            }
        }
    }
    public static class Play_on_benders
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);


            List<int>[] adj = new List<int>[n + 1];
            for (int i = 0; i <= n; i++) { adj[i] = new List<int>(); }

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                adj[int.Parse(tmp[0])].Add(int.Parse(tmp[1]));
            }

            int[] gr = new int[n + 1];
            for (int i = 1; i <= n; i++) if (adj[i].Count == 0) gr[i] = 0; else gr[i] = -1;

            bool f = true;
            while (f) {
                f = false;
                for (int i = 1; i <= n; i++) {
                    if (gr[i] != -1) continue;

                    var h = new HashSet<int>();
                    foreach (var c in adj[i]) h.Add(gr[c]);
                    if (h.Contains(-1)) continue;

                    int g = 0;
                    while (h.Contains(g)) g++;
                    gr[i] = g;
                    f = true;
                }
            }

            int q = int.Parse(Console.ReadLine());
            while (q-- > 0) {
                int k = int.Parse(Console.ReadLine());
                int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                int xor = 0;
                for (int i = 0; i < k; i++) xor ^= gr[A[i]];

                Console.WriteLine(xor == 0 ? "Iroh" : "Bumi");
            }
        }
    }

    public static class Permutation_game
    {
        static Dictionary<long, int> mem_permutation_game;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            mem_permutation_game = new Dictionary<long, int>();
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                Console.WriteLine(grundy(A, n) == 0 ? "Bob" : "Alice");
            }
        }
        static int grundy(int[] A, int n) {
            if (n == 1 || isSorted(A)) return 0;
            long r = 0;
            for (int i = 0; i < n; i++) r = r * 100 + A[i];
            if (mem_permutation_game.ContainsKey(r)) return mem_permutation_game[r];

            int[] B = new int[n - 1];
            for (int i = 0; i < n; i++) {
                int b = 0;
                for (int j = 0; j < n; j++) {
                    if (i == j) continue;
                    B[b++] = A[j];
                }
                int x = grundy(B, n - 1);
                if (x == 0) { mem_permutation_game.Add(r, 1); return 1; }
            }

            mem_permutation_game.Add(r, 0);
            return 0;
        }

        static bool isSorted(int[] A) {
            for (int i = 1; i < A.Length; i++) {
                if (A[i] < A[i - 1]) return false;
            }
            return true;
        }
    }

    public static class Kitty_and_Katty
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());

                Console.WriteLine(n == 1 || n % 2 == 0 ? "Kitty" : "Katty");
            }
        }
    }

    public static class Stone_Piles
    {
        public static void Start() {
            /* Construct Grundy Numbers
            int[] gr = new int[51];
            HashSet<int>[] arp = new HashSet<int>[51];
            for (int i = 3; i < 51; i++) {
                arp[i] = new HashSet<int>();
                getGR(i, new List<int>(), 0, arp, gr);
                int g = 0;
                while (arp[i].Contains(g)) g++;
                gr[i] = g;
            }
            */

            int[] gr = { 0, 0, 0, 1, 0, 2, 3, 4, 0, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21,
                           22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46 };
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                int xor = 0;
                foreach (var c in A) xor ^= gr[c];
                Console.WriteLine(xor == 0 ? "BOB" : "ALICE");
            }
        }
        /*static void getGR(int j, List<int> list, int prev, HashSet<int>[] arp, int[] gr) {
            if (j == 0) {
                if (list.Count == 1) return;
                int r = 0;
                int xor = 0;
                foreach (var item in list) { xor ^= gr[item]; r += item; }
                arp[r].Add(xor);
                Console.Write(r + " ");
            }

            for (int i = prev + 1; i < 100000; i++) {
                if (i > j) break;
                list.Add(i);
                getGR(j - i, list, i, arp, gr);
                list.RemoveAt(list.Count - 1);
            }
        }*/
    }

    public static class Move_the_Coins
    {
        public static void Start() {
            var sb = new StringBuilder();
            int n = int.Parse(Console.ReadLine());
            int[,] gr = new int[n + 1, 21];
            for (int i = 1; i < n; i += 2) {
                for (int j = 0; j < 21; j++) gr[i, j] = j;
            }

            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            var adj = new HashSet<int>[n + 1]; for (int i = 1; i <= n; i++) adj[i] = new HashSet<int>();
            int[] parent = new int[n + 1], depth = new int[n + 1];

            for (int i = 0; i < n - 1; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]);
                int r = int.Parse(tmp[1]);
                adj[l].Add(r); adj[r].Add(l);
            }

            setParents(adj, parent, depth, 1, -1, 0);

            int[] zero = new int[n], one = new int[n];
            zero[0] = dfs(adj, 1, -1, A, gr, 0, zero);
            one[0] = dfs(adj, 1, -1, A, gr, 1, one);
            int mX = zero[0];

            int q = int.Parse(Console.ReadLine());
            while (q-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]), r = int.Parse(tmp[1]);

                int d1 = depth[l] % 2, d2 = (depth[r] + 1) % 2;

                bool cycle = false;
                while (!cycle && r > 1) { r = parent[r]; cycle = r == l; }

                if (cycle) {
                    sb.Append("INVALID\n");
                } else {
                    int xor = mX;
                    if (d1 != d2) xor ^= zero[l - 1] ^ one[l - 1];
                    sb.Append(xor == 0 ? "NO\n" : "YES\n");
                }
            }
            Console.WriteLine(sb.ToString());
        }

        static int dfs(HashSet<int>[] adj, int fr, int no, int[] A, int[,] gr, int d, int[] zero) {
            int xor = gr[d, A[fr - 1]];
            foreach (var item in adj[fr]) {
                if (item == no) continue;
                xor ^= dfs(adj, item, fr, A, gr, d + 1, zero);
            }
            zero[fr - 1] = xor;
            return xor;
        }

        static void setParents(HashSet<int>[] adj, int[] P, int[] D, int fr, int no, int d) {
            D[fr] = d;
            foreach (var item in adj[fr]) {
                if (item == no) continue;
                P[item] = fr;
                setParents(adj, P, D, item, fr, d + 1);
            }
        }
    }
}
