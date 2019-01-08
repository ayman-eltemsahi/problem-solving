using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.IO;

namespace Hackerrank.Bit_Manipulation
{
    public static class Counter_game
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                ulong n = ulong.Parse(Console.ReadLine());
                int r = 0, s = 0;

                for (int i = 63; i >= 0; i--)
                    r += (int)((n >> i) & 1);

                while (((n >> s) & 1) == 0) s++;

                Console.WriteLine((s + r) % 2 == 1 ? "Richard" : "Louise");
            }
        }
    }
    public static class Flipping_bits
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                uint n = uint.Parse(Console.ReadLine());

                // direct solution
                //Console.WriteLine(~n);

                // another solution
                uint k = 0;

                for (int i = 31; i >= 0; i--) {
                    k <<= 1;
                    k += (((n >> i) & 1) ^ 1);
                }
                Console.WriteLine(k);
            }
        }
        static void bin(uint n) {
            for (int i = 31; i >= 0; i--) {
                Console.Write((int)((n >> i) & 1));
            }
            Console.WriteLine();
        }
    }

    public static class AND_product
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                long a = long.Parse(tmp[0]);
                long b = long.Parse(tmp[1]);
                if (a == 0) { Console.WriteLine(0); continue; }
                if (a == b) { Console.WriteLine(a); continue; }

                var aa = DecimalToBinary(a);
                while (aa[0] == 0) aa.RemoveAt(0);
                var bb = DecimalToBinary(b);
                while (bb[0] == 0) bb.RemoveAt(0);


                if (aa.Count < bb.Count) {
                    b = (int)Math.Pow(2, aa.Count) - 1;
                    for (int i = 0; i < bb.Count; i++) {
                        bb[i] = 1;
                    }
                }

                long final = 0;
                for (int i = 0; i < aa.Count; i++) {
                    if (aa[i] + bb[i] == 2) {
                        final += (long)Math.Pow(2, aa.Count - i - 1);
                    } else if (aa[i] + bb[i] == 1) break;
                }
                Console.WriteLine(final);
            }
        }
        static List<int> DecimalToBinary(long x) {
            var bn = new List<int>();
            while (x >= 0) {
                if (x == 0) {
                    bn.Add(0);
                    break;
                }
                bn.Add((int)(x % 2));
                x /= 2;
            }
            bn.Reverse();
            return bn;
        }
    }

    public static class Sansa_and_XOR
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                ulong n = ulong.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);

                int xor = 0;
                for (ulong i = 0; i < n; i++) {
                    ulong tmp = (i + n + i * (n - 1)) % 2;
                    if (tmp == 1) xor ^= arr[i];
                }

                Console.WriteLine(xor);
            }
        }
    }

    public static class Xor_sequence
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                ulong l = ulong.Parse(tmp[0]);
                ulong r = ulong.Parse(tmp[1]);

                ulong x = 0;
                if ((l - r) % 2 == 0) {
                    x ^= xor(0, l - 1);
                    x ^= xor_skip1(l, r);
                } else {
                    x ^= xor_skip1(l + 1, r);
                }
                Console.WriteLine(x);
            }
        }
        static ulong xor(ulong a, ulong b) {
            ulong ab = 0;
            if ((1 + b - a) % 2 == 0) {
                ab += (1 + b - a) / 2 % 2;
            } else if (b % 2 == 0) {
                ab += (b - a) / 2 % 2;
            } else {
                ab += (1 + (b - a) / 2) % 2;
            }
            for (int i = 1; i < 64; i++) {
                double p = (ulong)Math.Pow(2, i);

                ulong low = (ulong)Math.Ceiling(a / p);
                ulong high = (ulong)(b / p);

                ulong one = 0;
                if (low % 2 == 0) { one += (ulong)(p * low) - a; }
                if (high % 2 == 1) { one += 1 + b - (ulong)(p * high); }
                if (one % 2 == 1) { ab += (ulong)p; }
            }
            return ab;
        }
        static ulong xor_skip1(ulong a, ulong b) {
            ulong ab = 0;
            // power of 0
            if (a % 2 == 1) {
                ulong ta = (ulong)Math.Ceiling(b - a + 1 / 2.0);
                ta = (ulong)Math.Ceiling(ta / 2.0);
                ab += (ta % 2);
            }

            ulong one = 0;
            // power of 1
            ulong na = (ulong)Math.Ceiling(b - a + 1 / 2.0);
            na = (ulong)Math.Ceiling(na / 2.0);
            one += na / 2;
            if (na % 2 == 1 && a / 2 % 2 == 1) one++;
            if (one % 2 == 1) ab += 2;

            // the rest
            for (int i = 2; i < 64; i++) {
                double p = Math.Pow(2, i);

                ulong low = (ulong)Math.Ceiling(a / p);
                ulong high = (ulong)(b / p);

                one = 0;
                if (high == low) {
                    if (a == low * p) {
                        if ((a / (ulong)p) % 2 == 1) {
                            one += (2 + b - a) / 2;
                        }
                    } else if (b == high * p) {
                        if ((b / (ulong)p) % 2 == 1) one += (a + 1) % 2;
                        else one += (1 + b - a) / 2;
                    } else {
                        if ((a / (ulong)p) % 2 == 1) one += (1 + (ulong)(low * p) - a) / 2;
                        else {
                            if (a % 2 == 0) one += (2 + b - (ulong)(high * p)) / 2; else one += (1 + b - (ulong)(high * p)) / 2;
                        }
                    }
                } else if (low > high) {
                    if ((a / (ulong)p) % 2 == 1) {
                        one += (2 + b - a) / 2;
                    }
                } else {
                    if ((a / (ulong)p) % 2 == 1) {

                        ulong diff = (ulong)p * low - a;
                        one += diff / 2;
                        if (diff % 2 == 1) one++;
                    }
                    if (high % 2 == 1) {
                        ulong diff = b - (ulong)p * high;
                        one += diff / 2;
                        if (a % 2 == 1 && diff % 2 == 1) one++;
                        if (a % 2 == 0) one++;
                    }
                }
                if (one % 2 == 1) { ab += (ulong)p; }
            }
            return ab;
        }
    }

    public static class Cipher
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            string ciph = Console.ReadLine();

            if (k == 1) { Console.WriteLine(ciph); return; }
            if (n == 1) { Console.WriteLine(ciph[0]); return; }

            int[] ans = new int[n];
            int[] sum = new int[n];

            int ones = 0;
            for (int i = 0; i < Math.Min(n, k); i++) {
                if (ciph[i] - '0' == ones % 2) ans[i] = 0; else { ans[i] = 1; ones++; }
                sum[i] = ans[i]; if (i > 0) sum[i] += sum[i - 1];
            }

            for (int i = k; i < n; i++) {
                int s = sum[i - 1] - sum[i - k];
                if (ciph[i] - '0' == s % 2) ans[i] = 0; else ans[i] = 1;
                sum[i] = sum[i - 1] + ans[i];
            }
            Console.WriteLine(string.Join("", ans));
        }
    }

    public static class Xoring_Ninja
    {
        public static void Start() {
            /* if you check, you'll find that there is a patten the result is equal to 
                        pow(2,n-1) * k  
                    where k is | operator applied to all numbers of the array                        
             */
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                string[] tmp = Console.ReadLine().Split(' ');

                int k = 0;
                for (int i = 0; i < n; i++) {
                    int a = int.Parse(tmp[i]);
                    k |= a;
                }

                BigInteger sum = k * BigInteger.Pow(2, n - 1);
                Console.WriteLine(sum % 1000000007);
            }
        }
    }

    public static class Manipulative_Numbers
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[][] k = new int[n][];
            for (int i = 0; i < n; i++) k[i] = new int[32];

            int top = 0;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < 32; j++) {
                    if (((arr[i] >> j) & 1) == 1) {
                        top = Math.Max(top, j);
                        k[i][j]++;
                    }
                }
            }

            for (int i = 0; i < n; i++) {
                for (int j = top - 1; j >= 0; j--) {
                    if (k[i][j] != k[i][j + 1]) k[i][j] = 2;
                }
            }

            for (int i = 31; i >= 0; i--) {
                int one = 0, zero = 0;
                for (int j = 0; j < n; j++) {
                    if (k[j][i] == 1) one++; else if (k[j][i] == 0) zero++;
                }
                if (one <= n / 2 && zero <= n / 2) { Console.WriteLine(i); break; }
            }
        }
    }

    public static class XOR_key
    {
        static int xor_key;
        public static void Start() {
            var sb = new StringBuilder();
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int q = int.Parse(tmp[1]);
                int[] A = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);

                int lr = 20;

                trie root = new trie(0, 0);
                for (int i = 0; i < n; i++) root.insert(A[i], lr, i);

                for (int i = 0; i < q; i++) {
                    tmp = Console.ReadLine().Split(' ');
                    xor_key = int.Parse(tmp[0]);
                    int l = int.Parse(tmp[1]) - 1;
                    int r = int.Parse(tmp[2]) - 1;

                    int cur = root.getMax(~xor_key, lr, l, r);
                    sb.Append(cur).Append("\n");
                }
            }
            sb.Length--;
            Console.WriteLine(sb.ToString());
        }
        class trie
        {
            int s;
            trie zero, one;
            List<int> min, max;
            public trie(int s, int m) { this.s = s; min = new List<int>(); max = new List<int>(); }
            public void insert(int num, int i, int p) {
                if (max.Count > 0 && max[max.Count - 1] == p - 1) max[max.Count - 1] = p;
                else { min.Add(p); max.Add(p); }

                if (i == 0) return;
                if (((num >> (i - 1)) & 1) == 1) {
                    if (one == null) one = new trie(1, p);
                    one.insert(num, i - 1, p);
                } else {
                    if (zero == null) zero = new trie(0, p);
                    zero.insert(num, i - 1, p);
                }
            }

            public int getMax(int num, int i, int l, int r) {
                int ans = (s ^ ((xor_key >> (i)) & 1)) * (1 << i);
                if (i == 0) return ans;

                if (zero == null) ans += one.getMax(num, i - 1, l, r);
                else if (one == null) ans += zero.getMax(num, i - 1, l, r);
                else {
                    if (((num >> (i - 1)) & 1) == 1) {
                        if (one.overlap(l, r)) ans += one.getMax(num, i - 1, l, r);
                        else ans += zero.getMax(num, i - 1, l, r);
                    } else {
                        if (zero.overlap(l, r)) ans += zero.getMax(num, i - 1, l, r);
                        else ans += one.getMax(num, i - 1, l, r);
                    }
                }
                return ans;
            }

            bool overlap(int l, int r) {
                int k = min.BinarySearch(l);
                if (k < 0) k = ~k; else return true;

                if (k > 0 && min[k - 1] <= r && max[k - 1] >= l) return true;
                if (k < min.Count && min[k] <= r && max[k] >= l) return true;
                return false;
            }
        }
    }

    public static class Twos_complement
    {
        public static void Start() {
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int A = int.Parse(tmp[0]);
                int B = int.Parse(tmp[1]);
                long l = 0;

                if (A >= 0) {
                    l = countOnes_0_To_n(B) - countOnes_0_To_n(A - 1);
                } else if (B < 0) {
                    l = countOnes_0_To_n(B + (1 << 31)) - countOnes_0_To_n((A + (1 << 31)) - 1) + ((long)B - (long)A + 1L);
                } else {
                    l = countOnes_0_To_n(B) + countOnes_0_To_n(int.MaxValue) - countOnes_0_To_n(A + (1 << 31) - 1) - (long)A;
                }

                Console.WriteLine(l);
            }
        }
        static long countOnes_0_To_n(int n, int k = 31) {
            if (n < 0) return 0;
            if (k == 0) return (n & 1);
            long r = 0;

            int s = n / (1 << (k));

            r += Math.BigMul(s, 1 << k) / 2;
            if (((n >> k) & 1) == 1) r += n - s * (1 << (k)) + 1;
            r += countOnes_0_To_n(n, k - 1);
            return r;
        }
    }

    public static class Changing_Bits
    {
        public static void Start() {
            StringBuilder sb = new StringBuilder();
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int q = int.Parse(tmp[1]);

            int i;
            int[] A = new int[n + 1], B = new int[n + 1];

            var line = Console.ReadLine();
            for (i = n - 1; i >= 0; i--) if (line[i] == '1') { A[n - 1 - i]++; }
            line = Console.ReadLine();
            for (i = n - 1; i >= 0; i--) if (line[i] == '1') { A[n - 1 - i]++; }

            SegmentTree tree = new SegmentTree(n + 1, A, B);
            while (q-- > 0) {
                tmp = Console.ReadLine().Trim().Split(' ');

                if (tmp[0] == "set_a") {
                    i = int.Parse(tmp[1]);
                    A[i] = int.Parse(tmp[2]);
                    tree.update(i, A[i] + B[i]);
                } else if (tmp[0] == "set_b") {
                    i = int.Parse(tmp[1]);
                    B[i] = int.Parse(tmp[2]);
                    tree.update(i, A[i] + B[i]);
                } else {
                    sb.Append(get_ChangingBits(tree, A, B, int.Parse(tmp[1]), n));
                }
            }

            Console.WriteLine(sb.ToString());
        }
        static int get_ChangingBits(SegmentTree tree, int[] A, int[] B, int p, int n) {
            if (p > n) return 0;
            int c = A[p] + B[p];
            if (p == 0) return c & 1;

            if (tree.RangeMax(0, p - 1) != 2) return c & 1;
            if (tree.RangeMin(0, p - 1) != 0) return ++c & 1;

            int far, near, mid, min;

            far = 0; near = p - 1;
            while (near - far > 1) {
                mid = (near + far) / 2;
                min = tree.RangeMin(mid, near);
                if (min == 0) far = mid; else near = mid;
            }

            if (A[near] + B[near] == 0) far = near;
            if (far > p - 2) return c & 1;

            if (tree.RangeMax(far, p - 1) == 2) c++;

            return c & 1;
        }
        class SegmentTree
        {
            delegate int compare(int a, int b);

            int sz, n, ar;
            int[] Min, Max;

            public SegmentTree(int n1, int[] A, int[] B) {
                sz = (int)(Math.Ceiling(Math.Log(n1, 2)));
                n = 1 << sz;
                sz = (2 * (int)Math.Pow(2, sz)) - 1;

                ar = 0;
                while (2 * ar + 1 < sz) ar = 2 * ar + 1;

                Min = new int[sz];
                Max = new int[sz];

                for (int i = 0; i < sz; i++) {
                    Min[i] = int.MaxValue;
                    Max[i] = int.MinValue;
                }

                BuildTree(A, B, Min, 0, n - 1, 0, (a, b) => Math.Min(a, b), n1, int.MaxValue);
                BuildTree(A, B, Max, 0, n - 1, 0, (a, b) => Math.Max(a, b), n1, int.MinValue);
            }

            int BuildTree(int[] A, int[] B, int[] segA, int fr, int to, int seg, compare comp, int n, int bad) {
                if (fr == to) {
                    segA[seg] = fr >= n ? bad : A[fr] + B[fr];
                } else {
                    int mid = (to + fr) / 2;
                    segA[seg] = comp(BuildTree(A, B, segA, fr, mid, 2 * seg + 1, comp, n, bad),
                                     BuildTree(A, B, segA, mid + 1, to, 2 * seg + 2, comp, n, bad));
                }
                return segA[seg];
            }

            public int RangeMin(int l, int r) {
                return RangeQuery(l, r, 0, n - 1, 0, (a, b) => Math.Min(a, b), true);
            }

            public int RangeMax(int l, int r) {
                return RangeQuery(l, r, 0, n - 1, 0, (a, b) => Math.Max(a, b), false);
            }

            int RangeQuery(int l, int r, int fr, int to, int seg, compare comp, bool min) {
                if (fr >= l && to <= r) return min ? Min[seg] : Max[seg];

                if (fr > r || to < l) return min ? int.MaxValue : int.MinValue;

                int mid = fr + (to - fr) / 2;
                return comp(RangeQuery(l, r, fr, mid, 2 * seg + 1, comp, min),
                            RangeQuery(l, r, mid + 1, to, 2 * seg + 2, comp, min));
            }

            public void update(int i, int k) {
                i += ar;
                Min[i] = k; Max[i] = k;
                updateOne((i - 1) / 2, k);
            }

            void updateOne(int i, int k) {
                Min[i] = Math.Min(Min[2 * i + 1], Min[2 * i + 2]);
                Max[i] = Math.Max(Max[2 * i + 1], Max[2 * i + 2]);
                if (i > 0) updateOne((i - 1) / 2, k);
            }
        }
    }
}
