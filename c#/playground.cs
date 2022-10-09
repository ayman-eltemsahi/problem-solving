using System;

public class Solution {
  public int FindContentChildren(int[] g, int[] s) {
    Array.Sort(g);
    Array.Sort(s);

    int i = g.Length - 1, j = s.Length - 1;
    int res = 0;
    while (i >= 0 && j >= 0) {
      if (s[j] >= g[i]) {
        res++;
        i--; j--;
      } else {
        i--;
      }
    }

    return res;
  }
}
