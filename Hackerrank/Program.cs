using Algo;
using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hackerrank
{
    class Program
    {

        [STAThread]
        static void MainP(string[] args) {
            Console.WriteLine("".PadLeft(79, '_') + "\n");
            rand = new Random(); stopwatch = Stopwatch.StartNew();
            var timer = new System.Threading.Timer(memoryCalc, null, 0, 100);
            //Console.SetIn(new StreamReader("input"));

            AVLNodeTest.Test();

            Statistics();
        }



        static long countPrimeXor(int n, int[] arr) {
            Dictionary<int, long> A = new Dictionary<int, long>(), B = new Dictionary<int, long>();

            for (int i = 0; i < n; i++) {
                var x = arr[i];
                foreach (var it in A) {
                    if (B.ContainsKey(it.Key ^ x)) B[it.Key ^ x] = (it.Value + B[it.Key ^ x]) % MOD;
                    else B[it.Key ^ x] = it.Value;
                }
                if (B.ContainsKey(x)) B[x]++; else B[x] = 1;

                foreach (var it in B) {
                    if (A.ContainsKey(it.Key)) A[it.Key] = (A[it.Key] + it.Value) % MOD;
                    else A[it.Key] = it.Value;
                }
                B.Clear();
            }
            long ans = 0;
            foreach (var item in A) {
                if (Algo.Utility.IsPrime(item.Key))
                    ans = (ans + item.Value) % MOD;
            }
            return ans;
        }

        static void generateForGraphViz(HashSet<int>[] adj) {
            StringBuilder sb = new StringBuilder();
            sb.Append("graph G {");
            for (int i = 0; i < adj.Length; i++) {
                foreach (var item in adj[i]) {
                    if (item > i) continue;
                    sb.Append(item + " -- " + i + ";");
                }
            }
            sb.Append("}");
            System.Windows.Forms.Clipboard.SetText(sb.ToString());
            Console.WriteLine("Text is in Clipboard");
        }

        public static void log(string s) {
            File.AppendAllText("C:\\log.txt", DateTime.Now.ToLongTimeString() + "  :::  " + s + Environment.NewLine);
            Console.WriteLine(s);
        }

        #region ___Constants
        const string Alphabet_U = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string Alphabet_L = "abcdefghijklmnopqrstuvwxyz";
        const int MOD = 1000000007;
        public static Stopwatch stopwatch;
        static int maxMemory = 0; static Random rand;
        static void memoryCalc(object o) { maxMemory = Math.Max(maxMemory, (int)(GC.GetTotalMemory(true) / 1000)); }
        public static void Statistics(bool restartSW = false, bool restartMemory = false) {
            Thread.Sleep(100);
            Console.WriteLine(String.Format("Elapsed Time : {0} ms\nPeak Memory  : {1} kbs\n{2}",
                                            stopwatch.ElapsedMilliseconds.ToString("N0").PadRight(7, ' '),
                                            maxMemory.ToString("N0").PadRight(7, ' '),
                                            "".PadLeft(79, '_')));

            Console.WriteLine();
            if (restartSW) stopwatch.Restart();
            if (restartMemory) maxMemory = 0;
        }
        #endregion

        #region ___Collection

        private static T[] Init<T>(int size) where T : new() { var ret = new T[size]; for (int i = 0; i < size; i++) ret[i] = new T(); return ret; }
        #region ___SORTING
        public static void TestSorting() {
            while (true) {
                int n = int.Parse(Console.ReadLine());
                int[] A = new int[n], B = new int[n];
                for (int i = 0; i < n; i++) {
                    A[i] = rand.Next(-1000, 1000); B[i] = A[i];
                }

                stopwatch.Restart();
                Console.WriteLine("Starting Merge Sort...");
                mergeSort(A, 0, n - 1);
                //Console.WriteLine(string.Join(" ", A));

                Statistics(restartSW: true, restartMemory: true);

                Console.WriteLine("Starting for Quick Sort...");
                quickSort(B, 0, n - 1);
                //Console.WriteLine(string.Join(" ", B));
                Statistics(restartSW: true, restartMemory: true);
            }
        }

        static int partition(int[] arr, int l, int r, int x) {
            // Search for x in arr[l..r] and move it to end
            int i, at;
            for (i = l; i < r; i++)
                if (arr[i] == x)
                    break;


            at = arr[i];
            arr[i] = arr[r];
            arr[r] = at;

            // Standard partition algorithm
            i = l;
            for (int j = l; j <= r - 1; j++) {
                if (arr[j] <= x) {
                    at = arr[i];
                    arr[i] = arr[j]; arr[j] = at;
                    i++;
                }
            }
            at = arr[i];
            arr[i] = arr[r]; arr[r] = at;
            return i;
        }

        static int findMedian(int[] arr, int a, int n) {
            Array.Sort(arr, a, n);  // Sort the array
            return arr[a + n / 2];   // Return middle element
        }

        static int kthSmallest(int[] arr, int l, int r, int k) {
            // If k is smaller than number of elements in array
            if (k > 0 && k <= r - l + 1) {
                int n = r - l + 1; // Number of elements in arr[l..r]

                // Divide arr[] in groups of size 5, calculate median
                // of every group and store it in median[] array.
                int i; int[] median = new int[(n + 4) / 5]; // There will be floor((n+4)/5) groups;
                for (i = 0; i < n / 5; i++)
                    median[i] = findMedian(arr, l + i * 5, 5);
                if (i * 5 < n) { //For last group with less than 5 elements
                    median[i] = findMedian(arr, l + i * 5, n % 5);
                    i++;
                }

                // Find median of all medians using recursive call.
                // If median[] has only one element, then no need
                // of recursive call
                int medOfMed = (i == 1) ? median[i - 1] :
                                         kthSmallest(median, 0, i - 1, i / 2);

                // Partition the array around a random element and
                // get position of pivot element in sorted array
                int pos = partition(arr, l, r, medOfMed);

                // If position is same as k
                if (pos - l == k - 1)
                    return arr[pos];
                if (pos - l > k - 1)  // If position is more, recur for left
                    return kthSmallest(arr, l, pos - 1, k);

                // Else recur for right subarray
                return kthSmallest(arr, pos + 1, r, k - pos + l - 1);
            }

            // If k is more than number of elements in array
            return int.MaxValue;
        }

        static void quickSort(int[] arr, int l, int h) {
            if (l < h) {
                // Find size of current subarray
                int n = h - l + 1;

                // Find median of arr[].
                int med = kthSmallest(arr, l, h, n / 2);

                // Partition the array around median
                int p = partition(arr, l, h, med);

                // Recur for left and right of partition
                quickSort(arr, l, p - 1);
                quickSort(arr, p + 1, h);
            }
        }
        #endregion

        #region ___SCC
        public static void SCC() {
            Console.WriteLine("Starting");
            StringBuilder sb = new StringBuilder();
            int aka = 0;
            int[] A = new int[6000000], B = new int[6000000];
            using (StreamReader sr = File.OpenText("C:\\scc2.txt")) {

                string s = String.Empty;
                while ((s = sr.ReadLine()) != null) {
                    var ttmp = s.Split(' ');
                    A[aka] = int.Parse(ttmp[0]);
                    B[aka] = int.Parse(ttmp[1]);
                    aka++;
                }
            }

            log("Finished Reading the file...");
            int mx = 900000;
            HashSet<int>[] map = new HashSet<int>[mx];
            for (int i = 0; i < mx; i++) map[i] = new HashSet<int>();
            log("Finished First Set...");
            HashSet<int>[] mapB = new HashSet<int>[mx];
            for (int i = 0; i < mx; i++) mapB[i] = new HashSet<int>();
            log("Finished Second Set...");

            bool[] exist = new bool[mx], V = new bool[mx];
            log("Started Reading the file...");

            for (int i = 0; i < aka; i++) {
                if (i % 10000 == 0) {
                    log(Math.Round(100.0 * (double)i / (double)aka, 4) + " %          ");
                }
                int l = A[i] % 1000000;
                int r = B[i] % 1000000;

                map[l].Add(r);
                mapB[r].Add(l);
                exist[l] = exist[r] = true;
            }
            log("Assigned All...");

            Statistics();

            Stack<int> S = new Stack<int>();
            for (int i = 0; i < mx; i++) {
                if (!exist[i] || V[i]) continue;
                FillReverse(mapB, i, V, S);
            }

            log("Finished First DFS...");
            log("Stack contains " + S.Count + " items");
            V = new bool[mx];

            List<int> L = new List<int>();
            while (S.Count > 0) {
                if (S.Count % 10000 == 0) {
                    log("S.Count = " + S.Count);
                }
                int cur = S.Pop();
                if (V[cur]) continue;
                int r = 0;
                DFS_SCC(map, cur, V, ref r);
                L.Add(r);
            }
            L.Sort();

            log(string.Join(",", L));


            log("Adios Amigo.........");
            for (int i = 0; i < 10; i++) {
                Console.ReadLine();
            }
        }

        static void DFS_SCC(HashSet<int>[] map, int p, bool[] V, ref int r) {
            r++;
            V[p] = true;
            foreach (var node in map[p]) {
                if (V[node]) continue;
                DFS_SCC(map, node, V, ref r);
            }
        }

        static void FillReverse(HashSet<int>[] mapB, int p, bool[] V, Stack<int> S) {
            V[p] = true;
            foreach (var node in mapB[p]) {
                if (V[node]) continue;
                FillReverse(mapB, node, V, S);
            }
            S.Push(p);
        }
        #endregion

        static void ConstructCNKTable() {
            int n = 200;
            long[,] C = new long[n, n];
            C[0, 0] = 1;
            for (int i = 1; i < n; i++) {
                C[i, 0] = C[i, i] = 1;
                for (int j = 1; j <= i; j++)
                    C[i, j] = (C[i - 1, j] + C[i - 1, j - 1]) % 1000000007;
            }
        }

        static long CNK(long n, long k) {
            if (n < k) return 0;
            if (n == k || k == 0) return 1;
            return (n * CNK(n - 1, k - 1)) / k;
        }

        static void Number_Of_Inversions() {
            int n = 100000;
            int[] A = new int[n];

            Console.WriteLine(_mergeSort_Inversions(A, new int[n], 0, n - 1));
        }

        static long _mergeSort_Inversions(int[] A, int[] helper, int left, int right) {
            long inv_count = 0;
            if (right > left) {

                int mid = (right + left) / 2;

                // divide
                inv_count = _mergeSort_Inversions(A, helper, left, mid);
                inv_count += _mergeSort_Inversions(A, helper, mid + 1, right);

                // combine
                inv_count += merge_Inversions(A, helper, left, mid + 1, right);
            }
            return inv_count;
        }

        static long merge_Inversions(int[] A, int[] helper, int left, int mid, int right) {
            int i, j, k;
            long inv_count = 0;

            i = left;
            j = mid;
            k = left;
            while ((i <= mid - 1) && (j <= right)) {
                if (A[i] <= A[j]) {
                    helper[k++] = A[i++];
                } else {
                    helper[k++] = A[j++];

                    inv_count += mid - i;
                }
            }

            while (i <= mid - 1) helper[k++] = A[i++];

            while (j <= right) helper[k++] = A[j++];

            for (i = left; i <= right; i++) A[i] = helper[i];

            return inv_count;
        }

        static void PrettyPrintPolynomial(float[] c) {
            int y = Console.CursorTop;
            int x = Console.CursorLeft;
            bool first = true;
            for (int i = c.Length - 1; i >= 0; i--) {
                if (c[i] == 0) continue;

                if (c[i] > 0 && !first) {
                    setCursor(x, y + 1);
                    Console.Write(" + ");
                    x += 3;
                } else if (c[i] < 0) {
                    setCursor(x, y + 1);
                    Console.Write(" - ");
                    x += 3;
                }

                var ci = Math.Abs(c[i]).ToString();
                setCursor(x, y + 1);
                Console.Write(ci);
                x += ci.Length;

                if (i > 0) {
                    setCursor(x++, y + 1);
                    Console.Write("x");
                }

                if (i > 1) {
                    setCursor(x, y);
                    Console.Write(i);
                    x += i.ToString().Length;
                } else x++;

                first = false;
            }
            setCursor(0, y + 2);
        }

        static void setCursor(int x, int y) {
            Console.CursorTop = y;
            Console.CursorLeft = x;
        }

        static void testFFT() {
            while (true) {
                int n = 0;
                try { n = int.Parse(Console.ReadLine()); } catch { continue; }
                stopwatch.Restart();
                float[] a = new float[n], b = new float[n];

                for (int i = 0; i < n; i++) {
                    a[i] = rand.Next(-10, 10);
                    b[i] = rand.Next(-10, 10);
                }

                Console.WriteLine("Fast Fourier Transform...");
                float[] c = FFT.MultiplyTwoPolynomialsFFT(a, b);

                Statistics(restartSW: true);

                Console.WriteLine("O(n^2) multiplication...");
                float[] d = Traditional.multiplyTraditional(a, b, true);

                Statistics(restartSW: true);
                //PrettyPrintPolynomial(c);
            }
        }

        public class InverseComparer : IComparer<int>
        {
            public int Compare(int x, int y) {
                return y.CompareTo(x);
            }
        }

        #region ___karger
        static void Karger() {
            int n = 200;
            var file = File.ReadAllLines("C:\\karger"); int ifile = 0;
            HashSet<int>[] adj = new HashSet<int>[n];
            for (int i = 0; i < n; i++) adj[i] = new HashSet<int>();

            for (int i = 0; i < file.Length; i++) {
                var tmt = file[ifile++].Trim().Split('\t');
                int hcur = int.Parse(tmt[0]) - 1;
                for (int j = 1; j < tmt.Length; j++) {
                    int th = int.Parse(tmt[j]) - 1;
                    adj[hcur].Add(th);
                    adj[th].Add(hcur);
                }
            }
            int optimum = int.MaxValue;
            int runHowManyTimes = 100;
            while (runHowManyTimes-- > 0) {
                rand = new Random(rand.Next());
                int[] vals = new int[n];
                for (int i = 0; i < n; i++) {
                    vals[i] = i;
                }

                int s = rand.Next(0, n);
                while (!KargerUtil(adj, vals, s, n)) {
                    s = rand.Next(0, n);
                }

                int cuts = 0;
                for (int i = 0; i < 200; i++) {
                    if (vals[i] == 1) continue;
                    foreach (var item in adj[i]) {
                        cuts += vals[item];
                    }
                }
                optimum = Math.Min(optimum, cuts);
            }

            Console.WriteLine(optimum);
        }

        static bool KargerUtil(HashSet<int>[] adj, int[] vals, int p, int n) {
            foreach (var item in adj[p]) {
                if (item == p) continue;

                var min = Math.Min(vals[item], vals[p]);
                var max = Math.Max(vals[item], vals[p]);
                if (min == max || min + max == 1) continue;

                bool good = true;
                for (int i = 0; i < n; i++) {
                    if (vals[i] == max) vals[i] = min;
                    if (vals[i] > 1) good = false;
                }
                return good;
            }

            return false;
        }
        #endregion

        #region ___Merge_Sort
        static long ans_mergeSort;
        static void mergeSort(int[] arr, int l, int r) {
            if (l < r) {
                int m = l + (r - l) / 2;
                mergeSort(arr, l, m);
                mergeSort(arr, m + 1, r);
                merge(arr, l, m, r);
            }
        }
        static void merge(int[] arr, int l, int m, int r) {
            int i, j, k;
            int n1 = m - l + 1;
            int n2 = r - m;

            int[] L = new int[n1], R = new int[n2];

            for (i = 0; i < n1; i++) L[i] = arr[l + i];
            for (j = 0; j < n2; j++) R[j] = arr[m + 1 + j];

            i = j = 0;
            k = l;
            while (i < n1 && j < n2) {
                if (L[i] <= R[j]) {
                    arr[k] = L[i++];
                } else {
                    arr[k] = R[j++];
                    ans_mergeSort += j + m - k;
                }
                k++;
            }

            while (i < n1)
                arr[k++] = L[i++];


            while (j < n2)
                arr[k++] = R[j++];

        }
        #endregion

        #region ___Writing
        public static void WriteInABox<T>(T s, int width = 15, int height = 6) {
            int x = Console.CursorLeft, y = Console.CursorTop;

            for (int i = 0; i < width; i++) {
                WriteAt("=", x + i, y);
                WriteAt("=", x + i, y + height);
            }
            for (int i = 1; i < height; i++) {
                WriteAt("||", x, y + i);
                WriteAt("||", x + width - 2, y + i);
            }

            WriteAt(s.ToString(), x + width / 2 - s.ToString().Length / 2, y + height / 2);

            Console.CursorTop = y + height + 1;
            Console.CursorLeft = x;
        }

        static void Test_Coordinates() {
            while (true) {
                int n = 20;
                int topX = 20, topY = 15;
                int[] px = new int[n], py = new int[n];
                for (int i = 0; i < n; i++) {
                    px[i] = rand.Next(-1 * topX, topX);
                    py[i] = rand.Next(-1 * topY, topY);
                }
                Draw_Points(topX, topY, px, py, 1);
                Console.ReadLine();
            }
        }

        public static void Draw_Points(int X, int Y, int[] px, int[] py, int scale = 1, int rightPadding = 5, string s = "x") {
            Y /= scale; X /= scale;
            Y *= 2; X *= 2;
            int sX = rightPadding;
            int sY = Console.CursorTop;

            for (int i = 1; i <= Y; i++) {
                WriteAt("|", sX + X / 2, sY + i);
            }
            for (int i = 0; i < X; i++) {
                WriteAt("-", sX + i, sY + Y / 2);
            }

            for (int i = 0; i < px.Length; i++) {
                WriteAt(s, sX + (px[i] / scale) + X / 2, sY + Y / 2 - (py[i] / scale));
            }


            Console.CursorTop = sY + Y;
            Console.CursorTop = sY;
            Console.CursorLeft = 0;
        }

        static void WriteAt(string s, int x, int y) {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            Console.Write(s);
        }

        #endregion

        #region ___Clustering K-means
        class K_Means
        {
            public static void Start() {
                var clusters = new List<Point>
            {
                new Point(2,10),
                new Point(4,9),
                new Point(5,8)
            };

                List<Point> P = new List<Point>()
            {
                new Point(2,10),
                new Point(2,5),
                new Point(8,4),
                new Point(5,8),
                new Point(7,5),
                new Point(6,4 ),
                new Point(1,2),
                new Point(4,9)
            };

                for (int i = 0; i < P.Count; i++) {
                    P[i].name = "A" + (i + 1);
                }

                clusters[0].name = "C1";
                clusters[1].name = "C2";
                clusters[2].name = "C3";
                for (int __ = 0; __ < 10; __++) {

                    Console.WriteLine(new string('=', 40));
                    Console.WriteLine("ROUND " + (__ + 1));
                    foreach (var p in P) {
                        Console.WriteLine();
                        foreach (var cl in clusters) {
                            var cur = p.distance(cl);
                            var orig = p.distanceToCluster();

                            Console.WriteLine(p.ToString() + "  " + cl.ToString() + "  " + cur.ToString("F2"));
                            if (cur < orig) {
                                p.C = cl;
                            }
                        }
                    }
                    Console.WriteLine(new string('-', 20));

                    foreach (var cl in clusters) {
                        double a = 0, b = 0; int i = 0;
                        foreach (var p in P.Where(_ => _.C == cl)) {
                            i++;
                            a += p.X;
                            b += p.Y;
                        }
                        cl.X = a / i;
                        cl.Y = b / i;
                    }
                    Console.WriteLine(new string('=', 40));
                    displayColusters(P);
                    foreach (var item in clusters) {
                        Console.WriteLine(item.X);
                    }
                }
            }
            private static void displayColusters(List<Point> P) {
                foreach (var p in P) {
                    Console.WriteLine(p.name + " " + p.C.ToString());
                }
            }
        }
        class Point
        {
            public double X, Y;
            public Point C;
            public string name;
            public Point(double x = 0, double y = 0, Point c = null) { X = x; Y = y; C = c; }
            public override String ToString() {
                if (name[0] == 'C') return name + String.Format("   ({0}, {1})", X.ToString("F2"), Y.ToString("F2"));
                return name;
            }
            public double distance(Point p) {
                return distance(p.X, p.Y);
            }
            public double distance(double a, double b) {
                return Math.Sqrt(sq(a - X) + sq(b - Y));
            }
            public static double sq(double a) { return a * a; }

            public double distanceToCluster() {
                if (C == null) return double.MaxValue;
                return distance(C);
            }
        }
        #endregion


        #endregion
    }
}
