using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.IO;
using System.Threading;

namespace Hackerrank.Strings
{
    public static class Richie_Rich
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]), k = int.Parse(tmp[1]);
            string left = Console.ReadLine();
            if (n == 1) {
                if (k > 0) left = "9";
                Console.WriteLine(left);
                return;
            }
            string right = new string(left.Reverse().ToArray()).Substring(0, n / 2);
            string middle = "";
            if (n % 2 == 1) { middle = left[n / 2].ToString(); }
            left = left.Substring(0, n / 2);

            var changedl = new List<int>();
            var changedr = new List<int>();
            StringBuilder sl = new StringBuilder(left), sr = new StringBuilder(right);

            left = null; right = null;

            int i = 0;
            while (i < n / 2 && k > 0) {
                if (sl[i] != sr[i]) {
                    k--;
                    if (sl[i] > sr[i]) {
                        sr[i] = sl[i];
                        changedr.Add(i);
                    } else {
                        sl[i] = sr[i];
                        changedl.Add(i);
                    }
                }
                i++;
            }

            if (!sl.Equals(sr)) { Console.WriteLine(-1); } else {

                i = 0;
                while (i < n / 2 && k > 0) {
                    if (int.Parse(sl[i].ToString()) < 9) {
                        if (changedr.Contains(i) || changedl.Contains(i)) k++;
                        if (k >= 2) {
                            sl[i] = '9';
                            sr[i] = '9';
                            k -= 2;
                        }
                    }
                    i++;
                }
                if (middle != "" && middle != "9" && k > 0) middle = "9";
                Console.WriteLine(sl.ToString() + middle + new string(sr.ToString().Reverse().ToArray()));
            }
        }
    }

    public static class Common_Child
    {
        public static void Start() {
            string a = Console.ReadLine();
            string b = Console.ReadLine();

            int n = a.Length, m = b.Length;

            int[,] map = new int[n + 1, m + 1];

            for (int i = 1; i <= n; i++) {
                for (int j = 1; j <= m; j++) {

                    if (a[i - 1] == b[j - 1]) {
                        map[i, j] = map[i - 1, j - 1] + 1;
                    } else {
                        map[i, j] = Math.Max(map[i - 1, j], map[i, j - 1]);
                    }

                }
            }
            Console.WriteLine(map[n, m]);
        }
    }

    public static class Two_Two
    {
        public static void Start() {
            string[] l2 = new string[200], l4 = new string[200], l6 = new string[200], l8 = new string[200];
            int i2 = 0, i4 = 0, i6 = 0, i8 = 0;

            BigInteger bi = 1;
            for (int i = 1; i < 801; i++) {
                bi *= 2;
                string g = new string(bi.ToString().Reverse().ToArray());
                switch (g[0]) {
                    case '2': l2[i2++] = g; break;
                    case '4': l4[i4++] = g; break;
                    case '6': l6[i6++] = g; break;
                    case '8': l8[i8++] = g; break;
                    default:
                        break;
                }
            }

            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                string line = new string(Console.ReadLine().Reverse().ToArray());
                int n = line.Length;

                int count = 0;
                for (int i = 0; i < n - 1; i++) {
                    switch (line[i]) {
                        case '1': count++; break;
                        case '3':
                        case '5':
                        case '7':
                        case '9': break;
                        case '2': if (line[i + 1] % 2 == 1) checkS(l2, line, i, n, ref count); else count++; break;
                        case '4': if (line[i + 1] % 2 == 0) checkS(l4, line, i, n, ref count); else count++; break;
                        case '6': if (line[i + 1] % 2 == 1) checkS(l6, line, i, n, ref count); break;
                        case '8': if (line[i + 1] % 2 == 0) checkS(l8, line, i, n, ref count); else count++; break;
                        default: break;
                    }
                }
                switch (line[n - 1]) {
                    case '1':
                    case '2':
                    case '4':
                    case '8': count++; break;
                    default: break;
                }
                Console.WriteLine(count);
            }
        }
        static void checkS(string[] L, string line, int i, int n, ref int count) {
            for (int j = 0; j < 200; j++) {
                string tmp = L[j];
                int len = tmp.Length;
                if (n - i >= len) {
                    bool ch = true;
                    for (int k = 1; k < len; k++) {
                        if (line[i + k] != tmp[k]) { ch = false; break; }
                    }
                    if (ch) count++;
                }
            }
        }
    }

    public static class Sherlock_and_Valid_String
    {
        public static void Start() {
            string line = Console.ReadLine();
            int[] arr = new int[26];
            foreach (var c in line) arr[c - 'a']++;

            arr = new List<int>(Enumerable.Range(0, 26)
                                .Where(x => arr[x] != 0)
                                .Select(x => arr[x]))
                                .OrderBy(x => x)
                                .ToArray();

            int n = arr.Length, diff = 0;

            for (int i = 1; i < n; i++) { if (arr[i] != arr[i - 1]) diff++; }

            Console.WriteLine((arr.Length == 1 || diff == 0 || (arr[0] == 1 && arr[1] != 1) ||
                               (diff == 1 && (arr[n - 1] - arr[n - 2] == 1))) ?
                              "YES" : "NO");
        }
    }

    public static class Bear_and_Steady_Gene
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            string s = Console.ReadLine();
            n = s.Length;

            Console.WriteLine(makeSteady(s, n).ToString().PadLeft(50, ' '));
        }

        static int makeSteady(string bear, int n) {
            int[] A = new int[n], C = new int[n], G = new int[n], T = new int[n];
            int a = 0, c = 0, g = 0, t = 0;

            switch (bear[0]) {
                case 'A': a++; A[0] = 1; break;
                case 'C': c++; C[0] = 1; break;
                case 'G': g++; G[0] = 1; break;
                case 'T': t++; T[0] = 1; break;
                default: break;
            }

            for (int l = 1; l < n; l++) {
                switch (bear[l]) {
                    case 'A': a++; A[l]++; ContinueSumArray(A, C, G, T, l); break;
                    case 'C': c++; C[l]++; ContinueSumArray(A, C, G, T, l); break;
                    case 'G': g++; G[l]++; ContinueSumArray(A, C, G, T, l); break;
                    case 'T': t++; T[l]++; ContinueSumArray(A, C, G, T, l); break;
                    default: break;
                }
            }

            a = Math.Max(0, a - n / 4); c = Math.Max(0, c - n / 4); g = Math.Max(0, g - n / 4); t = Math.Max(0, t - n / 4);

            int cnt = a + c + g + t;
            int cnt2 = (a > 0 ? 1 : 0) + (c > 0 ? 1 : 0) + (g > 0 ? 1 : 0) + (t > 0 ? 1 : 0);
            if (cnt == 0) return 0; else if (cnt == 1) return 1;

            List<int> AJ = new List<int>(), CJ = new List<int>(), GJ = new List<int>(), TJ = new List<int>();

            if (A[0] == 1) AJ.Add(0); else if (C[0] == 1) CJ.Add(0); else if (G[0] == 1) GJ.Add(0); else TJ.Add(0);
            for (int k = 1; k < n; k++) {
                if (A[k] > A[k - 1]) AJ.Add(k);
                else if (C[k] > C[k - 1]) CJ.Add(k);
                else if (G[k] > G[k - 1]) GJ.Add(k);
                else TJ.Add(k);
            }

            int[] AA = AJ.ToArray(), CC = CJ.ToArray(), GG = GJ.ToArray(), TT = TJ.ToArray();
            int AAL = AA.Length, CCL = CC.Length, GGL = GG.Length, TTL = TT.Length;
            AJ = CJ = GJ = TJ = null;

            int final = n;
            for (int k = 0; k < n - cnt; k++) {
                int max = 0, tmpcnt = cnt2;
                CheckCharacter(n, a, AA, AAL, k, ref max, ref tmpcnt);
                CheckCharacter(n, c, CC, CCL, k, ref max, ref tmpcnt);
                CheckCharacter(n, g, GG, GGL, k, ref max, ref tmpcnt);
                CheckCharacter(n, t, TT, TTL, k, ref max, ref tmpcnt);

                if (tmpcnt == 0 && max > 0) final = Math.Min(final, max);
            }
            return final;
        }

        static void ContinueSumArray(int[] A, int[] C, int[] G, int[] T, int l) {
            A[l] += A[l - 1]; C[l] += C[l - 1]; G[l] += G[l - 1]; T[l] += T[l - 1];
        }

        static void CheckCharacter(int n, int a, int[] AA, int AAL, int k, ref int max, ref int tmpcnt) {
            if (a > 0) {
                int i = -1, tmp = k;
                while (i < 0 && tmp < n) { i = Array.BinarySearch(AA, tmp); tmp++; }
                if (i > -1 && i + a - 1 < AAL) { max = Math.Max(max, AA[i + a - 1] - k + 1); tmpcnt--; }
            }
        }
    }

    public static class Reverse_Shuffle_Merge
    {
        public static void Start() {
            const string alph = "abcdefghijklmnopqrstuvwxyz";
            string s = new string(Console.ReadLine().Reverse().ToArray()), final = "";

            int n = s.Length;
            int[] ltrs = new int[26];
            foreach (var c in s) ltrs[alph.IndexOf(c)]++;

            Dictionary<char, int> take = new Dictionary<char, int>(), skip = new Dictionary<char, int>();

            for (int y = 0; y < 26; y++) if (ltrs[y] > 0) { take.Add(alph[y], ltrs[y] / 2); skip.Add(alph[y], ltrs[y] / 2); }
            char[] keys = take.Keys.ToArray();

            for (int i = 0; i < n; i++) {
                if (final.Length == n / 2) break;
                int t = 0;
                while (take[keys[t]] == 0) t++;

                if (s[i] == keys[t]) { final += s[i]; take[s[i]]--; } else {
                    if (skip[s[i]] > 0) {
                        char cont = s[i];
                        Dictionary<char, int> dp = duplicateDic(skip);
                        for (int j = i + 1; j < n; j++) {
                            dp[s[j - 1]]--;
                            if (take[s[j]] == 0) continue;
                            if (s[j] == keys[t]) { cont = s[j]; break; }
                            if (dp[s[j]] == 0) { if (s[j] < cont) cont = s[j]; break; }
                            if (s[j] < cont) cont = s[j];
                        }
                        if (cont >= s[i] && take[s[i]] > 0) { final += s[i]; take[s[i]]--; } else skip[s[i]]--;
                    } else {
                        final += s[i]; take[s[i]]--;
                    }
                }
            }

            Console.WriteLine(final);
        }

        static Dictionary<char, int> duplicateDic(Dictionary<char, int> skip) {
            var c = new Dictionary<char, int>();
            foreach (var key in skip.Keys) c.Add(key, skip[key]);
            return c;
        }
    }

    public static class Build_a_String
    {
        static Dictionary<int, List<int>> w;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int A = int.Parse(tmp[1]);
                int B = int.Parse(tmp[2]);

                string line = Console.ReadLine();

                int[] map = new int[26];
                for (int i = 0; i < 26; i++) map[i] = n + 1;
                for (int i = 0; i < n; i++) map[line[i] - 'a'] = Math.Min(i, map[line[i] - 'a']);

                w = new Dictionary<int, List<int>>();
                for (int i = 0; i < n - 1; i++) {
                    int m = line[i] * 100 + line[i + 1];
                    if (w.ContainsKey(m)) w[m].Add(i); else w[m] = new List<int> { i };
                }

                var dict = new Dictionary<int, long>();
                dict[n] = 0;

                for (int i = n - 1; i >= 0; i--) {
                    BUILD(dict, line, n, map, A, B, i);
                }
                long ans = dict[0];
                Console.WriteLine(ans);
            }
        }

        static void BUILD(Dictionary<int, long> dict, string line, int n, int[] map, int A, int B, int k) {
            dict[k] = A + dict[k + 1];

            int b = 0;
            if (k < n - 1) {
                foreach (var c in w[line[k] * 100 + line[k + 1]]) {
                    if (c + 1 >= k) break;

                    int b2 = c + 2; int k2 = k + 2;
                    for (; b2 < k && k2 < n; b2++, k2++) {
                        if (line[b2] != line[k2]) break;
                    }
                    b = Math.Max(b, b2 - c);
                }
            }

            if (b == 0) {
                if (map[line[k] - 'a'] < k) b++;
            }

            if (b != 0) dict[k] = Math.Min(dict[k], dict[k + b] + B);
        }
    }

    public static class Yet_Another_KMP_Problem
    {
        public static void Start() {
            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int mini = -1, min = int.MaxValue, first = -1, second = -1;
            for (int i = 0; i < 26; i++) {
                if (A[i] == 0) continue;
                if (first == -1) first = i; else if (second == -1) second = i;
                if (A[i] < min) { mini = i; min = A[i]; }
            }

            var sb = new StringBuilder();
            sb.Append((char)(mini + 'a'));
            A[mini]--;

            if (mini == first && second != -1) {
                while (A[first] > 0) {
                    sb.Append((char)(first + 'a')).Append((char)(second + 'a'));
                    A[first]--; A[second]--;
                }
            }

            for (int i = 0; i < 26; i++) sb.Append("".PadLeft(A[i], (char)(i + 'a')));
            Console.WriteLine(sb.ToString());
        }
    }

    public static class String_Similarity
    {
        public static void Start() {
            int t = int.Parse(Console.ReadLine());
            while (t-- > 0) {
                var s = Console.ReadLine().Trim();
                int n = s.Length;
                long[] z = new long[n];
                int L = 0, R = 0;
                for (int i = 1; i < n; i++) {
                    if (i > R) {
                        L = R = i;
                        while (R < n && s[R - L] == s[R]) R++;
                        z[i] = R - L; R--;
                    } else {
                        int k = i - L;
                        if (z[k] < R - i + 1) z[i] = z[k];
                        else {
                            L = i;
                            while (R < n && s[R - L] == s[R]) R++;
                            z[i] = R - L; R--;
                        }
                    }
                }
                Console.WriteLine(n + z.Sum());
            }
        }
    }

    public static class Ashton_and_String
    {
        public static void Start() {
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                string str = Console.ReadLine();
                int k = int.Parse(Console.ReadLine());
                var suffixArr = Library.LCP.buildSuffixArray(str, str.Length);
                var lcp = Library.LCP.kasai(str, suffixArr);
                Console.WriteLine(getKth(str, suffixArr, lcp, k));
            }
        }
        static char getKth(string str, List<int> suffixArr, List<int> lcp, int k) {
            --k;
            int n = str.Length, a, b;

            int i = 0;
            while (i < n) {
                a = suffixArr[i]; b = a + (i > 0 ? lcp[i - 1] : 0);
                while (b < n) {
                    if (k < b - a + 1) return str[a + k];
                    k -= b - a + 1;
                    b++;
                }
                i++;
            }

            return ' ';
        }
    }

    public static class String_Function_Calculation
    {
        public static void Start() {
            var line = Console.ReadLine();
            int n = line.Length;

            var suffixArray = Library.LCP.buildSuffixArray(line, n);
            var lcp = Library.LCP.kasai(line, suffixArray);

            int i = 0, mx = n;
            Stack<int> S = new Stack<int>();

            while (i < n) {
                if (S.Count == 0 || lcp[S.Peek()] <= lcp[i]) {
                    S.Push(i++);
                } else {
                    mx = Math.Max(mx, lcp[S.Pop()] * (S.Count == 0 ? i : i - S.Peek()));
                }
            }

            while (S.Count > 0) {
                mx = Math.Max(mx, lcp[S.Pop()] * (S.Count == 0 ? i : i - S.Peek()));
            }

            Console.WriteLine(mx);
        }
    }

    public static class Find_Strings
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            string[] arr = new string[n];
            for (int i = 0; i < n; i++) {
                arr[i] = Console.ReadLine();
            }

            int[] lastN = new int[arr.Sum(x => x.Length)];
            var suf = Library.LCP.buildSuffixArrayForMany(arr, lastN);

            var str = arr.Aggregate("", (x, y) => x + y);
            var lcp = Library.LCP.buildLCPArrayForMany(arr, suf, lastN);

            int q = int.Parse(Console.ReadLine());
            for (int i = 0; i < q; i++) {
                int k = int.Parse(Console.ReadLine());
                Console.WriteLine(getKth(str, k, suf, lcp, lastN));
            }
        }
        static string getKth(string str, int k, int[] suffix, int[] lcp, int[] lastN) {
            int n = str.Length, ni, a, b, i = 0;

            while (i < n) {
                a = suffix[i]; b = a + (i > 0 ? lcp[i - 1] : 0);
                ni = lastN[i];
                if (k > ni - b) k -= (ni - b);
                else {
                    while (k > 1) { b++; k--; }
                    return str.Substring(a, b - a + 1);
                }
                i++;
            }
            return "INVALID";
        }
    }

    public static class Palindromic_Border
    {
        const int MOD = 1000000007;
        public static void Start() {
            Thread t = new Thread(() => {
                var txt = Console.ReadLine();
                var tree = Library.Palindromic_Tree.palindromic_tree_num(txt);
                long ans = 0;
                foreach (var item in tree) {
                    ans += one23(item - 1);
                    ans %= MOD;
                }
                Console.WriteLine(ans);
            }, 100000000);
            t.Start();
        }
        static long one23(long n) {
            return (n * (n + 1)) / 2;
        }
    }

    public static class Square_Subsequences
    {
        const int MOD = 1000000007;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                string l = Console.ReadLine();
                int ln = l.Length, ans = 0;

                for (int i = 0; i < ln; i++) {
                    for (int j = i + 1; j < ln; j++) {
                        if (l[i] != l[j]) continue;

                        string top = l.Substring(i, j - i);
                        string bottom = l.Substring(j);

                        ans += Common_Sequences(top, bottom);
                        ans %= MOD;
                    }
                }
                Console.WriteLine(ans);
            }
        }
        static int Common_Sequences(string top, string bottom) {
            int n = top.Length, m = bottom.Length;
            int[][] w = new int[n + 1][];
            for (int i = 0; i <= n; i++) w[i] = new int[m + 1];

            // 2 is because it's missing the first letter
            for (int i = 2; i <= n; ++i) {
                for (int j = 2; j <= m; ++j) {
                    if (top[i - 1] == bottom[j - 1]) {
                        w[i][j] = w[i][j - 1] + w[i - 1][j] + 1;
                    } else {
                        w[i][j] = w[i][j - 1] + w[i - 1][j] - w[i - 1][j - 1];
                    }
                    w[i][j] %= MOD;
                }

            }
            // 1 is because it was missing the first letter
            return 1 + w[n][m];
        }
    }
}
