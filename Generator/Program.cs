using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator {
    class Program {


        static Random rand = new Random();
        [STAThread]
        static void Main(string[] args) {
            var testCases = new Part(Min: 100000, Max: 100000, Col: 2, Row: 1);
            var array = new Part(Min: 1, Max: 100000000, Col: testCases.first(), Row: 1);
            var queries = new Part(Min: 1, Max: testCases.first(), Col: 3, Row: testCases.second());
            var v = new View(testCases,  array, queries);
            v.Copy();
            v.Show();
        }


        private static void Nominating_Group_Leaders() {
            StringBuilder sb = new StringBuilder();
            int minn = 100000, ming = 100000;
            int n = rand.Next(3, minn);
            int[] v = new int[n];
            for (int i = 0; i < n; i++) v[i] = rand.Next(0, n);
            sb.AppendLine("1");
            sb.AppendLine(n.ToString());
            sb.AppendLine(string.Join(" ", v));

            int g = rand.Next(3, ming);
            sb.AppendLine(g.ToString());
            while (g-- > 0) {
                int l = rand.Next(0, Math.Max(n, 100));
                int r = rand.Next(l, n);
                int x = rand.Next(1, n + 1);
                sb.AppendLine(l + " " + r + " " + x);
            }
            Console.WriteLine(sb.ToString());
            Clipboard.SetText(sb.ToString());
        }

        private static void Spanning_Tree_Fraction() {
            int n = 10, m = 500;
            int min = 1, max = 101;
            List<edge> List = new List<edge>();
            var ies = Enumerable.Range(0, n).OrderBy(x => rand.Next()).ToList();
            for (int i = 0; i < n - 1; i++) {
                List.Add(new edge(ies[i], ies[i + 1], rand.Next(min, max), rand.Next(min, max)));
            }
            for (int i = n - 1; i < m; i++) {
                int u = rand.Next(0, n), v = rand.Next(0, n);
                List.Add(new edge(ies[u], ies[v], rand.Next(min, max), rand.Next(min, max)));
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(n + " " + m);
            foreach (var item in List) {
                sb.AppendLine(item.ToString());
            }
            Console.WriteLine(sb.ToString());
            Clipboard.SetText(sb.ToString());
        }
    }

    class edge {
        public int from, to, a, b;
        public edge(int u_, int v_, int a_, int b_) {
            from = u_; to = v_; a = a_; b = b_;
        }
        public override string ToString() {
            return from + " " + to + " " + a + " " + b;
        }
    }



}