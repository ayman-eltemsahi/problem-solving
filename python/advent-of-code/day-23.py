from read_input import *
from local_stuff import *


directions = [
  [(-1, -1), (-1, 0), (-1, 1)],
  [( 1, -1), ( 1, 0), ( 1, 1)],
  [(-1, -1), ( 0,-1), ( 1,-1)],
  [(-1,  1), ( 0, 1), ( 1, 1)],
]


grid = {}

def add(x, y):
  if x not in grid:
    grid[x] = {}
  grid[x][y] = True

def isEmpty(x, y):
  return x not in grid or y not in grid[x] or not grid[x][y]

def needsToMove(x, y):
  for i in [-1, 0, 1]:
    for j in [-1, 0, 1]:
      if (i != 0 or j != 0) and (x + i in grid) and (y + j in grid[x + i]):
        return True

  return False

def nextLocation(x, y, di):
  for i in range(4):
    ds = directions[(di + i) % 4]

    flag = True
    for dx, dy in ds:
      if not isEmpty(dx + x, dy + y):
        flag = False
        break

    if flag: return (ds[1][0] + x, ds[1][1] + y)

  return (x, y)

elves = []

for i, line in enumerate(lines):
  for j, c in enumerate(line):
    if c == '#':
      add(i, j)
      elves.append([i, j])


newPositions = {}

elves_count = len(elves)
new_locations = [None] * elves_count

di = 0
done = False
res = 0
while not done:
  res += 1
  if res % 1000 == 0: print(res)
  done = True

  for ei, (x, y) in enumerate(elves):
    if not needsToMove(x, y):
      new_locations[ei] = None
      continue

    new_x, new_y = nextLocation(x, y, di)
    # print((new_x, new_y), (x, y))
    new_locations[ei] = [new_x, new_y]
    if new_x == x and new_y == y:
      new_locations[ei] = None
      continue

    if new_x not in newPositions: newPositions[new_x] = defaultdict(int)
    newPositions[new_x][new_y] += 1

  for ei in range(elves_count):
    if new_locations[ei] is None: continue

    x, y = elves[ei]
    new_x, new_y = new_locations[ei]
    if newPositions[new_x][new_y] == 1:
      done = False
      del grid[x][y]
      elves[ei] = new_locations[ei]
      add(new_locations[ei][0], new_locations[ei][1])


  di += 1
  di %= 4
  newPositions = {}

print(res)

# min_x, min_y = 1000000000, 1000000000
# max_x, max_y = -min_x, -min_y
# for x, y in elves:
#   min_x = min(min_x, x)
#   min_y = min(min_y, y)
#   max_x = max(max_x, x)
#   max_y = max(max_y, y)


# print((max_x - min_x + 1), (max_y - min_y + 1))
# totalSize = (max_x - min_x + 1) * (max_y - min_y + 1)
# print(totalSize - elves_count)
