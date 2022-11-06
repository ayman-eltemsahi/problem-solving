from local_stuff import *

class UnionFind:
  def __init__(self, n):
    self.parent = list(range(0, n))

  def find(self, x):
    if self.parent[x] == x:
      return x
    self.parent[x] = self.find(self.parent[x])
    return self.parent[x]

  def connect(self, x, y):
    rootX, rootY = self.find(x), self.find(y)
    self.parent[rootX] = rootY

    return rootY

class Solution:
  def minimumCost(self, n: int, connections: List[List[int]]) -> int:
    uf = UnionFind(n + 1)
    res = 0
    for a, b, cost in sorted(connections, key=lambda x: x[2]):
      if uf.find(a) != uf.find(b):
        uf.connect(a, b)
        res += cost

    for i in range(1, n + 1):
      if uf.find(i) != uf.find(1): return -1

    return res
