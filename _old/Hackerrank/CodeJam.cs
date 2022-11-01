using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam
{
    public class CodeJam
    {
        static Random random = new Random();
        static StringBuilder sb = new StringBuilder();
        static void write(StringBuilder sb) {
            if (sb.Length > 0) --sb.Length;
            Console.WriteLine(sb.ToString());
            Console.SetOut(new StreamWriter("C:\\codejam\\output.sublime"));
            Console.WriteLine(sb.ToString());
            Console.Out.Close();
        }

        public static void Start() {
            Console.SetIn(new StreamReader("C:\\codejam\\input.sublime"));

            int TC = int.Parse(Console.ReadLine());
            for (int tc = 0; tc < TC; tc++) {
                Console.WriteLine(tc + 1);
                read();
                sb.Append(string.Format("Case #{0}: {1}\n", tc + 1, solve().ToString()));
            }
            write(sb);
        }


        const string IMPOSSIBLE = "IMPOSSIBLE";
        static int N, c;
        static int[] U;
        static int[] values = { 2, 3, 1, 5, 4, 6 };
        static void read() {
            var tmp = Console.ReadLine().Split(' ');
            N = int.Parse(tmp[0]);
            U = new int[6];
            c = 0;
            for (int i = 0; i < 6; i++) U[i] = int.Parse(tmp[i + 1]);
        }

        static object solve() {
            int[] w = new int[N];
            if (U[3] > U[0] + 1) return IMPOSSIBLE;
            if (U[5] > U[2] + 1) return IMPOSSIBLE;
            if (U[1] > U[4] + 1) return IMPOSSIBLE;

            return solve(U, w, 0);
        }
        static string[] str = { "R", "O", "Y", "G", "B", "V" };
        static int[] R = { 3, 2, 4 };
        static int[] Y = { 5, 4, 0 };
        static int[] B = { 1, 0, 2 };
        static string solve(int[] uni, int[] w, int v) {
            int un = -1;
            //if (c++ > 25000000) return IMPOSSIBLE;
            if (v == N) {
                un = w[v - 1];
                int st = w[0];
                if (valid(un, st)) return string.Join("", w.Select(x => str[x])); 
            } else {
                string tm;

                if (v == 0) {
                    foreach (var i in Enumerable.Range(0, 6).OrderByDescending(x => uni[x])) {
                        if (uni[i] > 0) {
                            w[0] = i; uni[i]--;
                            tm = solve(uni, w, v + 1);
                            if (tm != IMPOSSIBLE) return tm;
                            uni[i]++;
                        }
                    }
                } else {
                    un = w[v - 1];
                    switch (un) {
                        case 0:
                            foreach (var i in R) {
                                if (uni[i] > 0) {
                                    w[v] = i; uni[i]--;
                                    tm = solve(uni, w, v + 1);
                                    if (tm != IMPOSSIBLE) return tm;
                                    uni[i]++;
                                }
                            }
                            break;
                        case 2:
                            foreach (var i in Y) {
                                if (uni[i] > 0) {
                                    w[v] = i; uni[i]--;
                                    tm = solve(uni, w, v + 1);
                                    if (tm != IMPOSSIBLE) return tm;
                                    uni[i]++;
                                }
                            }
                            break;

                        case 4:
                            foreach (var i in B) {
                                if (uni[i] > 0) {
                                    w[v] = i; uni[i]--;
                                    tm = solve(uni, w, v + 1);
                                    if (tm != IMPOSSIBLE) return tm;
                                    uni[i]++;
                                }
                            }
                            break;
                        case 1:
                            if (uni[4] > 0) {
                                w[v] = 4; uni[4]--;
                                tm = solve(uni, w, v + 1);
                                if (tm != IMPOSSIBLE) return tm;
                                uni[4]++;
                            }
                            break;
                        case 3:
                            if (uni[0] > 0) {
                                w[v] = 0; uni[0]--;
                                tm = solve(uni, w, v + 1);
                                if (tm != IMPOSSIBLE) return tm;
                                uni[0]++;
                            }
                            break;
                        case 5:
                            if (uni[2] > 0) {
                                w[v] = 2; uni[2]--;
                                tm = solve(uni, w, v + 1);
                                if (tm != IMPOSSIBLE) return tm;
                                uni[2]++;
                            }
                            break;
                    }

                }
            }



            return IMPOSSIBLE;
        }

        static bool valid(int un, int st) {
            return (values[un] & values[st]) == 0;
        }
    }
}