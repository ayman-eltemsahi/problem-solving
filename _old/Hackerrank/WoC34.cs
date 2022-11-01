using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class PS
{

    static void Main2(String[] args) {
        Console.SetIn(new StreamReader("input"));
        solve();
    }
    const int MOD = 1000000007;
    static void solve() {
        int tc = int.Parse(Console.ReadLine());
        while (tc-- > 0) {
            int n = int.Parse(Console.ReadLine());
            long[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), long.Parse);
            long[] add = new long[n], mult = new long[n];
            mult[n - 1] = add[n - 1] = arr[n - 1];
            int k = 2;
            long m = arr[n - 1], m2 = 0;
            for (int i = n - 2; i >= 0; i--) {
                if (i == n - 2) {
                    add[i] = (arr[i] + arr[i + 1]) % MOD;
                    mult[i] = (arr[i] * arr[i + 1]) % MOD;
                    m += arr[i];
                    m %= MOD;
                    m2 += add[i + 1];
                    m2 %= MOD;
                    continue;
                }
                add[i] = (arr[i] * k) % MOD + add[i + 1] + mult[i + 1];
                add[i] %= MOD;

                mult[i] = (m * arr[i]) % MOD;
                mult[i] += m2;
                mult[i] %= MOD;
                k = (2 * k) % MOD;

                m += arr[i];
                m %= MOD;
                m2 += add[i + 1];
                m2 %= MOD;
            }
            Console.WriteLine((add[0] + mult[0]) % MOD);
        }
    }
}