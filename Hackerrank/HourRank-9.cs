using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank
{
    class HourRank_9
    {

        #region ___Fair_Rations
        public static void Fair_Rations() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x) % 2);

            int count = 0;
            if (arr.Sum() % 2 == 1) { Console.WriteLine("NO"); } else {
                int j = -1;
                for (int i = 0; i < n; i++) {
                    if (arr[i] == 1) {
                        if (j == -1) {
                            j = i;
                        } else {
                            count += 2 * (i - j);
                            j = -1;
                        }
                    }
                }
                Console.WriteLine(count);
            }

        }
        #endregion

        #region ___Mandragora_Forest
        public static void Mandragora_Forest() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                decimal[] h = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToDecimal(x));

                Array.Sort(h);
                decimal[] sum = new decimal[n];
                sum[0] = h[0];
                for (int i = 1; i < n; i++) {
                    sum[i] = sum[i - 1] + h[i];
                }

                decimal experience = 0;
                for (int i = n - 1; i >= 0; i--) {
                    int tmps = i + 1;
                    decimal tmp = 0;
                    tmp += (tmps * (sum[n - 1] - (i - 1 >= 0 ? sum[i - 1] : 0)));
                    if (tmp > experience) { experience = tmp; }
                }
                Console.WriteLine(experience);
            }
        }
        #endregion

        #region ___Longest_Mod_Path
        public static void Longest_Mod_Path() {
            // Editorial
            // https://www.hackerrank.com/contests/hourrank-9/challenges/longest-mod-path/editorial
            int n = int.Parse(Console.ReadLine()); // number of rooms

            HashSet<Tuple<int, decimal>>[] adjacent = new HashSet<Tuple<int, decimal>>[n + 1];
            for (int i = 0; i <= n; i++) adjacent[i] = new HashSet<Tuple<int, decimal>>();

            for (int i = 0; i < n; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]);
                int r = int.Parse(tmp[1]);
                decimal x = decimal.Parse(tmp[2]);
                adjacent[l].Add(Tuple.Create(r, x));
                adjacent[r].Add(Tuple.Create(l, x * -1));

            }

            decimal[] distance = new decimal[n + 1];
            bool[] visited = new bool[n + 1];
            distance[1] = 0;
            visited[1] = true;

            HashSet<int>[] parents = new HashSet<int>[n + 1];
            for (int i = 0; i <= n; i++) parents[i] = new HashSet<int>();

            Queue<int> Q = new Queue<int>();
            Q.Enqueue(1);
            decimal cycle = 0;
            while (Q.Count > 0) {
                int root = Q.Dequeue();
                foreach (var node in adjacent[root]) {
                    int point = node.Item1; decimal val = distance[root] + node.Item2;
                    if (parents[root].Contains(point)) continue;
                    parents[point].Add(root);
                    if (!visited[point]) {
                        visited[point] = true;
                        distance[point] = val;
                        Q.Enqueue(point);
                    } else {
                        cycle = Math.Abs(distance[point] - val);
                    }
                }
            }


            int q = int.Parse(Console.ReadLine());
            while (q-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int s = int.Parse(tmp[0]);
                int e = int.Parse(tmp[1]);
                int m = int.Parse(tmp[2]);

                decimal a = gcd(cycle, m);

                // avoid negative mod output
                decimal disDiff = (distance[e] - distance[s]) % a;
                Console.WriteLine(m - a + (a + disDiff) % a);
            }

        }
        static decimal gcd(decimal a, decimal b) {
            if (a == 0) return b;
            return gcd(b % a, a);
        }
        #endregion
    }
}
