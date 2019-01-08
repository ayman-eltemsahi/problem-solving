using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetIn(new System.IO.StreamReader(@"C:\Users\MyPC\Documents\Visual Studio 2015\Projects\Hackerrank\FutureSkill\input"));

            int n = int.Parse(Console.ReadLine());
            int m = 101;
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            bool[,] dp = new bool[n, m + 1];

            for (int i = 0; i < n; i++) dp[0, arr[i] % 101] = true;
            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (!dp[i - 1, j]) continue;

                    dp[i, mod(j * arr[i])] = true;
                    dp[i, mod(j + arr[i])] = true;
                    dp[i, mod(j - arr[i])] = true;
                }
            }

            var signs = new List<string>();
            var last = 0;
            for (int i = n - 1; i >= 0; i--)
            {
                for (int j = 1; j <= m; j++)
                {
                    if (mod(j * arr[i]) == last)
                    {
                        signs.Add("*");
                        break;
                    }
                    //else if (dp[i, mod(j + arr[i - 1])])
                    //{
                    //    signs.Add("+");
                    //    break;
                    //}
                    //else if (dp[i, mod(j - arr[i - 1])])
                    //{
                    //    signs.Add("-");
                    //    break;
                    //}
                }
            }

            for (int j = 1; j <= m; j++)
            {
                if (dp[n - 1, j]) Console.WriteLine(j);
            }
        }

        static int mod(int m)
        {
            return ((m % 101) + 101) % 101;
        }
    }
}
