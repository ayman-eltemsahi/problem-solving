using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam
{
    class QP3
    {
        static void Main_p3()
        {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0)
            {
                solve();
            }
        }

        static void solve()
        {
            int A = int.Parse(Console.ReadLine());
            int len = 3 * (int)Math.Ceiling(A / 3.0);
            if (len < 3) len = 3;
            bool[,] board = new bool[3, len];

            int index = 0, gi = 0, gj = 0;
            while (true)
            {
                Console.WriteLine(2 + " " + Math.Min(index + 2, len - 1));
                var line = Console.ReadLine();
                if (line == "0 0") return;
                if (line == "-1 -1") return;

                var tmp = line.Split(' ');
                gi = int.Parse(tmp[0]) - 1;
                gj = int.Parse(tmp[1]) - 1;
                if (!board[gi, gj])
                {
                    board[gi, gj] = true;
                    while (index < len && board[0, index] && board[1, index] && board[2, index]) index++;
                }

            }

        }
    }
}
