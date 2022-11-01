using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Hackerrank.Combinatorics
{
    public static class Anti_Palindromic_Strings
    {
        const int MOD = 1000000007;
        public static void Start() {
            var sb = new StringBuilder();
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                long n = long.Parse(tmp[0]);
                long m = long.Parse(tmp[1]);

                long ans = m;
                if (n != 1) {
                    ans = (m * (m - 1)) % MOD;
                    ans *= modpow(m - 2, n - 2, MOD);
                    ans %= MOD;
                }

                sb.Append(ans).Append("\n");
            }
            sb.Length--;
            Console.WriteLine(sb.ToString());
        }
        static long modpow(long bas, long exp, long modulus) {
            bas %= modulus;
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) == 1) result = (result * bas) % modulus;
                bas = (bas * bas) % modulus;
                exp >>= 1;
            }
            return result;
        }
    }

    public static class A_Chocolate_Fiesta
    {
        const int MOD = 1000000007;
        static long[] fac, inversefac;
        public static void Start() {
            fac = new long[100001];
            inversefac = new long[100001];
            fac[0] = fac[1] = inversefac[0] = inversefac[1] = 1;
            for (int i = 2; i < 100001; i++) {
                fac[i] = (i * fac[i - 1]) % MOD;
                inversefac[i] = modInverse((int)fac[i], MOD);
            }

            Console.ReadLine();
            int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            long even = 0, odd = 0;
            foreach (var a in A) if (a % 2 == 0) even++; else odd++;

            long odds = 0, evens = 0;
            for (int i = 2; i <= odd; i += 2) {
                odds += CNKSpecial(odd, i);
                odds %= MOD;
            }
            for (int i = 1; i <= even; i++) {
                evens += CNKSpecial(even, i);
                evens %= MOD;
            }

            long ans = (odds * evens) % MOD;
            ans += evens + odds;
            ans %= MOD;
            Console.WriteLine(ans);
        }
        static int modInverse(int a, int m) {
            int t, q;
            int x0 = 0, x1 = 1;

            while (a > 1) {
                q = a / m; t = m;
                m = a % m; a = t; t = x0;
                x0 = x1 - q * x0; x1 = t;
            }

            x1 %= MOD;
            if (x1 < 0) return x1 + MOD;
            return x1;
        }

        static long CNKSpecial(long n, long k) {
            if (n < k) return 0;
            if (n == k || k == 0) return 1;

            long inverse = (inversefac[n - k] * inversefac[k]) % MOD;
            return (fac[n] * inverse) % MOD;
        }
    }

    public static class Fibonacci_Finding
    {
        const int MOD = 1000000007;
        public static void Start() {
            StringBuilder sb = new StringBuilder();
            int _tc_ = int.Parse(Console.ReadLine());
            while (_tc_-- > 0) {
                var tmp = Console.ReadLine().Split(' ');
                long a = long.Parse(tmp[0]);
                long b = long.Parse(tmp[1]);
                int n = int.Parse(tmp[2]);


                long ans = (a * Fib(n - 1)) % MOD + (b * Fib(n)) % MOD;
                ans %= MOD;
                sb.Append(ans).Append("\n");
            }
            sb.Length--;
            Console.WriteLine(sb.ToString());
        }
        static long[,] mulMatrix(long[,] A, long[,] B) {
            long[,] C = new long[2, 2];
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    for (int k = 0; k < 2; k++)
                        C[i, j] = (C[i, j] + A[i, k] * B[k, j]) % MOD;
            return C;
        }
        static long[,] powMatrix(long[,] A, int p) {
            if (p == 1)
                return A;
            if (p % 2 == 1)
                return mulMatrix(A, powMatrix(A, p - 1));
            long[,] X = powMatrix(A, p / 2);
            return mulMatrix(X, X);
        }
        static long Fib(int N) {
            if (N < 1) return 0;
            long[] F1 = new long[2];
            F1[0] = F1[1] = 1;

            long[,] T = new long[2, 2];
            T[0, 0] = 0;
            T[0, 1] = T[1, 0] = T[1, 1] = 1;

            if (N == 1) return 1;
            T = powMatrix(T, N - 1);
            long res = 0;
            for (int i = 0; i < 2; i++)
                res = (res + T[0, i] * F1[i]) % MOD;
            return res;
        }
    }
}
