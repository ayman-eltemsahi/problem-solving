using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

class SolutionI
{
    const int MAXN = 100009;
    static int len;
    static string s;
    static int[] iSA = new int[MAXN], SA = new int[MAXN];
    static int[] cnt = new int[MAXN], next_gen = new int[MAXN], lcp = new int[MAXN];
    static int[,] LCP = new int[MAXN, 22];
    static bool[] bh = new bool[MAXN], b2h = new bool[MAXN], m_arr = new bool[MAXN];

    StringBuilder sb = new StringBuilder();
    static void Main22()
    {
        var tmp = Console.ReadLine().Split(' ');
        int n = int.Parse(tmp[0]);
        int q = int.Parse(tmp[1]);

        s = Console.ReadLine();
        len = n;

        SuffixSort(len);
        ConstructLCP();

        Palindromic_Tree.palindromic_tree(s);
        //for (int i = 0; i < len; i++) {
        //    addLetter(i);
        //}

        //for (int i = 2; i <= num; i++) {
        //    adj[tree[i].sufflink].pb(i);
        //}

        //dfs(1);

        /*     for (int i = 3; i <= num; i++) {
                 A.pb(mp(mp(tree[i].l, tree[i].r), tree[i].num));
             }

             sort(all(A), cmp);
             vl bs, has;
             RollingHash obj;
             obj.Construct();
             ll v = 0;
             for (auto it: A) {
                 v += it.sd;
                 bs.pb(v);
                 has.pb(obj.GetForwardHash(it.ft.ft + 1, it.ft.sd + 1));
             }
             long k;
             while (q-- > 0) {
                 cin >> k;
                 if (k > v) cout << -1 << "\n";
                 else {
                     auto it = lower_bound(all(bs), k);
                     cout << has[it - bs.begin()] << "\n";
                 }
             }*/
    }



