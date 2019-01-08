using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam {
    class R1P2 {

        public static void Main0() {
            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++) {
                solve();
            }
        }

        static void solve() {
            int N = int.Parse(Console.ReadLine());

            int[] occur = new int[N];
            bool[] v = new bool[N];

            for (int i = 0; i < N; i++) {
                int[] pref = Array.ConvertAll(Console.ReadLine().Split(' ').Skip(1).ToArray(), int.Parse);
                foreach (var item in pref) occur[item]++;
                pref = pref.OrderBy(x => occur[x]).ToArray();

                bool f = false;
                foreach (var item in pref) {
                    if (!v[item]) {
                        f = true;
                        v[item] = true;
                        Console.WriteLine(item);
                        break;
                    }
                }

                if (!f) Console.WriteLine(-1);
            }
        }
    }
}