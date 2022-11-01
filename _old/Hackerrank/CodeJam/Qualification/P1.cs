using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam
{
    class QP1
    {
        static StringBuilder sb = new StringBuilder();
        public static void Main_p1()
        {
            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++)
            {
                sb.AppendLine($"Case #{i}: {solve()}");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        private static string solve()
        {
            var tmp = Console.ReadLine().Split(' ');
            long d = long.Parse(tmp[0]);
            char[] P = tmp[1].Trim().ToCharArray();

            int tries = 0;
            while (GetDamage(P) > d)
            {
                tries++;
                bool flag = true;
                for (int i = P.Length - 1; flag && i > 0; i--)
                {
                    if (P[i] == 'S' && P[i - 1] == 'C')
                    {
                        P[i] = 'C'; P[i - 1] = 'S';
                        flag = false;
                    }
                }
                if (flag) return "IMPOSSIBLE";
            }


            return tries.ToString();
        }

        private static long GetDamage(char[] p)
        {
            long power = 1, damage = 0;
            foreach (var c in p)
            {
                if (c == 'C') power <<= 1;
                else damage += power;
            }
            return damage;
        }
    }
}
