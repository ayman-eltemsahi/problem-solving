using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam
{
    class QP2
    {
        static StringBuilder sb = new StringBuilder();
        static void Main_P2()
        {
            int tc = int.Parse(Console.ReadLine());
            for (int i = 1; i <= tc; i++)
            {
                sb.AppendLine($"Case #{i}: {solve()}");
            }
            --sb.Length;
            Console.WriteLine(sb.ToString());
        }

        static string solve()
        {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            int[] odd = new int[n / 2];
            int[] even = new int[n - n / 2];
            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0) even[i / 2] = arr[i];
                else odd[i / 2] = arr[i];
            }

            Array.Sort(odd);
            Array.Sort(even);

            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0) arr[i] = even[i / 2];
                else arr[i] = odd[i / 2];
            }

            for (int i = 0; i < n - 1; i++)
            {
                if (arr[i] > arr[i + 1]) return i.ToString();
            }

            return "OK";
        }
    }
}
