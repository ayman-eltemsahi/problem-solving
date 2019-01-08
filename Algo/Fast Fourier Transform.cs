using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Library {
    public static class FFT {
        public static int www = 0;
      
        public static double[] MultiplyTwoPolynomialsFFT(double[] a, double[] b, long mod) {
            int len = Math.Max(a.Length, b.Length);
            int n = (int)Math.Pow(2, Math.Ceiling(Math.Log(len, 2)));
            int max = n;// a.Length + b.Length-1;

            Complex[] A = new Complex[n], B = new Complex[n];

            for (int i = 0; i < Math.Min(n, a.Length); i++) A[i] = new Complex(a[i], 0);
            for (int i = 0; i < Math.Min(b.Length, n); i++) B[i] = new Complex(b[i], 0);

            Complex w = getPrimitiveRootOfUnity(n);
            Complex[] F_A = Divide(A, n, w);
            Complex[] F_B = Divide(B, n, w);

            Complex w_ = new Complex(1, 0);
            for (int i = 0; i < n - 1; i++) {
                F_A[i] *= F_B[i];
                //F_A[i] = new Complex(F_A[i].Real % mod, F_A[i].Imaginary);
                w_ *= w;
            }
            F_A[n - 1] *= F_B[n - 1];

            Complex[] C = Divide(F_A, n, w_);

            var ret = new double[max];
            for (int i = 0; i < max; i++) {
                ret[i] = C[i].Real / n;
                ret[i] %= mod;
                ret[i] = (mod + ret[i]) % mod;
            }
            return ret;
        }


        static Complex[] Divide(Complex[] A, int m, Complex w) {
            if (m == 1) return A;

            Complex[] A_even = new Complex[m / 2], A_odd = new Complex[m / 2];

            for (int i = 0; i < m; i += 2) {
                A_even[i / 2] = A[i];
                A_odd[i / 2] = A[i + 1];
            }
            Complex[] F_even = Divide(A_even, m / 2, w * w);
            Complex[] F_odd = Divide(A_odd, m / 2, w * w);
            Complex[] F = new Complex[m];
            Complex x = new Complex(1, 0);
            for (int j = 0; j < m / 2; ++j) {
                F[j] = F_even[j] + x * F_odd[j];
                F[j + m / 2] = F_even[j] - x * F_odd[j];
                x = x * w;
            }
            return F;
        }


        public static float[] MultiplyTwoPolynomialsFFT(float[] a, float[] b) {
            int len = Math.Max(a.Length, b.Length);
            int n = (int)Math.Pow(2, 1 + Math.Ceiling(Math.Log(len, 2)));

            Polynomial A = new Polynomial(n), B = new Polynomial(n);

            for (int i = 0; i < a.Length; i++) A.Values[i] = new Complex(a[i], 0);
            for (int i = 0; i < b.Length; i++) B.Values[i] = new Complex(b[i], 0);

            Complex w = getPrimitiveRootOfUnity(n);
            Polynomial F_A = Divide(A, n, w);
            Polynomial F_B = Divide(B, n, w);
            Polynomial F_C = new Polynomial(n);
            for (int i = 0; i < n; i++) F_C.Values[i] = F_A.Values[i] * F_B.Values[i];
            // w_ = w^{-1}
            Complex w_ = new Complex(1, 0);
            for (int i = 0; i < n - 1; i++) w_ *= w;
            // 2 last statement to compute result polynomial, result going to be located in C
            Polynomial C = Divide(F_C, n, w_);
            for (int i = 0; i < n; i++) C.Values[i] *= 1.0 / n;

            float[] ret = new float[n];
            for (int i = 0; i < n; i++) {
                ret[i] = (float)Math.Round(C.Values[i].Real, 5);
            }
            return ret;
        }

        struct Polynomial {
            int size;
            public Complex[] Values;
            public Polynomial(int s) {
                size = s;
                Values = new Complex[size];
            }
        }

        static Polynomial Divide(Polynomial A, int m, Complex w) {
            if (m == 1) return A;

            Polynomial A_even = new Polynomial(m / 2), A_odd = new Polynomial(m / 2);

            for (int i = 0; i < m; i += 2) {
                A_even.Values[i / 2] = A.Values[i];
                A_odd.Values[i / 2] = A.Values[i + 1];
            }
            Polynomial F_even = Divide(A_even, m / 2, w * w);
            Polynomial F_odd = Divide(A_odd, m / 2, w * w);
            Polynomial F = new Polynomial(m);
            Complex x = new Complex(1, 0);
            for (int j = 0; j < m / 2; ++j) {
                F.Values[j] = F_even.Values[j] + x * F_odd.Values[j];
                F.Values[j + m / 2] = F_even.Values[j] - x * F_odd.Values[j];
                x = x * w;
            }
            return F;
        }

        static Complex getPrimitiveRootOfUnity(int gen) {
            return new Complex(Math.Cos(2 * Math.PI / gen), Math.Sin(2 * Math.PI / gen));
        }
    }

    public static class Traditional {
        public static float[] multiplyTraditional(float[] A, float[] B, bool progress = false) {
            if (progress) Console.WriteLine();
            int m = A.Length, n = B.Length;
            float[] prod = new float[m + n - 1];

            // Initialize the porduct polynomial
            for (int i = 0; i < m + n - 1; i++)
                prod[i] = 0;

            // Multiply two polynomials term by term

            // Take ever term of first polynomial
            for (int i = 0; i < m; i++) {
                if (progress) {
                    Console.CursorLeft = 0;
                    Console.Write(100 * Math.Round((float)i / (float)m, 4) + "%");
                }
                // Multiply the current term of first polynomial
                // with every term of second polynomial.
                for (int j = 0; j < n; j++)
                    prod[i + j] += A[i] * B[j];
            }

            if (progress) Console.WriteLine();
            return prod;
        }
    }

    public static class Lagrange {
        public static int Lagrange_interpolation(int[] Q, int xi, int n) {
            var result = 0; // Initialize result

            for (int i = 0; i < n; i++) {
                var term = Q[i];
                for (int j = 0; j < n; j++) {
                    if (j != i)
                        term = term * (xi - j) / (i - j);
                }

                // Add current term to result
                result += term;
            }

            return result;
        }
    }
}
