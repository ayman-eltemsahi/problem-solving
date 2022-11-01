using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_2019 {
    class QP2 {
        static StringBuilder sb = new StringBuilder();
        public static void Main() {
            Console.SetIn(new System.IO.StreamReader("input"));
            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++) {
                sb.AppendLine($"Case #{i}: {solve()}");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static string solve() {
            int n = int.Parse(Console.ReadLine());
            var moves = Console.ReadLine();
            var a = solve(n, moves, 'S', 'E');
            if (a == null) return solve(n, moves, 'E', 'S');
            return a;
        }

        static string solve(int n, string moves, char S, char E) {
            if (moves[0] != E) return null;
            if (moves[moves.Length - 1] == S) {
                return new string(S, n - 1) + new string(E, n - 1);
            }

            var j = 0;
            for (int i = 1; i < moves.Length; i++) {
                if (moves[i] == S) j++;
                if (moves[i] == S && moves[i - 1] == S) break;
            }
            j--;

            return new string(S, j) + new string(E, n - 1) + new string(S, n - j - 1);
        }
    }
}
