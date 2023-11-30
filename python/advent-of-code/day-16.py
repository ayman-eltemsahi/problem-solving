from read_input import *
from local_stuff import *


n = len(lines)
flow_rates = []
graph = []
indices = {}

for i, line in enumerate(lines):
  valve, rate, items = line.split('#')
  rate = int(rate)
  indices[valve] = i
  flow_rates.append(rate)
  graph.append(items.split(', '))

for i in range(n):
  for j in range(len(graph[i])):
    graph[i][j] = indices[graph[i][j]]

print(flow_rates)
print(graph)
print('\n\n')

@cache
def check(a, b, t1, t2, mask, rem):
  if t1 < -1 or t2 < -1: return 0
  if not rem or (t1 <= 1 and t2 <= 1): return 0
  if a > b:
    return check(b, a, t2, t1, mask, rem)

  # open a
  r3 = 0
  if t1 > 1 and flow_rates[a] > 0 and ((mask >> a) & 1) == 0:
    for x in graph[a]:
      r3 = max(r3, check(x, b, t1 - 2, t2, mask | (1 << a), rem - 1))
    r3 += (flow_rates[a] * (t1 - 1))

  # open b
  r4 = 0
  if t2 > 1 and flow_rates[b] > 0 and ((mask >> b) & 1) == 0:
    for y in graph[b]:
      r4 = max(r4, check(a, y, t1, t2 - 2, mask | (1 << b), rem - 1))
    r4 += (flow_rates[b] * (t2 - 1))

  # open nothing
  r1 = 0
  for x in graph[a]:
    r1 = max(r1, check(x, b, t1 - 1, t2, mask, rem))

  r5 = 0
  for y in graph[b]:
    r5 = max(r5, check(a, y, t1, t2 - 1, mask, rem))

  return max(r1, r3, r4, r5)


res = check(indices['AA'], indices['AA'], 26, 26, 0, 15)
print(res)

