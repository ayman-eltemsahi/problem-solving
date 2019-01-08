using Algo;
using API.Models;
using Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class DefaultController : ApiController
    {
        static Node T = new Node(0);
        static AVLNode AVLT = null;
        public tree Get() {
            return draw();
            var list = new List<int>();
            var l = new List<edge>();
            //T.fill(list, l);
            if (AVLT != null) fillAVL(AVLT, list, l);

            return new tree(list, l);
        }

        public tree Post(int id) {
            if (AVLT == null) {
                AVLT = new AVLNode(id);
                return Get();
            }
            //AVLT = AVLT.Insert(AVLT, id);
            //T.insert(id);
            return Get();
        }


        private void fillAVL(AVLNode node, List<int> list, List<edge> l) {
            if (node.left != null) { l.Add(new edge(node.value, node.left.value)); fillAVL(node.left, list, l); }
            list.Add(node.value);
            if (node.right != null) { l.Add(new edge(node.value, node.right.value)); fillAVL(node.right, list, l); }
        }
        
        private tree draw() {
            Console.SetIn(new StreamReader(@"C:\Users\MyPC\Documents\Visual Studio 2015\Projects\Hackerrank\Hackerrank\bin\Debug\input"));
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]); m = n - 1;
            HashSet<int>[] adj = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();

            for (int i = 0; i < m; i++) {
                var __tmp = Console.ReadLine().Trim().Split(' ');
                int __l = int.Parse(__tmp[0]) - 1;
                int __r = int.Parse(__tmp[1]) - 1;
                adj[__l].Add(__r); adj[__r].Add(__l);
            }

            return new tree(adj,true);
        }
    }

    public class Node
    {
        public int val;
        public int max;
        public Node left, right;
        public Node(int v) { val = v; max = v; }
        public void insert(int v) {
            if (v > val) {
                if (right == null) right = new Node(v); else right.insert(v);
                if (v > max) max = v;
            } else {
                if (left == null) left = new Node(v); else left.insert(v);
            }
        }

        public void fill(List<int> list, List<edge> l) {
            list.Add(val);
            if (left != null) { l.Add(new edge(val, left.val)); left.fill(list, l); }
            if (right != null) { l.Add(new edge(val, right.val)); right.fill(list, l); }

        }
    }


}
