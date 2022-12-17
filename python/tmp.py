import time
import random
from read_input import *
from local_stuff import *

# for line in lines:
#   print(line)
print('\033c')
inf = 1000000000
n, m = len(lines), len(lines[0])

def printAt(x, y, *args):
  print("\033[%d;%dH" % (x, y), end='')
  print(*args, end='')

start = (0, 0)
target = (0, 0)
for i in range(n):
  for j in range(m):
    if lines[i][j] == 'S':
      start = (i, j)
    elif lines[i][j] == 'E':
      target = (i, j)

dist = [[inf] * m for _ in range(n)]
q = []
q.append((0, start[0], start[1]))
dist[start[0]][start[1]] = 0

while q:
  _, i, j = heappop(q)
  ch_c = ord('a' if lines[i][j] == 'S' else lines[i][j])

  ll = list(reversed([(1, 0), (-1, 0), (0, 1), (0, -1)]))
  random.shuffle(ll)
  random.shuffle(ll)
  for dx, dy in ll:
    x, y = i + dx, j + dy
    if x >= 0 and x < n and y >= 0 and y < m and dist[x][y] > dist[i][j] + 1:
      ch_d = ord('z' if lines[x][y] == 'E' else lines[x][y])

      if ch_d - ch_c == 1 or ch_d <= ch_c:
        dist[x][y] = dist[i][j] + 1
        heappush(q, (dist[x][y], x, y))


print(dist[target[0]][target[1]])


for i in range(n):
  for j in range(m):
    if lines[i][j] == 'E' or lines[i][j] == 'S':
      printAt(i + 5, j + 5, f'\033[32m\033[7m\033[1m{lines[i][j]}\033[0m')
    else: printAt(i + 5, j + 5, lines[i][j])

track = []
def backtrack(i, j):
  track.append((i, j))
  if 1 == dist[i][j]:
    return
  ch_c = ord('a' if lines[i][j] == 'S' else lines[i][j])

  ll = list(reversed([(1, 0), (-1, 0), (0, 1), (0, -1)]))
  # random.shuffle(ll)
  for dx, dy in ll:
    x, y = i + dx, j + dy
    if x >= 0 and x < n and y >= 0 and y < m and dist[x][y] == dist[i][j] - 1:
      ch_d = ord('z' if lines[x][y] == 'E' else lines[x][y])

      if ch_c - ch_d == 1 or ch_d >= ch_c:
        printAt(x + 5, y + 5, f'\033[32m\033[7m\033[1m{lines[x][y]}\033[0m')
        backtrack(x, y)
        printAt(x + 5, y + 5, lines[x][y])
        # sys.stdout.flush()
        # time.sleep(0.001)


@cache
def count_paths(i, j):
  if 1 == dist[i][j]: return 1
  ch_c = ord('a' if lines[i][j] == 'S' else lines[i][j])

  r = 0
  for dx, dy in [(1, 0), (-1, 0), (0, 1), (0, -1)]:
    x, y = i + dx, j + dy
    if x >= 0 and x < n and y >= 0 and y < m and dist[x][y] == dist[i][j] - 1:
      ch_d = ord('z' if lines[x][y] == 'E' else lines[x][y])

      if ch_c - ch_d == 1 or ch_d >= ch_c:
        r += count_paths(x, y)

  return r


backtrack(target[0], target[1])
paths_cnt = count_paths(target[0], target[1])
# print('paths_cnt', paths_cnt)
# exit(0)

track.reverse()
# print(track)
# exit(0)

# for i in range(n):
#   for j in range(m):
#     if lines[i][j] == 'E' or lines[i][j] == 'S':
#       printAt(i + 5, j + 5, f'\033[32m\033[7m\033[1m{lines[i][j]}\033[0m')
#     else: printAt(i + 5, j + 5, lines[i][j])

# for i, j in track:
#   if lines[i][j] == 'E' or lines[i][j] == 'S':
#     printAt(i + 5, j + 5, f'\033[32m\033[7m\033[1m{lines[i][j]}\033[0m')
#   else: printAt(i + 5, j + 5, f'\033[33m\033[7m\033[1m{lines[i][j]}\033[0m')
#   sys.stdout.flush()
#   time.sleep(0.001)



printAt(500, 0, '')
