
import java.util.ArrayList;
import java.util.List;

class Solution {
  char[][] board;
  boolean[] cols = new boolean[10];
  boolean[] diag1 = new boolean[20];
  boolean[] diag2 = new boolean[20];
  List<List<String>> res = new ArrayList<>();

  public List<List<String>> solveNQueens(int n) {
    board = new char[n][n];
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < n; j++) {
        board[i][j] = '.';
      }
    }

    check(0, n);
    return res;
  }

  void check(int i, int n) {
    if (i == n) {
      add_solution(n);
      return;
    }

    for (int j = 0; j < n; j++) {
      if (!cols[j] && !diag1[i + j] && !diag2[n - 1 + j - i]) {
        board[i][j] = 'Q';
        cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = true;

        check(i + 1, n);

        board[i][j] = '.';
        cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = false;
      }
    }
  }

  void add_solution(int n) {
    List<String> q = new ArrayList<>();
    for (int i = 0; i < n; i++) {
      StringBuilder cur = new StringBuilder();
      for (int j = 0; j < n; j++) {
        cur.append(board[i][j]);
      }
      q.add(cur.toString());
    }

    res.add(q);
  }
}
