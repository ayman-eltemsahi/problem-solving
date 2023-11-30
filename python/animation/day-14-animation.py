import time
from read_input import *
from local_stuff import *

lines = [
  '498,4 -> 498,6 -> 496,6',
  '503,4 -> 502,4 -> 502,9 -> 494,9'
]

print('\033c')
grid = [[False] * 200 for _ in range(1200)]

def printAt(x, y, *args):
  x -= 480
  y += 2
  print("\033[%d;%dH" % (y, x), end='')
  print(*args, end='')

lowest = 0
for line in lines:
  parts = line.split(' -> ')
  for i in range(1, len(parts)):
    fr_a, fr_b = map(int, parts[i - 1].split(','))
    a, b = map(int, parts[i].split(','))
    grid[fr_a][fr_b] = grid[a][b] = True
    printAt(fr_a, fr_b, '█')
    printAt(a, b, '█')
    lowest = max(lowest, b, fr_b)

    dx = 0 if a == fr_a else (a - fr_a) // abs(a - fr_a)
    dy = 0 if b == fr_b else (b - fr_b) // abs(b - fr_b)

    while fr_a != a or fr_b != b:
      grid[fr_a][fr_b] = True
      printAt(fr_a, fr_b, '█')
      lowest = max(lowest, fr_b)
      fr_a += dx
      fr_b += dy

lowest += 2
for i in range(0, 1200):
  grid[i][lowest] = True
  if i > 480 and i < 520:
    printAt(i, lowest, '█')

printAt(500, 0, '+')

def drop():
  x, y = 500, 0
  if grid[x][y]: return False

  while True:
    prev = (x, y)
    pos = [(x, y + 1), (x - 1, y + 1), (x + 1, y + 1)]
    found = False
    for nx, ny in pos:
      if not grid[nx][ny]:
        x, y = nx, ny
        found = True
        break

    printAt(prev[0], prev[1], ' ')
    printAt(x, y, f'\033[32m\033[1mo\033[0m')
    printAt(500, 0, f'\033[34m\033[7m+\033[0m')
    sys.stdout.flush()
    time.sleep(0.0001)
    if not found:
      grid[x][y] = True
      printAt(x, y, f'\033[33m\033[1mo\033[0m')
      printAt(500, 0, f'\033[34m\033[7m+\033[0m')
      return True

res = 0
while drop():
  res += 1

printAt(490, 20, '\n')
# print(res)
