from local_stuff import *


class Solution:
  def key(self, board: List[List[int]]) -> int:
    r = 0
    for i in [0, 1]:
      for j in [0, 1, 2]:
        r = r * 10 + board[i][j]

    return r

  def get_candidates(self, board: List[List[int]]):
    ans = []
    for i in [0, 1]:
      for j in [0, 1, 2]:
        if board[i][j] == 0:
          for dx, dy in [(1, 0), (-1, 0), (0, 1), (0, -1)]:
            x, y = i + dx, j + dy
            if x >= 2 or x < 0 or y >= 3 or y < 0: continue
            cpy = [board[0].copy(), board[1].copy()]
            cpy[i][j] = cpy[x][y]
            cpy[x][y] = 0
            ans.append(cpy)
          break

    return ans

  def slidingPuzzle(self, board: List[List[int]]) -> int:
    res = 0
    q = [board]

    start = self.key(board)
    target = self.key([[1,2,3],[4,5,0]])
    if start == target: return 0
    seen = set([start])

    while q:
      q2 = q
      q = []
      res += 1
      for b in q2:
        for candidate in self.get_candidates(b):
          k = self.key(candidate)
          if k == target: return res
          if k not in seen:
            seen.add(k)
            q.append(candidate)

    return -1

