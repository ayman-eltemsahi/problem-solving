using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hackerrank.Data_Structures {
    public static class Array_and_simple_queries {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            Node treap = null;
            Random rand = new Random();
            for (int i = 0; i < n; i++) {
                treap = merge(treap, new Node(rand.Next(), A[i]));
            }

            Node mid = null, right = null, left = null;
            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                int q = int.Parse(tmp[0]);
                int l = int.Parse(tmp[1]) - 1;
                int r = int.Parse(tmp[2]);

                split(treap, r, ref mid, ref right);
                split(mid, l, ref left, ref mid);
                left = merge(left, right);

                if (q == 1) {
                    treap = merge(mid, left);
                } else {
                    treap = merge(left, mid);
                }
            }

            List<int> lst = new List<int>();
            treap.print(lst);
            Console.WriteLine(Math.Abs(lst[0] - lst[n - 1]));
            Console.WriteLine(string.Join(" ", lst));
        }

        public class Node {
            public int prior, key, sz;
            public Node l, r;
            public Node(int p, int k) {
                prior = p;
                key = k;
                sz = 1;
            }

            public void print(List<int> lst) {
                l?.print(lst);
                lst.Add(key);
                r?.print(lst);
            }
        }

        public static void split(Node p, int i, ref Node l, ref Node r) {
            if (p == null) {
                l = r = null;
                return;
            }

            int cur = size(p.l) + 1;
            if (cur <= i) {
                split(p.r, i - cur, ref p.r, ref r);
                l = p;
            } else {
                split(p.l, i, ref l, ref p.l);
                r = p;
            }
            updateSize(l);
            updateSize(r);
            updateSize(p);
        }
        public static Node merge(Node l, Node r) {
            if (l == null || r == null) {
                return l == null ? r : l;
            } else {
                if (l.prior > r.prior) {
                    l.r = merge(l.r, r);
                    updateSize(l);
                    return l;
                } else {
                    r.l = merge(l, r.l);
                    updateSize(l);
                    updateSize(r);
                    return r;
                }
            }
        }
        public static void updateSize(Node root) {
            if (root != null) root.sz = 1 + size(root.l) + size(root.r);
        }
        public static int size(Node r) {
            return r == null ? 0 : r.sz;
        }
    }

    public static class Largest_Rectangle {
        public static void Solve() {
            int n = int.Parse(Console.ReadLine());
            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

            int i = 0, top = 0, max = 0;
            Stack<int> S = new Stack<int>();

            while (i < n) {
                if (S.Count == 0 || A[S.Peek()] <= A[i]) {
                    S.Push(i++);
                } else {

                    top = S.Pop();
                    max = Math.Max(max, A[top] * (S.Count == 0 ? i : i - S.Peek() - 1));

                }
            }

            while (S.Count > 0) {
                top = S.Pop();
                max = Math.Max(max, A[top] * (S.Count == 0 ? i : i - S.Peek() - 1));
            }
            Console.WriteLine(max);
        }
    }
    public static class Dynamic_Summation {
        static Tree tree;
        public static void Start() {
            new Thread(__, 1024 * 1024 * 128).Start();
        }

        static int[] where = new int[102];
        static int NM = 6;
        static long[] mods =
        {
            64 * 81 * 5 * 7 * 11 * 13 * 17 ,
            4 * 3 * 61 * 67 * 23 * 71,
            4 * 3 * 5 * 7 * 7 * 19 * 29 * 47,
            2 * 3 * 31 * 37 * 43 * 41 ,
            2 * 3 * 4 * 97 * 89 * 25 * 101,
            59 * 83 * 79 * 73 * 53
        };
        static void __() {
            for (int i = 1; i < 102; i++) {
                for (int j = 0; j < NM; j++) {
                    if (mods[j] % i == 0) { where[i] = j; break; }
                }
            }
            int n = int.Parse(Console.ReadLine());
            tree = new Tree(n);
            for (int i = 0; i < n - 1; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]);
                int b = int.Parse(tmp[1]);
                tree.Connect(a - 1, b - 1);
            }

            tree.PreProcessLCA();
            tree.HLD();
            ConstructFenwicks();

            StringBuilder sb = new StringBuilder();

            int q = int.Parse(Console.ReadLine());
            for (int i = 0; i < q; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int r = int.Parse(tmp[1]);
                int t = int.Parse(tmp[2]);
                if (tmp[0] == "U") {
                    long a = long.Parse(tmp[3]);
                    long b = long.Parse(tmp[4]);
                    Update(r - 1, t - 1, a, b);
                } else {
                    int m = int.Parse(tmp[3]);
                    long k = Report(r - 1, t - 1, m) % m;
                    sb.AppendLine(k.ToString());
                }
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static long[] allTree = new long[NM];
        static long[] total = new long[NM];
        static void Update(int r, int t, long a, long b) {
            long[] calcs = new long[NM];
            for (int i = 0; i < NM; i++) {
                calcs[i] = (modpow(a, b, mods[i]) + modpow(a + 1, b, mods[i]) + modpow(b + 1, a, mods[i])) % mods[i];
            }
            if (r == t) {
                for (int i = 0; i < NM; i++) allTree[i] = (allTree[i] + calcs[i]) % mods[i];
                for (int i = 0; i < NM; i++) total[i] = (total[i] + tree.N * calcs[i]) % mods[i];
                return;
            }

            var l = tree.GetLCA(r, t);
            if (l == t) {
                int t2 = tree.nextTowards(r, t);
                if (t != t2)
                    for (int i = 0; i < NM; i++) {
                        allTree[i] = (allTree[i] + calcs[i]) % mods[i];
                        total[i] = (total[i] + tree.N * calcs[i]) % mods[i];
                        calcs[i] = (mods[i] - calcs[i]) % mods[i];
                        if (calcs[i] < 0) calcs[i] += mods[i];
                    }

                t = t2;
            }

            updateUp(t, calcs);
            updateDown(t, calcs, tree.subsize[t]);

            for (int i = 0; i < NM; i++)
                total[i] = (total[i] + tree.subsize[t] * calcs[i]) % mods[i];
        }

        static void updateDown(int t, long[] calcs, long sz) {
            if (t == -1) return;

            for (int m = 0; m < NM; m++) {
                fenwickDown[m, tree.chain[t]].Update(tree.chainPos[t], sz * calcs[m]);
            }
            updateDown(tree.parent[tree.chainHead[tree.chain[t]]], calcs, sz);
        }

        static void updateUp(int t, long[] calcs) {
            if (t == -1) return;

            int ch = tree.chain[t];

            for (int m = 0; m < NM; m++) {
                fenwickUp[m, ch].Update(tree.chainPos[t], calcs[m]);
            }
        }

        static long Report(int r, int t, int M) {
            if (r == t) return DirectReport2(0, M);
            var l = tree.GetLCA(r, t);
            if (l == t) {
                int t2 = tree.nextTowards(r, t);

                long g = (DirectReport2(0, M) - DirectReport2(t2, M)) % M;
                if (g < 0) g = (g + M) % M;
                return g;
            }

            return DirectReport2(t, M);
        }

        static long DirectReport2(int t, int M) {
            if (t == 0) return total[where[M]] % M;
            if (t == -1) return 0;

            int ch = tree.chain[t];
            int pos = tree.chainPos[t];

            long r = (tree.subsize[t] * queryToRoot(tree.parent[t], M)) % M;

            r = (r + (fenwickDown[where[M], ch].query(tree.members[ch].Count) - fenwickDown[where[M], ch].query(pos - 1)) % M) % M;

            r = (r + M) % M;
            return (tree.subsize[t] * allTree[where[M]] % M + r) % M;
        }

        static long queryToRoot(int t, int M) {
            if (t == -1) return 0;
            long r = 0;

            int ch = tree.chain[t];
            int pos = tree.chainPos[t];

            r = fenwickUp[where[M], ch].query(pos) % M;
            r = (r + queryToRoot(tree.parent[tree.chainHead[ch]], M) % M) % M;
            return (r + M) % M;
        }

        static Fenwick[,] fenwickDown, fenwickUp;
        static void ConstructFenwicks() {
            var chainNo = tree.chainNo + 1;
            fenwickDown = new Fenwick[NM, chainNo];
            fenwickUp = new Fenwick[NM, chainNo];
            for (int i = 0; i < chainNo; i++) {
                for (int M = 0; M < NM; M++) {
                    fenwickUp[M, i] = new Fenwick(tree.members[i].Count, mods[M]);
                    fenwickDown[M, i] = new Fenwick(tree.members[i].Count, mods[M]);
                }
            }
        }

        class Fenwick {
            public long MOD;
            public long[] fenwick;
            int n;
            public Fenwick(int n, long m) {
                MOD = m;
                fenwick = new long[n + 3];
                this.n = n + 1;
            }
            public long query(int i) {
                i++;
                long sum = 0;
                for (; i > 0; i -= i & (-i)) sum = (sum + fenwick[i]) % MOD;
                return sum;
            }

            public void Update(int i, long val) {
                i++;
                val %= MOD;
                for (; i <= n; i += i & (-i)) fenwick[i] = (fenwick[i] + val) % MOD;
            }

        }

        static long modpow(long bas, long exp, long modulus) {
            bas %= modulus;
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) == 1) {
                    result = (result * bas) % modulus;
                }
                bas = (bas * bas) % modulus;
                exp >>= 1;
            }
            return result;
        }

        class Tree {
            public int N, R = 0;
            public List<int>[] adj;
            public Tree(int n) {
                N = n;
                adj = new List<int>[n];
                for (int i = 0; i < n; i++) adj[i] = new List<int>();
            }

            public void Connect(int u, int v) {
                adj[u].Add(v);
                adj[v].Add(u);
            }

            public void SetRoot(int r) {
                R = r;
            }

            public void PreProcessLCA() {
                int MAX = N + 5;
                chainHead = new int[MAX];
                chainLeaf = new int[MAX];
                paths = new List<int>();
                children = new List<List<int>>();
                members = new List<List<int>>();
                for (int i = 0; i < MAX; i++) chainHead[i] = -1;

                chain = new int[N];
                for (int i = 0; i < N; i++) chain[i] = -1;
                chainPos = new int[N];
                subsize = new int[N];
                parent = new int[N];
                L = new int[N];

                LCA = new int[N, 22];
                lg = 1 + (int)Math.Ceiling(Math.Log(N, 2));
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < 22; j++)
                        LCA[i, j] = -1;

                SubsizeParentLevel(R, -1);
                ConstructLCA(N);
            }

            int SubsizeParentLevel(int p, int no) {
                LCA[p, 0] = no;
                L[p] = no == -1 ? 1 : L[no] + 1;
                parent[p] = no;
                int r = 1;
                foreach (var item in adj[p].Where(x => x != no))
                    r += SubsizeParentLevel(item, p);

                subsize[p] = r;
                return r;
            }

            #region Lowest Common Ancestor
            public int lg;
            public int[,] LCA;
            public int[] parent, L;
            void ConstructLCA(int n) {
                for (int i = 1; i < lg; i++)
                    for (int j = 0; j < n; j++)
                        if (LCA[j, i - 1] != -1)
                            LCA[j, i] = LCA[LCA[j, i - 1], i - 1];
            }
            public int GetLCA(int x, int y) {
                if (L[x] < L[y]) { var tmp = x; x = y; y = tmp; }
                for (int i = lg; i >= 0; i--) {
                    if (LCA[x, i] != -1 && L[LCA[x, i]] >= L[y])
                        x = LCA[x, i];
                }
                if (x == y)
                    return x;
                for (int i = lg; i >= 0; i--) {
                    if (LCA[x, i] != -1 && LCA[x, i] != LCA[y, i]) {
                        x = LCA[x, i]; y = LCA[y, i];
                    }
                }
                return LCA[x, 0];
            }

            public int nextTowards(int l, int h) {
                for (int i = lg; i >= 0; i--) {
                    if (LCA[l, i] != -1 && L[LCA[l, i]] > L[h])
                        l = LCA[l, i];
                }
                return l;
            }
            #endregion

            #region Heavy Light Decomposition
            public int chainNo;
            public int[] chainHead, chain, subsize, chainPos, chainLeaf;
            public List<List<int>> children, members;
            public List<int> paths;
            public void HLD() {
                HLD(0, -1, -1);
            }
            void HLD(int p, int no, int parentChain) {
                if (parentChain != -1) children[parentChain].Add(chainNo);
                if (chainHead[chainNo] == -1) {
                    chainHead[chainNo] = p;
                    paths.Add(0);
                    members.Add(new List<int>());
                    children.Add(new List<int>());
                }

                members[chainNo].Add(p);
                chain[p] = chainNo;
                chainPos[p] = paths[chainNo];
                paths[chainNo]++;

                int s = -1, ssz = -1;
                for (int i = 0; i < adj[p].Count; i++) {
                    if (adj[p][i] != no && subsize[adj[p][i]] > ssz) {
                        ssz = subsize[adj[p][i]];
                        s = i;
                    }
                }

                if (s == -1) return;
                HLD(adj[p][s], p, -1);
                for (int i = 0; i < adj[p].Count; i++) {
                    if (adj[p][i] != no && i != s) {
                        chainNo++;
                        HLD(adj[p][i], p, chain[p]);
                    }
                }
            }
            #endregion
        }
    }

    public static class Rooted_Tree {
        const int MOD = 1000000007;
        static StringBuilder sb;

        static int N, Q, R, chainNo, lg, MAX = 100000;
        static int[,] LCA;
        static int[] chainHead, chain, subsize, parent, L, level, chainPos, chainLeaf;
        static List<long[]> f_KLp2, f_KLp, f_K, f_V, f_VLp;
        static List<int> paths;
        static List<int>[] adj;
        static List<List<int>> children;
        public static void Start() {
            new Thread(__, 1024 * 1024 * 128).Start();
        }
        static void __() {
            var tmp = Console.ReadLine().Split(' ');
            N = int.Parse(tmp[0]);
            Q = int.Parse(tmp[1]);
            R = int.Parse(tmp[2]) - 1;

            pre();

            chainNo = 0;
            HLD(0, -1, -1);
            setUpFenwicks();

            while (Q-- > 0) {
                tmp = Console.ReadLine().Split(' ');
                int a = read(int.Parse(tmp[1]) - 1);
                int b = int.Parse(tmp[2]);

                if (tmp[0] == "U") {
                    Update(a, b, int.Parse(tmp[3]));
                } else {
                    sb.Append(Report(a, read(b - 1))).Append("\n");
                }
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }
        static void setUpFenwicks() {
            for (int i = 0; i < N; i++) {
                if (adj[i].Count == 1) {
                    chainLeaf[chain[i]] = i;
                }
            }
            chainNo++;
            f_V = new List<long[]>();
            f_VLp = new List<long[]>();
            f_K = new List<long[]>();
            f_KLp = new List<long[]>();
            f_KLp2 = new List<long[]>();
            for (int i = 0; i < chainNo; i++) {
                int len = level[chainLeaf[i]] - level[chainHead[i]] + 1;

                f_V.Add(new long[len + 1]);
                f_VLp.Add(new long[len + 1]);
                f_K.Add(new long[len + 1]);
                f_KLp.Add(new long[len + 1]);
                f_KLp2.Add(new long[len + 1]);
            }
            chainNo--;
        }
        static int read(int i) { if (i == R) return 0; if (i == 0) return R; return i; }
        static void pre() {
            sb = new StringBuilder();
            adj = new List<int>[N];
            for (int i = 0; i < N; i++) adj[i] = new List<int>();
            for (int i = 0; i < N - 1; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int a = read(int.Parse(tmp[0]) - 1);
                int b = read(int.Parse(tmp[1]) - 1);
                adj[a].Add(b); adj[b].Add(a);
            }

            chainHead = new int[MAX];
            chainLeaf = new int[MAX];
            paths = new List<int>();
            children = new List<List<int>>();
            for (int i = 0; i < MAX; i++) chainHead[i] = -1;

            chain = new int[N];
            for (int i = 0; i < N; i++) chain[i] = -1;
            chainPos = new int[N];
            subsize = new int[N];
            parent = new int[N];
            level = new int[N];
            L = new int[N];

            LCA = new int[N, 22];
            lg = (int)Math.Ceiling(Math.Log(N, 2));
            for (int i = 0; i < N; i++)
                for (int j = 0; j < 22; j++)
                    LCA[i, j] = -1;

            SubsizeParentLevel(0, -1, 1);
            ConstructLCA(N);
        }
        static int getLca(int x, int y) {
            if (L[x] < L[y]) { var tmp = x; x = y; y = tmp; }
            for (int i = lg; i >= 0; i--) {
                if (LCA[x, i] != -1 && L[LCA[x, i]] >= L[y])
                    x = LCA[x, i];
            }
            if (x == y)
                return x;
            for (int i = lg; i >= 0; i--) {
                if (LCA[x, i] != -1 && LCA[x, i] != LCA[y, i]) {
                    x = LCA[x, i]; y = LCA[y, i];
                }
            }
            return LCA[x, 0];
        }
        static int SubsizeParentLevel(int p, int no, int l) {
            LCA[p, 0] = no;
            L[p] = no == -1 ? 1 : L[no] + 1;
            parent[p] = no;
            level[p] = l;
            int r = 1;
            l++;
            foreach (var item in adj[p].Where(x => x != no))
                r += SubsizeParentLevel(item, p, l);

            subsize[p] = r;
            return r;
        }
        static void ConstructLCA(int n) {
            for (int i = 1; i < lg; i++)
                for (int j = 1; j < n; j++)
                    if (LCA[j, i - 1] != -1)
                        LCA[j, i] = LCA[LCA[j, i - 1], i - 1];
        }
        static long Report(int A, int B) {
            int x = getLca(A, B);
            var u = (FenwickToRoot(B) - FenwickToRoot(x)) % MOD;
            u = (u + (FenwickToRoot(A) - FenwickToRoot(parent[x])) % MOD) % MOD;
            if (u < 0) return MOD + u;
            return u;
        }
        static long FenwickToRoot(int a, int _lf = -1, int _lc = -1) {
            if (a == -1) return 0;
            int ch = chain[a];
            int pos = chainPos[a];
            int leaf = chainLeaf[ch];

            int Lf = _lf == -1 ? level[leaf] : _lf;
            int Lc = _lc == -1 ? level[a] : _lc;

            var v = query_fenwick(f_V[ch], pos);
            var vlp = query_fenwick(f_VLp[ch], pos);
            var klp2 = query_fenwick(f_KLp2[ch], pos);
            var klp = query_fenwick(f_KLp[ch], pos);
            var k = query_fenwick(f_K[ch], pos);

            long ans = (Lc * v - vlp) % MOD;

            var one = (k * (Lc * Lc + Lc)) % MOD;
            var two = klp2;
            var three = (-(klp * (2 * Lc + 1))) % MOD;
            var xx = (one + two + three) % MOD;

            ans = (ans + (xx * 500000004) % MOD) % MOD; // *500000004 = /2

            ans = (ans + FenwickToRoot(parent[chainHead[ch]], Lf, Lc)) % MOD;
            if (ans < 0) return ans + MOD;
            return ans;
        }
        static void Update(int a, long V, long K, int d = 0) {
            var ch = chain[a];
            var pos = chainPos[a];
            var lvl = level[a];

            var Lp = lvl;

            var a_f_K = f_K[ch];
            var a_f_KLp = f_KLp[ch];
            var a_f_KLp2 = f_KLp2[ch];

            int len = a_f_K.Length - 1;

            updatefenwick(f_V[ch], len, pos, V);
            updatefenwick(a_f_K, len, pos, K);
            updatefenwick(f_VLp[ch], len, pos, (V * Lp) % MOD - V);
            updatefenwick(a_f_KLp, len, pos, (K * Lp) % MOD);
            updatefenwick(a_f_KLp2, len, pos, (((K * Lp) % MOD) * Lp) % MOD);

        }
        static long query_fenwick(long[] fenwick, int i) {
            i++;
            long sum = 0;
            for (; i > 0; i -= i & (-i)) sum = (sum + fenwick[i]) % MOD;
            return sum;
        }
        static void updatefenwick(long[] fenwick, int n, int i, long val) {
            i++;
            for (; i <= n; i += i & (-i)) fenwick[i] = (fenwick[i] + val) % MOD;
        }
        static void HLD(int p, int no, int parentChain) {
            if (parentChain != -1) children[parentChain].Add(chainNo);
            if (chainHead[chainNo] == -1) {
                chainHead[chainNo] = p;
                paths.Add(0);
                children.Add(new List<int>());
            }

            chain[p] = chainNo;
            chainPos[p] = paths[chainNo];
            paths[chainNo]++;
            int s = -1, ssz = -1;
            for (int i = 0; i < adj[p].Count; i++) {
                if (adj[p][i] != no && subsize[adj[p][i]] > ssz) {
                    ssz = subsize[adj[p][i]];
                    s = i;
                }
            }
            if (s == -1) return;
            HLD(adj[p][s], p, -1);
            for (int i = 0; i < adj[p].Count; i++) {
                if (adj[p][i] != no && i != s) {
                    chainNo++;
                    HLD(adj[p][i], p, chain[p]);
                }
            }
        }
    }

    public static class Easy_Addition {
        const int MOD = 1000000007;
        static Tree tree;
        public static void Start() {
            new Thread(__, 1024 * 1024 * 128).Start();
        }
        static void __() {
            var tmp = Console.ReadLine().Split(' ');
            int N = int.Parse(tmp[0]);
            long R = long.Parse(tmp[1]);
            tree = new Tree(N);
            tree.read1();

            tree.PreProcessLCA();
            tree.HLD();
            ConstructArrays(R);

            tmp = Console.ReadLine().Split(' ');
            int U = int.Parse(tmp[0]);
            int Q = int.Parse(tmp[1]);

            for (int i = 0; i < U; i++) {
                tmp = Console.ReadLine().Split(' ');
                long a1 = long.Parse(tmp[0]);
                long d1 = long.Parse(tmp[1]);
                long a2 = long.Parse(tmp[2]);
                long d2 = long.Parse(tmp[3]);
                int u = int.Parse(tmp[4]) - 1;
                int v = int.Parse(tmp[5]) - 1;

                long A = mod(a1 * a2);
                long B = mod(mod(a1 * d2) + mod(a2 * d1));
                long C = mod(d1 * d2);
                int lca = tree.GetLCA(u, v);
                Update(u, lca, A, B, C, 0);

                if (lca != v) {
                    var lcanext = tree.nextTowards(v, lca);
                    UpdateRev(lcanext, v, A, B, C, tree.L[u] - tree.L[lca] + 1);
                }
            }

            foreach (var _ in first) _.accum();
            foreach (var _ in second) _.accum();
            foreach (var _ in third) _.accum();
            foreach (var _ in first2) _.accum();
            foreach (var _ in second2) _.accum();
            foreach (var _ in third2) _.accum();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Q; i++) {
                tmp = Console.ReadLine().Split(' ');
                int u = int.Parse(tmp[0]) - 1;
                int v = int.Parse(tmp[1]) - 1;
                int lca = tree.GetLCA(u, v);

                long ans = mod(queryUp(u, lca) + queryUpRev(lca, u));

                if (lca != v) {
                    var lcanext = tree.nextTowards(v, lca);
                    ans = mod(ans + queryUp(v, lcanext));
                    ans = mod(ans + queryUpRev(lcanext, v));
                }
                sb.AppendLine(ans.ToString());
            }

            --sb.Length;
            Console.WriteLine(sb.ToString());
        }
        static long mod(long v) { v %= MOD; if (v < 0) v += MOD; return v; }
        static long queryUp(int d, int u) {
            int chd = tree.chain[d];
            int chu = tree.chain[u];

            long r = 0;
            int st = first[chd].n - 1 - tree.chainPos[d];
            int en = first[chd].n - 1;
            if (chd == chu) {
                en -= tree.chainPos[u];
            } else {
                r = queryUp(tree.parent[tree.chainHead[chd]], u);
            }
            r = mod(r + first[chd].query(st, en));
            r = mod(r + second[chd].query(st, en));
            r = mod(r + third[chd].query(st, en));
            return r;
        }
        static long queryUpRev(int u, int d) {
            int chu = tree.chain[u];
            int chd = tree.chain[d];

            long r = 0;
            int st = 0, en = tree.chainPos[d];
            if (chu == chd) {
                st = tree.chainPos[u];
            } else {
                r = queryUpRev(u, tree.parent[tree.chainHead[chd]]);
            }
            r = mod(r + first2[chd].query(st, en));
            r = mod(r + second2[chd].query(st, en));
            r = mod(r + third2[chd].query(st, en));
            return r;
        }

        static void Update(int d, int u, long A, long B, long C, int z) {
            int chd = tree.chain[d];
            int chu = tree.chain[u];

            int st = first[chd].n - 1 - tree.chainPos[d];
            int en = first[chd].n - 1;
            if (chd == chu) {
                en -= tree.chainPos[u];
            } else {
                Update(tree.parent[tree.chainHead[chd]], u, A, B, C, z + tree.chainPos[d] + 1);
            }
            first[chd].append(A, st, en, z);
            second[chd].append(B, st, en, z);
            third[chd].append(C, st, en, z);
        }
        static void UpdateRev(int u, int d, long A, long B, long C, int z0) {
            int chu = tree.chain[u];
            int chd = tree.chain[d];
            int st = 0;
            int en = tree.chainPos[d];
            if (chu == chd) {
                st = tree.chainPos[u];
            } else {
                UpdateRev(u, tree.parent[tree.chainHead[chd]], A, B, C, z0);
                z0 = tree.L[tree.chainHead[chd]] - tree.L[u] + z0;
            }
            first2[chd].append(A, st, en, z0);
            second2[chd].append(B, st, en, z0);
            third2[chd].append(C, st, en, z0);
        }

        static List<First> first = new List<First>();
        static List<Second> second = new List<Second>();
        static List<Third> third = new List<Third>();
        static List<First> first2 = new List<First>();
        static List<Second> second2 = new List<Second>();
        static List<Third> third2 = new List<Third>();
        static void ConstructArrays(long R) {
            int chainNo = tree.chainNo + 1;
            for (int i = 0; i < chainNo; i++) {
                int c = tree.members[i].Count;
                first.Add(new First(c, R));
                second.Add(new Second(c, R));
                third.Add(new Third(c, R));

                first2.Add(new First(c, R));
                second2.Add(new Second(c, R));
                third2.Add(new Third(c, R));
            }
        }

        class First {
            public int n;
            public long R;
            public long[] A, arr;
            public First(int _n, long _r) {
                n = _n;
                R = _r;
                A = new long[n + 1];
            }
            public void accum() {
                arr = new long[n];
                arr[0] = mod(A[0]);
                for (int i = 1; i < n; i++) {
                    A[i] = mod(A[i] + mod(A[i - 1] * R));
                    arr[i] = mod(arr[i - 1] + A[i]);
                }
            }

            public void append(long b, int s, int e, int z2) {
                int z = e - s + 1 + z2;
                long rz = modpow(R, z);
                long rz2 = modpow(R, z2);
                A[s] = mod(A[s] + mod(b * rz2));
                A[e + 1] = mod(A[e + 1] - mod(b * rz));
            }
            public long query(int s, int e) {
                long r = mod(arr[e]);
                return s == 0 ? r : mod(r - arr[s - 1]);
            }
        }
        class Second {
            public int n;
            public long R;
            public long[] A, B, arr;
            public Second(int _n, long _r) {
                n = _n;
                R = _r;
                A = new long[n + 1];
                B = new long[n + 1];
            }
            public void accum() {
                arr = new long[n];
                arr[0] = mod(A[0]);
                for (int i = 1; i < n; i++) {
                    A[i] = mod(A[i] + mod(mod(A[i - 1] * R) + mod(B[i - 1] * R)));
                    B[i] = mod(B[i] + mod(B[i - 1] * R));
                    arr[i] = mod(arr[i - 1] + A[i]);
                }
            }

            public void append(long b, int s, int e, int z2) {
                int z = e - s + 1 + z2;
                long rz = modpow(R, z);
                long rz2 = modpow(R, z2);
                A[s] = mod(A[s] + mod(mod(b * z2) * rz2));
                B[s] = mod(B[s] + mod(b * rz2));
                A[e + 1] = mod(A[e + 1] - mod(mod(b * z) * rz));
                B[e + 1] = mod(B[e + 1] - mod(b * rz));
            }
            public long query(int s, int e) {
                long r = mod(arr[e]);
                return s == 0 ? r : mod(r - arr[s - 1]);
            }
        }
        class Third {
            public int n;
            public long R;
            public long[] A, B, C, arr;
            public Third(int _n, long _r) {
                n = _n;
                R = _r;
                A = new long[n + 1];
                B = new long[n + 1];
                C = new long[n + 1];
            }
            public void accum() {
                arr = new long[n];
                arr[0] = mod(A[0]);
                for (int i = 1; i < n; i++) {
                    A[i] = mod(A[i] + mod(mod(A[i - 1] * R) + mod(2 * B[i - 1] * R) + mod(C[i - 1] * R)));
                    B[i] = mod(B[i] + mod(mod(B[i - 1] * R) + mod(C[i - 1] * R)));
                    C[i] = mod(C[i] + mod(C[i - 1] * R));
                    arr[i] = mod(arr[i - 1] + A[i]);
                }
            }

            public void append(long b, int s, int e, int z2) {
                int z = e - s + 1 + z2;
                long rz = modpow(R, z);
                long rz2 = modpow(R, z2);

                A[s] = mod(A[s] + mod(mod(mod(b * z2) * z2) * rz2));
                B[s] = mod(B[s] + mod(mod(b * z2) * rz2));
                C[s] = mod(C[s] + mod(b * rz2));
                A[e + 1] = mod(A[e + 1] - mod(mod(mod(b * z) * z) * rz));
                B[e + 1] = mod(B[e + 1] - mod(mod(b * z) * rz));
                C[e + 1] = mod(C[e + 1] - mod(b * rz));

            }
            public long query(int s, int e) {
                long r = mod(arr[e]);
                return s == 0 ? r : mod(r - arr[s - 1]);
            }
        }
        class Tree {
            public int N, R = 0;
            public List<int>[] adj;
            public Tree(int n) {
                N = n;
                adj = new List<int>[n];
                for (int i = 0; i < n; i++) adj[i] = new List<int>();
            }

            public void Connect(int u, int v) {
                adj[u].Add(v);
                adj[v].Add(u);
            }

            public void PreProcessLCA() {
                int MAX = N + 5;
                chainHead = new int[MAX];
                chainLeaf = new int[MAX];
                paths = new List<int>();
                children = new List<List<int>>();
                members = new List<List<int>>();
                for (int i = 0; i < MAX; i++) chainHead[i] = -1;

                chain = new int[N];
                for (int i = 0; i < N; i++) chain[i] = -1;
                chainPos = new int[N];
                subsize = new int[N];
                parent = new int[N];
                L = new int[N];

                LCA = new int[N, 22];
                lg = 1 + (int)Math.Ceiling(Math.Log(N, 2));
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < 22; j++)
                        LCA[i, j] = -1;

                SubsizeParentLevel(R, -1);
                ConstructLCA(N);
            }

            int SubsizeParentLevel(int p, int no) {
                LCA[p, 0] = no;
                L[p] = no == -1 ? 1 : L[no] + 1;
                parent[p] = no;
                int r = 1;
                foreach (var item in adj[p].Where(x => x != no))
                    r += SubsizeParentLevel(item, p);

                subsize[p] = r;
                return r;
            }

            #region Lowest Common Ancestor
            public int lg;
            public int[,] LCA;
            public int[] parent, L;
            void ConstructLCA(int n) {
                for (int i = 1; i < lg; i++)
                    for (int j = 0; j < n; j++)
                        if (LCA[j, i - 1] != -1)
                            LCA[j, i] = LCA[LCA[j, i - 1], i - 1];
            }
            public int GetLCA(int x, int y) {
                if (L[x] < L[y]) { var tmp = x; x = y; y = tmp; }
                for (int i = lg; i >= 0; i--) {
                    if (LCA[x, i] != -1 && L[LCA[x, i]] >= L[y])
                        x = LCA[x, i];
                }
                if (x == y)
                    return x;
                for (int i = lg; i >= 0; i--) {
                    if (LCA[x, i] != -1 && LCA[x, i] != LCA[y, i]) {
                        x = LCA[x, i]; y = LCA[y, i];
                    }
                }
                return LCA[x, 0];
            }

            public int nextTowards(int l, int h) {
                for (int i = lg; i >= 0; i--) {
                    if (LCA[l, i] != -1 && L[LCA[l, i]] > L[h])
                        l = LCA[l, i];
                }
                return l;
            }
            #endregion

            #region Heavy Light Decomposition
            public int chainNo;
            public int[] chainHead, chain, subsize, chainPos, chainLeaf;
            public List<List<int>> children, members;
            public List<int> paths;
            public void HLD() {
                HLD(0, -1, -1);
            }
            void HLD(int p, int no, int parentChain) {
                if (parentChain != -1) children[parentChain].Add(chainNo);
                if (chainHead[chainNo] == -1) {
                    chainHead[chainNo] = p;
                    paths.Add(0);
                    members.Add(new List<int>());
                    children.Add(new List<int>());
                }

                members[chainNo].Add(p);
                chain[p] = chainNo;
                chainPos[p] = paths[chainNo];
                paths[chainNo]++;

                int s = -1, ssz = -1;
                for (int i = 0; i < adj[p].Count; i++) {
                    if (adj[p][i] != no && subsize[adj[p][i]] > ssz) {
                        ssz = subsize[adj[p][i]];
                        s = i;
                    }
                }

                if (s == -1) return;
                HLD(adj[p][s], p, -1);
                for (int i = 0; i < adj[p].Count; i++) {
                    if (adj[p][i] != no && i != s) {
                        chainNo++;
                        HLD(adj[p][i], p, chain[p]);
                    }
                }
            }

            public void read1() {
                for (int i = 0; i < N - 1; i++) {
                    var tmp = Console.ReadLine().Split(' ');
                    int a = int.Parse(tmp[0]);
                    int b = int.Parse(tmp[1]);
                    Connect(a - 1, b - 1);
                }
            }
            #endregion
        }
        static long modpow(long bas, long exp) {
            bas %= MOD;
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) == 1) result = (result * bas) % MOD;
                bas = (bas * bas) % MOD;
                exp >>= 1;
            }
            return result;
        }
    }

    public static class Recalling_Early_Days_GP_with_Trees {
        const int MOD = 100711433;
        static Tree tree;
        static long R;
        public static void Start() {
            new Thread(__, 1024 * 1024 * 128).Start();
        }
        static void __() {
            var tmp = Console.ReadLine().Split(' ');
            int N = int.Parse(tmp[0]);
            R = long.Parse(tmp[1]);
            tree = new Tree(N);
            tree.read1();

            tree.PreProcessLCA();
            tree.HLD();
            ConstructArrays(R);

            tmp = Console.ReadLine().Split(' ');
            int U = int.Parse(tmp[0]);
            int Q = int.Parse(tmp[1]);

            for (int i = 0; i < U; i++) {
                tmp = Console.ReadLine().Split(' ');
                int u = int.Parse(tmp[0]) - 1;
                int v = int.Parse(tmp[1]) - 1;
                int x = int.Parse(tmp[2]);

                int lca = tree.GetLCA(u, v);
                Update(u, lca, x);

                if (lca != v) {
                    var lcanext = tree.nextTowards(v, lca);
                    UpdateRev(lcanext, v, mod(x * modpow(R, tree.L[u] - tree.L[lca] + 1)));
                }
            }

            foreach (var _ in up) _.accum();
            foreach (var _ in down) _.accum();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Q; i++) {
                tmp = Console.ReadLine().Split(' ');
                int u = int.Parse(tmp[0]) - 1;
                int v = int.Parse(tmp[1]) - 1;
                int lca = tree.GetLCA(u, v);

                long ans = mod(queryUp(u, lca) + queryUpRev(lca, u));

                if (lca != v) {
                    var lcanext = tree.nextTowards(v, lca);
                    ans = mod(ans + queryUp(v, lcanext));
                    ans = mod(ans + queryUpRev(lcanext, v));
                }
                sb.AppendLine(ans.ToString());
            }

            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static long mod(long v) { v %= MOD; if (v < 0) v += MOD; return v; }
        static long queryUp(int d, int u) {
            int chd = tree.chain[d], chu = tree.chain[u];

            long r = 0;
            int st = up[chd].n - 1 - tree.chainPos[d], en = up[chd].n - 1;
            if (chd == chu) {
                en -= tree.chainPos[u];
            } else {
                r = queryUp(tree.parent[tree.chainHead[chd]], u);
            }
            r = mod(r + up[chd].query(st, en));
            return r;
        }

        static long queryUpRev(int u, int d) {
            int chu = tree.chain[u], chd = tree.chain[d];

            long r = 0;
            int st = 0, en = tree.chainPos[d];
            if (chu == chd) {
                st = tree.chainPos[u];
            } else {
                r = queryUpRev(u, tree.parent[tree.chainHead[chd]]);
            }
            r = mod(r + down[chd].query(st, en));
            return r;
        }


        static void Update(int d, int u, long x) {
            int chd = tree.chain[d], chu = tree.chain[u];

            int st = up[chd].n - 1 - tree.chainPos[d], en = up[chd].n - 1;
            if (chd == chu) {
                en -= tree.chainPos[u];
            } else {
                Update(tree.parent[tree.chainHead[chd]], u, mod(x * modpow(R, 1 + tree.chainPos[d])));
            }
            up[chd].append(x, st, en);
        }

        static void UpdateRev(int u, int d, long x) {
            int chu = tree.chain[u], chd = tree.chain[d];

            int st = 0, en = tree.chainPos[d];
            if (chu == chd) {
                st = tree.chainPos[u];
            } else {
                UpdateRev(u, tree.parent[tree.chainHead[chd]], x);
                x = mod(x * modpow(R, tree.L[tree.chainHead[chd]] - tree.L[u]));
            }
            down[chd].append(x, st, en);
        }

        static List<Store> up = new List<Store>(), down = new List<Store>();
        static void ConstructArrays(long R) {
            int chainNo = tree.chainNo + 1;
            for (int i = 0; i < chainNo; i++) {
                int c = tree.members[i].Count;
                up.Add(new Store(c));
                down.Add(new Store(c));
            }
        }

        class Store {
            public int n;
            public long[] A;
            public Store(int _n) {
                n = _n;
                A = new long[n + 1];
            }
            public void accum() {
                for (int i = 1; i < n; i++) A[i] = mod(A[i] + mod(A[i - 1] * R));
                for (int i = 1; i < n; i++) A[i] = mod(A[i] + A[i - 1]);
            }

            public void append(long x, int s, int e) {
                x = mod(x);
                long cut = modpow(R, e - s + 1);
                A[s] = mod(A[s] + x);
                A[e + 1] = mod(A[e + 1] - mod(cut * x));
            }
            public long query(int s, int e) {
                return s == 0 ? mod(A[e]) : mod(mod(A[e]) - A[s - 1]);
            }
        }

        class Tree {
            public int N, R = 0;
            public List<int>[] adj;
            public Tree(int n) {
                N = n;
                adj = new List<int>[n];
                for (int i = 0; i < n; i++) adj[i] = new List<int>();
            }

            public void Connect(int u, int v) {
                adj[u].Add(v);
                adj[v].Add(u);
            }

            public void PreProcessLCA() {
                int MAX = N + 5;
                chainHead = new int[MAX];
                chainLeaf = new int[MAX];
                paths = new List<int>();
                children = new List<List<int>>();
                members = new List<List<int>>();
                for (int i = 0; i < MAX; i++) chainHead[i] = -1;

                chain = new int[N];
                for (int i = 0; i < N; i++) chain[i] = -1;
                chainPos = new int[N];
                subsize = new int[N];
                parent = new int[N];
                L = new int[N];

                LCA = new int[N, 22];
                lg = 1 + (int)Math.Ceiling(Math.Log(N, 2));
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < 22; j++)
                        LCA[i, j] = -1;

                SubsizeParentLevel(R, -1);
                ConstructLCA(N);
            }

            int SubsizeParentLevel(int p, int no) {
                LCA[p, 0] = no;
                L[p] = no == -1 ? 1 : L[no] + 1;
                parent[p] = no;
                int r = 1;
                foreach (var item in adj[p].Where(x => x != no))
                    r += SubsizeParentLevel(item, p);

                subsize[p] = r;
                return r;
            }

            #region Lowest Common Ancestor
            public int lg;
            public int[,] LCA;
            public int[] parent, L;
            void ConstructLCA(int n) {
                for (int i = 1; i < lg; i++)
                    for (int j = 0; j < n; j++)
                        if (LCA[j, i - 1] != -1)
                            LCA[j, i] = LCA[LCA[j, i - 1], i - 1];
            }
            public int GetLCA(int x, int y) {
                if (L[x] < L[y]) { var tmp = x; x = y; y = tmp; }
                for (int i = lg; i >= 0; i--) {
                    if (LCA[x, i] != -1 && L[LCA[x, i]] >= L[y])
                        x = LCA[x, i];
                }
                if (x == y)
                    return x;
                for (int i = lg; i >= 0; i--) {
                    if (LCA[x, i] != -1 && LCA[x, i] != LCA[y, i]) {
                        x = LCA[x, i]; y = LCA[y, i];
                    }
                }
                return LCA[x, 0];
            }

            public int nextTowards(int l, int h) {
                for (int i = lg; i >= 0; i--) {
                    if (LCA[l, i] != -1 && L[LCA[l, i]] > L[h])
                        l = LCA[l, i];
                }
                return l;
            }
            #endregion

            #region Heavy Light Decomposition
            public int chainNo;
            public int[] chainHead, chain, subsize, chainPos, chainLeaf;
            public List<List<int>> children, members;
            public List<int> paths;
            public void HLD() {
                HLD(0, -1, -1);
            }
            void HLD(int p, int no, int parentChain) {
                if (parentChain != -1) children[parentChain].Add(chainNo);
                if (chainHead[chainNo] == -1) {
                    chainHead[chainNo] = p;
                    paths.Add(0);
                    members.Add(new List<int>());
                    children.Add(new List<int>());
                }

                members[chainNo].Add(p);
                chain[p] = chainNo;
                chainPos[p] = paths[chainNo];
                paths[chainNo]++;

                int s = -1, ssz = -1;
                for (int i = 0; i < adj[p].Count; i++) {
                    if (adj[p][i] != no && subsize[adj[p][i]] > ssz) {
                        ssz = subsize[adj[p][i]];
                        s = i;
                    }
                }

                if (s == -1) return;
                HLD(adj[p][s], p, -1);
                for (int i = 0; i < adj[p].Count; i++) {
                    if (adj[p][i] != no && i != s) {
                        chainNo++;
                        HLD(adj[p][i], p, chain[p]);
                    }
                }
            }

            public void read1() {
                for (int i = 0; i < N - 1; i++) {
                    var tmp = Console.ReadLine().Split(' ');
                    int a = int.Parse(tmp[0]);
                    int b = int.Parse(tmp[1]);
                    Connect(a - 1, b - 1);
                }
            }
            #endregion
        }
        static long modpow(long bas, long exp) {
            bas %= MOD;
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) == 1) result = (result * bas) % MOD;
                bas = (bas * bas) % MOD;
                exp >>= 1;
            }
            return result;
        }
    }

    public static class Heavy_Light_White_Falcon {
        static int N;
        static List<int>[] adj;
        static void Start() {
            new Thread(__, 1024 * 1024 * 128).Start();
        }
        static void __() {
            var tmp = Console.ReadLine().Split(' ');
            N = int.Parse(tmp[0]);
            int Q = int.Parse(tmp[1]);
            adj = new List<int>[N];
            for (int i = 0; i < N; i++) adj[i] = new List<int>();

            for (int i = 0; i < N - 1; i++) {
                tmp = Console.ReadLine().Split(' ');
                int u = int.Parse(tmp[0]);
                int v = int.Parse(tmp[1]);
                adj[u].Add(v); adj[v].Add(u);
            }


            subsize = new int[N];
            parent = new int[N];
            L = new int[N];

            lg = 1 + (int)Math.Log(N, 2);
            LCA = new int[N, lg];
            for (int n = 0; n < N; n++)
                for (int i = 0; i < lg; i++)
                    LCA[n, i] = -1;

            dfs(0, -1);
            for (int i = 1; i < lg; i++)
                for (int n = 0; n < N; n++)
                    if (LCA[n, i - 1] != -1)
                        LCA[n, i] = LCA[LCA[n, i - 1], i - 1];

            chain = new int[N];
            chainHead = new int[N];
            chainPos = new int[N];
            members = new List<int>[N];
            members[0] = new List<int>();
            chainHead[0] = 0;
            HLD(0, -1);

            chainNo++;
            segment = new int[chainNo][];
            for (int i = 0; i < chainNo; i++) {
                int n = members[i].Count;
                int sz = 2 + (int)Math.Log(n, 2);
                sz = 1 << sz;
                segment[i] = new int[sz];
            }

            StringBuilder sb = new StringBuilder();
            while (Q-- > 0) {
                tmp = Console.ReadLine().Split(' ');
                int q = int.Parse(tmp[0]);
                int u = int.Parse(tmp[1]);
                int v = int.Parse(tmp[2]);
                if (q == 1) {
                    update(segment[chain[u]], chainPos[u], v, 0, members[chain[u]].Count, 0);
                } else {
                    int lca = getlca(u, v);
                    int k = Math.Max(query(u, lca), query(v, lca));
                    sb.AppendLine(k.ToString());
                }
            }

            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static int query(int d, int u) {
            int chd = chain[d], chu = chain[u];
            if (chd == chu)
                return getMax(segment[chd], chainPos[u], chainPos[d], 0, members[chd].Count, 0);
            return Math.Max(getMax(segment[chd], 0, chainPos[d], 0, members[chd].Count, 0), query(parent[chainHead[chd]], u));
        }

        static int lg, chainNo;
        static int[] L, subsize, parent, chainHead, chainPos, chain;
        static int[,] LCA;
        static List<int>[] members;
        static int[][] segment;

        static int dfs(int fr, int no) {
            parent[fr] = no;
            LCA[fr, 0] = no;
            L[fr] = no == -1 ? 1 : 1 + L[no];
            int r = 1;
            foreach (var item in adj[fr]) {
                if (item == no) continue;
                r += dfs(item, fr);
            }
            subsize[fr] = r;
            return r;
        }

        static int getlca(int x, int y) {
            if (L[x] < L[y]) return getlca(y, x);

            for (int i = lg - 1; i >= 0; i--) {
                if (LCA[x, i] != -1 && L[LCA[x, i]] >= L[y])
                    x = LCA[x, i];
            }
            if (x == y) return x;

            for (int i = lg - 1; i >= 0; i--) {
                if (LCA[x, i] != -1 && LCA[x, i] != LCA[y, i]) {
                    x = LCA[x, i]; y = LCA[y, i];
                }
            }
            return LCA[x, 0];
        }

        static void HLD(int fr, int no) {
            chain[fr] = chainNo;
            chainPos[fr] = members[chainNo].Count;
            members[chainNo].Add(fr);

            int s = -1;
            foreach (var item in adj[fr]) {
                if (item == no) continue;
                if (s == -1 || subsize[item] > subsize[s])
                    s = item;
            }

            if (s == -1) return;

            HLD(s, fr);

            foreach (var item in adj[fr]) {
                if (item == no || item == s) continue;
                chainNo++;
                members[chainNo] = new List<int>();
                chainHead[chainNo] = item;
                HLD(item, fr);
            }

        }
        static void update(int[] seg, int i, int val, int st, int en, int ss) {
            if (i < st || i > en) return;
            if (st == en) {
                seg[ss] = val;
                return;
            }

            int mid = (st + en) / 2;
            update(seg, i, val, st, mid, 2 * ss + 1);
            update(seg, i, val, mid + 1, en, 2 * ss + 2);
            seg[ss] = Math.Max(seg[2 * ss + 1], seg[2 * ss + 2]);
        }

        static int getMax(int[] seg, int fr, int to, int st, int en, int ss) {
            if (fr <= st && to >= en) return seg[ss];
            if (fr > en || to < st) return int.MinValue;
            int mid = (st + en) / 2;
            return Math.Max(getMax(seg, fr, to, st, mid, 2 * ss + 1), getMax(seg, fr, to, mid + 1, en, 2 * ss + 2));
        }
    }

    public static class Lazy_White_Falcon {
        static int N, passi, lg;
        static int[] pass1, pass2, L;
        static int[,] LCA;
        static long[] val, fenwick;
        static List<int>[] adj;
        static void Start() {
            new Thread(__, 1024 * 1024 * 128).Start();
        }
        static void __() {
            var tmp = Console.ReadLine().Split(' ');
            N = int.Parse(tmp[0]);
            int Q = int.Parse(tmp[1]);
            adj = new List<int>[N];
            for (int i = 0; i < N; i++) adj[i] = new List<int>();

            for (int i = 0; i < N - 1; i++) {
                tmp = Console.ReadLine().Split(' ');
                int u = int.Parse(tmp[0]);
                int v = int.Parse(tmp[1]);
                adj[u].Add(v); adj[v].Add(u);
            }

            L = new int[N];
            val = new long[N];
            pass1 = new int[N];
            pass2 = new int[N];
            fenwick = new long[2 * N + 2];

            lg = 1 + (int)Math.Log(N, 2);
            LCA = new int[N, lg];
            for (int n = 0; n < N; n++)
                for (int i = 0; i < lg; i++)
                    LCA[n, i] = -1;
            dfs(0, -1);
            for (int i = 1; i < lg; i++)
                for (int n = 0; n < N; n++)
                    if (LCA[n, i - 1] != -1)
                        LCA[n, i] = LCA[LCA[n, i - 1], i - 1];


            StringBuilder sb = new StringBuilder();
            while (Q-- > 0) {
                tmp = Console.ReadLine().Split(' ');
                int q = int.Parse(tmp[0]);
                int u = int.Parse(tmp[1]);
                int v = int.Parse(tmp[2]);
                if (q == 1) {
                    update(fenwick, passi, pass1[u], -val[u] + v);
                    update(fenwick, passi, pass2[u], val[u] - v);
                    val[u] = v;
                } else {
                    int lca = getlca(u, v);
                    long k = query(fenwick, pass1[u]) + query(fenwick, pass1[v]) - 2 * query(fenwick, pass1[lca]) + val[lca];
                    sb.AppendLine(k.ToString());
                }
            }

            --sb.Length;
            Console.WriteLine(sb.ToString());
        }


        static void dfs(int fr, int no) {
            LCA[fr, 0] = no;
            L[fr] = no == -1 ? 1 : 1 + L[no];
            pass1[fr] = ++passi;
            foreach (var item in adj[fr]) if (item != no) dfs(item, fr);
            pass2[fr] = ++passi;
        }

        static int getlca(int x, int y) {
            if (L[x] < L[y]) return getlca(y, x);
            for (int i = lg - 1; i >= 0; i--)
                if (LCA[x, i] != -1 && L[LCA[x, i]] >= L[y])
                    x = LCA[x, i];
            if (x == y) return x;
            for (int i = lg - 1; i >= 0; i--)
                if (LCA[x, i] != -1 && LCA[x, i] != LCA[y, i]) {
                    x = LCA[x, i]; y = LCA[y, i];
                }
            return LCA[x, 0];
        }

        public static long query(long[] fenwick, int i) {
            long sum = 0;
            for (i = i + 1; i > 0; i -= i & (-i)) sum += fenwick[i];
            return sum;
        }

        public static void update(long[] fenwick, int n, int i, long val) {
            for (i = i + 1; i <= n; i += i & (-i)) fenwick[i] += val;
        }
    }

    public static class Self_Driving_Bus {
        static int[,] LCA, MIN, MAX;
        static int N, lg;
        static int[] L, subsize, Lev, R, parent, fenwick;
        static List<int>[] adj;
        static void Start() {
            new Thread(__, 1024 * 1024 * 128).Start();
        }
        static void __() {
            N = int.Parse(Console.ReadLine());
            adj = new List<int>[N];
            for (int i = 0; i < N; i++) adj[i] = new List<int>();

            for (int i = 0; i < N - 1; i++) {
                var tmp = Console.ReadLine().Trim().Split(' ');
                int l = int.Parse(tmp[0]) - 1;
                int r = int.Parse(tmp[1]) - 1;
                adj[l].Add(r); adj[r].Add(l);
            }

            pre();

            Stack<int> S = new Stack<int>();
            L = new int[N];
            for (int i = 0; i < N; i++) L[i] = N;
            for (int i = 0; i < N; i++) {
                while (S.Count > 0 && MinNode(S.Peek(), i) < S.Peek()) {
                    L[S.Pop()] = i;
                }
                S.Push(i);
            }

            S = new Stack<int>();
            R = new int[N];
            for (int i = 0; i < N; i++) R[i] = -1;
            for (int i = N - 1; i >= 0; i--) {
                while (S.Count > 0 && MaxNode(S.Peek(), i) > S.Peek()) {
                    R[S.Pop()] = i;
                }
                S.Push(i);
            }

            long ans = 0;
            List<int>[] _r = new List<int>[N];
            for (int i = 0; i < N; i++) _r[i] = new List<int>();
            for (int i = 0; i < N; i++) _r[R[i] + 1].Add(i);
            fenwick = new int[N + 3];
            for (int i = 0; i < N; i++) {
                foreach (var j in _r[i]) add(j, 1);
                ans += sum(L[i] - 1) - (i == 0 ? 0 : sum(i - 1));
            }
            Console.WriteLine(ans);
        }

        static long sum(int i) {
            i++; long sum = 0;
            for (; i > 0; i -= i & (-i)) sum = (sum + fenwick[i]);
            return sum;
        }

        static void add(int i, int v) {
            i++; for (; i <= N; i += i & (-i)) fenwick[i] = (fenwick[i] + v);
        }

        private static void pre() {
            parent = new int[N];
            Lev = new int[N];
            subsize = new int[N];
            LCA = new int[N, 22];
            MIN = new int[N, 22];
            MAX = new int[N, 22];
            lg = 1 + (int)Math.Ceiling(Math.Log(N, 2));
            for (int i = 0; i < N; i++) {
                for (int j = 0; j < 22; j++) {
                    LCA[i, j] = -1;
                    MIN[i, j] = int.MaxValue;
                    MAX[i, j] = int.MinValue;
                }
            }
            parentLevel(0, 0);
            ConstructLCA();
        }

        static int MaxNode(int p, int q) {
            var l = GetLCA(p, q);
            return Math.Max(GetMAX(p, l), GetMAX(q, l));
        }

        static int MinNode(int p, int q) {
            var l = GetLCA(p, q);
            return Math.Min(GetMIN(p, l), GetMIN(q, l));
        }

        static void parentLevel(int p, int no) {
            LCA[p, 0] = no;
            MIN[p, 0] = p;
            MAX[p, 0] = p;
            Lev[p] = no == -1 ? 1 : Lev[no] + 1;
            parent[p] = no;
            foreach (var item in adj[p].Where(x => x != no)) parentLevel(item, p);
        }

        static void ConstructLCA() {
            for (int i = 1; i < lg; i++)
                for (int j = 0; j < N; j++)
                    if (LCA[j, i - 1] != -1) {
                        LCA[j, i] = LCA[LCA[j, i - 1], i - 1];
                        MIN[j, i] = Math.Min(MIN[j, i - 1], MIN[LCA[j, i - 1], i - 1]);
                        MAX[j, i] = Math.Max(MAX[j, i - 1], MAX[LCA[j, i - 1], i - 1]);
                    }
        }

        static int GetLCA(int x, int y) {
            if (Lev[x] < Lev[y]) { var tmp = x; x = y; y = tmp; }
            for (int i = lg; i >= 0; i--) {
                if (LCA[x, i] != -1 && Lev[LCA[x, i]] >= Lev[y])
                    x = LCA[x, i];
            }
            if (x == y) return x;
            for (int i = lg; i >= 0; i--) {
                if (LCA[x, i] != -1 && LCA[x, i] != LCA[y, i]) {
                    x = LCA[x, i]; y = LCA[y, i];
                }
            }
            return LCA[x, 0];
        }

        static int GetMIN(int p, int q) {
            if (p == q) return p;
            var d = Lev[p] - Lev[q] + 1;
            var mn = Math.Min(p, q);
            for (int i = lg; i >= 0; i--) {
                if (d >> i <= 0) continue;
                mn = Math.Min(mn, MIN[p, i]);
                p = LCA[p, i];
                d = Lev[p] - Lev[q] + 1;
            }
            return mn;
        }

        static int GetMAX(int p, int q) {
            if (p == q) return p;
            var d = Lev[p] - Lev[q] + 1;
            var mx = Math.Max(p, q);
            for (int i = lg; i >= 0; i--) {
                if (d >> i <= 0) continue;
                mx = Math.Max(mx, MAX[p, i]);
                p = LCA[p, i];
                d = Lev[p] - Lev[q] + 1;
            }
            return mx;
        }

    }

}
