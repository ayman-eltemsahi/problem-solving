using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackerrank.Sorting
{
    public static class Bigger_is_Greater
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                string line = Console.ReadLine();

                bool na = true;
                for (int i = line.Length - 1; i >= 0; i--) {
                    if (!na) break;
                    int g = -1;

                    for (int j = i + 1; j < line.Length; j++) {
                        if (line[j] > line[i]) {
                            if (g == -1 || line[j] < line[g]) g = j;
                        }
                    }
                    if (g == -1) continue;
                    var sb = new StringBuilder();

                    sb.Append(line.Substring(0, i) + line[g] + line.Substring(i + 1, g - i - 1) + line[i] + line.Substring(g + 1));

                    var aux = sb.ToString().Substring(i + 1);
                    var temp = aux.ToCharArray();
                    Array.Sort(temp);
                    aux = new string(temp);
                    Console.Write(sb.ToString().Substring(0, i + 1));
                    Console.WriteLine(aux);
                    na = false; break;
                }

                if (na) { Console.WriteLine("no answer"); }
            }
        }
    }
    public static class Almost_Sorted
    {
        static void Start() {
            int n = int.Parse(Console.ReadLine());
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

            string o = "";

            if (isSorted(arr)) o = "yes";
            else {
                int l = -1, r = 0;
                for (int i = 1; i < n; i++) {
                    if (arr[i] < arr[i - 1]) {
                        if (l == -1) l = i - 1;
                        r = i;
                    }
                }

                int[] tmp = new int[r - l + 1];
                for (int i = l; i <= r; i++) {
                    tmp[i - l] = arr[i];
                }

                int[] ar2 = new int[n];
                for (int i = 0; i < n; i++) { ar2[i] = arr[i]; }
                for (int i = l; i <= r; i++) {
                    ar2[i] = tmp[r - i];
                }
                if (isSorted(ar2)) {
                    string w = r - l == 1 ? "swap " : "reverse ";
                    o = "yes" + Environment.NewLine + w + (l + 1) + " " + (r + 1);
                } else {
                    int tp = arr[r];
                    arr[r] = arr[l];
                    arr[l] = tp;
                    if (isSorted(arr)) {
                        o = "yes" + Environment.NewLine + "swap " + (l + 1) + " " + (r + 1);
                    } else o = "no";
                }
            }
            Console.WriteLine(o);
        }
        static bool isSorted(int[] arr) {
            for (int i = 1; i < arr.Length; i++) {
                if (arr[i] < arr[i - 1]) return false;
            }
            return true;
        }
    }

    public static class Insertion_Sort_Advanced_Analysis
    {
        static long insSortAnalyans;
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                insSortAnalyans = 0;
                mergeSort(arr, 0, n - 1);
                Console.WriteLine(insSortAnalyans);
            }
        }
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
                    arr[k] = L[i];
                    i++;
                } else {
                    arr[k] = R[j];
                    j++;
                    insSortAnalyans += j + m - k;
                }
                k++;
            }

            while (i < n1) {
                arr[k] = L[i];
                i++;
                k++;
            }

            while (j < n2) {
                arr[k] = R[j];
                j++;
                k++;
            }
        }
    }
}
