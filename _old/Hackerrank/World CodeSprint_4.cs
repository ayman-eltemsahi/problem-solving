using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank
{
    class World_CodeSprint_4 {
        #region ___Minimum_Distances
        public static void Minimum_Distances() {
            int n = int.Parse(Console.ReadLine());
            var A = new List<int>[100001];
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

            for (int i = 0; i < n; i++) {
                int a = arr[i];
                if (A[a] == null) {
                    A[a] = new List<int> { i };
                } else {
                    A[a].Add(i);
                }
            }

            int ans = 1000000;
            for (int i = 1; i < 100001; i++) {
                if (A[i] != null) {
                    A[i].Sort();
                    for (int j = 0; j < A[i].Count - 1; j++) {
                        ans = Math.Min(ans, Math.Abs(A[i][j] - A[i][j + 1]));
                    }
                }
            }
            Console.WriteLine(ans == 1000000 ? -1 : ans);
        }
        #endregion

        #region ___Equal_Stacks
        public static void Equal_Stacks() {
            var tmp = Console.ReadLine().Split(' ');
            int a = int.Parse(tmp[0]);
            int b = int.Parse(tmp[1]);
            int c = int.Parse(tmp[2]);

            int[] arr1 = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            int[] arr2 = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            int[] arr3 = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

            Array.Reverse(arr1); Array.Reverse(arr2); Array.Reverse(arr3);


            List<int> A = new List<int> { 0 }, B = new List<int> { 0 }, C = new List<int> { 0 };

            for (int i = 0; i < a; i++) A.Add(arr1[i] + A[i]);
            for (int i = 0; i < b; i++) B.Add(arr2[i] + B[i]);
            for (int i = 0; i < c; i++) C.Add(arr3[i] + C[i]);

            for (int i = A.Count - 1; i >= 0; i--) {
                int g = A[i];
                if (B.BinarySearch(g) >= 0 && C.BinarySearch(g) >= 0) {
                    Console.WriteLine(g);
                    return;
                }
            }

            Console.WriteLine(0);
        }
        #endregion

        #region ___A_or_B
        static Dictionary<char, bool[]> mapAB; static Dictionary<string, char> pamAB;
        public static void A_or_B() {
            mapAB = new Dictionary<char, bool[]>();
            mapAB.Add('0', new bool[] { false, false, false, false });
            mapAB.Add('1', new bool[] { false, false, false, true });
            mapAB.Add('2', new bool[] { false, false, true, false });
            mapAB.Add('3', new bool[] { false, false, true, true });
            mapAB.Add('4', new bool[] { false, true, false, false });
            mapAB.Add('5', new bool[] { false, true, false, true });
            mapAB.Add('6', new bool[] { false, true, true, false });
            mapAB.Add('7', new bool[] { false, true, true, true });
            mapAB.Add('8', new bool[] { true, false, false, false });
            mapAB.Add('9', new bool[] { true, false, false, true });
            mapAB.Add('A', new bool[] { true, false, true, false });
            mapAB.Add('B', new bool[] { true, false, true, true });
            mapAB.Add('C', new bool[] { true, true, false, false });
            mapAB.Add('D', new bool[] { true, true, false, true });
            mapAB.Add('E', new bool[] { true, true, true, false });
            mapAB.Add('F', new bool[] { true, true, true, true });
            pamAB = new Dictionary<string, char>();
            pamAB.Add("0000", '0');
            pamAB.Add("0001", '1');
            pamAB.Add("0010", '2');
            pamAB.Add("0011", '3');
            pamAB.Add("0100", '4');
            pamAB.Add("0101", '5');
            pamAB.Add("0110", '6');
            pamAB.Add("0111", '7');
            pamAB.Add("1000", '8');
            pamAB.Add("1001", '9');
            pamAB.Add("1010", 'A');
            pamAB.Add("1011", 'B');
            pamAB.Add("1100", 'C');
            pamAB.Add("1101", 'D');
            pamAB.Add("1110", 'E');
            pamAB.Add("1111", 'F');
            int q = int.Parse(Console.ReadLine());
            while (q-- > 0) {
                int k = int.Parse(Console.ReadLine());
                bool[] A = new bool[200005], B = new bool[200005], C = new bool[200005];
                binary(Console.ReadLine(), A);
                binary(Console.ReadLine(), B);
                binary(Console.ReadLine(), C);



                for (int i = 0; i < 200005; i++) {
                    bool c = C[i];
                    bool a = A[i];
                    bool b = B[i];

                    if (!c && !a && !b) continue;
                    if (c && (a || b)) continue;

                    if (!c) {
                        if (a) {
                            if (k > 0) { A[i] = !A[i]; k--; } else break;
                        }
                        if (b) {
                            if (k > 0) { B[i] = !B[i]; k--; } else break;
                        }
                    } else {
                        if (!a && !b) {
                            if (k > 0) {
                                B[i] = !B[i];
                                k--;
                            } else break;
                        }
                    }
                }



                if (k > 0) {
                    for (int i = 200004; i >= 0; i--) {
                        bool a = A[i];
                        bool b = B[i];

                        if (a && !b) {
                            if (k > 1) {
                                A[i] = !A[i];
                                B[i] = !B[i];
                                k -= 2;
                            }
                        } else if (a && b) {
                            if (k > 0) {
                                A[i] = !A[i];
                                k--;
                            } else break;
                        }

                    }
                }

                if (ABC(A, B, C)) {
                    StringBuilder sb = new StringBuilder();
                    bool xx = false;
                    for (int i = 200003; i >= 0; i -= 4) {
                        string g = A[i] ? "1" : "0";
                        g += A[i - 1] ? "1" : "0";
                        g += A[i - 2] ? "1" : "0";
                        g += A[i - 3] ? "1" : "0";
                        xx = xx | (g != "0000");
                        if (xx) sb.Append(pamAB[g]);
                    }
                    Console.WriteLine(clean(sb.ToString()));
                    sb = new StringBuilder();
                    xx = false;
                    for (int i = 200003; i >= 0; i -= 4) {
                        string g = B[i] ? "1" : "0";
                        g += B[i - 1] ? "1" : "0";
                        g += B[i - 2] ? "1" : "0";
                        g += B[i - 3] ? "1" : "0";
                        xx = xx | (g != "0000");
                        if (xx) sb.Append(pamAB[g]);
                    }
                    Console.WriteLine(clean(sb.ToString()));

                } else {
                    Console.WriteLine(-1);
                }

            }
        }

        static string clean(string p) {
            if (p == "") return "0";
            if (p.Length < 2) return p;
            int i = 0;
            while (i < p.Length && p[i] == 48) i++;
            if (i >= p.Length) return "0";
            return p.Substring(i);
        }

        static bool ABC(bool[] A, bool[] B, bool[] C) {
            for (int i = 0; i < 200005; i++) {
                if ((A[i] | B[i]) != C[i]) return false;
            }
            return true;
        }
        static void binary(string x, bool[] w) {
            int r = 0;
            for (int i = x.Length - 1; i >= 0; i--) {
                var g = mapAB[x[i]];
                w[r] = g[3];
                w[r + 1] = g[2];
                w[r + 2] = g[1];
                w[r + 3] = g[0];
                r += 4;
            }
        }
        #endregion

        #region ___Roads_in_HackerLand
        public static void Roads_in_HackerLand() {
            //var rnd = new Random();
            //int start = 101, start2 = 105;
            //for (int i = 0; i < 30; i++) {
            //    Console.WriteLine("{0} {1} {2}", start +i, rnd.Next(1,start+i), start2 + i);
            //}
            //return;

            solve2();
            //return;
            //if (m > 100) {
            //    solve1(n, m);
            //} else {
            //    solve2(n, m);
            //}
            Console.WriteLine("\n1111110000000000000100000011101010010011100101111111011010101011001010100100001101000111001000110111110001110001101011010100110000010101110010");
        }

        static void solve3(int n, int m) {
            var tuple = new Tuple<int, int>[m];
            HashSet<int>[] adj = new HashSet<int>[n + 1];
            for (int i = 0; i < n + 1; i++) adj[i] = new HashSet<int>();

            int d = 100000;
            var map = new Dictionary<int, int>();
            for (int i = 0; i < m; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]);
                int b = int.Parse(tmp[1]);
                int ag = int.Parse(tmp[2]);
                adj[a].Add(b);
                adj[b].Add(a);
                map.Add(Math.Min(a, b) * d + Math.Max(a, b), ag);
                tuple[ag] = Tuple.Create(Math.Min(a, b), Math.Max(a, b));
            }

            int r = -1;
            for (int i = 1; i < n; i++) {
                r += i;
            }
            int[] w = new int[500005];
            tip = 0;
            Add(w, 0);

            for (int i = 1; i < m; i++) {

                var top = -1;
                foreach (var node in adj[i]) {
                    top = Math.Max(top, map[Math.Min(node, i) * d + Math.Max(node, i)]);
                }
            }

            Array.Reverse(w, 0, tip + 1);
            for (int i = 0; i <= tip; i++) {
                Console.Write(w[i]);
            }

        }

        static void solve1() {
            var g = File.ReadAllLines("C:\\road.sublime"); int si = 0;
            var tmp = g[si++].Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            BigInteger[][] map = new BigInteger[n + 1][];
            for (int i = 1; i <= n; i++) {
                map[i] = new BigInteger[n + 1];
                for (int j = 1; j <= n; j++) map[i][j] = -1;
            }
            for (int i = 1; i <= n; i++) map[i][i] = 0;

            for (int i = 0; i < m; i++) {
                tmp = g[si++].Split(' ');
                int a = int.Parse(tmp[0]);
                int b = int.Parse(tmp[1]);
                BigInteger c = BigInteger.Pow(2, int.Parse(tmp[2]));
                map[a][b] = c;
                map[b][a] = c;
            }

            //for (int fewafe = 0; fewafe < 5; fewafe++) {
            for (int k = 1; k <= n; k++) {
                for (int i = 1; i <= n; i++) {
                    for (int j = 1; j <= n; j++) {
                        if (i == j) continue;
                        var l = map[i][k];
                        var r = map[k][j];
                        if (l != -1 && r != -1) {
                            if (map[i][j] == -1 || l + r < map[i][j]) {
                                map[i][j] = map[j][i] = l + r;
                            }
                        }
                    }
                }
            }
            //}

            int roof = 0;
            BigInteger ans = 0;
            for (int i = 1; i <= n; i++) {
                for (int j = i + 1; j <= n; j++) {
                    if (map[i][j] == -1) {
                        roof++;
                        for (int k = 1; k <= n; k++) {
                            if (map[i][k] != -1 && map[k][j] != -1) {
                                Console.WriteLine(k);
                            }
                        }
                    }
                    ans += map[i][j];
                }
            }
            Console.WriteLine(roof);
            bin(ans);
        }

        static int[] w;
        static bool[][] hold;
        static void solve2() {
            var g = File.ReadAllLines("C:\\road.sublime"); int si = 0;
            var tmp = g[si++].Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            var tuple = new Tuple<int, int>[m];
            HashSet<int>[] adj = new HashSet<int>[n + 1];
            for (int i = 0; i < n + 1; i++) adj[i] = new HashSet<int>();
            int d = 1000000;
            var map = new Dictionary<int, int>();
            for (int i = 0; i < m; i++) {
                tmp = g[si++].Split(' ');
                int a = int.Parse(tmp[0]);
                int b = int.Parse(tmp[1]);
                int ag = int.Parse(tmp[2]);
                adj[a].Add(b);
                adj[b].Add(a);
                map.Add(Math.Min(a, b) * d + Math.Max(a, b), ag);
                tuple[ag] = Tuple.Create(Math.Min(a, b), Math.Max(a, b));
            }

            int r = -1;
            for (int i = 1; i < n; i++) {
                r += i;
            }

            tip = 0;
            hold = new bool[n + 1][]; for (int i = 0; i <= n; i++) hold[i] = new bool[n + 1];
            w = new int[500005];
            bool[] bigV = new bool[n + 1];
            for (int i = 0; i < m; i++) {
                track(tuple[i].Item1, tuple[i].Item2, tuple[i].Item1, new List<int> { i }, adj, map);
                track(tuple[i].Item2, tuple[i].Item1, tuple[i].Item2, new List<int> { i }, adj, map);
            }


            //bool[][] holder = new bool[n + 1][]; for (int t = 1; t <= n; t++) holder[t] = new bool[n + 1];
            //var list = new List<List<int>>();
            //var ids = new List<Tuple<int, int>>();
            //list.Add(new List<int> { 0 });
            //ids.Add(Tuple.Create(tuple[0].Item1, tuple[0].Item2));
            //holder[tuple[0].Item1][tuple[0].Item2] = true;

            //for (int i = 1; i < m; i++) {
            //    Queue<int> Q = new Queue<int>();

            //    int a = tuple[i].Item1, b = tuple[i].Item2;
            //    if (!holder[a][b]) {
            //        list.Add(new List<int> { i });
            //        ids.Add(Tuple.Create(a, b));
            //        holder[a][b] = true;
            //        Q.Enqueue(ids.Count - 1);
            //    }

            //    while (Q.Count > 0) {
            //        int c = Q.Dequeue();
            //        if (c == r) break;
            //        a = ids[c].Item1; b = ids[c].Item2;
            //        for (int j = 0; j < c; j++) {
            //            int aa = ids[j].Item1, bb = ids[j].Item2;

            //            int x = -1, y = -1;
            //            if (a == bb) {
            //                x = Math.Min(b, aa); y = Math.Max(b, aa);
            //            } else if (a == aa) {
            //                x = Math.Min(b, bb); y = Math.Max(b, bb);
            //            } else if (b == bb) {
            //                x = Math.Min(a, aa); y = Math.Max(a, aa);
            //            } else if (b == aa) {
            //                x = Math.Min(a, bb); y = Math.Max(a, bb);
            //            } else continue;

            //            if (!holder[x][y]) {
            //                var kar = new List<int>(list[j]);
            //                kar.AddRange(list[c]);
            //                list.Add(kar);
            //                ids.Add(Tuple.Create(x, y));
            //                holder[x][y] = true;
            //                Q.Enqueue(ids.Count - 1);
            //            }
            //        }
            //    }
            //}

            //tip = 0;
            //for (int i = 0; i < ids.Count; i++) {
            //    foreach (var c in list[i]) {
            //        Add(w, c);
            //    }
            //}

            Array.Reverse(w, 0, tip + 1);
            for (int i = 0; i <= tip; i++) {
                Console.Write(w[i]);
            }
            Console.WriteLine();
        }

        static void track(int head, int from, int proh, List<int> list, HashSet<int>[] adj, Dictionary<int, int> map) {
            const int d = 1000000;
            if (!hold[from][head]) {
                foreach (var c in list) Add(w, c);
                hold[from][head] = hold[head][from] = true;
            }

            int k = list.Last();

            int r = 10000000;
            foreach (var node in adj[from]) {
                if (node == proh) continue;
                int kk = map[Math.Min(from, node) * d + Math.Max(from, node)];
                //if (kk < k) continue;
                r = Math.Min(r, kk);
            }

            if (r == 10000000) return;

            foreach (var node in adj[from]) {
                if (node == proh) continue;
                int kk = map[Math.Min(from, node) * d + Math.Max(from, node)];
                if (kk != r) continue;
                list.Add(kk);
                track(head, node, from, list, adj, map);
                return;
            }
        }

        static int tip;
        static void Add(int[] w, int c) {
            if (w[c] == 0) {
                w[c] = 1;
                tip = Math.Max(c, tip);
                return;
            }
            w[c] = 0;
            Add(w, c + 1);
        }

        public static void bin(BigInteger x) {
            StringBuilder sb = new StringBuilder();
            bool r = true;
            while (x >= 0) {
                if (x == 0) {
                    if (r) sb.Append("0");
                    break;
                }
                r = r || x % 2 == 1;
                if (r) sb.Append(BigInteger.Remainder(x, 2).ToString());
                x /= 2;
            }
            var w = sb.ToString().Reverse().ToArray();
            int i = 0;
            while (w[i] == 48) i++;

            for (; i < w.Length; i++) {
                Console.Write(w[i]);
            }
        }

        static void AddItemToMap(List<int>[][] map, int a, int b, int n, List<int> list) {
            if (map[a] == null) {
                map[a] = new List<int>[n + 1];
            }
            if (map[a][b] == null) {
                map[a][b] = list;
            }
        }
        #endregion

        #region ___Gridland_Provinces
        public static void Gridland_Provinces() {
            int __ = 1;// int.Parse(Console.ReadLine());
            while (__-- > 0) {
                int n = 6 * 62;// int.Parse(Console.ReadLine());
                string top = "nvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaenvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwfttefawffdwfttefanvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwfttefanvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaenvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwfttefawffdwfttefanvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwfttefa";// Console.ReadLine();
                string bot = "nvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwnvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwfttefafttefaatttefaewfafasdfawttttefaeaasdfawttttenvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaenvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwfttefawffdwfttefanvmcnamcnweawefwewefaeurqowrpyqoteiljkfsdurqowtefaewffdwfttefafaefawttttefaeexqawefaws";// Console.ReadLine();

                n = top.Length;
                Console.WriteLine("n: {0}", n);

                string topR = new string(top.Reverse().ToArray());
                string botR = new string(bot.Reverse().ToArray());

                var A = new HashSet<int>();
                A.Add(hashF(top + botR));
                A.Add(hashF(bot + topR));
                King(n, top, bot, A);

                A.Add(hashF(topR + bot));
                A.Add(hashF(botR + top));
                King(n, topR, botR, A);



                Console.WriteLine(A.Count);
                Console.WriteLine(276028);
            }
        }

        static void King(int n, string top, string bot, HashSet<int> A) {
            HashSet<string> T = new HashSet<string>(), B = new HashSet<string>();
            T.Add(top[0].ToString() + bot[0]); B.Add(bot[0].ToString() + top[0]);
            for (int i = 1; i < n; i++) {
                string t = "";
                for (int j = i; j < n; j++) t += top[j];
                for (int j = n - 1; j >= i; j--) t += bot[j];

                foreach (var item in B) {
                    if (item[item.Length - 1] == '-')
                        A.Add(hashF(t + item.Substring(0, item.Length - 1)));
                    else A.Add(hashF(t + item));
                }

                t = "";
                for (int j = i; j < n; j++) t += bot[j];
                for (int j = n - 1; j >= i; j--) t += top[j];
                foreach (var item in T) {
                    if (item[item.Length - 1] == '-')
                        A.Add(hashF(t + item.Substring(0, item.Length - 1)));
                    else A.Add(hashF(t + item));
                }
                HashSet<string> T2 = new HashSet<string>(), B2 = new HashSet<string>();
                t = bot[i].ToString() + top[i];
                foreach (var item in T) {

                    if (item[item.Length - 1] != '-') {
                        B2.Add(top[i] + item + bot[i]);
                        T2.Add(t + item + '-');
                    } else T2.Add(t + item);
                }
                t = top[i].ToString() + bot[i];
                foreach (var item in B) {
                    if (item[item.Length - 1] != '-') {
                        T2.Add(bot[i] + item + top[i]);
                        B2.Add(t + item + '-');
                    } else B2.Add(t + item);
                }

                T = B2; B = T2;
            }
        }
        static int hashF(string s) {
            int len = s.Length;
            int r = 31 * s.GetHashCode() + len * 17;


            if (len > 200) {
                r -= s[139] + s[33] + (s[23] ^ s[66]) + (s[3] & s[66]) + (s[14] | s[101]);
            } else if (len > 100) {
                r += s[99] * 33249;
            } else if (len > 40) {
                r *= 7 * s[39];
            } else if (len > 20) {
                r += s[19] ^ 13;
            } else if (len > 10) {
                r += s[5] * 1313;
            } else if (len > 5) {
                r += s[4] + 9191;
            } else if (len > 3) {
                r += (s[2] << 2) * 11;
            } else r += 3562;


            return r * s[0].GetHashCode();
        }
        private static void BruteKing(int n, string top, string bot) {
            hgr = new HashSet<string>();
            for (int i = 0; i < n; i++) {
                bool[][] V = new bool[2][];
                V[0] = new bool[n]; V[1] = new bool[n];
                trace(0, i, n, 2 * n, top, bot, V, "");

                V[0] = new bool[n]; V[1] = new bool[n];
                trace(1, i, n, 2 * n, top, bot, V, "");
            }
            Console.WriteLine(hgr.Count);
        }

        static void trace(int x, int y, int n, int m, string top, string bot, bool[][] V, string cur) {
            if (V[x ^ 1][y] && y > 0 && !V[x][y - 1] && y + 1 < n && !V[x][y + 1] &&
                (!V[x ^ 1][y + 1] && !V[x ^ 1][y - 1])) return;
            V[x][y] = true;
            cur += x == 1 ? bot[y] : top[y];
            if (m == 1) { hgr.Add(cur); return; }
            m--;
            bool[][] g = new bool[2][];
            g[0] = new bool[n];
            g[1] = new bool[n];


            if (!V[x ^ 1][y]) {
                for (int rr = 0; rr < n; rr++) { g[0][rr] = V[0][rr]; g[1][rr] = V[1][rr]; }
                trace(x ^ 1, y, n, m, top, bot, g, cur);
            }

            if (y + 1 < n && !V[x][y + 1]) {
                for (int rr = 0; rr < n; rr++) { g[0][rr] = V[0][rr]; g[1][rr] = V[1][rr]; }
                trace(x, y + 1, n, m, top, bot, g, cur);
            }

            if (y > 0 && !V[x][y - 1]) {
                trace(x, y - 1, n, m, top, bot, V, cur);
            }

        }
        static HashSet<string> hgr;
        static string tour(int x, int n, string top, string bottom) {
            StringBuilder sb = new StringBuilder();
            for (int i = x; i < n; i++) {
                sb.Append(top[i]);
            }
            for (int i = n - 1; i >= 0; i--) {
                sb.Append(bottom[i]);
            }
            for (int i = 0; i < x; i++) {
                sb.Append(top[i]);
            }
            return sb.ToString();
        }
        static string[] tour2(int n, string top, string bottom) {
            string[] str = new string[4];
            StringBuilder sb = new StringBuilder();
            sb.Append(top[0]);
            for (int i = 0; i < n; i++) {
                sb.Append(bottom[i]);
            }
            for (int i = n - 1; i >= 1; i--) {
                sb.Append(top[i]);
            }

            str[0] = sb.ToString();
            sb.Clear();

            sb.Append(bottom[0]);
            for (int i = 0; i < n; i++) {
                sb.Append(top[i]);
            }
            for (int i = n - 1; i >= 1; i--) {
                sb.Append(bottom[i]);
            }
            str[1] = sb.ToString();
            sb.Clear();

            sb.Append(top[n - 1]);
            for (int i = n - 1; i >= 0; i--) {
                sb.Append(bottom[i]);
            }
            for (int i = 0; i < n - 1; i++) {
                sb.Append(top[i]);
            }
            str[2] = sb.ToString();
            sb.Clear();

            sb.Append(bottom[n - 1]);
            for (int i = n - 1; i >= 0; i--) {
                sb.Append(top[i]);
            }
            for (int i = 0; i < n - 1; i++) {
                sb.Append(bottom[i]);
            }
            str[3] = sb.ToString();

            return str;
        }
        #endregion

        #region ___

        #endregion

    }
}
