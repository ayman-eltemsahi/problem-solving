from read_input import *
from local_stuff import *

beacons = []

max_dist = 0
n = len(lines)
for line in lines:
  a, b, c, d = map(int, line.split(' '))
  dist = abs(a - c) + abs(b - d)
  beacons.append((a, b, c, d, dist))
  max_dist = max(max_dist, dist)


upper_bound = 4000000
# upper_bound = 20

def is_blocked(x, y):
  for a, b, c, d, dist in beacons:
    # if c == x and d == y: return True
    p_dist = abs(a - x) + abs(b - y)
    if p_dist <= dist: return True
  return False

def jump(x, y):
  z = y + 1
  for a, b, c, d, dist in beacons:
    # if c == p and d == q: return True
    p_dist = abs(a - x) + abs(b - y)
    if p_dist <= dist:
      g = dist - abs(a - x)
      # z = g + b
      z = max(z, g + b)
  return z

res = 0
for i in range(0, upper_bound + 1):
  if i % 10000 == 0: print('i:', i)
  j = 0
  while j <= upper_bound:
    if not is_blocked(i, j):
      print(i, j)
      res = i * 4000000 + j
      print(res)
      exit(0)

    j = jump(i, j)


print(res)
