using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank.UniversityCodeSprint3 {
    class Simple_Tree_Counting {
        static int N;
        static List<edge>[] adj;
        static List<edge> edges = new List<edge>();
        static void Main2() {
            Console.SetIn(new System.IO.StreamReader("input"));
            N = int.Parse(Console.ReadLine());
            adj = new List<edge>[N];
            for (int i = 0; i < N; i++) adj[i] = new List<edge>();
            for (int i = 0; i < N - 1; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]) - 1;
                int b = int.Parse(tmp[1]) - 1;
                int c = int.Parse(tmp[2]);
                var ed = new edge { from = a, to = b, color = c, i = edges.Count };
                edges.Add(ed);
                adj[a].Add(ed);
                adj[b].Add(ed);
            }
            init();
            int Q = int.Parse(Console.ReadLine());
            StringBuilder sb = new StringBuilder();
            while (Q-- > 0) {
                var tmp = Console.ReadLine().Split(' ');

                if (tmp[0] == "1") {
                    int ei = int.Parse(tmp[1]) - 1;
                    int ci = int.Parse(tmp[2]);
                    updateEdge(edges[ei], ci);
                } else if (tmp[0] == "2") {
                    int l = int.Parse(tmp[1]);
                    int r = int.Parse(tmp[2]);
                    sb.AppendLine(TS(l, r).ToString());
                } else {
                    sb.AppendLine(PX(int.Parse(tmp[1]) - 1).ToString());
                }
            }

            sb.Length--;
            Console.WriteLine(sb.ToString());
        }

        static int current = 0;
        static int[] compCount, compColor;
        static long[] fenwick;
        static void init() {
            var mx = 1000 * 2000;
            compColor = new int[mx];
            compCount = new int[mx];
            fenwick = new long[mx];
            for (int i = 0; i < N-1; i++) {
                edges[i].comp = 0;
            }
            dfs(0, -1, -1, 1);

            for (int i = 1; i <= current; i++) {
                var c = compColor[i];
                updatefenwick(c, simple(compCount[i]));
            }
        }

        static void dfs(int fr, int no, int clr, int component) {

            foreach (var e in adj[fr]) {
                if (e.comp != 0) continue;
                if (e.color == clr) {
                    e.setComp(component);
                    dfs(e.from == fr ? e.to : e.from, fr, e.color, e.comp);
                } else {
                    current++;
                    e.setComp(current);
                    dfs(e.from, -1, e.color, e.comp);
                    dfs(e.to, -1, e.color, e.comp);
                }
            }

        }

        static void dfs2(int fr, int no, int clr, int component) {

            foreach (var e in adj[fr]) {
                if (e.from == fr && e.to == no) continue;
                if (e.to == fr && e.from == no) continue;
                if (e.color != clr) continue;

                e.setComp(component);
                dfs2(e.from == fr ? e.to : e.from, fr, e.color, e.comp);
            }
        }

        static void updateEdge(edge e, int cn) {
            var co = e.color;
            if (co == cn) return;
            long _c = 0;

            updatefenwick(co, -simple(compCount[e.comp]));
            // the old
            var eql = -1;
            edge eeql = null;
            foreach (var es in adj[e.from]) {
                if (es == e) continue;
                if (es.color == co) { eeql = es; eql = es.from == e.from ? es.to : es.from; break; }
            }

            var eqr = -1;
            edge eeqr = null;
            foreach (var es in adj[e.to]) {
                if (es == e) continue;
                if (es.color == co) { eeqr = es; eqr = es.from == e.to ? es.to : es.from; break; }
            }


            if (eql != -1 && eqr != -1) {
                current++;
                dfs2(e.to, e.from, co, current);
                //updatefenwick(co, compCount[current]);
            }
            //updatefenwick(co, simple(compCount[e.comp] - 1));


            // the new
            eql = -1;
            edge qeql = null;
            foreach (var es in adj[e.from]) {
                if (es == e) continue;
                if (es.color == cn) { qeql = es; eql = es.from == e.from ? es.to : es.from; break; }
            }

            eqr = -1;
            edge qeqr = null;
            foreach (var es in adj[e.to]) {
                if (es == e) continue;
                if (es.color == cn) { qeqr = es; eqr = es.from == e.to ? es.to : es.from; break; }
            }

            _c = 0;
            if (qeqr != null) _c += simple(compCount[qeqr.comp]);
            if (qeql != null) _c += simple(compCount[qeql.comp]);
            updatefenwick(cn, -_c);

            e.color = cn;
            if (eql == -1 && eqr == -1) {
                current++;
                e.setComp(current);
            } else if (eqr == -1) {
                e.setComp(qeql.comp);
            } else if (eql == -1) {
                e.setComp(qeqr.comp);
            } else {
                e.setComp(qeqr.comp);
                dfs2(e.from, e.to, cn, qeqr.comp);
            }
            updatefenwick(cn, simple(compCount[e.comp]));

            _c = 0;
            if (eeqr != null) _c += simple(compCount[eeqr.comp]);
            if (eeql != null) _c += simple(compCount[eeql.comp]);
            updatefenwick(co, _c);

        }

        private static long simple(int v) {
            return Math.BigMul(v, v + 1) / 2;
        }

        static long TS(int l, int r) {
            long s = query_fenwick(r);

            if (l == 0) return s;
            return s - query_fenwick(l - 1);
        }

        static long PX(int v) {
            return simple(compCount[edges[v].comp]);
        }
        class edge {
            public int from, to, color, i, comp;

            internal void setComp(int component) {
                compCount[comp]--;
                comp = component;
                compColor[component] = color;
                compCount[component]++;
            }
        }

        static long query_fenwick(int i) {
            i++;
            long sum = 0;
            for (; i > 0; i -= i & (-i)) sum = (sum + fenwick[i]);
            return sum;
        }

        static void updatefenwick(int i, long val) {
            i++;
            for (int n = 3 * N; i <= n; i += i & (-i)) fenwick[i] = (fenwick[i] + val);
        }
    }


}
