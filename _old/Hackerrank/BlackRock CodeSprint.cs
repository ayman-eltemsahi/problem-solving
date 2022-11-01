using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hackerrank.BlackRock_CodeSprint
{
    public static class Currency_Arbitrage
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            while (n-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                decimal a = decimal.Parse(tmp[0]);
                decimal b = decimal.Parse(tmp[1]);
                decimal c = decimal.Parse(tmp[2]);

                decimal orig = 100000;
                orig /= a;
                orig /= b;
                orig /= c;

                orig -= 100000;

                Console.WriteLine(Math.Max(0, Math.Floor(orig)));
            }
        }
    }

    public static class Fixed_Income_Security_Trade_Allocation
    {
        public static void Start() {
            int t = int.Parse(Console.ReadLine());
            var tmp = Console.ReadLine().Split(' ');
            decimal MTZ = decimal.Parse(tmp[0]);
            decimal increment = decimal.Parse(tmp[1]);
            decimal units = decimal.Parse(tmp[2]);

            string[] ids = new string[t]; decimal[] orders = new decimal[t];
            var dic = new Dictionary<string, decimal>();

            decimal total_order = 0;
            for (int i = 0; i < t; i++) {
                tmp = Console.ReadLine().Split(' ');
                decimal a = decimal.Parse(tmp[1]);
                dic.Add(tmp[0], a);
                total_order += a;
            }

            // sorting 
            List<KeyValuePair<string, decimal>> lst = dic.ToList();
            lst.Sort(
                delegate (KeyValuePair<string, decimal> p1, KeyValuePair<string, decimal> p2)
                {
                    if (p1.Value == p2.Value) return p1.Key.CompareTo(p2.Key); else return p1.Value.CompareTo(p2.Value);
                }
            );

            dic = lst.ToDictionary(x => x.Key, y => y.Value);
            var ky = dic.Keys.ToArray();
            for (int i = 0; i < t; i++) {
                orders[i] = dic[ky[i]];
                ids[i] = ky[i];
            }

            decimal allocated = 0;

            for (int i = 0; i < t; i++) {
                allocated = 0;
                decimal p_a = (units * orders[i]) / total_order;

                if (p_a < MTZ) {
                    if (p_a > (MTZ / 2)) {
                        // allocation minimum trading tize
                        if (is_tradeable(orders[i] - MTZ, MTZ, increment)) allocated = MTZ;
                    }
                } else {
                    if (p_a >= orders[i]) {
                        // allocate portfolio order
                        allocated = orders[i];
                    } else {
                        if (!is_tradeable(p_a, MTZ, increment)) {
                            // round it down to the closest t a
                            p_a = roundToTradaeble(p_a, orders[i], MTZ, increment);
                        }
                        allocated = p_a;
                    }

                }
                dic[ids[i]] = allocated;
                // after recalculate total order
                total_order -= orders[i];
                units -= allocated;
            }

            dic = dic.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
            foreach (var k in dic.Keys) {
                Console.WriteLine("{0} {1}", k, (int)dic[k]);
            }
        }
        static bool is_tradeable(decimal amount, decimal mts, decimal increment) {
            if (amount == 0) return true;
            amount -= mts;
            if (amount < 0) return false;

            return amount % increment == 0;
        }
        static decimal roundToTradaeble(decimal amount, decimal order, decimal mts, decimal increment) {
            int w = 0;
            decimal am = amount;
            while (!is_tradeable(order - am, mts, increment)) {
                am = amount;
                am -= mts;
                if (am < 0) return 0;
                decimal low = Math.Floor(am / increment);
                am = mts + increment * (low - w);
                w++;
            }
            return amount;
        }
    }

    public static class Suggest_Better_Spending_Rates
    {
        static int sbpr_t, sbpr_p, sbpr_summation; static decimal sbpr_r;
        static Tuple<decimal, string>[] sbpr_res; static int[] sbpr_lower, sbpr_upper, sbpr_sent;
        public static void Start() {
            sbpr_res = new Tuple<decimal, string>[3];
            sbpr_res[0] = Tuple.Create((decimal)-1, ""); sbpr_res[1] = Tuple.Create((decimal)-1, ""); sbpr_res[2] = Tuple.Create((decimal)-1, "");
            var tmp = Console.ReadLine().Split(' ');
            sbpr_p = int.Parse(tmp[0]);
            sbpr_r = (decimal.Parse(tmp[1])) / 100; sbpr_r++;
            sbpr_t = int.Parse(tmp[2]);
            int threshold = int.Parse(tmp[3]);

            int[] times = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            sbpr_summation = times.Sum();

            sbpr_lower = new int[sbpr_t]; sbpr_upper = new int[sbpr_t]; sbpr_sent = new int[sbpr_t];
            for (int i = 0; i < sbpr_t; i++) {
                int aq = times[i] - threshold;
                if (aq < 0) sbpr_lower[i] = 0; else if (aq > 100) sbpr_lower[i] = 100; else sbpr_lower[i] = aq;
                sbpr_upper[i] = Math.Min(100, times[i] + threshold);
            }


            Console.WriteLine(Math.Round(sbpr_p * income(times), 3));

            Thread gt = new Thread(abcd, 1000000000);
            gt.Start();
        }
        static void abcd() {
            perm(0, 0);

            for (int i = 2; i >= 0; i--) {
                if (sbpr_res[i].Item2 == "") continue;
                Console.WriteLine("{0} - {1}", Math.Round(sbpr_p * sbpr_res[i].Item1, 3), sbpr_res[i].Item2);
            }
        }
        static void perm(int cur, int sum) {
            if (cur == sbpr_t) {
                if (sbpr_summation != sum) return;

                var ab = income(sbpr_sent);
                if (ab > sbpr_res[0].Item1) {
                    sbpr_res[0] = Tuple.Create(ab, string.Join(" ", sbpr_sent));
                    Array.Sort(sbpr_res, (x, y) => x.Item1.CompareTo(y.Item1));
                }
            } else {
                for (int j = sbpr_lower[cur]; j <= sbpr_upper[cur]; j++) {
                    sbpr_sent[cur] = j;
                    if (sum + j > sbpr_summation) return;
                    perm(cur + 1, sum + j);
                }
            }
        }
        static decimal income(int[] times) {
            try {
                int more = times.Length;
                int st = 0;
                decimal abc = 1;
                while (times[st] == 0) {
                    st++;
                    if (st >= more) { return 0; }
                    abc *= sbpr_r;
                }
                if (st >= more) return 0;
                abc *= sbpr_r * times[st] / 100;
                decimal fin = abc;
                int before = times[st];
                for (int tt = st + 1; tt < sbpr_t; tt++) {
                    if (times[tt] == 0) { abc *= sbpr_r; } else {

                        abc /= (100 * before);
                        abc *= sbpr_r * times[tt] * (100 - before);
                        before = times[tt];
                        fin += abc;
                    }
                }
                return fin;
            } catch (IndexOutOfRangeException e) {
                return 0;
            }
        }
    }

    public static class Portfolio_Manager
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            string[] arr = Console.ReadLine().Split(' ');


            var adj = new HashSet<int>[4 * n];
            for (int i = 0; i < 4 * n; i++) adj[i] = new HashSet<int>();
            decimal[] values = new decimal[4 * n];
            int j = 0;

            Queue<int> Q = new Queue<int>();
            values[0] = decimal.Parse(arr[0]); ;
            Q.Enqueue(0);

            int g = 1, rem = 1;
            int len = arr.Length;
            while (Q.Count > 0) {
                int pnt = Q.Dequeue();

                string left = g < len ? arr[g++] : "#", right = g < len ? arr[g++] : "#";
                if (left == "#" && right == "#") {
                } else if (left == "#") {
                    decimal r = decimal.Parse(right);
                    adj[j].Add(rem); values[rem] = r;
                    Q.Enqueue(rem++);
                } else if (right == "#") {
                    decimal l = decimal.Parse(left);
                    adj[j].Add(rem); values[rem] = l;
                    Q.Enqueue(rem++);
                } else {
                    decimal l = decimal.Parse(left);
                    adj[j].Add(rem); values[rem] = l; Q.Enqueue(rem++);
                    decimal r = decimal.Parse(right);
                    adj[j].Add(rem); values[rem] = r; Q.Enqueue(rem++);
                }

                j++;
            }

            decimal[] sum = new decimal[4 * n];
            for (int i = rem + 1; i >= 0; i--) {
                sum[i] = dfs(adj, values, sum, i);
            }
            Console.WriteLine(sum[0]);
        }
        static decimal dfs(HashSet<int>[] adj, decimal[] values, decimal[] sum, int i) {
            decimal all = 0;
            decimal under = 0;
            foreach (var item in adj[i]) {
                all += sum[item];
                foreach (var item2 in adj[item]) {
                    under += sum[item2];
                }
            }
            return Math.Max(all, values[i] + under);
        }
    }

    public static class Employee_Stock_Grants
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] rating = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int[] mini = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            var map = new Dictionary<int, int>();
            var map2 = new Dictionary<int, int>();
            for (int i = 0; i < n; i++) { map.Add(i, rating[i]); map2.Add(i, mini[i]); }

            map = map.OrderBy(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

            var keys = map.Keys.ToArray();

            var given = new Dictionary<int, int>();
            decimal sum = map2[keys[0]];
            given.Add(keys[0], map2[keys[0]]);

            for (int i = 1; i < n; i++) {
                int key = keys[i], val = map[key], min = map2[key];
                int tmp = Math.Max(min, 1 + getbef(keys, key, given, n, map, val));
                given.Add(key, tmp);
                sum += tmp;
            }
            for (int i = 0; i < n; i++) {
                Console.Write(given[i] + " ");
            }
            Console.WriteLine();
            Console.WriteLine(sum);
        }
        static int getbef(int[] keys, int i, Dictionary<int, int> given, int n, Dictionary<int, int> map, int val) {
            int r = 0;
            for (int j = Math.Max(0, i - 10); j < i; j++) {
                if (map[j] < val) {
                    r = Math.Max(r, given[j]);
                }
            }
            for (int j = i + 1; j < Math.Min(n, i + 11); j++) {
                if (map[j] < val) {
                    r = Math.Max(r, given[j]);
                }
            }

            return r;
        }
    }

    public static class Audit_Sale
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);  //  the number of lone securities you have
            int m = int.Parse(tmp[1]);  //  the maximum number of securities you wish to sell
            int k = int.Parse(tmp[2]);  //  the minimum number of securities you can definitely sell

            Tuple<int, decimal, decimal>[] map = new Tuple<int, decimal, decimal>[n];
            Dictionary<int, decimal> res = new Dictionary<int, decimal>();
            Dictionary<int, decimal> res2 = new Dictionary<int, decimal>();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++) {
                tmp = Console.ReadLine().Split(' ');
                decimal l = decimal.Parse(tmp[0]);
                decimal r = decimal.Parse(tmp[1]); sb.Append(l + "  " + r + "   loss: ");
                res.Add(i, l * r);
                res2.Add(i, l * 100);
                map[i] = Tuple.Create(i, l * (100 - r), l * r);// l * (100 - r));

                sb.Append((l * (100 - r)) +
                            "   acutual: " + (l * r) +
                            "   100: " + (l * 100) +
                            "   id: " + (l * r) +
                            "\n");
            }

            //if (k >= m) { Console.WriteLine(dealWithKOnly(n, m, res2)); return; } res2 = null;
            decimal sum = 0;
            bool[] taken = new bool[n];



            var list = map.ToList();
            list.Sort(delegate (Tuple<int, decimal, decimal> x, Tuple<int, decimal, decimal> y)
            {
                if (x.Item3 == y.Item3) {
                    return y.Item2.CompareTo(x.Item2);
                }
                return y.Item3.CompareTo(x.Item3);
            });

            map = new Tuple<int, decimal, decimal>[m];
            for (int i = 0; i < m; i++) {
                map[i] = list[i];
            }
            list = map.ToList();
            list.Sort(delegate (Tuple<int, decimal, decimal> x, Tuple<int, decimal, decimal> y)
            {
                if (x.Item2 == y.Item2) {
                    return y.Item3.CompareTo(x.Item3);
                }
                return y.Item2.CompareTo(x.Item2);
            });
            var res3 = list.ToDictionary(x => x.Item1, x => x.Item3);

            var res3Keys = res3.Keys.ToArray();
            for (int i = 0; i < k; i++) {
                sum += res2[res3Keys[i]];
                taken[res3Keys[i]] = true;
            }
            res = res.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            var resKeys = res.Keys.ToArray();

            int abc = 0;
            for (int i = 0; i < n; i++) {
                if (abc >= m - k) break;
                if (!taken[resKeys[i]]) {
                    sum += res[resKeys[i]];
                    abc++;
                }
            }

            Console.WriteLine();
            Console.WriteLine(sb.ToString());
            Console.WriteLine(sum);
            //var sss = File.ReadAllLines("D:\\trade.sublime"); int zzi = 0;
            //var tmp = sss[zzi++].Split(' ');
            //int n = int.Parse(tmp[0]);
            //int m = int.Parse(tmp[1]);
            //int k = int.Parse(tmp[2]);

            //Tuple<ulong, ulong>[] map = new Tuple<ulong, ulong>[n];
            //Dictionary<int, ulong> res2 = new Dictionary<int, ulong>();
            //Dictionary<int, ulong> res3 = new Dictionary<int, ulong>();

            //for (int i = 0; i < n; i++) {
            //    tmp = sss[zzi++].Split(' ');
            //    ulong l = ulong.Parse(tmp[0]);
            //    ulong r = ulong.Parse(tmp[1]);
            //    map[i] = Tuple.Create(l * 100, l * r);
            //    res2.Add(i, l * 100);
            //    res3.Add(i, l * r);
            //}


            //Console.WriteLine("starting...");

            //if (k >= m) { Console.WriteLine(dealWithKOnly(n, m, res2)); return; }
            //bool[] taken = new bool[n];

            //res3 = res3.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

            //var keys = res3.Keys.ToArray();


            //tour = 10; sera = false;
            //search(keys, map, 0, n, m, k, k, taken, 0);

            //Console.WriteLine();
            //Console.WriteLine(tot + "\n78318");

            //Dictionary<int, decimal> good = new Dictionary<int, decimal>();
            //Dictionary<int, decimal> bad = new Dictionary<int, decimal>();

            //for (int i = 0; i < n; i++) {
            //    tmp = sss[zzi++].Split(' ');
            //    decimal l = decimal.Parse(tmp[0]);
            //    decimal r = decimal.Parse(tmp[1]);
            //    good.Add(i, l * 100);
            //    bad.Add(i, l * r);
            //}
            //decimal sum = 0;
            //bool[] taken = new bool[n];

            //good = good.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            //bad = bad.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);

            //var gKeys = good.Keys.ToArray();
            //var bKeys = bad.Keys.ToArray();

            //int ig = 0, ib = 0;

            //m -= k;
            //while (m > 0 || k > 0) {
            //    if (m == 0) {
            //        if (ig < n && !taken[gKeys[ig]]) { sum += good[gKeys[ig]]; taken[gKeys[ig++]] = true; k--; } else ig++;
            //    } else if (k == 0) {
            //        if (ib < n && !taken[bKeys[ib]]) { sum += bad[bKeys[ib]]; taken[bKeys[ib++]] = true; m--; } else ib++;
            //    } else {
            //        while (taken[gKeys[ig]]) ig++;
            //        while (taken[bKeys[ib]]) ib++;

            //        decimal gd = good[gKeys[ig]], bd = bad[bKeys[ib]];
            //        if (ig == n - 1) { sum += good[gKeys[ig]]; taken[gKeys[ig++]] = true; k--; } else {
            //            int igg = ig + 1;
            //            while (igg < n && taken[gKeys[igg]]) igg++;
            //            if (gd > bd ) {
            //                sum += bad[bKeys[ib]]; taken[bKeys[ib++]] = true; m--;
            //            } else {
            //                sum += good[gKeys[ig]]; taken[gKeys[ig++]] = true; k--;
            //            }
            //        }
            //    }
            //}

            //Console.WriteLine(sum);

            //var map = new Tuple<int, decimal, decimal>[n];
            //Dictionary<int, decimal> res = new Dictionary<int, decimal>();
            //Dictionary<int, decimal> res2 = new Dictionary<int, decimal>();

            //decimal sum = 0;
            //for (int i = 0; i < n; i++) {
            //    tmp = sss[zzi++].Split(' ');
            //    decimal l = decimal.Parse(tmp[0]);
            //    decimal r = decimal.Parse(tmp[1]);
            //    res.Add(i, l * (100 - r));
            //    res2.Add(i, l * 100);
            //    map[i] = Tuple.Create(i, l * (100 - r), l * 100);
            //    sum += 100 * l;
            //}

            //bool[] taken = new bool[n];

            //var list = map.ToList();
            //list.Sort((x, y) => x.Item3.CompareTo(y.Item3));
            //var res3 = list.ToDictionary(x => x.Item1, y => y.Item3);

            //var res3Keys = res3.Keys.ToArray();

            //for (int i = 0; i < n-m; i++) {
            //    sum -= res2[res3Keys[i]];
            //    taken[res3Keys[i]] = true;
            //}

            //list.Sort((x, y) => x.Item2.CompareTo(y.Item2));
            //res3 = list.ToDictionary(x => x.Item1, y => y.Item3);

            //res3Keys = res3.Keys.ToArray();

            //k = m - k;
            //for (int i = 0; i < n; i++) {
            //    if (!taken[res3Keys[i]]) {
            //        sum -= res[res3Keys[i]];
            //        k--;
            //        if (k == 0) break;
            //    }
            //}

            //m -= k;

            //for (int i = 0; i < n; i++) {
            //    if (i == n - 1 || res2[res3Keys[i]] + res[res3Keys[i + 1]] > res[res3Keys[i]] + res2[res3Keys[i + 1]]) {
            //        sum += res2[res3Keys[i]];
            //        taken[res3Keys[i]] = true;
            //        k--;
            //        if (k == 0) break;
            //    }
            //}
            //res = res.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            //var resKeys = res.Keys.ToArray();

            //for (int i = 0; i < n; i++) {
            //    if (!taken[resKeys[i]]) {
            //        sum += res[resKeys[i]];
            //        m--;
            //        if (m == 0) break;
            //    }
            //}


            //Console.WriteLine(sum);

            //Console.WriteLine(78318);
        }
        static ulong tot = 0; static int tour; static bool sera;
        static void search(int[] keys, Tuple<ulong, ulong>[] map, int i, int n, int m, int k, int kk, bool[] taken, ulong sum) {
            if (k == 0) {
                m -= kk;
                if (m == 0) { if (sum > tot) { tot = sum; tour--; if (tour < 0) sera = true; } return; }
                for (int ii = 0; ii < n; ii++) {
                    int t = keys[ii];
                    if (!taken[t]) {
                        sum += map[t].Item2; m--;
                        if (m == 0) {
                            if (sum > tot) {
                                tot = sum; tour--;
                                if (tour < 0) sera = true;
                            }
                            return;
                        }
                    }
                }
                return;
            }

            if (i + 1 < n - k) {
                int t = keys[i + 1];
                taken[t] = true;
                search(keys, map, i + 1, n, m, k - 1, kk, taken, sum + map[t].Item1);
                taken[t] = false;
                t = keys[i + 2];
                taken[t] = true;
                search(keys, map, i + 2, n, m, k - 1, kk, taken, sum + map[t].Item1);
                taken[t] = false;
            } else if (i < n - k) {
                int t = keys[i + 1];
                taken[t] = true;
                search(keys, map, i + 1, n, m, k - 1, kk, taken, sum + map[t].Item1);
                taken[t] = false;
            }
        }
        static ulong dealWithKOnly(int n, int m, Dictionary<int, ulong> res) {
            ulong r = 0;
            res = res.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, y => y.Value);
            var keys = res.Keys.ToArray();
            for (int i = 0; i < Math.Min(n, m); i++) {
                r += res[keys[i]];
            }
            return r;
        }
    }

    public static class Trade_Analysis
    {
        const int MOD = 1000000007;
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            string[] tmp = Console.ReadLine().Split(' ');

            ulong sum = 0, ts = 1;
            ulong[] arr = new ulong[n], map = new ulong[n];
            for (int i = 0; i < n; i++) {
                ulong ar = ulong.Parse(tmp[i]);
                arr[i] = ar; map[i] = ar; sum += ar;
                ts *= ar;
                ts %= MOD;
            }
            sum %= MOD;
            ts = (ts * (ulong)n) % MOD;
            sum += ts;
            ts = 0;

            for (int i = 2; i < n; i++) {
                for (int j = n - i; j > 0; j--) {
                    map[j] = (map[j] + map[j + 1]) % MOD;
                }

                ts = 0;
                for (int j = 0; j < n - i + 1; j++) {
                    ulong ta = (map[j + 1] * arr[j]) % MOD;
                    ts = (ts % MOD + ta % MOD) % MOD;
                    map[j] = ta % MOD;
                }
                sum = (sum + (ts * (ulong)i) % MOD) % MOD;
            }

            Console.WriteLine(sum % MOD);
        }
    }

    public static class Perfect_Separating
    {
        public static void Start() {
            decimal[] cnk = { 1, 0, 2, 0, 6, 0, 20, 0, 70, 0, 252, 0, 924, 0, 3432, 0, 12870, 0, 48620, 0, 184756, 0, 705432, 0, 2704156, 0, 10400600, 0, 40116600, 0, 155117520, 0, 601080390, 0, 2333606220, 0, 9075135300, 0, 35345263800, 0, 137846528820, 0, 538257874440, 0, 2104098963720, 0, 8233430727600, 0, 32247603683100, 0, 126410606437752 };
            string aaa = Console.ReadLine();
            int n = aaa.Length;
            if (n % 2 == 1) {
                Console.WriteLine(0);
                return;
            }
            ps_line = new string[n + 1];

            bool[] inp = new bool[n];
            int a = 0, b = 0;
            for (int i = 0; i < n; i++) {
                if (aaa[i] == 'a') { a++; inp[i] = true; } else b++;
                ps_line[i] = aaa.Substring(i);
            }

            if (a % 2 == 1 || b % 2 == 1) {
                Console.WriteLine(0);
                return;
            }

            if (a == 0 || b == 0) {
                Console.WriteLine(cnk[n]);
                return;
            }

            ps_map = new Dictionary<string, long>();
            long sum = go(inp, a / 2, b / 2, n, 0, "", "");
            Console.WriteLine(sum);
        }
        static Dictionary<string, long> ps_map;
        static string[] ps_line;
        static long go(bool[] lll, int ra, int rb, int n, int k, string left, string right) {
            int len = Math.Min(left.Length, right.Length);
            for (int i = 0; i < len; i++) if (right[i] != left[i]) return 0;

            if (ra == 0 && rb == 0) { if (left == right + ps_line[k]) return 1; return 0; }

            if (n - k < ra + rb) return 0;

            var key = left + k + right;
            if (ps_map.ContainsKey(key)) return ps_map[key];
            long r = 0;

            for (int i = k; i < n; i++) {
                if (lll[i]) {
                    if (ra > 0) r += go(lll, ra - 1, rb, n, i + 1, left + "a", right);
                    right += "a";
                } else {
                    if (rb > 0) r += go(lll, ra, rb - 1, n, i + 1, left + "b", right);
                    right += "b";
                }
            }
            ps_map.Add(key, r);
            return r;
        }
    }
}
