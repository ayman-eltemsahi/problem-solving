using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Algo
{
    public static class BinarySearch
    {
        public static int LowerBound(int[] A, int m) {
            int lo = 0, hi = A.Length - 1;
            int mid;
            while (lo <= hi) {
                mid = (lo + hi) / 2;
                if (m <= A[mid]) {
                    hi = mid - 1;
                } else {
                    lo = mid + 1;
                }
            }
            return lo < A.Length && A[lo] == m ? lo : ~lo;
        }
        public static int LowerBoundRecursive(int[] A, int m) {
            return LowerBoundRecursive(A, m, 0, A.Length - 1);
        }
        private static int LowerBoundRecursive(int[] A, int m, int lo, int hi) {
            int mid;
            if (lo <= hi) {
                mid = (lo + hi) / 2;
                if (m <= A[mid]) {
                    return LowerBoundRecursive(A, m, lo, mid - 1);
                } else {
                    return LowerBoundRecursive(A, m, mid + 1, hi);
                }
            }
            return lo < A.Length && A[lo] == m ? lo : ~lo;
        }



        public static int UpperBound(int[] A, int m) {
            int lo = 0, hi = A.Length - 1, mid;
            while (lo <= hi) {
                mid = (lo + hi) / 2;
                if (m >= A[mid])
                    lo = mid + 1;
                else
                    hi = mid - 1;
            }
            return hi < 0 || (hi < A.Length && A[hi] == m) ? hi : ~hi;
        }
        public static int UpperBoundRecursive(int[] A, int m) {
            return UpperBoundRecursive(A, m, 0, A.Length - 1);
        }
        private static int UpperBoundRecursive(int[] A, int m, int lo, int hi) {
            int mid;
            if (lo <= hi) {
                mid = (lo + hi) / 2;
                if (m >= A[mid]) {
                    return UpperBoundRecursive(A, m, mid + 1, hi);
                } else {
                    return UpperBoundRecursive(A, m, lo, mid - 1);
                }
            }
            return hi < 0 || (hi < A.Length && A[hi] == m) ? hi : ~hi;
        }
    }


    public static class BinarySearchTest
    {
        static Random rand = new Random();
        public static void LowerBoundTest(int n, Func<int[], int, int> binaryFun, CancellationToken token) {
            int[] arr = null;

            var test = new Tester(7);
            test.Start();

            int a, i, index = 0;
            while (!token.IsCancellationRequested) {
                if ((index++) % 1000 == 0) {
                    arr = Enumerable
                       .Range(0, n)
                       .Select(x => rand.Next(0, n / 2))
                       .OrderBy(x => x)
                       .ToArray();
                }


                a = rand.Next(0, n / 2);
                i = 0;
                while (i < n) {
                    if (arr[i] == a) break;
                    if (arr[i] > a) { i = ~i; break; }
                    i++;
                }
                if (i == n) i = ~n;
                int j = binaryFun(arr, a);


                test.Assert(i == j);
            }
        }

        public static void Test() {
            int n = 1001;
            CancellationTokenSource source = new CancellationTokenSource();
            var t1 = Task.Run(() => {
                LowerBoundTest(n, BinarySearch.LowerBound, source.Token);
                LowerBoundTest(n, BinarySearch.LowerBoundRecursive, source.Token);
            });
            var t2 = Task.Run(() => {
                UpperBoundTest(n, BinarySearch.UpperBound, source.Token);
                UpperBoundTest(n, BinarySearch.UpperBoundRecursive, source.Token);
            });

            Console.ReadKey(false);
            source.Cancel();
        }

        public static void UpperBoundTest(int n, Func<int[], int, int> binaryFun, CancellationToken token) {
            int[] arr = null;

            var test = new Tester(11);
            test.Start();

            int a, i, index = 0;
            while (!token.IsCancellationRequested) {
                if ((index++) % 1000 == 0) {
                    arr = Enumerable
                       .Range(0, n)
                       .Select(x => rand.Next(0, n / 2))
                       .OrderBy(x => x)
                       .ToArray();
                }

                a = rand.Next(0, n / 2);
                i = n - 1;
                while (i >= 0) {
                    if (arr[i] == a) break;
                    if (arr[i] < a) { i = ~i; break; }
                    i--;
                }
                int j = binaryFun(arr, a);

                test.Assert(i == j);
            }
        }
    }
}
