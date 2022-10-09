using System;
using System.Text;
using System.Collections.Generic;

public class Solution {
  char[,] board;
  bool[] cols = new bool[10];
  bool[] diag1 = new bool[20];
  bool[] diag2 = new bool[20];
  IList<IList<string>> res = new List<IList<string>>();

  public IList<IList<string>> SolveNQueens(int n) {
    board = new char[n, n];
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < n; j++) {
        board[i, j] = '.';
      }
    }

    Check(0, n);
    return res;
  }

  void Check(int i, int n) {
    if (i == n) {
      AddSolution(n);
      return;
    }

    for (int j = 0; j < n; j++) {
      if (!cols[j] && !diag1[i + j] && !diag2[n - 1 + j - i]) {
        board[i, j] = 'Q';
        cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = true;

        Check(i + 1, n);

        board[i, j] = '.';
        cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = false;
      }
    }
  }

  void AddSolution(int n) {
    var q = new List<String>();
    for (int i = 0; i < n; i++) {
      var cur = new StringBuilder();
      for (int j = 0; j < n; j++) {
        cur.Append(board[i, j]);
      }
      q.Add(cur.ToString());
    }

    res.Add(q);
  }
}
