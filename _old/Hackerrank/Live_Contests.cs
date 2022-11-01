#define debug
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Hackerrank
{
    class Live_Contests {
        const int MOD = 1000000007;
        static Random ran = new Random();

        public static void Clues_on_a_Binary_Path() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int d = int.Parse(tmp[2]);

            List<Tuple<int, bool>>[] adj = new List<Tuple<int, bool>>[n];
            for (int i = 0; i < n; i++) adj[i] = new List<Tuple<int, bool>>();

            for (int i = 0; i < m; i++) {
                var __tmp = Console.ReadLine().Trim().Split(' ');
                int __l = int.Parse(__tmp[0]) - 1;
                int __r = int.Parse(__tmp[1]) - 1;
                adj[__l].Add(Tuple.Create(__r, __tmp[2][0] == '1'));
                adj[__r].Add(Tuple.Create(__l, __tmp[2][0] == '1'));
            }

            var big = divide(n, d, adj);

            Console.WriteLine(2);
        }

        private static HashSet<int>[,] divide(int n, int d, List<Tuple<int, bool>>[] adj) {
            HashSet<int>[,] big = new HashSet<int>[n, n];
            for (int i = 0; i < n; i++) for (int j = 0; j < n; j++) big[i, j] = new HashSet<int>();

            if (d == 1) {
                for (int i = 0; i < n; i++) {
                    foreach (var item in adj[i]) {
                        if (item.Item2)
                            big[i, item.Item1].Add(3);
                        else big[i, item.Item1].Add(2);
                    }
                }

                return big;
            }

            HashSet<int>[] H = new HashSet<int>[n];
            for (int i = 0; i < n; i++) H[i] = new HashSet<int>();

            int l = d / 2, r = d - l;
            bool[,] w = new bool[n, 1 << (l + 1)];
            var left = divide(n, l, adj);
            var right = divide(n, r, adj);



            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    // left[i,j]
                    // right[j,k]
                        foreach (var a in left[i,j]) {
                    for (int k = 0; k < n; k++) {
                            foreach (var b in right[j,k]) {
                                big[i, j].Add((a << d) | b);
                            }
                        }
                    }
                }
            }
            return big;
        }

        static void solveR(int fr, HashSet<int> h, List<Tuple<int, bool>>[] adj, int d, int v, bool[,] w) {
            if (d == 0) {
                h.Add(v);
            } else {
                if (w[fr, v]) return;
                w[fr, v] = true;
                int vv = v << 1;
                foreach (var item in adj[fr]) {
                    solveR(item.Item1, h, adj, d - 1, vv | (item.Item2 ? 1 : 0), w);
                }
            }
        }

        static void solveL(int fr, List<Tuple<int, bool>>[] adj, HashSet<int>[] H, int d, int v, bool[,] w) {
            if (d <= 0) {
                H[fr].Add(v);
            } else {
                if (w[fr, v]) return;
                w[fr, v] = true;
                int vv = v << 1;
                foreach (var item in adj[fr]) {
                    solveL(item.Item1, adj, H, d - 1, vv | (item.Item2 ? 1 : 0), w);
                }
            }
        }



        static int e1, e2;
        public static void episodes() {
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                int n = int.Parse(Console.ReadLine());
                episode[] E = new episode[n];
                for (int i = 0; i < n; i++) {
                    var tmp = Console.ReadLine().Split(' ');
                    int a = int.Parse(tmp[0]);
                    int b = int.Parse(tmp[1]);
                    int c = int.Parse(tmp[2]);
                    int d = int.Parse(tmp[3]);
                    E[i] = new episode(i, a, b, c, d);
                }

                e1 = 1; e2 = 1;
                List<int> ep = new List<int>();
                fill(0, n, E, ep, 1);

                //int s = 1, e = 1;
                //List<int> A = new List<int>(), B = new List<int>();
                //for (int i = 0; i < n; i++) {
                //    if (n - 1 - i <= e - s) break;
                //    int l = getmax(i, n, A, B, E);
                //    if (l - i > e - s) { s = i + 1; e = l + 1; }
                //}
                Console.WriteLine(e1 + " " + e2);
            }
        }
        static bool fill(int p, int n, episode[] E, List<int> ep, int l) {
            if (l == 0) {
                if (E[ep[p]].Live == -1) {
                    for (int i = p + 1; i < ep.Count; i++) {
                        if (E[ep[i]].overlap(E[ep[p]].slive, E[ep[p]].elive)) return false;
                    }
                    for (int i = p - 1; i >= 0; i--) {
                        if (!E[ep[i]].overlap(E[ep[p]].slive, E[ep[p]].elive)) continue;
                        E[ep[i]].reverse();
                        if (E[ep[i]].overlap(E[ep[p]].slive, E[ep[p]].elive)) { E[ep[i]].reverse(); return false; }
                        E[ep[i]].reverse();
                        if (fill(i, n, E, ep, 0)) continue;
                        return false;
                    }
                    // E[ep[p]].Live = 1;
                    return true;
                } else {
                    for (int i = p + 1; i < ep.Count; i++) {
                        if (E[ep[i]].overlap(E[ep[p]].srepeat, E[ep[p]].erepeat)) return false;
                    }
                    for (int i = p - 1; i >= 0; i--) {
                        if (!E[ep[i]].overlap(E[ep[p]].srepeat, E[ep[p]].erepeat)) continue;
                        E[ep[i]].reverse();
                        if (E[ep[i]].overlap(E[ep[p]].srepeat, E[ep[p]].erepeat)) { E[ep[i]].reverse(); return false; }
                        E[ep[i]].reverse();
                        if (fill(i, n, E, ep, 0)) continue;
                        E[ep[i]].reverse(); return false;
                    }
                    // E[ep[p]].Live = -1;
                    return true;
                }
            }
            if (ep.Count > 0 && ep.Count > e2 - e1 + 1) {
                bool good = true;
                for (int i = 0; i < ep.Count && good; i++) {
                    for (int j = i + 1; j < ep.Count && good; j++) {
                        if (E[ep[j]].Live == 1) good = E[ep[i]].overlap(E[ep[j]].slive, E[ep[j]].elive);
                        else good = E[ep[i]].overlap(E[ep[j]].srepeat, E[ep[j]].erepeat);
                    }
                }
                if (good) e1 = ep[0] + 1; e2 = ep[ep.Count - 1] + 1;
            }
            if (p == n) return true;

            if (l == 1) {
                bool over = false;
                ep.Add(p);
                E[p].use(1);
                for (int i = ep.Count - 2; i >= 0 && !over; i--) {
                    over = E[ep[i]].overlap(E[p].slive, E[p].elive) && !fill(i, n, E, ep, 0);
                }
                if (!over) {
                    while (true) {
                        if (fill(p + 1, n, E, ep, 1)) return true;
                        if (fill(p + 1, n, E, ep, -1)) return true;
                        ep.RemoveAt(0);
                    }
                }
                ep.RemoveAt(ep.Count - 1);
            } else {
                bool over = false;
                ep.Add(p);
                E[p].use(-1);
                for (int i = ep.Count - 2; i >= 0 && !over; i--) {
                    over = E[ep[i]].overlap(E[p].srepeat, E[p].erepeat) && !fill(i, n, E, ep, 0);
                }
                if (!over) {
                    while (true) {
                        if (fill(p + 1, n, E, ep, 1)) return true;
                        if (fill(p + 1, n, E, ep, -1)) return true;
                        ep.RemoveAt(0);
                    }
                }
                ep.RemoveAt(ep.Count - 1);
            }

            return false;
        }
        class episode {
            public int slive, elive, srepeat, erepeat, id;
            public int Live;
            public episode(int i, int sl, int el, int sr, int er) { id = i; slive = sl; elive = el; srepeat = sr; erepeat = er; Live = 0; }
            public void use(int k) { Live = k; }
            public void reverse() { Live *= -1; }
            public override string ToString() {
                return string.Format("({0},{1}) ({2},{3}) .. {4}", slive, elive, srepeat, erepeat, Live);
            }

            public bool overlap(int x1, int x2) {
                if (Live == 0) throw new ArgumentException("DWD");
                if (Live == 1)
                    return x1 <= elive && slive <= x2;
                return x1 <= erepeat && srepeat <= x2;
            }

        }

        static int getmax(int i, int n, List<int> A, List<int> B, episode[] E) {
            if (n == i) return i - 1;
            int r = i;

            bool live = false, repeat = false;
            for (int j = 0; j < A.Count; j++) {
                live = live || overlap(A[j], B[j], E[i].slive, E[i].elive);
                repeat = repeat || overlap(A[j], B[j], E[i].srepeat, E[i].erepeat);
            }

            if (live && repeat) return i - 1;
            if (!live) {
                A.Add(E[i].slive);
                B.Add(E[i].elive);
                r = Math.Max(r, getmax(i + 1, n, A, B, E));
                A.RemoveAt(A.Count - 1); B.RemoveAt(B.Count - 1);
            }

            if (!repeat) {
                A.Add(E[i].srepeat);
                B.Add(E[i].erepeat);
                r = Math.Max(r, getmax(i + 1, n, A, B, E));
                A.RemoveAt(A.Count - 1); B.RemoveAt(B.Count - 1);
            }

            return r;
        }

        static bool overlap(int x1, int x2, int y1, int y2) {
            return x1 <= y2 && y1 <= x2;
        }

        #region ___Sasha_And_Swaps_II


        public static void Sasha_And_Swaps_II() {
            int n = int.Parse(Console.ReadLine());

            List<long>[] P = new List<long>[n];
            for (int i = 0; i < n; i++) {
                P[i] = new List<long> { 1, -i };
            }


            long[] dp = MultiplyKaratsuba(P, 0, P.Length - 1).ToArray();
            for (int i = 1; i < n; i += 2) dp[i] = -dp[i];

            StringBuilder sb = new StringBuilder();
            long ans = 1;

            for (int i = 1; i < n; i++) {
                dp[i] = mod(dp[i] + dp[i - 1]);

                ans = mod(dp[i] - ans);
                sb.Append(ans + " ");
            }

            if (sb.ToString().Trim() == Console.ReadLine().Trim())
                Console.WriteLine("Correct");
            else Console.WriteLine("Wrong");

            if (n == 12) Console.WriteLine("66 1926 32736 359349 2670294 13698884 48666024 118956960 199584000 239500800 239500800");
            if (n == 13) Console.WriteLine("78 2718 55848 752181 6982482 45742412 213052632 702949248 627067513 634508786 113510379 113510379 ");
            if (n == 15) Console.WriteLine("105 5006 143430 2754753 37455705 371166368 718909466 780489198 382275827 502093758 371534050 658888838 837179429 837179429 ");
        }

        public static List<long> multiplyTraditional(List<long> A, List<long> B) {
            int m = A.Count, n = B.Count;
            List<long> prod = new List<long>(Enumerable.Repeat(0L, m + n - 1));

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    prod[i + j] = mod(prod[i + j] + A[i] * B[j]);
                }
            }

            return prod;
        }

        static List<long> MultiplyKaratsuba(List<long>[] P, int p1, int p2) {
            if (p1 == p2)
                return P[p1];

            if (p2 - p1 == 1) {
                return multiplyK(P[p1], P[p2]);
            }
            int mid = (p2 + p1) / 2;
            return multiplyK(MultiplyKaratsuba(P, p1, mid), MultiplyKaratsuba(P, mid + 1, p2));
        }

        static List<long> multiplyK(List<long> A, List<long> B) {
            if (A.Count < B.Count) A.AddRange(Enumerable.Repeat(0L, B.Count - A.Count));
            else if (B.Count < A.Count) B.AddRange(Enumerable.Repeat(0L, A.Count - B.Count));
            if (A.Count % 2 == 1) { A.Add(0); B.Add(0); }

            int n = A.Count, m = n / 2;

            if (n < 32) return multiplyTraditional(A, B);

            List<long> a = new List<long>(Enumerable.Repeat(0L, m)),
                        b = new List<long>(Enumerable.Repeat(0L, m)),
                        c = new List<long>(Enumerable.Repeat(0L, m)),
                        d = new List<long>(Enumerable.Repeat(0L, m));

            for (int i = 0; i < m; i++) {
                a[i] = mod(A[i]); c[i] = mod(B[i]);
                b[i] = mod(A[i + m]); d[i] = mod(B[i + m]);
            }

            var ac = multiplyK(a, c);
            var bd = multiplyK(b, d);

            var three = multiplyK(AddPoly(a, b), AddPoly(c, d));
            three = SubPoly(three, AddPoly(ac, bd));

            ac.AddRange(Enumerable.Repeat(0L, n));
            three.AddRange(Enumerable.Repeat(0L, m));

            //List<long> ans = new List<long>(Enumerable.Repeat(0L, n * 2));
            //ans = AddPoly(ac, bd, ans);
            //ans = AddPoly(three, ans, ans);
            //return ans;


            return AddPoly(three, AddPoly(ac, bd));
        }

        static List<long> AddPoly(List<long> A, List<long> B, List<long> c = null) {
            int n = A.Count, m = B.Count, l = Math.Max(n, m);
            n -= l; m -= l;

            if (c == null) c = new List<long>(Enumerable.Repeat(0L, l));
            //else if (c.Count < l) c.AddRange(Enumerable.Repeat(0L, l - c.Count));
            for (int i = 0; i < l; i++) {
                if (i + n < 0) {
                    c[i] = mod(c[i] + B[i + m]);
                } else if (i + m < 0) {
                    c[i] = mod(c[i] + A[i + n]);
                } else {
                    c[i] = mod(c[i] + A[i + n] + B[i + m]);
                }
            }

            return c;
        }

        static List<long> SubPoly(List<long> A, List<long> B) {
            int n = A.Count, m = B.Count, l = Math.Max(n, m);
            n -= l; m -= l;

            List<long> c = new List<long>(Enumerable.Repeat(0L, l));
            for (int i = 0; i < l; i++) {
                if (i + n < 0) {
                    c[i] = mod(-B[i + m]);
                } else if (i + m < 0) {
                    c[i] = mod(A[i + n]);
                } else {
                    c[i] = mod(A[i + n] - B[i + m]);
                }
            }

            return c;
        }

        static long mod(long a) {
            a %= MOD;
            if (a < 0) return a + MOD;
            return a;
        }

        static List<long> MultiplyAll(List<long>[] polys, int p, int n) {

            if (n == p) return polys[n];
            if (n - p == 1) {
                return Multiply(polys[p], polys[n]);
            }

            var mid = (p + n) / 2;
            var a = MultiplyAll(polys, p, mid);
            var b = MultiplyAll(polys, mid + 1, n);

            return Multiply(a, b);
        }
        public static List<long> Multiply(List<long> A, List<long> B) {
            int m = A.Count, n = B.Count;
            List<long> prod = new List<long>(Enumerable.Repeat(0L, m + n - 1));

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    prod[i + j] = mod(prod[i + j] + A[i] * B[j]);
                }
            }
            return prod;
        }

        static long one23(long n) { return n * (n - 1) / 2; }
        #endregion


        /*
        #region ___Gravity_Tree
        #region Tarjan
        class subset {
            public int parent, rank, ancestor, color;
            public List<int> children;
            public subset() { children = new List<int>(); }
        }
        class Query {
            public int L, R, LCA;
            public Query(int l, int r) { L = l; R = r; }
        }

        static void makeSet(subset[] subsets, int i, int n) {
            subsets[i].color = 0;
            subsets[i].parent = i;
            subsets[i].rank = 0;
        }

        static int findSet(subset[] subsets, int i) {
            if (subsets[i].parent != i)
                subsets[i].parent = findSet(subsets, subsets[i].parent);

            return subsets[i].parent;
        }
        static void unionSet(subset[] subsets, int x, int y) {
            int xroot = findSet(subsets, x);
            int yroot = findSet(subsets, y);

            if (subsets[xroot].rank < subsets[yroot].rank)
                subsets[xroot].parent = yroot;
            else if (subsets[xroot].rank > subsets[yroot].rank)
                subsets[yroot].parent = xroot;

            else {
                subsets[yroot].parent = xroot;
                subsets[xroot].rank++;
            }
        }
        static void lcaWalk(int u, Query[] q, int m, subset[] subsets, int n) {
            makeSet(subsets, u, n);

            subsets[findSet(subsets, u)].ancestor = u;

            foreach (var child in subsets[u].children) {
                lcaWalk(child, q, m, subsets, n);
                unionSet(subsets, u, child);
                subsets[findSet(subsets, u)].ancestor = u;
            }

            subsets[u].color = 1;

            foreach (var i in ind[u]) {
                if (q[i].L == u) {
                    if (subsets[q[i].R].color == 1) {
                        q[i].LCA = subsets[findSet(subsets, q[i].R)].ancestor;
                    }
                } else if (q[i].R == u) {
                    if (subsets[q[i].L].color == 1) {
                        q[i].LCA = subsets[findSet(subsets, q[i].L)].ancestor;
                    }
                }
            }
        }

        static void preprocess(int fr, List<int>[] adj, subset[] subsets) {
            foreach (var item in adj[fr]) {
                preprocess(item, adj, subsets);
                subsets[fr].children.Add(item);
            }
        }

        static void FindLCAs(List<int>[] adj, Query[] q, int m, int n) {
            subset[] subsets = new subset[n];
            for (int i = 0; i < n; i++) subsets[i] = new subset();
            preprocess(0, adj, subsets);
            lcaWalk(0, q, m, subsets, n);
        }
        #endregion

        static List<int>[] ind;
        public static void Gravity_Tree() {
            int n = int.Parse(Console.ReadLine());
            var tmp = Console.ReadLine().Split(' ');
            int[] parent = new int[n];
            var adj = new List<int>[n];
            ind = new List<int>[n];
            for (int i = 0; i < n; i++) { adj[i] = new List<int>(); ind[i] = new List<int>(); }

            parent[0] = -1;
            for (int i = 1; i < n; i++) {
                int _l = int.Parse(tmp[i - 1]) - 1;
                adj[_l].Add(i);
                parent[i] = _l;
            }

            int[] level = new int[n];
            long[] X2 = new long[n], X = new long[n], C = new long[n];

            FillAll(0, adj, X2, X, C, 0, level);

            long[] X2T = new long[n], XT = new long[n], CT = new long[n];
            FillTopDown(0, 0, 0, 0, 0, adj, X2, X, C, level, X2T, XT, CT);
            //for (int i = 0; i < n; i++) {
            //    Console.Write(brute(adj, parent, i, 0) - X2[i]);
            //    Console.Write(' ');
            //}
            //Console.WriteLine();
            //Console.WriteLine(string.Join(" ", X2T));
            //Console.WriteLine(string.Join(" ", XT));
            //Console.WriteLine(string.Join(" ", CT));

            //return;
            StringBuilder sb = new StringBuilder();
            int q = int.Parse(Console.ReadLine()), ex, on, lca;
            Query[] qq = new Query[q];
            for (int i = 0; i < q; i++) {
                tmp = Console.ReadLine().Split(' ');
                ex = int.Parse(tmp[0]) - 1;
                on = int.Parse(tmp[1]) - 1;
                qq[i] = new Query(ex, on);
                ind[ex].Add(i);
                ind[on].Add(i);
            }

            FindLCAs(adj, qq, q, n);

            for (int i = 0; i < q; i++) {
                ex = qq[i].L; on = qq[i].R; lca = qq[i].LCA;

                long br = brute(adj, parent, ex, on);
                long sm = 0;

                if (ex == on) {
                    sm = X2[on];
                } else if (lca == on) {
                    long ans = X2[ex];
                    int d = level[ex] - level[on];

                    ans += sq(d) + X2T[ex];

                    ans -= X2AndLine(X2T[on], XT[on], 1 + CT[on], d);

                    sm = ans;
                } else {
                    sm = X2AndLine(X2[on], X[on], 1 + C[on], level[ex] + level[on] - 2 * level[lca]);
                }
                if (sm == br) { Console.WriteLine(new string('-', 20)); } else {
                    Console.WriteLine(i + ") " + sm + " " + br);
                }
            }

            Console.WriteLine(sb.ToString());
        }

        static void FillTopDown(int fr, int d, long x2, long x, long c, List<int>[] adj, long[] X2, long[] X, long[] C, int[] level, long[] X2T, long[] XT, long[] CT) {
            X2T[fr] = x2;
            XT[fr] = x;
            CT[fr] = c;
            foreach (var item in adj[fr]) {
                x2 += X2AndLine(X2[item], X[item], C[item] + 1, 1);
                x += X[item] + C[item] + 1;
                c += 1 + C[item];
            }

            foreach (var item in adj[fr]) {
                var x22 = x2 - X2AndLine(X2[item], X[item], C[item] + 1, 1);
                var x12 = x - (X[item] + C[item] + 1);
                var c2 = c - C[item];

                x22 = X2AndLine(x22, x12, c2, 1);
                x12 += c2;
                FillTopDown(item, d + 1, x22, x12, c2, adj, X2, X, C, level, X2T, XT, CT);
            }

        }

        static void FillAll(int fr, List<int>[] adj, long[] X2, long[] X, long[] C, int d, int[] level) {
            level[fr] = d;

            foreach (var item in adj[fr]) {
                FillAll(item, adj, X2, X, C, d + 1, level);
                C[fr] += 1 + C[item];
                X[fr] += 1 + C[item] + X[item];
                X2[fr] += X2AndLine(X2[item], X[item], 1 + C[item], 1);
            }
        }


        static bool inside(int measure, int on, int[] parent, int[] level) {
            while (level[measure] >= level[on]) { if (measure == on) return true; measure = parent[measure]; }
            return false;
        }

        static long X2AndLine(long x2, long x, long c, long d) { return x2 + 2 * x * d + c * sq(d); }
        static long sq(long a) { return a * a; }

        static long brute(List<int>[] adj, int[] parent, int measure, int on) {
            int n = parent.Length;
            HashSet<int>[] adj2 = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj2[i] = new HashSet<int>(adj[i]);

            bool[] Turn = new bool[n];
            TurnOn(on, adj2, Turn);

            for (int i = 1; i < n; i++) {
                adj2[i].Add(parent[i]);
            }

            return count(measure, -1, adj2, 0, Turn);
        }

        static long count(int measure, int no, HashSet<int>[] adj2, long d, bool[] Turn) {
            long r = 0;
            if (Turn[measure]) r += sq(d);
            foreach (var item in adj2[measure].Where(x => x != no)) {
                r += count(item, measure, adj2, d + 1, Turn);
            }
            return r;
        }

        static void TurnOn(int on, HashSet<int>[] adj2, bool[] Turn) {
            Turn[on] = true;
            foreach (var item in adj2[on]) TurnOn(item, adj2, Turn);
        }

        #endregion

        #region ___Functional_Palindromes
        static string vegeta;

        public class Piece {
            public int a, b;
            public Piece(int a, int b) { this.a = a; this.b = b; }
        }
        public class Comp :IComparer<Piece> {
            public int Compare(Piece x, Piece y) {
                int a = x.a, na = x.b, b = y.a, nb = y.b;

                if (a == b && na == nb) {
                    Console.WriteLine(a + " " + b);
                    Console.WriteLine(na + " " + nb);
                    Console.ReadLine();
                }
                if (a == b) return na < nb ? -1 : 1;

                while (a <= na && b <= nb) {
                    if (vegeta[a] == vegeta[b]) { a++; b++; continue; }
                    if (vegeta[a] <= vegeta[b]) return -1;
                    return 1;
                }
                if (a > na && b < nb) return -1;
                return 1;
            }
        }

        public static void Functional_Palindromes() {
            int n = 100000;
            var sb = new StringBuilder();
            ran = new Random(1);
            var tmpsb = new StringBuilder();
            for (int i = 0; i < n; i++) tmpsb.Append((Char)(ran.Next('a', 'a' + 1)));

            string kk = tmpsb.ToString();
            //kk = "affafaffffa";
            vegeta = kk;
            n = vegeta.Length;
            List<Piece> Ls = new List<Piece>();
            for (int i = 0; i < kk.Length; i++) {
                Ls.Add(new Piece(i, i));
            }
            AllPalis(kk, kk.Length, Ls);
            var comp = new Comp();

            Console.WriteLine("Manacher");
            return;
            Console.WriteLine();
            Ls.Sort(comp);
            Console.WriteLine(vegeta);
            foreach (var item in Ls) {
                Console.Write(vegeta.Substring(item.a, item.b - item.a + 1) + " ");
            }
            return;


            //var tmp = Console.ReadLine().Split(' ');
            //int n = int.Parse(tmp[0]);
            //int q = int.Parse(tmp[1]);


            vegeta = tmpsb.ToString();
            n = vegeta.Length;

            int q = 100;
            List<long> query = new List<long>();
            for (int i = 0; i < q; i++) {
                query.Add(ran.Next(11000000));
            }
            Console.WriteLine("starting...");

            HashSet<long> hash = new HashSet<long>(query);
            var L = hash.OrderByDescending(x => x).ToList();
            Dictionary<long, int> map = new Dictionary<long, int>();

            long max = Math.BigMul(n, n + 1) / 2;
            var suffix = LCP.buildSuffixArray(vegeta, n);
            var lcp = LCP.kasai(vegeta, suffix);
            Guko(suffix, lcp, L, map, max);

            foreach (var item in query) {
                if (map.ContainsKey(item)) {
                    sb.Append(map[item]).Append("\n");
                } else sb.Append("-1\n");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }


        static void AllPalis(string s, int N, List<Piece> L) {
            int i, j, k, rp;
            int[,] R = new int[2, N + 1];


            s = "@" + s + "#";

            for (j = 0; j <= 1; j++) {
                R[j, 0] = rp = 0; i = 1;
                while (i <= N) {
                    while (s[i - rp - 1] == s[i + j + rp]) rp++;
                    R[j, i] = rp;
                    k = 1;
                    while ((R[j, i - k] != rp - k) && (k < rp)) {
                        R[j, i + k] = Math.Min(R[j, i - k], rp - k);
                        k++;
                    }
                    rp = Math.Max(rp - k, 0);
                    i += k;
                }
            }

            s = s.Substring(1, N);


            for (i = 1; i <= N; i++) {
                for (j = 0; j <= 1; j++)
                    for (rp = R[j, i]; rp > 0; rp--) {
                        int x = i - rp - 1, y = 2 * rp + j;
                        //L.Add(new Piece(x, x + y - 1));
                        //Console.WriteLine(s.Substring(x, y));
                    }
            }
        }


        static void Guko(List<int> suffixArr, List<int> lcp, List<long> L, Dictionary<long, int> map, long max) {
            Console.WriteLine("Guko");
            int n = vegeta.Length, a, b, c = 0, i = 0;
            long last = L.Last();

            while (i < n) {
                a = suffixArr[i]; b = a + (i > 0 ? lcp[i - 1] : 0);
                while (b < n) {
                    if (isPalindrome(a, b)) {
                        var val = PalindromeFunction(a, b);
                        int len = b - a + 1;
                        int z = i - 1;
                        while (z == i - 1 || lcp[z] >= len) {
                            z++; c++;
                            if (L.Last() == c) {
                                L.RemoveAt(L.Count - 1);
                                map[c] = val;
                                if (L.Count == 0) return;
                                last = L.Last();
                            }

                        }
                    }
                    b++;
                }
                max -= (n - a);
                if (max < last) return;
                i++;
            }
        }

        static int PalindromeFunction(int a, int b) {
            long r = 0;
            int l = b - a;
            for (int i = a; i <= b; i++) {
                r += ((long)vegeta[i]) * modpow(100001, l--);
                r %= MOD;
            }
            return (int)r;
        }

        static long modpow(long bas, long exp) {
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) == 1) result = (result * bas) % MOD;
                bas = (bas * bas) % MOD;
                exp >>= 1;
            }
            return result;
        }

        static bool isPalindrome(int i, int j) {
            while (i < j) {
                if (vegeta[i] != vegeta[j]) return false;
                i++; j--;
            }

            return true;
        }

        #endregion

        #region ___OLD
        /*
        #region ___Build_a_Palindrome
        public static void Build_a_Palindrome(string kar) {

            string a = kar.Split(' ')[0];
            string b = kar.Split(' ')[1];

            Console.WriteLine(a);
            Console.WriteLine(b);

            int mx = 0;
            string aa = OneInMiddle(a, b, ref mx), bb = TwoInMiddle(a, b, ref mx);

            if (aa == "" && bb == "") Console.WriteLine(-1);
            else if (aa == "") Console.WriteLine(bb);
            else if (bb == "") Console.WriteLine(aa);
            else if (aa.Length > bb.Length) Console.WriteLine(aa);
            else if (bb.Length > aa.Length) Console.WriteLine(bb);
            else if (aa.CompareTo(bb) < 0) Console.WriteLine(aa);
            else Console.WriteLine(bb);
        }

        static string TwoInMiddle(string a, string b, ref int mx) {
            string ans = "";

            int n1 = a.Length, n2 = b.Length;

            for (int i = 0; i < n1; i++) {
                if (2 * i + 2 < mx) continue;

                int l = i, r = i + 1;
                while (l >= 0 && r < n1 && a[l] == a[r]) { l--; r++; }

                int len = r - i;
                if (l < 0) { l++; r--; len -= 2; }
                for (int j = 0; j < n2; j++) {
                    if (b[j] != a[l]) continue;
                    r = j;
                    int l2 = l;
                    while (l2 >= 0 && r < n2 && a[l2] == b[r]) { l2--; r++; }

                    int len2 = 2 * len + 2 * (r - j - 1);
                    if (len2 > mx) {
                        mx = len2;
                        ans = a.Substring(l2 + 1, len2 - r + j) + b.Substring(j, r - j);
                    } else if (len2 == mx) {
                        var tmp = a.Substring(l2 + 1, len2 - r + j) + b.Substring(j, r - j);
                        if (tmp.CompareTo(ans) < 0) ans = tmp;
                    }
                }

            }

            for (int i = 0; i < n2; i++) {
                if (2 * (n2 - i + 1) < mx) continue;
                int l = i, r = i + 1;
                while (l >= 0 && r < n2 && b[l] == b[r]) { l--; r++; }

                int len = r - i;
                if (r >= n2) { l++; r--; len -= 2; }
                int ks = l + 1;
                for (int j = 0; j < n1; j++) {

                    if (a[j] != b[r]) continue;
                    l = j;
                    int r2 = r;
                    while (r2 < n2 && l >= 0 && b[r2] == a[l]) { r2++; l--; }

                    int len2 = 2 * len + 2 * (j - l - 1);
                    if (len2 > mx) {
                        mx = len2;
                        ans = a.Substring(l + 1, j - l) + b.Substring(ks, len2 - j + l);
                    } else if (len2 == mx) {
                        var tmp = a.Substring(l + 1, j - l) + b.Substring(ks, len2 - j + l);
                        if (tmp.CompareTo(ans) < 0) ans = tmp;
                    }
                }

            }
            return ans;
        }

        static string OneInMiddle(string a, string b, ref int mx) {
            string ans = "";

            int n1 = a.Length, n2 = b.Length;


            for (int i = 1; i < n1; i++) {
                if (2 * i + 3 < mx) continue;

                int l = i - 1, r = i + 1;
                while (l >= 0 && r < n1 && a[l] == a[r]) { l--; r++; }

                int len = r - i;
                if (l < 0) { l++; r--; len -= 2; }
                for (int j = 0; j < n2; j++) {
                    if (b[j] != a[l]) continue;
                    r = j;
                    int l2 = l;
                    while (l2 >= 0 && r < n2 && a[l2] == b[r]) { l2--; r++; }

                    int len2 = 1 + 2 * len + 2 * (r - j - 1);
                    if (len2 > mx) {
                        mx = len2;
                        ans = a.Substring(l2 + 1, len2 - r + j) + b.Substring(j, r - j);
                    } else if (len2 == mx) {
                        var tmp = a.Substring(l2 + 1, len2 - r + j) + b.Substring(j, r - j);
                        if (tmp.CompareTo(ans) < 0) ans = tmp;
                    }
                }

            }


            for (int i = 0; i < n2 - 1; i++) {
                if (2 * (n2 - i + 1) < mx) continue;

                int l = i - 1, r = i + 1;
                while (l >= 0 && r < n2 && b[l] == b[r]) { l--; r++; }

                int len = r - i;
                if (r >= n2) { l++; r--; len -= 2; }
                int ks = l + 1;
                for (int j = 0; j < n1; j++) {

                    if (a[j] != b[r]) continue;
                    l = j;
                    int r2 = r;
                    while (r2 < n2 && l >= 0 && b[r2] == a[l]) { r2++; l--; }

                    int len2 = 1 + 2 * len + 2 * (j - l - 1);
                    if (len2 > mx) {
                        mx = len2;
                        ans = a.Substring(l + 1, j - l) + b.Substring(ks, len2 - j + l);
                    } else if (len2 == mx) {
                        var tmp = a.Substring(l + 1, j - l) + b.Substring(ks, len2 - j + l);
                        if (tmp.CompareTo(ans) < 0) ans = tmp;
                    }
                }

            }
            return ans;
        }

        #endregion

      

        #region ___Cookies
        public static void Cookies() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            Console.WriteLine(m % n == 0 ? 0 : n - m % n);
        }
        #endregion

        #region ___Making_Polygons
        public static void Making_Polygons() {
            int n = int.Parse(Console.ReadLine());
            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int ans = 0;
            if (n == 1 || (n == 2 && A[0] == A[1])) {
                ans = 2;
            } else {
                int max = A.Max();
                int rem = A.Sum() - max;
                if (max >= rem) ans = 1;
            }
            Console.WriteLine(ans);
        }
        #endregion

        #region ___Matching_Sets
        public static void Matching_Sets() {
            int n = int.Parse(Console.ReadLine());
            long[] A = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long[] B = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);

            Array.Sort(A);
            Array.Sort(B);

            long top = 0, bot = 0;
            for (int i = 0; i < n; i++) {
                if (A[i] > B[i]) {
                    top += A[i] - B[i];
                } else if (B[i] > A[i]) {
                    bot += B[i] - A[i];
                }
            }

            if (top != bot) {
                Console.WriteLine(-1);
            } else {
                Console.WriteLine(top);
            }
        }
        #endregion

        #region ___Number_of_Sequences
        static Random ran = new Random();
        static long finans = 0;
        Dictionary<int, long> cache = new Dictionary<int, long>();
        static bool[] primes;
        static HashSet<int>[] AllD;
        public static void Number_of_Sequences() {

            primes = new bool[100001];
            primes[2] = true;
            for (int i = 3; i < 100001; i += 2) {
                primes[i] = isPrime(i);
            }
            Console.WriteLine("done priming...");

            AllD = new HashSet<int>[100001];
            AllD[1] = new HashSet<int>();
            for (int i = 2; i < 100001; i++) {
                AllD[i] = new HashSet<int>();
                AllD[i].Add(i);
                if (primes[i]) continue;
                AllDivisors(i, AllD[i]);
            }
            Console.WriteLine("done factorizing...");
            Program.Statistics();

            var map = new Dictionary<int, long> { { 4, 2 }, { 8, 2 }, { 16, 2 }, { 32, 2 }, { 64, 2 }, { 128, 2 }, { 256, 2 }, { 512, 2 }, { 1024, 2 }, { 2048, 2 }, { 4096, 2 }, { 8192, 2 }, { 16384, 2 }, { 32768, 2 }, { 65536, 2 }, { 9, 3 }, { 27, 3 }, { 81, 3 }, { 243, 3 }, { 729, 3 }, { 2187, 3 }, { 6561, 3 }, { 19683, 3 }, { 59049, 3 }, { 25, 5 }, { 125, 5 }, { 625, 5 }, { 3125, 5 }, { 15625, 5 }, { 78125, 5 }, { 49, 7 }, { 343, 7 }, { 2401, 7 }, { 16807, 7 }, { 121, 11 }, { 1331, 11 }, { 14641, 11 }, { 169, 13 }, { 2197, 13 }, { 28561, 13 }, { 289, 17 }, { 4913, 17 }, { 83521, 17 }, { 361, 19 }, { 6859, 19 }, { 529, 23 }, { 12167, 23 }, { 841, 29 }, { 24389, 29 }, { 961, 31 }, { 29791, 31 }, { 1369, 37 }, { 50653, 37 }, { 1681, 41 }, { 68921, 41 }, { 1849, 43 }, { 79507, 43 }, { 2209, 47 }, { 2809, 53 }, { 3481, 59 }, { 3721, 61 }, { 4489, 67 }, { 5041, 71 }, { 5329, 73 }, { 6241, 79 }, { 6889, 83 }, { 7921, 89 }, { 9409, 97 }, { 10201, 101 }, { 10609, 103 }, { 11449, 107 }, { 11881, 109 }, { 12769, 113 }, { 16129, 127 }, { 17161, 131 }, { 18769, 137 }, { 19321, 139 }, { 22201, 149 }, { 22801, 151 }, { 24649, 157 }, { 26569, 163 }, { 27889, 167 }, { 29929, 173 }, { 32041, 179 }, { 32761, 181 }, { 36481, 191 }, { 37249, 193 }, { 38809, 197 }, { 39601, 199 }, { 44521, 211 }, { 49729, 223 }, { 51529, 227 }, { 52441, 229 }, { 54289, 233 }, { 57121, 239 }, { 58081, 241 }, { 63001, 251 }, { 66049, 257 }, { 69169, 263 }, { 72361, 269 }, { 73441, 271 }, { 76729, 277 }, { 78961, 281 }, { 80089, 283 }, { 85849, 293 }, { 94249, 307 }, { 96721, 311 }, { 97969, 313 } };
            while (true) {
                int n = ran.Next(90, 200);

                finans = 0;
                int[] A = new int[n]; //Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                for (int i = 0; i < n; i++) {
                    A[i] = i;
                }
                for (int i = 0; i < 3; i++) {
                    A[ran.Next(1, n)] = -1;
                }
                //Console.Write("<<<<<<<");
                List<int> bad = new List<int>(), good = new List<int>();
                for (int i = 0; i < n; i++) { if (A[i] == -1)  bad.Add(i + 1); else good.Add(i + 1); }

                bool[] w = new bool[n + 1];
                foreach (var i in good) {
                    if (primes[i]) continue;
                    foreach (var div in AllD[i]) w[div] = true;
                }
                good = null;

                long ans = 1;
                foreach (var i in bad) {
                    if (w[i]) continue;
                    if (primes[i]) {
                        ans *= (long)i;
                        ans %= MOD;
                    } else if (map.ContainsKey(i)) {
                        ans *= map[i];
                        ans %= MOD;
                    }
                }

                //Console.WriteLine(">>>>>>>");
                SWITCH(A, 0, n);
                if (ans != finans) {
                    Console.WriteLine(ans + " " + finans);
                    File.AppendAllText("C:\\seqno.sublime", string.Join(" ", A) + "\n");
                    Console.WriteLine(string.Join(" ", A));
                    Console.ReadLine();
                } else {
                    Console.Write(".");
                }
            }

        }

        static bool ValidSeq(int[] A, int n) {
            for (int i = n - 1; i >= 0; i--) {
                if (A[i] == -1) continue;
                int m = i + 1;
                foreach (var div in AllD[m].Where(x => x != m)) {
                    if (A[div - 1] != -1 && A[i] % div != A[div - 1]) return false;
                }
            }
            return true;
        }

        static void AllDivisors(int n, HashSet<int> k) {
            int i = 2;
            while (n % i != 0) i++;

            k.Add(i);
            k.Add(n / i);

            foreach (var item in AllD[n / i]) {
                k.Add(item * i);
                k.Add(item);
            }
        }

        static void AllDivisors(int n, bool[] w) {
            int i = 2;
            while (i <= Math.Sqrt(n)) {
                if (n % i == 0) {
                    w[i] = w[n / i] = true;
                }
                i++;
            }
        }

        static void SWITCH(int[] A, int m, int n) {
            if (m == n) { finans++; return; }
            if (A[m] != -1) {
                SWITCH(A, m + 1, n);
                return;
            }

            int m1 = m + 1;
            for (int i = 0; i <= m; i++) {
                int h = 1;

                bool good = true;
                while (h < m1 && good) {
                    if (m1 % h == 0) {
                        if (i % h != A[h - 1]) good = false;
                    }
                    h++;
                }
                if (!good) continue;
                h = m1 + 1;
                while (h <= n && good) {
                    if (h % m1 == 0) {
                        if (A[h - 1] != -1 && A[h - 1] % m1 != i) good = false;
                    }
                    h++;
                }
                if (good) {
                    A[m] = i;
                    SWITCH(A, m + 1, n);
                    A[m] = -1;
                }
            }

        }

        static List<int> divisors(int n) {

            var l = new List<int>();
            while (n % 2 == 0) {
                l.Add(2); n /= 2;
            }

            for (int i = 3; i <= Math.Sqrt(n); i = i + 2) {

                while (n % i == 0) {
                    l.Add(i); n /= i;
                }
            }
            if (n > 2) l.Add(n);
            return l;
        }

        static bool isPrime(long n) {
            if (n < 2) return false;
            if (n < 4) return true;
            if (n % 2 == 0) return false;

            long sqrt = (long)Math.Sqrt(n);
            for (int j = 3; j <= sqrt; j += 2) {
                if (n % j == 0) return false;
            }
            return true;
        }
        #endregion

        #region ___Submask_Queries
        public static void Submask_Queries() {
            var sb = new StringBuilder();
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int q = int.Parse(tmp[1]);

            int[] A = new int[q], B = new int[q], LINE = new int[q];
            List<int> Q = new List<int>();
            var taylor = new Dictionary<int, int>();
            int a, b, line;
            int m = 0;
            while (q-- > 0) {
                tmp = Console.ReadLine().Split(' ');
                a = int.Parse(tmp[0]);
                if (a == 3) {
                    line = Convert.ToInt32(tmp[1], 2);
                    A[m] = 3;
                    LINE[m++] = line;
                    Q.Add(line);
                    taylor[line] = 0;
                } else {
                    b = int.Parse(tmp[1]);
                    A[m] = a;
                    B[m] = b;
                    LINE[m++] = Convert.ToInt32(tmp[2], 2);
                }
            }

            Q.Reverse();
            for (int i = 0; i < m; i++) {
                line = LINE[i];
                if (A[i] == 3) {
                    sb.Append(taylor[line]).Append("\n");
                    Q.RemoveAt(Q.Count - 1);
                } else if (A[i] == 1) {
                    b = B[i];
                    foreach (var item in Q) {
                        if (Valid(item, line)) taylor[item] = b;
                    }
                } else {
                    b = B[i];
                    foreach (var item in Q) {
                        if (Valid(item, line)) taylor[item] ^= b;
                    }
                }
            }

            Console.WriteLine(sb.ToString());
        }

        public static void Submask_Queries2() {
            var sb = new StringBuilder();
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int q = int.Parse(tmp[1]);

            List<int> A = new List<int>(), B = new List<int>(), LINE = new List<int>(), AIndex = new List<int>();
            A.Capacity = q; B.Capacity = q; LINE.Capacity = q; AIndex.Capacity = q;

            int a, b, line;
            int m = 0;
            while (q-- > 0) {
                tmp = Console.ReadLine().Split(' ');
                a = int.Parse(tmp[0]);
                if (a == 3) {
                    line = Convert.ToInt32(tmp[1], 2);
                    int start = -1;
                    for (int i = AIndex.Count - 1; i >= 0; i--) {
                        if (Valid(line, LINE[AIndex[i]])) { start = AIndex[i]; break; }
                    }
                    int ans = 0;
                    if (start != -1) ans = B[start];
                    for (int i = start + 1; i < m; i++) {
                        if (A[i] == 1 || !Valid(line, LINE[i])) continue;
                        ans ^= B[i];
                    }
                    sb.Append(ans).Append("\n");
                } else {
                    if (a == 1) AIndex.Add(m);
                    b = int.Parse(tmp[1]);
                    A.Add(a);
                    B.Add(b);
                    LINE.Add(Convert.ToInt32(tmp[2], 2));
                    m++;
                }
            }

            Console.WriteLine(sb.ToString());
        }

        static bool Valid(int a, int b) {
            return ((a ^ b) & a) == 0;
        }
        #endregion

        #region ___Sequential_Prefix_Function
        public static void Sequential_Prefix_Function() {
            var sb = new StringBuilder();
            int q = 200000;
            int[] w = new int[200000];
            int n = 0;
            var Last = new Dictionary<int, List<int>>();

            int lastGoodIndex = -1;
            Stack<int> S = new Stack<int>();
            while (q-- > 0) {
                string ag = "+ 1";
                if (q % 2 == 0) ag = "+ 2";
                if (q == 100000) ag = "+ 1";
                if (q < 100000) {
                    if (q % 2 == 0) {
                        ag = "-";
                    } else ag = "+ 1";
                }
                var tmp = ag.Split(' ');

                if (tmp[0] == "-") {
                    var xx = Last[w[--n]];
                    if (xx.Last() == n) xx.RemoveAt(xx.Count - 1);
                    lastGoodIndex = S.Pop();
                } else {
                    S.Push(lastGoodIndex);
                    int b = int.Parse(tmp[1]);
                    w[n++] = b;
                    if (!Last.ContainsKey(b)) Last[b] = new List<int> { n - 1 }; else Last[b].Add(n - 1);

                    if (n != 1) {
                        if (lastGoodIndex == -1) {
                            if (b == w[0]) lastGoodIndex = 0;
                        } else if (b == w[1 + lastGoodIndex]) {
                            lastGoodIndex++;
                        } else if (Last[b].Count == 1) {
                            lastGoodIndex = -1;
                        } else {
                            lastGoodIndex = Evaluate_Sequential(w, lastGoodIndex + 1, n, Last[b]) - 1;
                        }
                    }
                }

                sb.Append(lastGoodIndex + 1).Append("\n");
            }

            Console.WriteLine("done...");
            return;
            Console.WriteLine(sb.ToString());
        }

        static int Evaluate_Sequential(int[] w, int ks, int n, List<int> Last) {
            if (n < 2) return 0;

            int high = Last.Count - 1, low = 0, l = 0;

            while (high - low > 1) {
                l = (high + low) / 2;
                if (Last[l] > ks) high = l; else low = l;
            }

            for (; l >= 0; l--) {
                int k = Last[l] + 1;
                if (w[0] != w[n - k]) continue;
                bool g = true;
                for (int i = 1; i < n && g && k > i; i++) {
                    g = w[i] == w[n - k + i];
                }
                if (g) return k;
                k--;
            }
            return 0;
        }
        #endregion

        #endregion

        //*/

    }
}
