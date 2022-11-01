using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam {
    class R1P1 {

        static StringBuilder sb = new StringBuilder();
        public static void Main0() {
            Console.SetIn(new System.IO.StreamReader("input"));

            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++) {
                sb.AppendLine($"Case #{i}: {solve()}");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static string solve() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int l = int.Parse(tmp[1]);

            string[] words = new string[n];
            bool[,] ex = new bool[l, 26];
            var root = new Node();

            for (int i = 0; i < n; i++) {
                var w = Console.ReadLine();
                words[i] = w;

                var cur = root;
                for (int j = 0; j < l; j++) {
                    cur = cur.Add(w[j]);
                    ex[j, w[j] - 'A'] = true;
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < n; i++) {
                char c = words[i][0];
                sb.Length = 0;
                sb.Append(c);
                var cur = root.Get(c);
                for (int j = 1; j < l; j++) {
                    for (int k = 0; k < 26; k++) {
                        if (ex[j, k] && cur.Get((char)('A' + k)) == null) {
                            sb.Append((char)('A' + k));
                            if (j != l - 1)
                                sb.Append(words[i].Substring(j + 1));
                            return sb.ToString();
                        }
                    }
                }

            }

            return "-";
        }

        class Node {
            public Node[] children;
            public Node() {
                children = new Node[26];
            }

            internal Node Add(char c) {
                if (children[c - 'A'] == null) {
                    children[c - 'A'] = new Node();
                }

                return children[c - 'A'];
            }

            internal Node Get(char c) {
                return children[c - 'A'];
            }
        }

    }
}