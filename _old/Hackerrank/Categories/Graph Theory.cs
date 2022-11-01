using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank.Graph_Theory
{
    public static class Snakes_and_Ladders_The_Quickest_Way_Up
    {
        static Dictionary<int, int> visited;
        static void Start() {
            int tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {
                visited = new Dictionary<int, int>();

                int n = int.Parse(Console.ReadLine());
                int[] ladders_start = new int[n];
                int[] ladders_end = new int[n];
                for (int i_n = 0; i_n < n; i_n++) { var tmp = Console.ReadLine().Split(' '); ladders_start[i_n] = int.Parse(tmp[0]); ladders_end[i_n] = int.Parse(tmp[1]); }

                int m = int.Parse(Console.ReadLine());
                int[] snakes_start = new int[m];
                int[] snakes_end = new int[m];
                for (int i_m = 0; i_m < m; i_m++) { var tmp = Console.ReadLine().Split(' '); snakes_start[i_m] = int.Parse(tmp[0]); snakes_end[i_m] = int.Parse(tmp[1]); }

                // Breadth First Search
                bfs(n, ladders_start, ladders_end, m, snakes_start, snakes_end);
                // Dynamic Programming
                dp(n, ladders_start, ladders_end, m, snakes_start, snakes_end);
            }
        }

        static void bfs(int n, int[] ladders_start, int[] ladders_end, int m, int[] snakes_start, int[] snakes_end) {
            int final = 1000;
            var map = new List<int> { 100 };
            var holder = new List<int>();

            while (map.Count > 0) {
                for (int i = map.Count - 1; i >= 0; i--) {
                    int cur = map[i] / 100, moves = map[i] % 100;

                    map.RemoveAt(i);

                    if (holder.Contains(cur * 100 + moves)) continue; else holder.Add(cur * 100 + moves);

                    if (moves >= final) continue;

                    for (int step = 1; step <= 6; step++) {
                        int next = cur + step;
                        int iL = Array.IndexOf(ladders_start, next);
                        int iS = Array.IndexOf(snakes_start, next);

                        if (iL != -1) next = ladders_end[iL];
                        if (iS != -1) next = snakes_end[iS];

                        if (next == 100) { final = Math.Min(final, moves + 1); } else {
                            if (!map.Contains(next * 100 + moves + 1)) {
                                map.Add(next * 100 + moves + 1);
                            }
                        }
                    }
                }
            }

            Console.WriteLine(final == 1000 ? -1 : final);
        }

        static void dp(int n, int[] ladders_start, int[] ladders_end, int m, int[] snakes_start, int[] snakes_end) {
            int moves = 10000;

            move(n, ladders_start, ladders_end, m, snakes_start, snakes_end, 1, 0, ref moves);
            Console.WriteLine(moves == 10000 ? -1 : moves);
        }

        static void move(int n, int[] ladders_start, int[] ladders_end, int m, int[] snakes_start, int[] snakes_end, int start, int moves, ref int final) {
            if (start == 100) { final = Math.Min(final, moves); return; }
            if (start >= 94) { final = Math.Min(final, moves + 1); return; }

            if (visited.ContainsKey(start)) {
                if (visited[start] <= moves) return; else visited[start] = moves;
            } else {
                visited.Add(start, moves);
            }

            int i = 6; bool normal = false;
            while (i > 0 && !normal) {
                if (start + i <= 100 && Array.IndexOf(snakes_start, start + i) == -1 && Array.IndexOf(ladders_start, start + i) == -1) {
                    if (start + i == 100) { final = Math.Min(final, moves + 1); break; }
                    move(n, ladders_start, ladders_end, m, snakes_start, snakes_end, start + i, moves + 1, ref final);
                    normal = true;
                }
                i--;
            }

            i = 6;
            while (i > 0) {
                if (start + i < 100) {

                    int index = Array.IndexOf(ladders_start, start + i);
                    if (index > -1) {
                        move(n, ladders_start, ladders_end, m, snakes_start, snakes_end, ladders_end[index], moves + 1, ref final);
                    }

                    index = Array.IndexOf(snakes_start, start + i);
                    if (index > -1) {
                        move(n, ladders_start, ladders_end, m, snakes_start, snakes_end, snakes_end[index], moves + 1, ref final);
                    }
                }
                i--;
            }
        }
    }

    public static class Dijkstra_Shortest_Reach_2
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);
                HashSet<Tuple<int, int>>[] adj = new HashSet<Tuple<int, int>>[n];
                for (int i = 0; i < n; i++) adj[i] = new HashSet<Tuple<int, int>>();

                for (int i = 0; i < m; i++) {
                    tmp = Console.ReadLine().Split(' ');
                    int l = int.Parse(tmp[0]) - 1;
                    int r = int.Parse(tmp[1]) - 1;
                    int c = int.Parse(tmp[2]);
                    adj[l].Add(Tuple.Create(r, c));
                    adj[r].Add(Tuple.Create(l, c));
                }
                int s = int.Parse(Console.ReadLine()) - 1;

                int[] distance = new int[n];
                for (int i = 0; i < n; i++) distance[i] = -1;
                distance[s] = 0;

                Queue<int> Q = new Queue<int>();
                Q.Enqueue(s);

                while (Q.Count > 0) {
                    int cur = Q.Dequeue();

                    int dis = distance[cur];

                    foreach (var node in adj[cur]) {
                        int dis2 = dis + node.Item2;
                        if (distance[node.Item1] == -1 || distance[node.Item1] > dis2) {
                            distance[node.Item1] = dis2;
                            Q.Enqueue(node.Item1);
                        }
                    }
                }

                var sb = new StringBuilder();
                for (int i = 0; i < n; i++) {
                    if (i == s) continue;
                    sb.Append(distance[i]).Append(" ");
                }
                Console.WriteLine(sb.ToString());
            }
        }

        public static void Shortest_Reach() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);
                int[,] graph = new int[n, n];

                for (int i = 0; i < m; i++) {
                    tmp = Console.ReadLine().Split(' ');
                    int l = int.Parse(tmp[0]) - 1;
                    int r = int.Parse(tmp[1]) - 1;
                    int c = int.Parse(tmp[2]);
                    int x = graph[l, r];
                    if (x == 0 || x > c) {
                        graph[l, r] = graph[r, l] = c;
                    }
                }
                int s = int.Parse(Console.ReadLine()) - 1;
                int[] distance = new int[n];
                for (int i = 0; i < n; i++) distance[i] = -1;
                bool[] V = new bool[n];
                distance[s] = 0;

                int u = s;
                while (u != -1) {
                    V[u] = true;
                    for (int v = 0; v < n; v++) {
                        if (!V[v] && graph[u, v] != 0) {
                            if (distance[v] == -1 || distance[v] > distance[u] + graph[u, v]) {
                                distance[v] = distance[u] + graph[u, v];
                            }
                        }

                    }
                    u = minDistance(distance, V);
                }

                var sb = new StringBuilder();
                for (int i = 0; i < n; i++) {
                    if (i == s) continue;
                    sb.Append(distance[i]).Append(" ");
                }
                Console.WriteLine(sb.ToString());
            }
        }

        static int minDistance(int[] distance, bool[] V) {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V.Length; v++) {
                if (!V[v] && distance[v] != -1 && distance[v] <= min) {
                    min = distance[v];
                    min_index = v;
                }
            }

            return min_index;
        }

    }

    public static class Prim_MST_Special_Subtree
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int[] start = new int[m], end = new int[m], weight = new int[m];
            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                start[i] = int.Parse(tmp[0]);
                end[i] = int.Parse(tmp[1]);
                weight[i] = int.Parse(tmp[2]);
            }
            int startPoint = int.Parse(Console.ReadLine());


            bool[] visited = new bool[n];
            bool[] full = new bool[n];

            HashSet<int>[] hash = new HashSet<int>[n];
            for (int i = 0; i < n; i++) { hash[i] = new HashSet<int>(); }
            for (int i = 0; i < m; i++) {
                hash[start[i] - 1].Add(i);
                hash[end[i] - 1].Add(i);
            }
            visited[startPoint - 1] = true;

            var stack = new List<int> { startPoint };

            int nn = n;
            double r = 0;
            while (nn > 1) {
                int min = int.MaxValue, e = -1;

                for (int k = stack.Count - 1; k >= 0; k--) {
                    int i = stack[k];
                    if (!full[i - 1] && visited[i - 1]) {

                        full[i - 1] = true;
                        foreach (var j in hash[i - 1]) {

                            if (start[j] == i && !visited[end[j] - 1]) {
                                full[i - 1] = false;
                                if (weight[j] < min) { min = weight[j]; e = end[j]; }
                            } else if (end[j] == i && !visited[start[j] - 1]) {
                                full[i - 1] = false;
                                if (weight[j] < min) { min = weight[j]; e = start[j]; }
                            }

                        }
                        if (full[i - 1]) stack.RemoveAt(k);
                    }
                }

                visited[e - 1] = true;
                nn--;
                r += min;
                if (!stack.Contains(e)) stack.Add(e);
            }

            Console.WriteLine(r);
        }

        struct Node
        {
            public Node(int s) {
                start = -1;
                end = -1;
                weight = -1;
            }
            public Node(int s, int e, int w) {
                start = s;
                end = e;
                weight = w;
            }
            public int start;
            public int end;
            public int weight;
            public bool contains(int i) {
                return start == i || end == i;
            }
            public int getEnd(int i) {
                if (start == i) return end;
                return start;
            }
            public bool isEmpty() {
                return start == -1;
            }
        }
    }

    public static class Kruskal_MST_Really_Special_Subtree
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int[] start = new int[m], end = new int[m];//, weight = new int[m];
            Dictionary<int, int> weight = new Dictionary<int, int>();
            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                start[i] = int.Parse(tmp[0]);
                end[i] = int.Parse(tmp[1]);
                weight.Add(i, int.Parse(tmp[2]));
            }
            int startPoint = int.Parse(Console.ReadLine());

            weight = weight.OrderBy(x => x.Value).ToDictionary(a => a.Key, b => b.Value);
            List<int>[] sets = new List<int>[n];
            for (int i = 0; i < n; i++) { sets[i] = new List<int> { i + 1 }; }

            int r = 0;

            foreach (var key in weight.Keys) {
                var curWeight = weight[key];

                int ls = -1, rs = -1;
                int left = start[key], right = end[key];
                for (int i = 0; i < n && (ls < 0 || rs < 0); i++) {
                    if (sets[i].Contains(left)) { ls = i; }
                    if (sets[i].Contains(right)) { rs = i; }
                }

                if (ls == rs) continue;

                sets[ls].AddRange(sets[rs]);
                sets[rs].Clear();

                r += curWeight;
            }

            Console.WriteLine(r);
        }
    }

    public static class Floyd__City_of_Blinding_Lights
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);


            int[][] map = new int[n + 1][];
            for (int i = 0; i <= n; i++) { map[i] = new int[n + 1]; for (int j = 0; j <= n; j++) map[i][j] = 10000000; }
            for (int i = 0; i <= n; i++) map[i][i] = 0;

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                map[int.Parse(tmp[0])][int.Parse(tmp[1])] = int.Parse(tmp[2]);
            }
            int q = int.Parse(Console.ReadLine());

            for (int k = 1; k <= n; k++)
                for (int i = 1; i <= n; i++)
                    for (int j = 1; j <= n; j++)
                        map[i][j] = Math.Min(map[i][j], map[i][k] + map[k][j]);

            while (q-- > 0) {
                tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]);
                int b = int.Parse(tmp[1]);
                Console.WriteLine(map[a][b] < 10000000 ? map[a][b] : -1);
            }
        }
    }

    public static class Even_Tree
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);

            HashSet<int>[] directChildren = new HashSet<int>[n + 1];
            for (int i = 1; i <= n; i++) directChildren[i] = new HashSet<int>();
            HashSet<int>[] children = new HashSet<int>[n + 1];
            for (int i = 1; i <= n; i++) children[i] = new HashSet<int>();
            HashSet<int>[] parents = new HashSet<int>[n + 1];
            for (int i = 1; i <= n; i++) parents[i] = new HashSet<int>();
            bool[] parent = new bool[n + 1];

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]);
                int b = int.Parse(tmp[1]);
                parent[a] = true;
                parents[a].Add(b);
                foreach (var par in parents[b]) {
                    parents[a].Add(par);
                }
                directChildren[b].Add(a);
                children[b].Add(a);
                foreach (var par in parents[b]) {
                    children[par].Add(a);
                }
            }

            int root = -1;
            for (int i = 1; i <= n; i++) if (!parent[i]) { root = i; break; }

            int count = 0;
            dfs(children, directChildren, root, ref count);
            Console.WriteLine(count);
        }

        static void dfs(HashSet<int>[] children, HashSet<int>[] directChildren, int root, ref int count) {
            foreach (var node in directChildren[root]) {
                if (children[node].Count % 2 == 1) count++;
                dfs(children, directChildren, node, ref count);
            }
        }
    }

    public static class Cut_the_tree
    {
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));
            int[] sums = new int[n];

            bool[] _parent = new bool[n + 1];
            HashSet<int>[] adj = new HashSet<int>[n + 1];
            for (int i = 1; i <= n; i++) {
                adj[i] = new HashSet<int>();
            }

            for (int i = 0; i < n - 1; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]);
                int r = int.Parse(tmp[1]);
                adj[r].Add(l);
                adj[l].Add(r);
                _parent[r] = true;
            }
            int root = -1;
            for (int i = 1; i <= n; i++) {
                if (!_parent[i]) { root = i; break; }
            }

            int curdiff = int.MaxValue;
            sums[root - 1] = dfs(adj, root, 0, arr, sums);
            for (int i = 0; i < n; i++) {
                curdiff = Math.Min(curdiff, Math.Abs(sums[root - 1] - 2 * sums[i]));
            }
            Console.WriteLine(curdiff);
        }

        static int dfs(HashSet<int>[] adj, int root, int p, int[] arr, int[] sums) {
            foreach (var node in adj[root]) {
                if (node == p) continue;
                sums[node - 1] = dfs(adj, node, root, arr, sums);
            }
            int ret = arr[root - 1];
            foreach (var node in adj[root]) {
                if (node == p) continue;
                ret += sums[node - 1];
            }
            return ret;
        }
    }

    public static class Clique
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);

                int low = 1, high = n + 1;
                while (low + 1 < high) {
                    int mid = (high + low) / 2;
                    if (turan(n, mid) < m) low = mid; else high = mid;
                }

                Console.WriteLine(high);
            }
        }

        static int turan(int n, int k) {
            int result = (n % k) * (k - n % k) * (1 + n / k) * (n / k);
            result += NxN_1(n % k) * sq(1 + n / k) / 2;
            result += NxN_1(k - n % k) * sq((n / k)) / 2;
            return result;
        }
        static int sq(int n) { return n * n; }
        static int NxN_1(int n) { return n * (n - 1); }

    }

    public static class Crab_Graphs
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);  // the number of nodes
                int t = int.Parse(tmp[1]);  // max number of feet in the crab graph,
                int m = int.Parse(tmp[2]);  // number of edges

                // since the indexing is 1-based, reserve 0 for source and 1 for sink
                int[][] graph = new int[2 * n + 2][];
                for (int _ = 0; _ < 2 * n + 2; _++) graph[_] = new int[2 * n + 2];

                for (int i = 0; i < m; i++) {
                    tmp = Console.ReadLine().Split(' ');
                    int a_ = int.Parse(tmp[0]);
                    int b_ = int.Parse(tmp[1]);

                    graph[2 * a_][2 * b_ + 1] = 1;
                    graph[2 * b_][2 * a_ + 1] = 1;

                    graph[0][2 * a_] = t;
                    graph[2 * a_][0] = t;

                    graph[0][2 * b_] = t;
                    graph[2 * b_][0] = t;

                    graph[1][2 * a_ + 1] = 1;
                    graph[2 * a_ + 1][1] = 1;

                    graph[1][2 * b_ + 1] = 1;
                    graph[2 * b_ + 1][1] = 1;
                }

                // 0 is source and 1 is sink
                Console.WriteLine(fordFulkerson(graph, 0, 1, graph.Length));
            }
        }

        static int fordFulkerson(int[][] graph, int source, int sink, int n) {
            int u, v;

            int[][] residual = new int[n][];
            for (int _ = 0; _ < n; _++) residual[_] = new int[n];

            for (u = 0; u < n; u++) for (v = 0; v < n; v++) residual[u][v] = graph[u][v];

            int[] parent = new int[n];

            int max_flow = 0;

            while (bfs(residual, source, sink, parent, n)) {
                int path = int.MaxValue;
                v = sink;
                while (v != source) {
                    u = parent[v];
                    path = Math.Min(path, residual[u][v]);

                    v = parent[v];
                }

                v = sink;
                while (v != source) {
                    u = parent[v];
                    residual[u][v] -= path;
                    residual[v][u] += path;

                    v = parent[v];
                }
                max_flow += path;
            }
            return max_flow;
        }
        static bool bfs(int[][] residual, int source, int sink, int[] parent, int n) {
            bool[] visited = new bool[n];

            Queue<int> Q = new Queue<int>();
            Q.Enqueue(source);
            visited[source] = true;
            parent[source] = -1;

            while (Q.Count > 0) {
                int u = Q.Dequeue();

                for (int v = 0; v < n; v++) {
                    if (visited[v] == false && residual[u][v] > 0) {
                        Q.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            return (visited[sink] == true);
        }
    }

    public static class Journey_to_the_Moon
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int N = int.Parse(tmp[0]);
            int I = int.Parse(tmp[1]);
            List<HashSet<int>> adj = new List<HashSet<int>>();

            bool[] seen = new bool[N];
            for (int i = 0; i < I; i++) {
                tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]);
                int r = int.Parse(tmp[1]);

                seen[l] = true;
                seen[r] = true;
                bool exist = false;
                HashSet<int> a = null, b = null;
                foreach (var h in adj) {
                    if (h.Contains(l)) { exist = true; h.Add(r); a = h; } else if (h.Contains(r)) { exist = true; h.Add(l); b = h; }
                }
                if (a != null && b != null) {
                    b.UnionWith(a);
                    a.Clear();
                    for (int j = adj.Count - 1; j >= 0; j--) {
                        if (adj[j].Count == 0) { adj.RemoveAt(j); break; }
                    }
                }
                if (!exist) adj.Add(new HashSet<int> { l, r });
            }

            List<long> w = new List<long>();
            for (int i = 0; i < adj.Count; i++) {
                w.Add(adj[i].Count);
            }
            for (int i = 0; i < N; i++) {
                if (!seen[i]) w.Add(1);
            }
            int n = w.Count;
            var ww = w.ToArray();
            for (int i = 1; i < n; i++) {
                ww[i] += ww[i - 1];
            }
            long ans = 0;
            for (int i = 0; i < n; i++) {
                ans += w[i] * (ww[n - 1] - ww[i]);
            }
            Console.WriteLine(ans);
        }

        static void AddRec(int from, int to, bool[] seen, HashSet<int>[] adj) {
            if (seen[from]) return;
            foreach (var node in adj[from]) {
                if (node == to) continue;
                if (node > to) adj[to].Add(node);
                AddRec(node, to, seen, adj);
            }
        }
    }

    public static class Minimum_Penalty_Path
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);

            var w2 = new HashSet<int>[n + 1, n + 1];
            for (int i = 0; i <= n; i++)
                for (int j = 0; j <= n; j++)
                    w2[i, j] = new HashSet<int>();

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                int u = int.Parse(tmp[0]);
                int v = int.Parse(tmp[1]);
                int c = int.Parse(tmp[2]);
                w2[u, v].Add(c);
                w2[v, u].Add(c);
            }

            tmp = Console.ReadLine().Split(' ');
            int a = int.Parse(tmp[0]);
            int b = int.Parse(tmp[1]);

            var dis = new HashSet<int>[n + 1];
            for (int i = 0; i <= n; i++) dis[i] = new HashSet<int>();

            dis[b].Add(0);
            int[] w = new int[n + 1];
            for (int i = 0; i <= n; i++) w[i] = 100;
            Queue<int> Q = new Queue<int>();
            Q.Enqueue(b);
            while (Q.Count > 0) {
                int x = Q.Dequeue();
                foreach (var cur in dis[x]) {
                    for (int i = 1; i <= n; i++) {
                        if (i == x || i == b) continue;

                        bool ar = false;
                        int nw = int.MaxValue;
                        foreach (var item in w2[x, i]) {
                            nw = item | cur;
                            bool ignore = w[i] != 100 && (int)Math.Log(nw, 2) > w[i];
                            if (!ignore && dis[i].Add(nw)) { ar = true; w[i] = Math.Min(w[i], (int)Math.Log(nw, 2)); }
                        }
                        if (ar && !Q.Contains(i)) Q.Enqueue(i);
                    }
                }
            }

            int ans = -1;
            foreach (var item in dis[a]) {
                if (ans == -1 || item < ans) ans = item;
            }
            Console.WriteLine(ans);
        }
    }

    public static class Rust_and_Murderer
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int m = int.Parse(tmp[1]);

                int[] distance = new int[n + 1];
                for (int i = 0; i <= n; i++) distance[i] = 1;

                HashSet<int>[] roads = new HashSet<int>[n + 1];
                for (int i = 0; i <= n; i++) roads[i] = new HashSet<int>();
                for (int i = 0; i < m; i++) {
                    tmp = Console.ReadLine().Split(' ');
                    int a = int.Parse(tmp[0]);
                    int b = int.Parse(tmp[1]);
                    roads[a].Add(b); roads[b].Add(a);
                }

                int s = int.Parse(Console.ReadLine());
                var sroad = roads[s];
                foreach (var item in sroad) distance[item] = int.MaxValue;

                distance[s] = 0;
                Queue<int> Q = new Queue<int>();
                Q.Enqueue(s);
                for (int i = 1; i <= n; i++) {
                    if (sroad.Contains(i)) distance[i] = int.MaxValue; else Q.Enqueue(i);
                }
                while (Q.Count > 0) {
                    int z = Q.Count;
                    int cur = Q.Dequeue(), dis = distance[cur] + 1;

                    var hr = new HashSet<int>();
                    var croad = roads[cur];
                    foreach (var i in sroad) {
                        if (i == cur || croad.Contains(i)) continue;
                        if (distance[i] > dis) {
                            distance[i] = dis;
                            Q.Enqueue(i);
                            if (dis == 2) hr.Add(i);
                        }
                    }
                    foreach (var item in hr) {
                        sroad.Remove(item);
                    }
                }

                for (int i = 1; i <= n; i++) {
                    if (i == s) continue;
                    Console.Write(distance[i] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    public static class Demanding_Money
    {
        static int mx;
        static HashSet<int>[] roads;
        static Dictionary<int, HashSet<int>> bag;
        static HashSet<int> second;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int[] C = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            if (m == 0) {
                int z = 0;
                foreach (var c in C.Where(x => x == 0)) z++;
                Console.WriteLine("{0} {1}", C.Sum(), Math.Pow(2, z));
                return;
            } else if (m == n * (n - 1) / 2) {
                int _x = C.Max(), _w = 0;
                for (int i = 0; i < n; i++) if (C[i] == _x) _w++;
                Console.WriteLine("{0} {1}", _x, _w);
                return;
            }

            roads = new HashSet<int>[n];
            for (int i = 0; i < n; i++) roads[i] = new HashSet<int>();

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                int l = int.Parse(tmp[0]) - 1;
                int r = int.Parse(tmp[1]) - 1;
                roads[l].Add(r); roads[r].Add(l);
            }
            mx = 0;
            bag = new Dictionary<int, HashSet<int>>(); bag.Add(0, new HashSet<int>());
            second = new HashSet<int>();

            int before = 0, zero = 0;
            long V = 0;
            for (int i = 0; i < n; i++) {
                if (roads[i].Count == 0) {
                    if (C[i] == 0) zero++; else before += C[i];
                    V = setbit(i, V);
                }
            }

            for (int i = 0; i < n; i++) {
                if (isset(i, V)) continue;
                TrackThis(n, i, C[i], V, C, new List<int> { i });
            }

            long y = bag[mx].Count; y = y == 0 ? 1 : y;
            Console.WriteLine("{0} {1}", mx + before, y * Math.Pow(2, zero));
        }

        static void TrackThis(int n, int h, int cur, long V, int[] C, List<int> set) {
            V = setbit(h, V);
            V = Lock_Adjacent_Houses(h, V);
            bool still = false;

            for (int i = 0; i < n; i++) {
                if (isset(i, V) || C[i] == 0) continue;

                int gr = ADD(set, i);
                if (second.Add(Hash(set))) {
                    TrackThis(n, i, cur + C[i], V, C, set);
                    still = true;
                }
                set.RemoveAt(gr);
            }

            if (still || cur < mx) return;

            for (int i = 0; i < n; i++) {
                if (isset(i, V) || C[i] != 0) continue;

                int gr = ADD(set, i);
                if (second.Add(Hash(set))) {
                    TrackThis(n, i, cur, V, C, set);
                    still = true;
                }
                set.RemoveAt(gr);
            }

            if (still) return;
            mx = cur;
            if (!bag.ContainsKey(mx)) bag.Add(mx, new HashSet<int>());

            bag[mx].Add(Hash(set));
            Zeroes(set, C, 0, 19);
        }

        static int ADD(List<int> set, int h) {
            var i = set.BinarySearch(h);
            if (i < 0) i = ~i;
            set.Insert(i, h);
            return i;
        }

        static void Zeroes(List<int> set, int[] C, int fr, int r) {
            for (int i = fr; i < set.Count; i++) {
                if (C[set[i]] == 0) Zeroes(set, C, i + 1, r);
                r = r * 31 + set[i];
            }
            bag[mx].Add(r);
        }

        static long Lock_Adjacent_Houses(int house, long V) {
            foreach (var h in roads[house]) V = setbit(h, V); return V;
        }

        static bool isset(int i, long V) { return (V >> i & (long)1) == 1; }

        static long setbit(int i, long V) { return V | (long)1 << i; }

        static int Hash(List<int> next) {
            int r = 19; foreach (var i in next) r = r * 31 + i; return r;
        }
    }

    public static class Kth_Ancestor
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                List<int>[] map = new List<int>[100000];
                for (int i = 0; i < 100000; i++) map[i] = new List<int>();
                for (int i = 0; i < n; i++) {
                    var tmp = Console.ReadLine().Split(' ');
                    int x = int.Parse(tmp[0]) - 1;
                    int y = int.Parse(tmp[1]) - 1;
                    if (y != -1) {
                        map[x].Add(y);
                        AddParents(y, map[x], map, 0);
                    }
                }

                int q = int.Parse(Console.ReadLine());
                while (q-- > 0) {
                    var tmp = Console.ReadLine().Split(' ');
                    if (tmp[0] == "0") {
                        int y = int.Parse(tmp[1]) - 1;
                        int x = int.Parse(tmp[2]) - 1;
                        map[x].Add(y);
                        AddParents(y, map[x], map, 0);
                    } else if (tmp[0] == "1") {
                        int x = int.Parse(tmp[1]) - 1;
                        map[x].Clear();
                    } else {
                        int x = int.Parse(tmp[1]) - 1;
                        int k = int.Parse(tmp[2]);

                        x = GetParent(x, k, map);
                        x++;
                        Console.WriteLine(x);
                    }
                }
            }
        }
        static int GetParent(int n, int k, List<int>[] map) {
            if (k == 0) return n;
            int max = (int)Math.Log(k, 2);

            if (max >= map[n].Count) return -1;
            int nk = (int)Math.Pow(2, max);
            return GetParent(map[n][max], k - nk, map);
        }
        static void AddParents(int n, List<int> map, List<int>[] Bigmap, int i) {
            if (i >= Bigmap[n].Count) return;

            int c = Bigmap[n][i];
            map.Add(c);
            AddParents(c, map, Bigmap, i + 1);
        }
    }

    public static class Kingdom_Connectivity
    {
        static bool infinite;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);

            infinite = false;
            bool[] V = new bool[n];
            long[] roads = new long[n];
            List<int>[] map = new List<int>[n];
            for (int i = 0; i < n; i++) {
                roads[i] = -1;
                map[i] = new List<int>();
            }

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Trim().Split(' ');
                int a = int.Parse(tmp[0]) - 1;
                int b = int.Parse(tmp[1]) - 1;
                map[a].Add(b);
            }

            dfs(0, map, roads, n, V);
            if (infinite) Console.WriteLine("INFINITE PATHS"); else Console.WriteLine(roads[0]);
        }
        static bool dfs(int fr, List<int>[] map, long[] roads, int n, bool[] V) {
            const int M_M = 1000000000;
            if (V[fr]) return true;
            if (infinite) return true;

            V[fr] = true;
            long store = 0;
            bool cur = false;
            foreach (var node in map[fr]) {
                if (node == n - 1) {
                    store++;
                } else {
                    if (roads[node] == -1) {
                        bool tmp = dfs(node, map, roads, n, V);
                        if (tmp) {
                            cur = true;
                            continue;
                        }
                    }
                    store += roads[node];
                    store %= M_M;
                }
            }

            if (cur && store > 0) infinite = true;

            roads[fr] = store;

            V[fr] = false;
            return false;
        }
    }

    public static class Favorite_sequence
    {
        public static void Start() {
            List<int>[] hb = new List<int>[1000001];
            List<int>[] adj = new List<int>[1000001];
            int[] inedge = new int[1000001];
            for (int i = 0; i < 1000001; i++) { adj[i] = new List<int>(); hb[i] = new List<int>(); }

            List<int> fin = new List<int>(), nodes = new List<int>(), Q = new List<int>();
            int n = int.Parse(Console.ReadLine());


            for (int k = 0; k < n; k++) {
                int m = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                nodes.Add(arr[0]);
                for (int i = 1; i < m; i++) {
                    nodes.Add(arr[i]);
                    hb[arr[i]].Add(arr[i - 1]);
                    adj[arr[i - 1]].Add(arr[i]);
                }
            }
            nodes = nodes.Distinct().ToList();
            nodes.Sort();

            foreach (var i in nodes) {
                inedge[i] = hb[i].Count;
                hb[i] = null;
                adj[i].Sort();

                if (inedge[i] == 0) Q.Add(i);
            }

            while (Q.Count > 0) {
                int j = Q[0];
                Q.RemoveAt(0);
                fin.Add(j);

                foreach (var s in adj[j]) {
                    inedge[s]--;
                    if (inedge[s] == 0) {
                        int i = Q.BinarySearch(s);
                        Q.Insert(~i, s);
                    }
                }
            }

            Console.WriteLine(string.Join(" ", fin));
        }
    }

    public static class Subset_Component
    {
        static long ans;
        public static void Start() {
            int n = int.Parse(Console.ReadLine());
            ulong[] A = Array.ConvertAll(Console.ReadLine().Split(' '), ulong.Parse);

            int len = 1;
            ans = 64;
            while (len <= n) {
                search(A, n, len, 0, 0, 0);
                len++;
            }
            Console.WriteLine(ans);
        }
        static void search(ulong[] A, long n, int len, ulong mask, long c1, int p) {
            if (len == 0) {
                ans += 64 + c1;
                for (int i = 0; i < 64; i++) {
                    if (((mask >> i) & 1) == 1) ans--;
                }
            } else {
                for (int i = p; i < n; i++) {
                    long c2 = c1;
                    if (A[i] != 0 && (mask & A[i]) == 0) c2++;
                    search(A, n, len - 1, mask | A[i], c2, i + 1);
                }
            }
        }
    }

    public static class Problem_solving
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int k = int.Parse(tmp[1]);
                int[] A = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);

                bool[] V = new bool[n];
                int[] loc = new int[n];
                for (int i = 0; i < n; i++) loc[i] = -1;

                for (int i = 0; i < n; i++) {
                    V = new bool[n];
                    МОЖНО(i, A, V, loc, n, k);
                }

                Console.WriteLine(loc.Count(x => x == -1));
            }
        }

        static bool МОЖНО(int p, int[] A, bool[] V, int[] loc, int n, int k) {
            if (V[p]) return false;
            V[p] = true;

            for (int i = 0; i < p; i++) {
                if (Math.Abs(A[p] - A[i]) < k) continue;

                if (loc[i] == -1 || МОЖНО(loc[i], A, V, loc, n, k)) {
                    loc[i] = p;
                    return true;
                }
            }

            return false;
        }
    }

    public static class Jack_goes_to_Rapture
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int e = int.Parse(tmp[1]);
            HashSet<Tuple<int, int>>[] adj = new HashSet<Tuple<int, int>>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<Tuple<int, int>>();

            for (int i = 0; i < e; i++) {
                tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]) - 1;
                int b = int.Parse(tmp[1]) - 1;
                int c = int.Parse(tmp[2]);
                adj[a].Add(Tuple.Create(b, c));
                adj[b].Add(Tuple.Create(a, c));
            }

            int[] cost = new int[n];
            for (int i = 0; i < n; i++) cost[i] = -1;
            cost[0] = 0;

            Queue<int> Q = new Queue<int>();
            Q.Enqueue(0);

            while (Q.Count > 0) {
                int cur = Q.Dequeue();
                int sofar = cost[cur];
                foreach (var item in adj[cur]) {
                    int next = item.Item1, price = Math.Max(item.Item2, sofar);

                    if (cost[next] == -1 || price < cost[next]) {
                        Q.Enqueue(next);
                        cost[next] = price;
                    }
                }
            }
            Console.WriteLine(cost[n - 1] == -1 ? "NO PATH EXISTS" : cost[n - 1].ToString());
        }
    }

    public static class Matrix
    {
        static long removed;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            bool[] machines = new bool[n];
            HashSet<Tuple<int, int>>[] adj = new HashSet<Tuple<int, int>>[n];
            HashSet<long> destroyed = new HashSet<long>();

            for (int i = 0; i < n; i++) adj[i] = new HashSet<Tuple<int, int>>();

            for (int i = 0; i < n - 1; i++) {
                var t_m_p = Console.ReadLine().Split(' ');
                int _l_ = int.Parse(t_m_p[0]);
                int _r_ = int.Parse(t_m_p[1]);
                int _x_ = int.Parse(t_m_p[2]);
                adj[_l_].Add(Tuple.Create(_r_, _x_)); adj[_r_].Add(Tuple.Create(_l_, _x_));
            }

            for (int i = 0; i < k; i++) machines[int.Parse(Console.ReadLine())] = true;

            removed = 0;
            for (int i = 0; i < n; i++) {
                if (!machines[i]) continue;
                dfs(i, -1, adj, machines, destroyed, null);
            }

            Console.WriteLine(removed);
        }
        static void dfs(int fr, int no, HashSet<Tuple<int, int>>[] adj, bool[] machines, HashSet<long> destroyed, Tuple<int, int, int> gift) {
            long key = Math.BigMul(fr, 1000000);
            foreach (var node in adj[fr].Where(x => x.Item1 != no)) {
                int name = node.Item1, time = node.Item2;
                if (destroyed.Contains(key + name)) continue;

                var bill = gift == null ? null : Tuple.Create(gift.Item1, gift.Item2, gift.Item3);
                if (bill == null || time < bill.Item3) {
                    bill = Tuple.Create(fr, name, time);
                }

                if (machines[name]) {
                    Destroy_Road(bill.Item1, bill.Item2, bill.Item3, destroyed);
                } else {
                    dfs(name, fr, adj, machines, destroyed, bill);
                }
            }

        }

        static void Destroy_Road(int a, int b, int time, HashSet<long> destroyed) {
            long f = Math.BigMul(a, 1000000) + (long)b;
            if (destroyed.Contains(f)) return;
            destroyed.Add(f);
            destroyed.Add(Math.BigMul(b, 1000000) + (long)a);
            removed += time;
        }
    }

    public static class Jeanie_s_Route
    {
        static Dictionary<long, Tuple<long, long>> cache;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            var A = Array.ConvertAll(Console.ReadLine().Split(' '), x => int.Parse(x) - 1);
            bool[] letters = new bool[n];
            for (int i = 0; i < k; i++) letters[A[i]] = true;

            var adj = new HashSet<Tuple<int, int>>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<Tuple<int, int>>();
            for (int i = 0; i < n - 1; i++) {
                var t_m_p = Console.ReadLine().Split(' ');
                int _l_ = int.Parse(t_m_p[0]) - 1;
                int _r_ = int.Parse(t_m_p[1]) - 1;
                int _c_ = int.Parse(t_m_p[2]);
                adj[_l_].Add(Tuple.Create(_r_, _c_)); adj[_r_].Add(Tuple.Create(_l_, _c_));
            }

            long ans = long.MaxValue;
            cache = new Dictionary<long, Tuple<long, long>>();
            foreach (var i in Enumerable.Range(0, n).Where(x => letters[x])) {
                var r = dfs(i, -1, letters, adj);
                if (r != null) ans = Math.Min(ans, r.Item2 + r.Item1);
            }
            Console.WriteLine(ans);
        }

        static Tuple<long, long> dfs(int fr, int no, bool[] letters, HashSet<Tuple<int, int>>[] adj) {
            long key = Math.BigMul(fr, 10000000) + no + 100;
            if (cache.ContainsKey(key)) return cache[key];

            long a = 0, b = 0;
            bool c = false;
            foreach (var node in adj[fr].Where(x => x.Item1 != no)) {
                int name = node.Item1, distance = node.Item2;
                if (letters[name]) {
                    var s = dfs(name, fr, letters, adj);
                    if (s == null) {
                        b = Math.Max(b, distance);
                        a += distance * 2;
                        c = true;
                    } else {
                        b = Math.Max(b, s.Item2 + distance);
                        a += s.Item1 + (s.Item2 + distance) * 2;
                        c = true;
                    }
                } else {
                    var s = dfs(name, fr, letters, adj);
                    if (s != null) {
                        b = Math.Max(b, s.Item2 + distance);
                        a += s.Item1 + (s.Item2 + distance) * 2;
                        c = true;
                    }
                }
            }

            if (!c) cache[key] = null;
            else cache[key] = Tuple.Create(a - b - b, b);

            return cache[key];
        }
    }

    public static class Bead_Ornaments
    {
        public static void Start() {
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] colors = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                BigInteger ornaments = 1;
                foreach (var color in colors) {
                    if (n > 1) ornaments *= color;
                    if (color <= 2) continue;
                    ornaments *= BigInteger.Pow(color, color - 2);
                    ornaments %= 1000000007;
                }

                if (n > 2) ornaments *= BigInteger.Pow(colors.Sum(), n - 2);
                Console.WriteLine(ornaments % 1000000007);
            }
        }
    }

    public static class Quadrant_Queries
    {
        public static void Start() {
            var sb = new StringBuilder();
            int n = int.Parse(Console.ReadLine());
            Point[] arr = new Point[n];
            for (int i = 0; i < n; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]);
                int b = int.Parse(tmp[1]);

                arr[i] = new Point(Math.Sign(a), Math.Sign(b));
            }

            int sz = (int)(Math.Ceiling(Math.Log(n, 2)));
            sz = (2 * (int)Math.Pow(2, sz)) - 1;
            LazySegmentTree seg = new LazySegmentTree(sz);

            seg.constructST(arr, n);

            int q = int.Parse(Console.ReadLine());
            while (q-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[1]) - 1;
                int b = int.Parse(tmp[2]) - 1;

                if (tmp[0] == "C") {
                    sb.Append(seg.getSum(n, a, b).ToString()).Append("\n");
                } else if (tmp[0] == "X") {
                    seg.updateRange(n, a, b, new Change(true, false));
                } else {
                    seg.updateRange(n, a, b, new Change(false, true));
                }
            }
            Console.WriteLine(sb.ToString());
        }

        public class LazySegmentTree
        {
            static Point[] tree;
            static Change[] lazy;

            public LazySegmentTree(int m) { tree = new Point[m]; lazy = new Change[m]; }
            public void updateRangeUtil(int si, int ss, int se, int us, int ue, Change dim) {
                if (lazy[si] != null) {
                    tree[si] += lazy[si];

                    if (ss != se) {
                        lazy[si * 2 + 1] += lazy[si];
                        lazy[si * 2 + 2] += lazy[si];
                    }

                    lazy[si] = null;
                }

                if (ss > se || ss > ue || se < us) return;

                if (ss >= us && se <= ue) {
                    tree[si] += dim;

                    if (ss != se) {
                        lazy[si * 2 + 1] += dim;
                        lazy[si * 2 + 2] += dim;
                    }
                    return;
                }

                int mid = (ss + se) / 2;
                updateRangeUtil(si * 2 + 1, ss, mid, us, ue, dim);
                updateRangeUtil(si * 2 + 2, mid + 1, se, us, ue, dim);

                tree[si] = tree[si * 2 + 1] + tree[si * 2 + 2];
            }

            public void updateRange(int n, int us, int ue, Change dim) {
                updateRangeUtil(0, 0, n - 1, us, ue, dim);
            }

            public Point getSumUtil(int ss, int se, int qs, int qe, int si) {
                if (lazy[si] != null) {
                    tree[si] += lazy[si];

                    if (ss != se) {
                        lazy[si * 2 + 1] += lazy[si];
                        lazy[si * 2 + 2] += lazy[si];
                    }

                    lazy[si] = null;
                }

                if (ss > se || ss > qe || se < qs) return new Point();

                if (ss >= qs && se <= qe) return tree[si];

                int mid = (ss + se) / 2;
                return getSumUtil(ss, mid, qs, qe, 2 * si + 1) +
                    getSumUtil(mid + 1, se, qs, qe, 2 * si + 2);
            }

            public Point getSum(int n, int qs, int qe) {
                return getSumUtil(0, n - 1, qs, qe, 0);
            }

            public void constructSTUtil(Point[] arr, int ss, int se, int si) {
                if (ss > se)
                    return;

                if (ss == se) {
                    tree[si] = new Point(arr[ss]);
                    return;
                }

                int mid = (ss + se) / 2;
                constructSTUtil(arr, ss, mid, si * 2 + 1);
                constructSTUtil(arr, mid + 1, se, si * 2 + 2);

                tree[si] = tree[si * 2 + 1] + tree[si * 2 + 2];
            }

            public void constructST(Point[] arr, int n) {
                constructSTUtil(arr, 0, n - 1, 0);
            }

        }
        public class Point
        {
            public int a1, a2, a3, a4;
            public Point() { }
            public Point(int a, int b, int c, int d) { a1 = a; a2 = b; a3 = c; a4 = d; }
            public Point(Point a) { a1 = a.a1; a2 = a.a2; a3 = a.a3; a4 = a.a4; }
            public Point(int a, int b) {
                if (a > 0) {
                    if (b > 0) a1++; else a4++;
                } else {
                    if (b > 0) a2++; else a3++;
                }
            }
            public static Point operator +(Point a, Point b) {
                return new Point(a.a1 + b.a1, a.a2 + b.a2, a.a3 + b.a3, a.a4 + b.a4);
            }
            public static Point operator +(Point a, Change c) {
                if (c.x) a.ReverseX();
                if (c.y) a.ReverseY();
                return a;
            }
            public void ReverseX() {
                int tmp = a1; a1 = a4; a4 = tmp;
                tmp = a2; a2 = a3; a3 = tmp;
            }
            public void ReverseY() {
                int tmp = a1; a1 = a2; a2 = tmp;
                tmp = a3; a3 = a4; a4 = tmp;
            }
            public override string ToString() { return string.Format("{0} {1} {2} {3}", a1, a2, a3, a4); }
        }
        public class Change
        {
            public bool x, y;
            public Change() { x = false; y = false; }
            public Change(bool x, bool y) { this.x = x; this.y = y; }
            public static Change operator +(Change a, Change b) {
                if (a == null) a = new Change();
                if (b == null) b = new Change();
                return new Change(a.x ^ b.x, a.y ^ b.y);
            }
        }
    }

    public static class HackerX
    {
        public static void Start() {
            int q = int.Parse(Console.ReadLine());

            List<missile> queries = new List<missile>();
            for (int i = 0; i < q; i++) {
                var tmp = Console.ReadLine().Split(' ');
                int t = int.Parse(tmp[0]);
                int f = int.Parse(tmp[1]);
                queries.Add(new missile(-(t + f), -(t - f)));
            }

            var comp = new missileComparer();
            queries = queries.OrderByDescending(x => x.first).ToList();

            List<missile> L = new List<missile>();
            for (int i = 0; i < q; i++) {

                int k = L.BinarySearch(queries[i], comp);
                if (k < 0) k = ~k;
                if (k >= L.Count)
                    L.Add(queries[i]);
                else {
                    L[k].second = queries[i].second;
                }
            }
            Console.WriteLine(L.Count);
        }

        public class missile
        {
            public int first;
            public int second;
            public missile(int f, int t) { first = f; second = t; }
        }

        public class missileComparer : IComparer<missile>
        {
            public int Compare(missile x, missile y) {
                return x.second.CompareTo(y.second);
            }
        }
    }

    public static class Journey_Scheduling
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            HashSet<int>[] adj = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();
            for (int i = 0; i < n - 1; i++) {
                var t_m_p = Console.ReadLine().Split(' ');
                int _l_ = int.Parse(t_m_p[0]) - 1;
                int _r_ = int.Parse(t_m_p[1]) - 1;
                adj[_l_].Add(_r_); adj[_r_].Add(_l_);
            }

            long[] distance = new long[n];
            int l = goFar(0, -1, adj, 0).Item1;
            var vl = goFar(l, -1, adj, 0);
            long large = vl.Item2;
            int r = vl.Item1;
            distance[l] = distance[r] = large;
            fillDistance(l, -1, 0, adj, distance);
            fillDistance(r, -1, 0, adj, distance);


            var sb = new StringBuilder();
            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                int s = int.Parse(tmp[0]) - 1;
                int k = int.Parse(tmp[1]);

                long ans = distance[s] + (k - 1) * large;
                sb.Append(ans).Append("\n");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }
        static void fillDistance(int fr, int no, long d, HashSet<int>[] adj, long[] distance) {
            distance[fr] = Math.Max(distance[fr], d);
            foreach (var node in adj[fr].Where(x => x != no)) {
                fillDistance(node, fr, d + 1, adj, distance);
            }
        }

        static Tuple<int, long> goFar(int fr, int no, HashSet<int>[] adj, long d) {
            Tuple<int, long> r = Tuple.Create(fr, d);
            foreach (var item in adj[fr].Where(x => x != no)) {
                var v2 = goFar(item, fr, adj, d + 1);
                if (v2.Item2 > r.Item2) r = v2;
            }
            return r;
        }
    }

    public static class Jogging_Cats
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]), m = int.Parse(tmp[1]), l, r;

            var adj = new List<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new List<int>();

            for (int i = 0; i < m; i++) {
                tmp = Console.ReadLine().Split(' ');
                l = int.Parse(tmp[0]) - 1;
                r = int.Parse(tmp[1]) - 1;
                adj[l].Add(r); adj[r].Add(l);
            }
            for (int i = 0; i < n; i++) adj[i].Sort();

            Dictionary<long, long> map = new Dictionary<long, long>();

            for (int i = 0; i < n; i++) {
                adj[i].ForEach(mid => {
                    if (mid > i) {
                        var ad = adj[mid];
                        int j = ad.BinarySearch(i) + 1;
                        for (; j < ad.Count; j++) {
                            long k = Math.BigMul(i, 1000000) + ad[j];
                            if (map.ContainsKey(k)) map[k]++; else map[k] = 1;
                        }
                    }
                });
            }

            Console.WriteLine(map.Values.Sum(x => x * (x - 1)) / 2);
        }
    }

    public static class ByteLandian_Tours
    {
        const int MOD = 1000000007;
        static long[] factorial;
        public static void Start() {
            factorial = new long[10007];
            factorial[0] = 1;
            for (int i = 1; i < 10007; i++) factorial[i] = (i * factorial[i - 1]) % MOD;

            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                int n = int.Parse(Console.ReadLine());
                HashSet<int>[] adj = new HashSet<int>[n];
                for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();
                for (int i = 0; i < n - 1; i++) {
                    var t_m_p = Console.ReadLine().Split(' ');
                    int _l_ = int.Parse(t_m_p[0]);
                    int _r_ = int.Parse(t_m_p[1]);
                    adj[_l_].Add(_r_); adj[_r_].Add(_l_);
                }

                long ans = 0;
                if (IsCaterpillar(adj, n)) {
                    ans = dfs(adj, n);
                }

                Console.WriteLine(ans);
            }
        }

        static bool IsCaterpillar(HashSet<int>[] adj, int n) {
            int[] path = new int[n];
            for (int i = 0; i < n; i++) path[i] = adj[i].Count;

            Enumerable.Range(0, n).Where(x => path[x] == 1).ToList().
                ForEach(x => path[adj[x].First()]--);
            return path.All(x => x <= 2);
        }

        static long dfs(HashSet<int>[] adj, int n) {
            if (adj[0].All(x => adj[x].Count == 1)) return factorial[adj[0].Count];

            bool path = n - adj.Where(x => x.Count == 1).Count() == 1;
            long ans = path ? 1 : 2;

            foreach (var node in adj.Where(x => x.Count != 1)) {
                int y = node.Where(x => adj[x].Count == 1).Count();
                ans *= factorial[y];
                ans %= MOD;
            }

            return ans;
        }
    }

    public static class Jim_and_his_LAN_Party
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);
            int q = int.Parse(tmp[2]);

            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), x => int.Parse(x) - 1);

            int[] time = new int[m];
            int[] req = new int[m];
            for (int i = 0; i < n; i++) req[A[i]]++;
            for (int i = 0; i < m; i++) {
                if (req[i] > 1) time[i] = -1;
            }

            LANParty[] map = new LANParty[n];
            for (int i = 0; i < n; i++) map[i] = new LANParty(A[i], i);
            int[] loc = new int[n];
            for (int i = 0; i < n; i++) loc[i] = i;


            for (int i = 0; i < q; i++) {
                tmp = Console.ReadLine().Split(' ');
                int a = int.Parse(tmp[0]) - 1;
                int b = int.Parse(tmp[1]) - 1;

                var m1 = map[loc[a]];
                var m2 = map[loc[b]];
                int d = loc[a];
                if (m1.people.Count < m2.people.Count) { var _ = m1; m1 = m2; m2 = _; d = loc[b]; }

                Merge(m1, m2, loc, req, time, i, d);

            }

            Console.WriteLine(string.Join("\n", time));
        }

        static void Merge(LANParty m1, LANParty m2, int[] loc, int[] req, int[] time, int t, int d) {
            foreach (var item in m2.people) {
                m1.people.Add(item);
                loc[item] = d;
            }

            foreach (var g in m2.game) {
                if (!m1.game.ContainsKey(g.Key)) m1.game[g.Key] = g.Value;
                else {
                    m1.game[g.Key] += g.Value;
                    if (m1.game[g.Key] == req[g.Key]) time[g.Key] = t + 1;
                }
            }
            m2 = null;
        }

        class LANParty
        {
            public Dictionary<int, int> game;
            public HashSet<int> people;
            public LANParty(int game, int mem) {
                this.game = new Dictionary<int, int> { { game, 1 } };
                people = new HashSet<int>();
                people.Add(mem);
            }
        }
    }

    public static class Diameter_Minimization
    {

        static int n, m, maxD;
        static StringBuilder ans;
        static int d = 10000;
        static List<int>[] adj;
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            n = int.Parse(tmp[0]);
            m = int.Parse(tmp[1]);
            adj = new List<int>[n];

            adj[0] = new List<int>();
            int current = m + 1;
            Queue<int> q1 = new Queue<int>();
            Queue<int> q2 = new Queue<int>();
            for (int i = 1; i <= m; i++) {
                adj[0].Add(i % n);
                q2.Enqueue(i % n);
            }

            while (q2.Count > 0) {
                var t__ = q1; q1 = q2; q2 = t__;
                while (q1.Count > 0) {
                    int i = q1.Dequeue();
                    adj[i] = new List<int>();
                    for (int j = 0; j < m; j++) {
                        adj[i].Add(current % n);
                        if (current < n) q2.Enqueue(current % n);
                        current++;
                    }
                }
            }

            for (int i = 0; i < n; i++) {
                if (adj[i].Count < m) {
                    for (int j = 0; j < n && adj[i].Count < m; j++) {
                        if (!adj[i].Contains(j)) adj[i].Add(j);
                    }
                }
            }

            maxD = 5 + (int)Math.Ceiling(Math.Log10(n) / Math.Log10(m));
            d = getDiameter();
            ans = new StringBuilder();
            for (int i = 0; i < n; i++)
                ans.Append(string.Join(" ", adj[i])).Append("\n");

            Console.WriteLine(d);
            Console.WriteLine(ans.ToString());
        }

        static int getDiameter() {
            int[] distance = new int[n];

            int r = 0;
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n; j++) distance[j] = 1000000;
                dfs(i, distance, 0);
                r = Math.Max(r, distance.Max());
            }
            return r;
        }

        static void dfs(int i, int[] dist, int cur) {
            if (cur >= maxD) return;
            if (dist[i] < cur) return;
            dist[i] = cur;
            foreach (var item in adj[i]) {
                dfs(item, dist, cur + 1);
            }
        }
    }

}
