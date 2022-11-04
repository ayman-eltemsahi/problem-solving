from local_stuff import *


class Solution:
  def minCost(self, grid: List[List[int]]) -> int:
    n, m = len(grid), len(grid[0])

    dist = [[100000] * m for _ in range(n)]
    dist[-1][-1] = 0

    q = [(0, n - 1, m - 1)]

    while q:
      _, i, j = heappop(q)
      cost = dist[i][j]

      for ind, (dx, dy) in enumerate([(0, -1), (0, 1), (-1, 0), (1, 0)]):
        x, y = i + dx, j + dy
        if not (0 <= x < n and 0 <= y < m): continue
        new_cost = cost if ind == grid[x][y] - 1 else cost + 1
        if dist[x][y] > new_cost:
          dist[x][y] = new_cost
          heappush(q, (new_cost, x, y))


    return dist[0][0]

