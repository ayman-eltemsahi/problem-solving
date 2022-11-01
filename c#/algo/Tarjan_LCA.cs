using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Tarjan_LCA
    {
        HashSet<int>[] ind;
        List<int>[] adj;
        int n, m;
        Query[] Q;

        public Tarjan_LCA(Query[] Q, List<int>[] adj) {
            n = adj.Length;
            m = Q.Length;
            this.adj = adj;
            this.Q = Q;
            ind = new HashSet<int>[n];
            for (int i = 0; i < n; i++) {
                ind[Q[i].L].Add(i);
                ind[Q[i].R].Add(i);
            }
        }

        void FindLCA() {
            subset[] subsets = new subset[n];
            for (int i = 0; i < n; i++) subsets[i] = new subset();
            preprocess(0, -1, subsets);
            lcaWalk(0, -1, subsets);
        }

        private class subset
        {
            public int parent, rank, ancestor, color;
            public List<int> children;
            public subset() { children = new List<int>(); }
        }

        private void makeSet(subset[] subsets, int i, int n) {
            if (i < 0 || i >= n) return;

            subsets[i].color = 0;
            subsets[i].parent = i;
            subsets[i].rank = 0;
        }

        private int findSet(subset[] subsets, int i) {
            if (subsets[i].parent != i)
                subsets[i].parent = findSet(subsets, subsets[i].parent);

            return subsets[i].parent;
        }

        private void unionSet(subset[] subsets, int x, int y) {
            int xroot = findSet(subsets, x);
            int yroot = findSet(subsets, y);

            if (subsets[xroot].rank < subsets[yroot].rank)
                subsets[xroot].parent = yroot;
            else if (subsets[xroot].rank > subsets[yroot].rank)
                subsets[yroot].parent = xroot;

            else {
                subsets[yroot].parent = xroot;
                subsets[xroot].rank++;
            }
        }

        private void lcaWalk(int u, int no, subset[] subsets) {
            makeSet(subsets, u, n);

            subsets[findSet(subsets, u)].ancestor = u;

            foreach (var child in subsets[u].children) {
                if (child == no) continue;
                lcaWalk(child, u, subsets);
                unionSet(subsets, u, child);
                subsets[findSet(subsets, u)].ancestor = u;
            }

            subsets[u].color = 1;

            foreach (var i in ind[u]) {
                if (Q[i].L == u) {
                    if (subsets[Q[i].R].color == 1) {
                        Q[i].LCA = subsets[findSet(subsets, Q[i].R)].ancestor;
                    }
                } else if (Q[i].R == u) {
                    if (subsets[Q[i].L].color == 1) {
                        Q[i].LCA = subsets[findSet(subsets, Q[i].L)].ancestor;
                    }
                }
            }

        }

        private void preprocess(int fr, int no, subset[] subsets) {
            foreach (var item in adj[fr]) {
                if (item == no) continue;
                preprocess(item, fr, subsets);
                subsets[fr].children.Add(item);
            }
        }

    }

    public class Query
    {
        public int L, R, LCA;
        public Query(int l, int r) { L = l; R = r; }
    }
}
