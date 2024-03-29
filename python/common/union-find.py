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

class UnionFindRank:
  def __init__(self, n):
    self.parent = list(range(0, n))
    self.rank = [1] * n

  def find(self, x):
    if self.parent[x] == x:
      return x
    self.parent[x] = self.find(self.parent[x])
    return self.parent[x]

  def connect(self, x, y):
    rootX, rootY = self.find(x), self.find(y)

    if rootX == rootY:
      return rootX
    if self.rank[rootX] < self.rank[rootY]:
      self.parent[rootX] = rootY
      self.rank[rootY] += self.rank[rootX]
      return rootY
    else:
      self.parent[rootY] = rootX
      self.rank[rootX] += self.rank[rootY]
      return rootX
