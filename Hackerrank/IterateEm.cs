using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
class Solution2 {
    const int MOD = 998244353;
    static int[] P;
    static void Main2(String[] args) {
        Console.SetIn(new StreamReader("input"));
        var tmp = Console.ReadLine().Split(' ');
        int n = int.Parse(tmp[0]);
        int k = int.Parse(tmp[1]);

        P = new int[n];
        for (int i = 0; i < n; i++) P[i] = i;
        int[] A = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
        int[] B = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

        for (int i = 0; i < n; i++) {
            if (go(A, B, i, n, k)) {
                Console.WriteLine(i);
                return;
            }
        }
        Console.WriteLine(-1);
    }

    static bool go(int[] A, int[] B, int p, int n, int k) {
        int[] g = new int[n];
        for (int i = 0; i < n; i++) {
            g[i] = (A[i] - B[(i + p) % n]) % MOD;
        }

        for (int i = n - 1; i > k; i--) {
            if (g[i] != 0 && !zero(i, n, i)) return true;
        }

        return false;
    }

    public static bool zero(int xi, int n, int i) {

        for (int j = 0; j < n; j++) {
            if (j != i && xi == j) return true;
        }


        return false;
    }

}