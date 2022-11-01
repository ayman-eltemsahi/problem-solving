using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hackerrank {
    class Week_of_Code___21 {
        const int MOD = 1000000007;

        #region ___The_Letter_N
        public static void The_Letter_N() {
            var gr = File.ReadAllLines("C:\\N.sublime"); int gri = 0;
            int n = int.Parse(gr[gri++]);
            double[] X = new double[n], Y = new double[n];
            for (int i = 0; i < n; i++) {
                var tmp = gr[gri++].Split(' ');
                X[i] = double.Parse(tmp[0]);
                Y[i] = double.Parse(tmp[1]);
            }

            long[][] above = new long[n][], below = new long[n][];
            for (int i = 0; i < n; i++) { above[i] = new long[n]; below[i] = new long[n]; }

            for (int i = 0; i < n; i++) {
                double ax = X[i], ay = Y[i];
                for (int j = i + 1; j < n; j++) {
                    double bx = X[j], by = Y[j];
                    for (int k = j + 1; k < n; k++) {
                        double cx = X[k], cy = Y[k];

                        var loc = PLoc(ax, ay, bx, by, cx, cy);
                        if (loc == 0) continue;

                        bool Au = MoreThan90(bx, by, ax, ay, cx, cy),
                            Bu = !Au || MoreThan90(ax, ay, bx, by, cx, cy),
                            Cu = !Au || !Bu || MoreThan90(ax, ay, cx, cy, bx, by);

                        if (Au && Bu && Cu) {
                            if (PLoc(ax, ay, cx, cy, bx, by) < 0) below[i][k]++;
                            else above[i][k]++;

                            if (PLoc(bx, by, cx, cy, ax, ay) < 0) below[j][k]++;
                            else above[j][k]++;

                            if (loc < 0) below[i][j]++;
                            else above[i][j]++;

                        } else if (!Au) {
                            AngleMe(above, below, i, j, k, ax, ay, bx, by, cx, cy, loc);
                        } else if (!Bu) {
                            AngleMe(above, below, j, i, k, bx, by, ax, ay, cx, cy, loc);
                        } else if (!Cu) {
                            AngleMe(above, below, k, j, i, cx, cy, bx, by, ax, ay, PLoc(bx, by, cx, cy, ax, ay));
                        }
                    }
                }
            }

            long ans = 0;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    ans += (above[i][j] * below[i][j]);

            Console.WriteLine(ans);
            Console.WriteLine(85506990);
        }
        static void AngleMe(long[][] above, long[][] below, int i, int j, int k, double ax, double ay, double bx, double by, double cx, double cy, double loc) {
            if (PLoc(cx, cy, bx, by, ax, ay) < 0) below[Math.Min(k, j)][Math.Max(k, j)]++;
            else above[Math.Min(k, j)][Math.Max(k, j)]++;

            double aax = ax + 1, aay = ay;

            if (by == ay) {
                aay++; aax--;
            } else if (ax != bx) {
                aay -= (bx - ax) / (by - ay);    //  aay -= (bx + 1) * (bx - ax) / (by - ay);
            }

            double locA = PLoc(ax, ay, aax, aay, cx, cy);

            if (Valid(ax, ay, bx, by, loc, locA)) {
                if (loc < 0) below[Math.Min(i, j)][Math.Max(i, j)]++;
                else above[Math.Min(i, j)][Math.Max(i, j)]++;
            } else {
                if (PLoc(ax, ay, cx, cy, bx, by) < 0) below[Math.Min(i, k)][Math.Max(i, k)]++;
                else above[Math.Min(i, k)][Math.Max(i, k)]++;
            }
        }

        static bool Valid(double ax, double ay, double bx, double by, double loc, double locA) {
            if (ax <= bx) {
                if (by >= ay) return loc != locA;
                return loc == locA;
            }
            if (ay >= by) return loc != locA;
            return loc == locA;
        }

        static int PLoc(double ax, double ay, double bx, double by, double cx, double cy) {
            if (ax < bx || (ax == bx && ay > by)) {
                double left = (ax - bx) * (cy - by), right = (ay - by) * (cx - bx);
                if (left > right) return 1;
                if (left < right) return -1;
                return 0;
            }
            double l = (bx - ax) * (cy - ay), r = (by - ay) * (cx - ax);
            if (l > r) return 1;
            if (l < r) return -1;
            return 0;
        }

        static bool MoreThan90(double ax, double ay, double bx, double by, double cx, double cy) {
            return (ax - bx) * (cx - bx) + (ay - by) * (cy - by) >= 0;
        }

        struct Point {
            public Point(double a, double b) {
                x = a; y = b;
            }
            public double x;
            public double y;
        }
        #endregion

        #region ___Counting_the_Ways
        public static void Counting_the_Ways() {
            int n = 1;// int.Parse(Console.ReadLine());
            //s int[] A = { 1,2,3 };// Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            n = A.Length;
            //var tmp = Console.ReadLine().Split(' ');
            long L = 1;// long.Parse(tmp[0]);
            //long R = 9;// long.Parse(tmp[1]);

            Program.stopwatch.Restart();
            Array.Sort(A);

            long ans = 0;
            long[] map = new long[R + 1];

            map[0] = 1;

            for (int i = 0; i < n; i++) {
                int a = 0;
                for (int j = A[i]; j <= R; j++) {
                    map[j] = (map[j] + map[a++]) % MOD;
                }
            }

            Console.WriteLine(string.Join(" ", map));
            for (int i = (int)L; i <= R; i++) {
                ans += map[i];
            }

            //for (int i = 1; i < R; i++) {
            //    Console.WriteLine(i + "  " + tw[i]+ "          " + (tw[i]- tw[i-1]));
            //}
            Console.WriteLine(ans);
            Counting_the_Ways2();
        }
        static int[] A = { 1, 2, 3, 4 }; static int R = 9;
        public static void Counting_the_Ways2() {
            int n = A.Length - 1;
            int L = 1;

            int[] next;
            int[] w = new int[R + 1];
            w[0] = 1;
            for (int i = 0; i < n; i++) {

                int[] w2 = new int[R + 1];

                next = new int[A[i + 1] + 1];

                for (int j = 0; j < R + 1; j++) {
                    w2[j] = w[j];
                    int a = j - A[i];
                    while (a >= 0) {
                        w2[j] += w[a];
                        a -= A[i];
                    }
                    next[j % A[i + 1]] = w2[j];
                }
                Console.WriteLine(string.Join(" ", next));
                w = w2;
            }

            Console.WriteLine(w.Sum());
        }
        #endregion
    }
}
