class Solution:
  def solveNQueens(self, n: int) -> List[List[str]]:
    board = [['.'] * n for _ in range(n)]
    cols = [False] * 10
    diag1 = [False] * 20
    diag2 = [False] * 20
    res = []

    def add_solution():
      res.append([''.join(x) for x in board])

    def solve(i: int):
      if i == n:
        add_solution()
        return

      for j in range(n):
        if not cols[j] and not diag1[i + j] and not diag2[n - 1 + j - i]:
          board[i][j] = 'Q'
          cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = True

          solve(i + 1)

          board[i][j] = '.'
          cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = False

    solve(0)
    return res
