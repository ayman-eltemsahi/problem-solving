using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class tree
    {
        public List<int> nodes;
        public List<edge> edges;
        public tree(List<int> n, List<edge> e) { nodes = n; edges = e; }

        public tree(IEnumerable<int>[] adj, bool addOne = false) {
            int n = adj.Length;
            nodes = new List<int>();
            edges = new List<edge>();

            for (int i = 0; i < n; i++) nodes.Add(addOne ? (i + 1) : i);
            for (int i = 0; i < n; i++) {
                foreach (var item in adj[i]) {
                    if (item >= i) {
                        if (addOne)
                            edges.Add(new edge(item + 1, i + 1));
                        else
                            edges.Add(new edge(item, i));
                    }
                }
            }
        }
    }
    public class edge
    {
        public int source, target;
        public edge(int a, int b) { source = a; target = b; }
    }
}