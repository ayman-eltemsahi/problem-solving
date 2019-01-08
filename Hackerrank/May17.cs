using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Hackerrank
{
    class May17 {

        #region ____Save_Humanity

        public static void Save_Humanity() {
            //int tc = int.Parse(Console.ReadLine());
            //for (int itc = 0; itc < tc; itc++) {
            //    save(Console.ReadLine());
            //}
            String[] s = File.ReadAllLines("D:\\sh_test1.sublime");
            int i = 0;
            int tc = int.Parse(s[i++]);
            for (int itc = 0; itc < tc; itc++) {
                save(s[i++]);
            }
        }

        private static void save(string p) {
            string patient = p.Split(' ')[0];
            string virus = p.Split(' ')[1];

            int i = 0;
            int lp = patient.Length;
            int lv = virus.Length;

            if (lv > lp) { Console.WriteLine("No Match!"); return; }
            if (lp == 1) { Console.WriteLine(0); return; }

            if (lv == 1) {
                string tmp = ""; for (int _ = 0; _ < lp; _++) { tmp += (_ + " "); }
                log(tmp);
                return;
            } else if (lv == 2) {
                string tmp = ""; for (int _ = 0; _ < lp - 1; _++) { if (patient[_] == virus[0] || patient[_ + 1] == virus[1]) tmp += _ + " "; }
                Console.WriteLine();
                return;
            }

            int upper = 0, inner = 0;
            StringBuilder s = new StringBuilder();
            while (i <= lp - lv) {
                upper++;
                int tmp = i;
                //i++;
                bool mis = false, forindex = false, f = true;
                for (int im = tmp; im < tmp + lv; im++) {
                    inner++;
                    if (im > tmp && !forindex && im + 1 < lp &&
                        (patient[im] == virus[0] || patient[im + 1] == virus[1])) { i = im; forindex = true; }
                    if (patient[im] != virus[im - tmp]) {
                        if (!mis) { mis = true; } else { f = false; break; }
                    }
                }

                if (f) s.Append(tmp + " ");
                if (i == tmp) i += lv;
            }

            log("upper = " + upper + ", inner = " + inner);
            log(string.IsNullOrEmpty(s.ToString()) ? "No Match!" : s.ToString());
            Console.WriteLine("done");
        }

        private static bool MatchWithOneMismatch(string p, string virus, ref int index) {
            index++;
            bool mis = false, forindex = false;
            for (int im = 0; im < p.Length; im++) {
                if (im > 0 && !forindex && im + 1 < virus.Length && (p[im] == virus[0] && p[im + 1] == virus[1])) { index += im; forindex = true; }
                if (p[im] != virus[im]) {
                    if (!mis) { mis = true; } else { return false; }
                }
            }
            return true;
        }

        static void log(string p) {
            File.AppendAllText("D:\\sh_sol.sublime", p + Environment.NewLine);
        }

        #endregion

       
        #region ___
        public static void Div_Span() {
            int mod = (int)Math.Pow(10, 9) + 7;
            int x = 2;
            int y = 2;
            int z = x + y;

            int count = 0;
            count += factorial(x);
            count += factorial(y);

            //for (int i = 0; i < x; i++) { count += factorial(i) * factorial(x - i); count %= mod; }
            //for (int i = 0; i < y; i++) { count += factorial(i) * factorial(y - i); count %= mod; }
            for (int i = 0; i < z; i++) { count += factorial(i) * factorial(z - i); count %= mod; }
        }

        static int factorial(int n) {
            if (n < 3) return n;
            return n * factorial(n - 1);
        }

        static Dictionary<string, int> dic;
        static int[] left, right, colors;
        public static void Unique_Colors() {
            var lines = File.ReadAllLines("D:\\colors.sublime");
            int il = 0;
            int n = int.Parse(lines[il++]);
            colors = Array.ConvertAll(lines[il++].Split(' '), x => int.Parse(x));
            bool crs = true;
            for (int i = 1; i < n; i++) {
                if (colors[i] != colors[i - 1]) { crs = false; break; }
            }
            left = new int[n - 1];
            right = new int[n - 1];
            for (int i = 0; i < n - 1; i++) {
                var tmp = lines[il++].Split(' ');
                left[i] = int.Parse(tmp[0]);
                right[i] = int.Parse(tmp[1]);
            }

            if (crs) {
                for (int i = 0; i < n; i++) {
                    Console.WriteLine(n);
                }
            } else {
                dic = new Dictionary<string, int>();
                for (int i = 0; i < n; i++) {
                    int c = 0;
                    for (int j = 0; j < n; j++) {
                        if (i == j) { c++; continue; }
                        int cur = 0;
                        var key = getKey(i + 1, j + 1);
                        int vl;
                        if (dic.TryGetValue(key, out vl)) { c += vl; } else {
                            f = true;
                            findColors(i + 1, i + 1, j + 1, -1, new HashSet<int>(), ref cur);
                            if (dic.Count < 2000967296 && !dic.ContainsKey(key)) dic.Add(key, cur);
                            c += cur;
                        }
                    }
                    Console.WriteLine(c);
                }
            }
        }
        static bool f;
        private static void findColors(int orig, int from, int where, int noback, HashSet<int> hash, ref int cnt) {
            if (!f) return;

            var key = getKey(orig, where);
            int vl;
            if (dic.TryGetValue(key, out vl)) { cnt = vl; f = false; return; }

            HashSet<int> hash2 = new HashSet<int>();
            foreach (var it in hash) {
                hash2.Add(it);
            }


            int comp = 0;
            if (hash2.Add(colors[from - 1])) { cnt++; comp = 1; }
            var key2 = getKey(orig, from);
            if (!dic.ContainsKey(key2)) { dic.Add(key2, cnt); }

            for (int i = 0; i < left.Length; i++) {
                if (!f) return;
                int to = -1;
                int fr = -1;
                if (left[i] == from && right[i] != noback) { fr = left[i]; to = right[i]; } else if (right[i] == from && left[i] != noback) { fr = right[i]; to = left[i]; }

                if (to == -1) continue;

                if (to == where) {
                    f = false;
                    if (hash2.Add(colors[where - 1])) cnt++;
                    if (!dic.ContainsKey(key)) dic.Add(key, cnt);
                    return;
                }

                findColors(orig, to, where, fr, hash2, ref cnt);
            }
            if (!f) return;
            cnt -= comp;
        }

        static int getKeyi(int i, int j) {
            return 1000000 * Math.Min(i, j) + Math.Max(i, j);
        }

        static string getKey(int i, int j) {
            return Math.Min(i, j) + " " + Math.Max(i, j);
        }
        #endregion
    }
}
