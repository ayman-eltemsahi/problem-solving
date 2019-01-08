using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Palindromic_Tree
    {
        static int mx = 100050;

        class node
        {
            public int[] next = new int[26];
            public int len, sufflink, num, start, end;
            public node() { }
            public node(int len, int sufflink) { this.len = len; this.sufflink = sufflink; }
        };

        static int len, num, suff;
        static string s;
        static node[] tree;
        static List<int>[] adj;

        static bool addLetter(int pos) {
            int cur = suff, curlen = 0;
            int let = s[pos] - 'a';

            while (true) {
                curlen = tree[cur].len;
                if (pos - 1 - curlen >= 0 && s[pos - 1 - curlen] == s[pos])
                    break;
                cur = tree[cur].sufflink;
            }
            if (tree[cur].next[let] != 0) {
                suff = tree[cur].next[let];
                tree[suff].num++;
                return false;
            }

            num++;
            suff = num;
            tree[num] = new node()
            {
                len = tree[cur].len + 2,
                end = pos,
                start = pos - tree[cur].len - 1
            };

            tree[cur].next[let] = num;
            if (tree[num].len == 1) {
                tree[num].sufflink = 2;
                tree[num].num = 1;
                return true;
            }

            ++tree[num].num;
            while (true) {
                cur = tree[cur].sufflink;
                curlen = tree[cur].len;
                if (pos - 1 - curlen >= 0 && s[pos - 1 - curlen] == s[pos]) {
                    tree[num].sufflink = tree[cur].next[let];
                    break;
                }
            }
            return true;

        }

        static void initTree() {
            tree = new node[mx];
            tree[1] = new node(-1, 1);
            tree[2] = new node(0, 1);
            num = 2; suff = 2;
            adj = new List<int>[mx];
            for (int i = 0; i < mx; i++) adj[i] = new List<int>();
        }

        static void CountOccurences(int p) {
            foreach (var item in adj[p]) {
                CountOccurences(item);
                tree[p].num += tree[item].num;
            }
        }

        public static List<int> palindromic_tree_num(string txt) {
            s = txt;
            len = s.Length;
            mx = len + 20;

            initTree();

            for (int i = 0; i < len; i++) {
                addLetter(i);
            }
            for (int i = 2; i <= num; i++) adj[tree[i].sufflink].Add(i);
            CountOccurences(1);

            List<int> all = new List<int>();
            for (int i = 3; i <= num; i++) {
                all.Add(tree[i].num);
                Console.WriteLine(tree[i].start + " " + tree[i].end);
            }

            return all;
        }

    }
}
