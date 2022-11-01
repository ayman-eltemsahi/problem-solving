using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo {
    public static class Utility {
        public static void bin(long a) {
            for (int i = 63; i >= 0; i--) {
                Console.Write((a >> i) & 1);
            }
            Console.WriteLine();
        }

        public static void bin(int a, int k = 32) {
            for (int i = k - 1; i >= 0; i--) {
                Console.Write((a >> i) & 1);
            }
            Console.WriteLine();
        }

        public static void bin(uint a, int k = 32) {
            for (int i = k - 1; i >= 0; i--) {
                Console.Write((a >> i) & 1);
            }
            Console.WriteLine();
        }

        public static decimal convertToBase(decimal x, decimal b) {
            decimal r = 1;
            while (x > 0) {
                r *= 10;
                if (x == 0) break;
                r += x % b;
                x = Math.Floor(x / b);
            }
            return get_reverse(r) / 10;
        }

        public static decimal get_reverse(decimal n) {
            decimal rev = 0, rem;
            while (n > 0) {
                rem = n % 10;
                n = n / 10;
                rev = rev * 10 + rem;
            }
            return rev;
        }

        public static long SumOfAllSubArrays(int n, long[] a0) {
            long[] sum = new long[n], sum2 = new long[n];
            sum[0] = a0[0];
            sum2[0] = a0[0];
            for (var i = 1; i < n; i++) { sum[i] = sum[i - 1] + a0[i]; }
            for (var i = 1; i < n; i++) { sum2[i] = sum2[i - 1] + sum[i]; }

            long r = a0[0];

            for (int i = 1; i < n; i++) {
                r += sum[i] * (i + 1);
                r -= sum2[i - 1];
            }

            return r;
        }

        public static long modPow(long bas, long exp, long modulus) {
            bas %= modulus;
            long result = 1;
            while (exp > 0) {
                if ((exp & 1) == 1) result = (result * bas) % modulus;
                bas = (bas * bas) % modulus;
                exp >>= 1;
            }
            return result;
        }

        public static int modInverse(int a, int m, int modulus) {
            int m0 = m, t, q;
            int x0 = 0, x1 = 1;

            while (a > 1) {
                // q is quotient
                q = a / m;
                t = m;

                // m is remainder now, process same as
                // Euclid's algo
                m = a % m; a = t;
                t = x0;

                x0 = x1 - q * x0;
                x1 = t;
            }

            x1 %= modulus;
            if (x1 < 0) return x1 + modulus;
            return x1;
        }

        public static int gcd(int a, int b) {
            if (a == 0) return b;
            return gcd(b % a, a);
        }

        public static int countOnes(ulong a) {
            var x = (a & 0x5555555555555555) + ((a & 0xaaaaaaaaaaaaaaaa) >> 1);
            x = (x & 0x3333333333333333) + ((x & 0xcccccccccccccccc) >> 2);
            x = (x & 0x0f0f0f0f0f0f0f0f) + ((x & 0xf0f0f0f0f0f0f0f0) >> 4);
            x = (x & 0x00ff00ff00ff00ff) + ((x & 0xff00ff00ff00ff00) >> 8);
            x = (x & 0x0000ffff0000ffff) + ((x & 0xffff0000ffff0000) >> 16);
            x = (x & 0x00000000ffffffff) + ((x & 0xffffffff00000000) >> 32);
            return (int)x;
        }

        public static int countOnes(int a) {
            a = a - ((a >> 1) & 0x55555555);
            a = (a & 0x33333333) + ((a >> 2) & 0x33333333);
            return (((a + (a >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
        }

        public static int countOnes(short a) {
            var x = (a & 0x5555) + ((a & 0xaaaa) >> 1);
            x = (x & 0x3333) + ((x & 0xcccc) >> 2);
            x = (x & 0x0f0f) + ((x & 0xf0f0) >> 4);
            x = (x & 0x00ff) + ((x & 0xff00) >> 8);
            return (int)x;
        }

        public static double sq(double a) { return a * a; }

        public static int lastBitIndex(int a) {
            for (int i = 31; i >= 0; i--) {
                if (((a >> i) & 1) == 1) return i;
            }
            return -1;
        }

        public static bool IsPrime(int x) {
            if (x == 2 || x == 3) return true;
            if (x < 2 || x % 2 == 0) return false;
            for (int i = 3, g = (int)Math.Ceiling(Math.Sqrt(x)); i < g; i += 2) {
                if (x % i == 0) return false;
            }
            return true;
        }

        public static bool Equal<T>(IList<T> A, IList<T> B) where T : IComparable<T> {
            int n = A.Count;
            int m = B.Count;
            if (n != m) return false;

            for (int i = 0; i < n; i++) {
                if (A[i].CompareTo(B[i]) != 0) return false;
            }

            return true;
        }

        public static IEnumerable<long> IntArrayToLongArray(IEnumerable<int> source) {
            return source.Select(x => (long)x);
        }

        public static IEnumerable<int> LongArrayToIntArray(IEnumerable<long> source) {
            return source.Select(x => (int)x);
        }

        public static void Watch(Action action) {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            action.Invoke();
            sw.Stop();

            Console.WriteLine("Time : " + (sw.ElapsedMilliseconds / 1000.0));
        }
    }
}
