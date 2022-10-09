/**
 * @param {number} n
 * @return {string[][]}
 */
var solveNQueens = function (n) {
  const board = Array(n)
    .fill(0)
    .map(() => Array(n).fill("."));
  const cols = [],
    diag1 = [],
    diag2 = [];
  const res = [];

  const add_solution = () => {
    res.push(board.map((item) => item.join("")));
  };

  const solve = (i) => {
    if (i == n) {
      add_solution();
      return;
    }

    for (let j = 0; j < n; j++) {
      if (!cols[j] && !diag1[i + j] && !diag2[n - 1 + j - i]) {
        board[i][j] = "Q";
        cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = true;

        solve(i + 1);

        board[i][j] = ".";
        cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = false;
      }
    }
  };

  solve(0);
  return res;
};
