using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_2019 {
    class QP1 {
        static StringBuilder sb = new StringBuilder();
        public static void Main_p1() {
            Console.SetIn(new System.IO.StreamReader("input"));
            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++) {
                sb.AppendLine($"Case #{i}: {solve()}");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static string solve() {
            var number = Console.ReadLine();
            var n1 = "";
            var n2 = "";

            for (int i = number.Length - 1; i >= 0; i--) {
                var n = number[i] - '0';
                if (n != 4) {
                    n1 = n + n1;
                    n2 = "0" + n2;
                } else {
                    n1 = "2" + n1;
                    n2 = "2" + n2;
                }
            }

            var j = 0;
            while (j < n2.Length && n2[j] == '0') j++;
            n2 = n2.Substring(j);
            if (n2 == "") n2 = "0";
            return n1 + " " + n2;
        }

    }
}
