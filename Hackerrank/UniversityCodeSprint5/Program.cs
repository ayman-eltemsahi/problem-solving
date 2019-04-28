using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank.UniversityCodeSprint5 {
    class Solution {
        static void Main2() {
            Console.SetIn(new System.IO.StreamReader("input"));
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                solve();
            }
        }

        static void solve() {
            long max = long.Parse(Console.ReadLine());
            long ans = 0;
            var primes = new List<long>();

            var pi = 1;
            for (long i = 2; ; i++) {
                long tr = i * i * i;
                if (tr > max) break;
                if (!isPrime(i)) continue;

                long count = max / tr;
                ans += count;

                decimal gr = max;
                gr /= tr;
                foreach (var item in primes) {
                    //ans -= pi;
                    ans -= (long)Math.Floor(gr / item);
                }

                pi++;
                primes.Add(tr);
                Console.WriteLine(ans);
            }

            Console.WriteLine(ans);
            Console.WriteLine();
            Console.WriteLine(1681);
        }

        static bool isPrime(long n) {
            if (n == 2) return true;
            if (n < 2) return false;
            if (n % 2 == 0) return false;
            for (long i = 3; i * i <= n; i += 2) {
                if (n % i == 0) return false;
            }
            return true;
        }
    }
}