    class smaller_first_char : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return s[x] - s[y];
        }
    }
    static void SuffixSort(int n)
    {
        for (int i = 0; i < n; ++i) {
            SA[i] = i;
        }
        Array.Sort(SA, new smaller_first_char());
        //sort(SA, SA + n, smaller_first_char);
        for (int i = 0; i < n; ++i) {
            bh[i] = i == 0 || s[SA[i]] != s[SA[i - 1]];
            b2h[i] = false;
        }
        for (int h = 1; h < n; h <<= 1) {
            int buckets = 0;
            for (int i = 0, j; i < n; i = j) {
                j = i + 1;
                while (j < n && !bh[j]) j++;
                next_gen[i] = j;
                buckets++;
            }
            if (buckets == n) break;
            for (int i = 0; i < n; i = next_gen[i]) {
                cnt[i] = 0;
                for (int j = i; j < next_gen[i]; ++j) {
                    iSA[SA[j]] = i;
                }
            }
            cnt[iSA[n - h]]++;
            b2h[iSA[n - h]] = true;
            for (int i = 0; i < n; i = next_gen[i]) {
                for (int j = i; j < next_gen[i]; ++j) {
                    int s = SA[j] - h;
                    if (s >= 0) {
                        int head = iSA[s];
                        iSA[s] = head + cnt[head]++;
                        b2h[iSA[s]] = true;
                    }
                }
                for (int j = i; j < next_gen[i]; ++j) {
                    int s = SA[j] - h;
                    if (s >= 0 && b2h[iSA[s]]) {
                        for (int k = iSA[s] + 1; !bh[k] && b2h[k]; k++) b2h[k] = false;
                    }
                }
            }
            for (int i = 0; i < n; ++i) {
                SA[iSA[i]] = i;
                bh[i] |= b2h[i];
            }
        }
        for (int i = 0; i < n; ++i) {
            iSA[SA[i]] = i;
        }
    }

    static void InitLCP(int n)
    {
        for (int i = 0; i < n; ++i)
            iSA[SA[i]] = i;
        lcp[0] = 0;
        for (int i = 0, h = 0; i < n; ++i) {
            if (iSA[i] > 0) {
                int j = SA[iSA[i] - 1];
                while (i + h < n && j + h < n && s[i + h] == s[j + h])
                    h++;
                lcp[iSA[i]] = h;
                if (h > 0)
                    h--;
            }
        }
    }

    static void ConstructLCP()
    {
        InitLCP(len);
        for (int i = 0; i < len; ++i)
            LCP[i, 0] = lcp[i];
        for (int j = 1; (1 << j) <= len; ++j) {
            for (int i = 0; i + (1 << j) - 1 < len; ++i) {
                if (LCP[i, j - 1] <= LCP[i + (1 << (j - 1)), j - 1])
                    LCP[i, j] = LCP[i, j - 1];
                else
                    LCP[i, j] = LCP[i + (1 << (j - 1)), j - 1];
            }
        }
    }

    static int GetLCP(int x, int y)
    {
        if (x == y) return len - SA[x];
        if (x > y) swap(ref x, ref y);
        int log = 0;
        while ((1 << log) <= (y - x)) ++log;
        --log;
        return Math.Min(LCP[x + 1, log], LCP[y - (1 << log) + 1, log]);
    }

    static void swap(ref int x, ref int y)
    {
        var t = x; x = y; y = t;
    }

    class RollingHash
    {
        long[] P = new long[MAXN], HashF = new long[MAXN], HashR = new long[MAXN];
        int prime, mod1, mod2;
        public RollingHash()
        {
            prime = 100001;
            mod1 = 1000000007;
            mod2 = 1897266401;
            P[0] = 1;
            for (int i = 1; i < MAXN; i++) {
                P[i] = 1L * P[i - 1] * prime % mod1;
            }
        }

        void Construct()
        {
            HashF[0] = HashR[len + 1] = 0;
            for (int i = 1; i <= len; i++) {
                HashF[i] = (1L * HashF[i - 1] * prime + s[i - 1]) % mod1;
                HashR[len - i + 1] = (1L * HashR[len - i + 2] * prime + s[len - i]) % mod1;
            }
        }

        long GetForwardHash(int l, int r)
        {
            if (l == 1) return HashF[r];
            long hash = HashF[r] - 1L * HashF[l - 1] * P[r - l + 1] % mod1;
            if (hash < 0) hash += mod1;
            return hash;
        }
        long GetBackwardHash(int l, int r)
        {
            if (r == len) return HashR[l];
            long hash = HashR[l] - 1L * HashR[r + 1] * P[r - l + 1] % mod1;
            if (hash < 0) hash += mod1;
            return hash;
        }
        bool IsPalin(int l, int r)
        {
            if (r < l) return true;
            return (GetForwardHash(l, r) == GetBackwardHash(l, r));
        }

    };


    static class Palindromic_Tree
    {
        static int mx = 100050;

        class node
        {
            public int[] next = new int[26];
            public int len, sufflink, num, start, end;
            public node() { }
            public node(int len, int sufflink) { this.len = len; this.sufflink = sufflink; }
        };

        static int len, num, suff;
        static string s;
        static node[] tree;
        static List<int>[] adj;

        static bool addLetter(int pos)
        {
            int cur = suff, curlen = 0;
            int let = s[pos] - 'a';

            while (true) {
                curlen = tree[cur].len;
                if (pos - 1 - curlen >= 0 && s[pos - 1 - curlen] == s[pos])
                    break;
                cur = tree[cur].sufflink;
            }
            if (tree[cur].next[let] != 0) {
                suff = tree[cur].next[let];
                tree[suff].num++;
                return false;
            }

            num++;
            suff = num;
            tree[num] = new node()
            {
                len = tree[cur].len + 2,
                end = pos,
                start = pos - tree[cur].len - 1
            };

            tree[cur].next[let] = num;
            if (tree[num].len == 1) {
                tree[num].sufflink = 2;
                tree[num].num = 1;
                return true;
            }

            ++tree[num].num;
            while (true) {
                cur = tree[cur].sufflink;
                curlen = tree[cur].len;
                if (pos - 1 - curlen >= 0 && s[pos - 1 - curlen] == s[pos]) {
                    tree[num].sufflink = tree[cur].next[let];
                    break;
                }
            }
            return true;

        }

        static void initTree()
        {
            tree = new node[mx];
            tree[1] = new node(-1, 1);
            tree[2] = new node(0, 1);
            num = 2; suff = 2;
            adj = new List<int>[mx];
            for (int i = 0; i < mx; i++) adj[i] = new List<int>();
        }

        static void CountOccurences(int p)
        {
            foreach (var item in adj[p]) {
                CountOccurences(item);
                tree[p].num += tree[item].num;
            }
        }

        public static List<Palindrome> palindromic_tree(string txt)
        {
            s = txt;
            len = s.Length;
            mx = len + 20;

            initTree();

            for (int i = 0; i < len; i++) addLetter(i);

            for (int i = 2; i <= num; i++) adj[tree[i].sufflink].Add(i);

            CountOccurences(1);

            List<Palindrome> all = new List<Palindrome>();
            for (int i = 3; i <= num; i++) {
                all.Add(new Palindrome(tree[i].start, tree[i].end, tree[i].num, 0));
            }

            return all;
        }

    }
    class Palindrome
    {
        public int start, end, function;
        public long count;
        public Palindrome() { }
        public Palindrome(int start, int end) : this(start, end, 0, 0) { }
        public Palindrome(int start, int end, long count) : this(start, end, count, 0) { }
        public Palindrome(int start, int end, long count, int function)
        {
            this.start = start; this.end = end; this.count = count; this.function = function;
        }
    }
}