from read_input import *
from local_stuff import *

inf = 1000000000


n, m = len(lines), len(lines[0])

directions = {
  '^': (-1, 0),
  'v': ( 1, 0),
  '<': (0, -1),
  '>': (0, 1)
}

blizzards = []

for i, line in enumerate(lines):
  for j, c in enumerate(line):
    if c in directions:
      blizzards.append((i, j, c))

print(n, m)
def to_map(input):
  mem = set()
  for p, q, _c in input:
    mem.add(p * 10000 + q)
  return mem

history = []
history_map = []
history.append(blizzards)
history_map.append(to_map(blizzards))
def blizzard_reach(t: int):
  while len(history) <= t:
    new_b = []
    for x, y, d in history[-1]:
      dx, dy = directions[d]
      a, b = x + dx, y + dy
      if d == 'v' and a == n - 1: a = 1
      if d == '^' and a == 0: a = n - 2
      if d == '>' and b == m - 1: b = 1
      if d == '<' and b == 0: b = m - 2

      # if a == 0 or b == 0 or a == n - 1 or b == m - 1:
      #   print(a, b, d)
      #   exit(1)
      new_b.append((a, b, d))

    history.append(new_b)
    history_map.append(to_map(new_b))

def has_blizzard(x, y, t):
  blizzard_reach(t)
  return (x * 10000 + y) in history_map[t]

s = set()
q = []

def add_to_q(x, y, d):
  key = x + y * 100000 + d * 10000000000
  if key in s: return
  s.add(key)
  heappush(q, (d, x, y))


# ======================================================================================
res = 100000
for i in range(1000):
  add_to_q(0, 1, i)

target = [n - 1, m - 2]
while q:
  d, x, y = heappop(q)

  d += 1
  if d >= res: continue
  # min_reach_dist = d + abs(x - target[0]) + abs(y - target[1]) - 1
  # if min_reach_dist >= res: continue

  for dx, dy in [(-1, 0), (1, 0), (0, 1), (0, -1)]:
    a, b = x + dx, y + dy
    if a == target[0] and b == target[1]:
      res = min(res, d)
      print('res', res)
      continue

    # print('ab', a, b, d)
    if a <= 0 or b <= 0 or a >= n - 1 or b >= m - 1: continue
    if not has_blizzard(a, b, d):
      add_to_q(a, b, d)

  if not has_blizzard(x, y, d):
    add_to_q(x, y, d)


# ======================================================================================
s.clear()
for i in range(1000):
  add_to_q(n - 1, m - 2, res + i)

res2 = 1000000
target = [0, 1]
while q:
  d, x, y = heappop(q)

  d += 1
  if d >= res2: continue
  # min_reach_dist = d + abs(x - target[0]) + abs(y - target[1]) - 1
  # if min_reach_dist >= res2: continue

  for dx, dy in [(-1, 0), (1, 0), (0, 1), (0, -1)]:
    a, b = x + dx, y + dy
    if a == target[0] and b == target[1]:
      res2 = min(res2, d)
      print('res2', res2)
      continue

    # print('ab', a, b, d)
    if a <= 0 or b <= 0 or a >= n - 1 or b >= m - 1: continue
    if not has_blizzard(a, b, d):
      add_to_q(a, b, d)

  if not has_blizzard(x, y, d):
    add_to_q(x, y, d)


# ======================================================================================
s.clear()
res3 = 1000000
for i in range(1000):
  add_to_q(0, 1, res2 + i)

target = [n - 1, m - 2]
while q:
  d, x, y = heappop(q)

  d += 1
  if d >= res3: continue
  # min_reach_dist = d + abs(x - target[0]) + abs(y - target[1]) - 1
  # if min_reach_dist >= res3: continue

  for dx, dy in [(-1, 0), (1, 0), (0, 1), (0, -1)]:
    a, b = x + dx, y + dy
    if a == target[0] and b == target[1]:
      res3 = min(res3, d)
      print('res3', res3)
      continue

    # print('ab', a, b, d)
    if a <= 0 or b <= 0 or a >= n - 1 or b >= m - 1: continue
    if not has_blizzard(a, b, d):
      add_to_q(a, b, d)

  if not has_blizzard(x, y, d):
    add_to_q(x, y, d)



print(res3)
