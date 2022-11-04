from local_stuff import *


# https://www.hackerearth.com/practice/algorithms/graphs/articulation-points-and-bridges/tutorial/
class Solution:
  def criticalConnections(self, n: int, connections: List[List[int]]) -> List[List[int]]:
    graph = [[] for _ in range(n)]
    for a, b in connections:
      graph[a].append(b)
      graph[b].append(a)

    visited = [False] * n
    disc = [-1] * n
    low = [-1] * n
    parent = [-1] * n

    res = []
    def dfs(vertex, time):
      visited[vertex] = True
      disc[vertex] = low[vertex] = time + 1
      child = 0

      for i in graph[vertex]:
        if not visited[i]:
          child += 1
          parent[i] = vertex
          dfs(i, time + 1)
          low[vertex] = min(low[vertex], low[i])
          if low[i] > disc[vertex]:
            res.append([vertex, i])
        elif parent[vertex] != i:
          low[vertex] = min(low[vertex], disc[i])

    dfs(0, 0)
    return res
