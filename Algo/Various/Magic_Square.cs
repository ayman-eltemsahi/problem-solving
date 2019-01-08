using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo.Various
{
    public static class MagicSquare
    {
        public static int[,] Calculate(int n) {
            if (n % 2 == 1)
                return OddMagicSquare(n);
            else if (n % 4 == 0)
                return DoublyEvenMagicSquare(n);
            else
                return SinglyEvenMagicSquare(n);
        }

        public static int[,] OddMagicSquare(int n) {
            int[,] magic = new int[n, n];

            int i = n / 2;
            int j = n - 1;

            for (int num = 1; num <= n * n;) {
                if (i == -1 && j == n) {
                    j = n - 2;
                    i = 0;
                } else {
                    if (j == n) j = 0;
                    if (i < 0) i = n - 1;
                }
                if (magic[i, j] != 0) {
                    j -= 2;
                    i++;
                    continue;
                } else magic[i, j] = num++;

                j++; i--;
            }

            return magic;

        }

        public static int[,] DoublyEvenMagicSquare(int n) {
            int[,] matrix = new int[n, n], I = new int[n, n], J = new int[n, n];

            int i, j, index = 1;

            for (i = 0; i < n; i++) {
                for (j = 0; j < n; j++) {
                    I[i, j] = ((i + 1) % 4) / 2;
                    J[j, i] = ((i + 1) % 4) / 2;
                    matrix[i, j] = index;
                    index++;
                }
            }

            for (i = 0; i < n; i++) {
                for (j = 0; j < n; j++) {
                    if (I[i, j] == J[i, j]) matrix[i, j] = n * n + 1 - matrix[i, j];
                }
            }

            return matrix;
        }

        public static int[,] SinglyEvenMagicSquare(int n) {
            int[,] matrix = new int[n, n];

            int p = n / 2, i, j, k, temp;

            var M = OddMagicSquare(p);


            for (i = 0; i < p; i++) {
                for (j = 0; j < p; j++) {
                    matrix[i, j] = M[i, j];
                    matrix[i + p, j] = M[i, j] + 3 * p * p;
                    matrix[i, j + p] = M[i, j] + 2 * p * p;
                    matrix[i + p, j + p] = M[i, j] + p * p;
                }
            }

            int[] I = new int[p];
            var J = new List<int>();

            for (i = 0; i < p; i++) I[i] = i + 1;

            k = (n - 2) / 4;

            for (i = 1; i <= k; i++) J.Add(i);

            for (i = n - k + 2; i <= n; i++) J.Add(i);

            for (i = 1; i <= p; i++) {
                for (j = 1; j <= J.Count; j++) {
                    temp = matrix[i - 1, J[j - 1] - 1];
                    matrix[i - 1, J[j - 1] - 1] = matrix[i + p - 1, J[j - 1] - 1];
                    matrix[i + p - 1, J[j - 1] - 1] = temp;
                }
            }

            i = k;
            j = 0;

            temp = matrix[i, j]; matrix[i, j] = matrix[i + p, j]; matrix[i + p, j] = temp;

            j = i;
            temp = matrix[i + p, j]; matrix[i + p, j] = matrix[i, j]; matrix[i, j] = temp;
            return matrix;
        }

    }
}