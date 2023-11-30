from read_input import *
from local_stuff import *

inf = 1000000000

instructions = lines[-1]
lines.pop()
lines.pop()

class Transition:
  def __init__(self, fr, to, op='%'):
    self.fr = fr
    self.to = to
    self.op = op

directions = {}
directions_cost = {}
directions['R'] = (0, 1)
directions['L'] = (0, -1)
directions['D'] = (1, 0)
directions['U'] = (-1, 0)
directions_cost['R'] = 0
directions_cost['D'] = 1
directions_cost['L'] = 2
directions_cost['U'] = 3
original_position = [{}, {}, {}, {}, {}, {}]


"""
  1 0
  2
4 3
5


r -> r, c -> c
  r = 0
  r = n - 1
  c = 0
  c = n -1

r -> n - r - 1
r -> n - c - 1

c -> n - c - 1
c -> n - r - 1


   1
   2
   3

0, c  : 0     , 'col'   vertical, from 0
r, 0  : 'row' , 0       horizontal, from 0



connection: 1 -> 2
  D -> 0, c

connection: 1 -> 0
  D -> r, 0

connection: 1 -> 4
  L -> n - r - 1, 0
    rotated twice -> r&r, c&c
    L -> R,   horizontal reverse, from n - 0 - 1

connection: 2 -> 4
  L -> 0, r
    rotated once -> r&c, c&r
    L -> D,   vertical, from 0

connection: 1 -> 5
  U -> c, 0
    rotated thrice -> r&c, c&r
    U -> R,   horizontal, from 0


"""

class CubeDescription:
  def __init__(self, grids: list[list[list[list[str]]]], grid_orientation: list[list[bool]], center: tuple[int, int], side_len: int):
    self.grids = grids
    self.grid_orientation = grid_orientation
    self.center = center
    self.side_len = side_len


class CubConnections:
  def __init__(self, up = None, down = None, left = None, right = None):
    # self.connections = [
    #   # [dx, dy, next_grid, next_dir]
    #   # up
    #   [-0.5,  0, -1, -1],
    #   # down
    #   [ 0.5,  0, -1, -1],
    #   # left
    #   [ 0, -0.5, -1, -1],
    #   # right
    #   [ 0,  0.5, -1, -1],
    # ]
    self.up = up
    self.down = down
    self.left = left
    self.right = right


def opposite_connection(k: int):
  if k == 0: return 1
  if k == 1: return 0
  if k == 2: return 3
  if k == 3: return 2


def read_sides(lines: list[str]) -> CubeDescription:
  n, m = len(lines), len(lines[0])
  side_len = max(n, m) // 4

  grid_orientation = [[False] * 4 for _ in range(4)]
  grids = [[None] * 4 for _ in range(4)]

  for i in range(4):
    for j in range(4):
      start_row, start_col = side_len * i, side_len * j
      if start_row >= n or start_col >= len(lines[start_row]) or lines[start_row][start_col] == ' ': continue

      grid_orientation[i][j] = True
      grid = [['.'] * side_len for _ in range(side_len)]
      for x in range(side_len):
        for y in range(side_len):
          grid[x][y] = lines[x + start_row][y + start_col]

      grids[i][j] = grid

  possible_centers = [(1, 1), (1, 2), (2, 1), (2, 2)]
  center = possible_centers[0]
  for a, b in possible_centers:
    if not grid_orientation[a][b]: continue
    if (grid_orientation[a - 1][b] and grid_orientation[a + 1][b]) or (grid_orientation[a][b - 1] and grid_orientation[a][b + 1]):
      center = (a, b)
      break

  print('grid orientation')
  for o in grid_orientation:
    print(''.join(map(lambda x: '█' if x else '_', o)))

  print('\ncenter:', center)

  return CubeDescription(grids, grid_orientation, center, side_len)



def process_adjacency(lines: list[str]):
  box = read_sides(lines)
  connections = [[None] * 4 for _ in range(4)]

  center = box.center

  grids = []
  for i in range(4):
    for j in range(4):
      if box.grids[i][j] is not None: grids.append((i, j))

  # sort nearer to center
  grids.sort(key=lambda g: abs(g[0] - center[0]) + abs(g[1] - center[1]))


  # define the connections for each grid
  connections = [CubConnections() for _ in range(len(grids))]

  # touching grids
  for i, (a, b) in enumerate(grids):
    for j, (x, y) in enumerate(grids):
      if i == j: continue
      diff = abs(a - x) + abs(b - y)
      if diff != 1: continue

      if a + 1 == x: connections[i].down  = (j, 'D')
      if a - 1 == x: connections[i].up    = (j, 'U')
      if b + 1 == y: connections[i].right = (j, 'R')
      if b - 1 == y: connections[i].left  = (j, 'L')


  # one fold
  for i, (a, b) in enumerate(grids):
    for j, (x, y) in enumerate(grids):
      if i == j: continue
      diff = abs(a - x) + abs(b - y)
      if diff != 2: continue

      if a + 1 == x: connections[i].down  = (j, 'D')
      if a - 1 == x: connections[i].up    = (j, 'U')
      if b + 1 == y: connections[i].right = (j, 'R')
      if b - 1 == y: connections[i].left  = (j, 'L')

  for distance in range(0, 2):
    for grid_a_i, (a, b) in enumerate(grids):

      for c1_i, conn in enumerate(connections[grid_a_i].connections):
        if conn[2] != -1: continue

        for grid_b_i, (x, y) in enumerate(grids):
          if grid_a_i == grid_b_i: continue
          if conn[2] != -1: break

          for c2_i, conn2 in enumerate(connections[grid_b_i].connections):
            if conn2[2] != -1: continue
            p, q = (a + conn[0]) - (x + conn2[0]), (b + conn[1]) - (y + conn2[1])
            if p == q: continue
            curr_distance = abs(p) + abs(q)
            if curr_distance != distance: continue

            conn[2] = grid_b_i
            conn[3] = c2_i

            conn2[2] = grid_a_i
            conn2[3] = opposite_connection(c1_i)
            break

  print(grids)
  print(connections[0].connections)






process_adjacency(lines)
exit(0)

