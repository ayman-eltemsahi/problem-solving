#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Solution {
 public:
  vector<vector<string>> res;
  vector<vector<char>> board;
  vector<bool> cols, diag1, diag2;

  void add_solution(int n) {
    vector<string> q(n);
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < n; j++) {
        q[i].push_back(board[i][j]);
      }
    }

    res.push_back(q);
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

  vector<vector<string>> solveNQueens(int n) {
    board.resize(n, vector<char>(n, '.'));
    cols.resize(10);
    diag1.resize(20);
    diag2.resize(20);

    check(0, n);

    return res;
  }
};
