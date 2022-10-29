from local_stuff import *


class TicTacToe:

  def __init__(self, n: int):
    self.n = n
    self.rows = [[0] * n, [0] * n]
    self.cols = [[0] * n, [0] * n]
    self.diag1 = [0, 0]
    self.diag2 = [0, 0]

  def move(self, row: int, col: int, player: int) -> int:
    player -= 1

    self.rows[player][row] += 1
    self.cols[player][col] += 1
    if self.rows[player][row] == self.n or self.cols[player][col] == self.n:
      return player + 1

    if row == col: self.diag1[player] += 1
    if row == self.n - col - 1: self.diag2[player] += 1
    if self.diag1[player] == self.n or self.diag2[player] == self.n:
      return player + 1

    return 0


# class TicTacToe:

#   def __init__(self, n: int):
#     self.n = n
#     self.game = [[0] * n for _ in range(n)]

#   def move(self, row: int, col: int, player: int) -> int:
#     self.game[row][col] = player

#     for i in range(self.n):
#       if all([self.game[i][j] == self.game[i][0] for j in range(self.n)]):
#         return self.game[i][0]

#       if all([self.game[j][i] == self.game[0][i] for j in range(self.n)]):
#         return self.game[0][i]

#     if all([self.game[j][j] == self.game[0][0] for j in range(self.n)]):
#       return self.game[0][0]

#     if all([self.game[self.n - j - 1][j] == self.game[self.n - 1][0] for j in range(self.n)]):
#       return self.game[self.n - 1][0]

#     return 0
