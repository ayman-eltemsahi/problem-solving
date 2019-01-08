using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackerrank.Implementation
{
    public static class Larry_s_Array
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                int c = 0;
                for (int i = 0; i < n - 1; i++) {
                    for (int j = i + 1; j < n; j++) {
                        if (arr[j] < arr[i]) c++;
                    }
                }

                Console.WriteLine(c % 2 == 0 ? "YES" : "NO");
            }
        }
    }

    public static class Kangaroo
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int x1 = int.Parse(tmp[0]);
            int v1 = int.Parse(tmp[1]);
            int x2 = int.Parse(tmp[2]);
            int v2 = int.Parse(tmp[3]);

            float t1 = (float)(x1 - x2) / (float)(v2 - v1);
            if (t1 < 0 || t1 != Math.Floor(t1)) {
                Console.WriteLine("NO");
            } else {
                Console.WriteLine("YES");
            }
        }
    }

    public static class Divisible_Sum_Pairs
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int mod = int.Parse(tmp[1]);

            int[] nodes = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x) % mod);

            int r = 0;
            for (int i = 0; i < n; i++) {
                for (int j = i + 1; j < n; j++) {
                    if ((nodes[i] + nodes[j]) % mod == 0) r++;
                }
            }
            Console.WriteLine(r);
        }
    }

    public static class Absolute_Permutation
    {
        static void Start() {
            int tc = int.Parse(Console.ReadLine());
            for (int itc = 0; itc < tc; itc++) {
                string[] tmp = Console.ReadLine().Split(' ');
                int n = int.Parse(tmp[0]);
                int k = int.Parse(tmp[1]);

                if (k == 0) {
                    for (int i = 1; i <= n; i++) { Console.Write(i + " "); }
                    Console.WriteLine(); continue;
                }

                int[] store = new int[n];
                bool[] sb = new bool[n];
                bool[] used = new bool[n];
                bool f = true;
                int ops = 0;
                while (f && ops < n) {

                    for (int i = 1; i <= n; i++) {
                        if (used[i - 1]) continue;
                        int l = -1, r = -1;
                        if (i + k <= n && !sb[i + k - 1]) r = i + k - 1;
                        if (i - k > 0 && !sb[i - k - 1]) l = i - k - 1;

                        if (l == -1 && r == -1) { f = false; break; }
                        if (l == -1) {
                            sb[r] = true;
                            store[r] = i;
                            used[i - 1] = true;
                            ops++;
                        } else if (r == -1) {
                            sb[l] = true;
                            store[l] = i;
                            used[i - 1] = true;
                            ops++;
                        }
                    }

                    for (int i = n; i > 0; i--) {
                        if (used[i - 1]) continue;
                        int l = -1, r = -1;
                        if (i + k <= n && !sb[i + k - 1]) r = i + k - 1;
                        if (i - k > 0 && !sb[i - k - 1]) l = i - k - 1;

                        if (l == -1 && r == -1) { f = false; break; }
                        if (l == -1) {
                            sb[r] = true;
                            store[r] = i;
                            used[i - 1] = true;
                            ops++;
                        } else if (r == -1) {
                            sb[l] = true;
                            store[l] = i;
                            used[i - 1] = true;
                            ops++;
                        }
                    }
                }

                if (!f && ops < n) { Console.WriteLine(-1); } else { Console.WriteLine(string.Join(" ", store)); }
            }
        }
    }

    public static class Algo__Matrix_Rotation
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int rows = int.Parse(tmp[0]);
            int cols = int.Parse(tmp[1]);
            int rotations = int.Parse(tmp[2]);

            int[][] matrix = new int[rows][];
            for (int i = 0; i < rows; i++) {
                matrix[i] = Array.ConvertAll(Console.ReadLine().Split(' '), Convert.ToInt32);
            }

            int depth = 0;

            while (rows - 2 * depth > 0 && cols - 2 * depth > 0) {
                int actualRotations = rotations % (2 * (rows - 2 * depth) + 2 * ((cols - 2 * depth) - 2));

                while (actualRotations-- > 0) {
                    int r = rows - depth - 1, c = cols - depth - 1;

                    int a = matrix[depth][c];
                    int b = matrix[r][depth];

                    for (int i = depth; i < r; i++) matrix[i][c] = matrix[i + 1][c];
                    matrix[r][c] = matrix[r][c - 1];
                    for (int i = r; i > depth; i--) matrix[i][depth] = matrix[i - 1][depth];
                    matrix[depth][depth] = matrix[depth][depth + 1];

                    for (int i = depth + 1; i < c - 1; i++) matrix[depth][i] = matrix[depth][i + 1];
                    matrix[depth][c - 1] = a;
                    for (int i = c - 1; i > depth + 1; i--) matrix[r][i] = matrix[r][i - 1];
                    matrix[r][depth + 1] = b;

                }
                depth++;
            }

            for (int i = 0; i < rows; i++) {
                Console.WriteLine(string.Join(" ", matrix[i]));
            }
        }
    }

    public static class New_Year_Chaos
    {
        public static void Start() {
            int tc = int.Parse(Console.ReadLine());
            while (tc-- > 0) {
                int n = int.Parse(Console.ReadLine());
                int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

                bool ch = false;

                for (int i = 0; i < n; i++) {
                    if (arr[i] - i - 1 > 2) { ch = true; break; }
                }

                if (ch) { Console.WriteLine("Too chaotic"); } else {
                    int count = 0;
                    bool searching = true;
                    while (searching) {
                        searching = false;
                        for (int i = 0; i < n - 1; i++) {
                            if (arr[i] > arr[i + 1]) {
                                swap(arr, i, i + 1);
                                searching = true;
                                count++;
                            }
                        }
                    }
                    Console.WriteLine(count);
                }
            }
        }
        static void swap(int[] arr, int a, int b) {
            int tmp = arr[a];
            arr[a] = arr[b];
            arr[b] = tmp;
        }
    }

    public static class Jumping_on_the_Clouds_Revisited
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int k = int.Parse(tmp[1]);
            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), x => Convert.ToInt32(x));

            int E = 100;

            int pos = 0;
            while (true) {
                pos += k;
                pos %= n;
                if (arr[pos] == 1) E -= 2;
                E -= 1;
                if (pos == 0) break;
            }
            Console.WriteLine(E);
        }
    }

    public static class Emas_Supercomputer
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int n = int.Parse(tmp[0]);
            int m = int.Parse(tmp[1]);

            int G = 0;
            bool[,] map = new bool[n, m];
            for (int i = 0; i < n; i++) {
                var line = Console.ReadLine();
                for (int j = 0; j < m; j++) {
                    map[i, j] = line[j] == 'G';
                    if (map[i, j]) G++;
                }
            }

            int ans = 1;

            if (G < 2) Console.WriteLine(0);
            else {
                for (int i = 0; i < n; i++) {
                    for (int j = 0; j < m; j++) {
                        if (!map[i, j]) continue;

                        int h = 1, v = 1;
                        while (j - h >= 0 && j + h < m && map[i, j - h] && map[i, j + h]) h++;
                        while (i - v >= 0 && i + v < n && map[i - v, j] && map[i + v, j]) v++;

                        h = Math.Min(h, v);

                        for (int p = j - h + 1; p < j + h; p++) map[i, p] = false;
                        for (int p = i - h + 1; p < i + h; p++) map[p, j] = false;

                        int a = -1, b = -1, c = -1;
                        SEARCH(map, out a, out b, out c, n, m);

                        ans = Math.Max(ans, (4 * h - 3) * (4 * c - 3));

                        for (int p = j - h + 1; p < j + h; p++) map[i, p] = true;
                        for (int p = i - h + 1; p < i + h; p++) map[p, j] = true;
                    }
                }
                Console.WriteLine(ans);
            }
        }
        static void SEARCH(bool[,] map, out int a, out int b, out int c, int n, int m) {
            a = b = c = -1;

            for (int i = 0; i < n; i++) {
                for (int j = 0; j < m; j++) {
                    if (!map[i, j]) continue;

                    int h = 1, v = 1;
                    while (j - h >= 0 && j + h < m && map[i, j - h] && map[i, j + h]) h++;
                    while (i - v >= 0 && i + v < n && map[i - v, j] && map[i + v, j]) v++;

                    h = Math.Min(h, v);

                    if (h > c) {
                        c = h; a = i; b = j;
                    }
                }
            }
            if (c < 0) c = 0;
        }
    }

    public static class The_Bomberman_Game
    {
        public static void Start() {
            var tmp = Console.ReadLine().Split(' ');
            int R = int.Parse(tmp[0]);
            int C = int.Parse(tmp[1]);
            int N = int.Parse(tmp[2]);

            bool[,] map = new bool[R, C];
            bool[,] map2 = new bool[R, C];

            for (int i = 0; i < R; i++) {
                var gar = Console.ReadLine();
                for (int j = 0; j < C; j++) {
                    map[i, j] = gar[j] == 79;
                }
            }

            if (N % 2 == 0) {
                for (int i = 0; i < R; i++) {
                    for (int j = 0; j < C; j++) {
                        Console.Write("O");
                    }
                    Console.WriteLine();
                }
            } else if (N == 1) {
                for (int i = 0; i < R; i++) {
                    for (int j = 0; j < C; j++) {
                        Console.Write(map[i, j] ? "O" : ".");
                    }
                    Console.WriteLine();
                }
            } else {
                for (int i = 0; i < R; i++) {
                    for (int j = 0; j < C; j++) {
                        map2[i, j] = !map[i, j];
                    }
                }

                for (int i = 0; i < R; i++)
                    for (int j = 0; j < C; j++)
                        if (ValidBomb(R, C, map, i, j)) map2[i, j] = false;


                map = map2;
                map2 = new bool[R, C];
                for (int i = 0; i < R; i++)
                    for (int j = 0; j < C; j++)
                        map2[i, j] = !map[i, j];
                for (int i = 0; i < R; i++)
                    for (int j = 0; j < C; j++)
                        if (ValidBomb(R, C, map, i, j)) map2[i, j] = false;


                for (int i = 0; i < R; i++)
                    for (int j = 0; j < C; j++)
                        map2[i, j] = !map[i, j];
                for (int i = 0; i < R; i++)
                    for (int j = 0; j < C; j++)
                        if (ValidBomb(R, C, map, i, j)) map2[i, j] = false;

                N--;
                if (N % 4 == 0) {
                    for (int i = 0; i < R; i++) {
                        for (int j = 0; j < C; j++) {
                            Console.Write(map2[i, j] ? "O" : ".");
                        }
                        Console.WriteLine();
                    }
                } else {
                    for (int i = 0; i < R; i++) {
                        for (int j = 0; j < C; j++) {
                            Console.Write(map[i, j] ? "O" : ".");
                        }
                        Console.WriteLine();
                    }
                }
            }


        }
        static bool ValidBomb(int R, int C, bool[,] map, int i, int j) {
            return map[i, j] || (i > 0 && map[i - 1, j]) || (i < R - 1 && map[i + 1, j]) || (j > 0 && map[i, j - 1]) || (j < C - 1 && map[i, j + 1]);
        }
    }

    public static class Strange_Counter
    {
        public static void Start() {
            long n = long.Parse(Console.ReadLine());
            long val = 3, val2 = 1, tt = n, inc = 1;
            while (tt - val > 0) {
                tt -= val;
                val2 += val;
                inc += 1;
                val *= 2;

            }

            Console.WriteLine(3 * (long)Math.Pow(2, inc - 1) - (n - val2));
        }
    }
}
