using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank
{
    class NP_Complete {

        const int n = 7;

        static int[,] gcd = new int[n, n];
        static bool[] col = new bool[n];
        static int[] spies = new int[n];
        static bool working;
        static int[] X = new int[n], Y = new int[n];
        static int xy;
        static List<int> indices = new List<int>();
        static double[] S = new double[10000], b = new double[10000];
        static int gcdf(int a, int b) {
            return b == 0 ? a : gcdf(b, a % b);
        }
        public static void fillGCD() {
            for (int i = 1; i < n; i++) {
                for (int j = 1; j < n; j++) {
                    gcd[i, j] = gcdf(i, j);
                }
            }
        }
        public static void Spies_Revised() {
            fillGCD();
            xy = 0;

            //indices = new List<int> { 31, 18, 22, 15, 26, 33, 10, 30, 7, 16, 24, 17, 36, 29, 12, 8, 37, 35, 27, 3, 23, 6, 11, 48, 43, 25, 13, 5, 1, 40, 32, 39, 14, 46, 2, 50, 41, 28, 21, 49, 4, 20, 9, 38, 34, 19, 47, 44, 42, 45 };
            //for (int i = 0; i < indices.Count; i++) indices[i]--;

            for (int i = 0; i < n; i++) { X[i] = Y[i] = spies[i] = -1; indices.Add(i); }

            int[] xxx;
            Shuffle(indices);
            xxx = indices.ToArray();

            while (!TrackSpiesB(xxx)) {
                Shuffle(indices);
                xxx = indices.ToArray();
                Console.Write("__  ");
            }

            FoundAnswer2(xxx);
        }

        static Random ran = new Random();
        static bool TrackSpiesB(int[] spy) {

            int u, v = -1, c = 0;
            int conflict = Conflicts(spy, ref v);

            while (conflict > 0) {
                //v = ran.Next(0, n);
                u = v;
                while (v == u) u = ran.Next(0, n);
                swap(spy, u, v);
                //Console.WriteLine(u + " " + v);
                int v2 = 0;
                var con = Conflicts(spy, ref v);
                if (con >= conflict) swap(spy, u, v); else { conflict = con; v = v2; }
                if (c++ > 250000) { Console.WriteLine(conflict); return false; }
            }
            return true;
        }

        private static void swap(int[] spy, int u, int v) {
            var tmp = spy[u];
            spy[u] = spy[v];
            spy[v] = tmp;
        }

        static int Conflicts(int[] spy, ref int u) {
            var h = 0;
            //HashSet<int>[] kk = new HashSet<int>[n];

            int x, y, x2, y2, gc;
            u = -1;
            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    x = j - i; y = spy[j] - spy[i];

                    gc = gcd[x, Math.Abs(y)];
                    x /= gc; y /= gc;

                    if (Math.Abs(y) == x) { u = i; h += 100; }

                    for (int k = j + 1; k < n; k++) {
                        x2 = k - j; y2 = spy[k] - spy[j];
                        gc = gcd[x2, Math.Abs(y2)];
                        x2 /= gc; y2 /= gc;
                        if (x == x2 && y == y2) {
                            h++; if (u == -1)u = i; }
                    }
                }
            }

            return h;

            //if (h.Count != 0) return h.Count;

            //for (int i = 0; i < n; i++) {
            //    for (int j = i + 1; j < n; j++) {
            //        x = j - i; y = spy[j] - spy[i];
            //        gc = gcd[x, Math.Abs(y)];
            //        x /= gc; y /= gc;

            //        if (kk[x] == null) {
            //            kk[x] = new HashSet<int> { y };
            //        } else {
            //            if (!kk[x].Add(y)) { u = i; h.Add(j); }
            //        }
            //    }
            //}


            //for (i = 0; i < n; i++) {
            //    if (i == x) continue;
            //    j = spy[i];
            //    if (i == x || j == y) { h.Add(i); continue; }
            //    if (Math.Abs(x - i) == Math.Abs(y - j)) { h.Add(i); }
            //}

            //for (i = 0; i < n; i++) {
            //    if (i == x) continue;
            //    j = spy[i];
            //    var a = i - x; var b = j - y;
            //    for (int i2 = i + 1; i2 < n; i2++) {
            //        if (i2 == x) continue;
            //        if (h.Contains(i) && h.Contains(i2)) continue;
            //        int j2 = spy[i2];
            //        if (a * (j2 - y) == (i2 - x) * b) { h.Add(i2); h.Add(i); }
            //    }
            //}

        }
        static void FoundAnswer2(int[] xxx) {
            var sb = new StringBuilder();
            sb.Append(n + "\\n" + string.Join(" ", xxx) + "\n");
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (xxx[i] == j + 1) sb.Append('S'); else sb.Append('*');
                }
                sb.Append("\n");
            }
            working = false;

            //File.AppendAllText("C:\\spies.sublime", "".PadLeft(50, '_') + "\n" + n + "\n" + sb.ToString());
            Console.WriteLine(sb.ToString());
        }
        #region ___SpiesB

        #endregion

        #region ___SpiesA
        static void TrackSpies(int i, int n, int p) {
            if (!working) return;
            if (p == n) {
                FoundAnswer(n);
                return;
            }

            foreach (var j in indices) {
                if (col[j] || Equation(i, j)) continue;


                col[j] = true;
                spies[i] = j + 1;
                X[xy] = i; Y[xy++] = j;

                TrackSpies(i + 1, n, p + 1);

                xy--;
                col[j] = false;


            }

        }

        static void FoundAnswer(int n) {
            var sb = new StringBuilder();
            sb.Append(n + "\\n" + string.Join(" ", spies) + "\n");
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) {
                    if (spies[i] == j + 1) sb.Append('S'); else sb.Append('*');
                }
                sb.Append("\n");
            }
            working = false;

            File.AppendAllText("C:\\spies.sublime", "".PadLeft(50, '_') + "\n" + n + "\n" + sb.ToString());
            Console.WriteLine(sb.ToString());
        }

        static bool Equation(double x, double y) {
            for (int i = 0; i < xy; i++) {
                var a = X[i] - x; var b = Y[i] - y;
                if (Math.Abs(a) == Math.Abs(b)) return true;
                for (int j = i + 1; j < xy; j++) {
                    if (a * (Y[j] - y) == (X[j] - x) * b) return true;
                }
            }
            return false;
        }

        static void Shuffle<T>(IList<T> list) {
            var ran = new Random();
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = ran.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        #endregion
    }
}
