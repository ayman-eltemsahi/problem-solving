using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Algo {
    public static class PolynomialMultiplication {

        static Complex[] roots;

        public static void CalculateRoots(int n) {
            roots = new Complex[n];

            for (int i = 1; i < n; i++) {
                double m = 2.0 * Math.PI / i;
                roots[i] = new Complex(Math.Cos(m), Math.Sin(m));
            }
        }

        public static long[] FFT(long[] a, long[] b) {

            var _a = a.Select(x => (double)x).ToArray();
            var _b = b.Select(x => (double)x).ToArray();
            return FFT(_a, _b)
                    .Select(x => (long)Math.Round(x))
                    .ToArray();
        }

        public static double[] FFT(double[] a, double[] b) {

            int len = Math.Max(a.Length, b.Length);
            int n = 1 << (1 + (int)Math.Ceiling(Math.Log(len, 2)));
            CalculateRoots(n + 1);

            Complex[] A = new Complex[n], B = new Complex[n];

            for (int i = 0; i < Math.Min(n, a.Length); i++) A[i] = new Complex(a[i], 0);
            for (int i = 0; i < Math.Min(b.Length, n); i++) B[i] = new Complex(b[i], 0);

            Divide(ref A);
            Divide(ref B);

            for (int i = 0; i < n; i++) {
                A[i] *= B[i];
                A[i] = new Complex(A[i].Real, -A[i].Imaginary);
            }

            Divide(ref A);

            var ret = new double[n];
            for (int i = 0; i < n; i++) {
                ret[i] = A[i].Real / n;
            }
            return ret;
        }

        static void Divide(ref Complex[] A) {
            int n = A.Length;
            if (n == 1) return;

            Complex w = roots[n];

            Complex[] A_even = new Complex[n / 2], A_odd = new Complex[n / 2];

            for (int i = 0; i < n; i += 2) {
                A_even[i / 2] = A[i];
                A_odd[i / 2] = A[i + 1];
            }
            Divide(ref A_even);
            Divide(ref A_odd);

            Complex x = new Complex(1, 0);

            for (int j = 0; j < n / 2; ++j) {
                A[j] = A_even[j] + x * A_odd[j];
                A[j + n / 2] = A_even[j] - x * A_odd[j];
                x = x * w;
            }
        }
    }
}
