from read_input import *
from local_stuff import *

inf = 1000000000

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


# def transition(r, c, gi, dir):
#   if gi == 0:
#     if dir == 'D': return (0, c, 3, 'D')
#     if dir == 'U': return (0, n - c - 1, 1, 'D')
#     if dir == 'L': return (0, r, 2, 'D')
#     if dir == 'R': return (n - r - 1, n - 1, 5, 'L')

#   if gi == 1:
#     if dir == 'R': return (r, 0, 2, 'R')
#     if dir == 'L': return (n - 1, n - r - 1, 5, 'U')
#     if dir == 'U': return (0, n - c - 1, 0, 'D')
#     if dir == 'D': return (n - 1, n - c - 1, 4, 'U')

#   if gi == 2:
#     if dir == 'R': return (r, 0, 3, 'R')
#     if dir == 'L': return (r, n - 1, 1, 'L')
#     if dir == 'U': return (c, 0, 0, 'R')
#     if dir == 'D': return (n - c - 1, 0, 4, 'R')

#   if gi == 3:
#     if dir == 'R': return (0, n - r - 1, 5, 'D')
#     if dir == 'L': return (r, n - 1, 2, 'L')
#     if dir == 'U': return (n - 1, c, 0, 'U')
#     if dir == 'D': return (0, c, 4, 'D')

#   if gi == 4:
#     if dir == 'R': return (r, 0, 5, 'R')
#     if dir == 'L': return (n - 1, n - r - 1, 2, 'U')
#     if dir == 'U': return (n - 1, c, 3, 'U')
#     if dir == 'D': return (n - 1, n - c - 1, 1, 'U')

#   if gi == 5:
#     if dir == 'R': return (n - r - 1, n - 1, 0, 'L')
#     if dir == 'L': return (r, n - 1, 4, 'L')
#     if dir == 'U': return (n - c - 1, n - 1, 3, 'L')
#     if dir == 'D': return (n - c - 1, 0, 1, 'R')


def transition(r, c, gi, dir):
  if gi == 0:
    if dir == 'D': return (c, n -1, 2, 'L')
    if dir == 'U': return (n - 1, c, 5, 'U')
    if dir == 'L': return (r, n - 1, 1, 'L')
    if dir == 'R': return (n - r - 1, n - 1, 3, 'L')

  if gi == 1:
    if dir == 'R': return (r, 0, 0, 'R') #
    if dir == 'L': return (n - r - 1, 0, 4, 'R')
    if dir == 'U': return (c, 0, 5, 'R')
    if dir == 'D': return (0, c, 2, 'D')

  if gi == 2:
    if dir == 'R': return (n - 1, r, 0, 'U') #
    if dir == 'L': return (0, r, 4, 'D')
    if dir == 'U': return (n - 1, c, 1, 'U') #
    if dir == 'D': return (0, c, 3, 'D')

  if gi == 3:
    if dir == 'R': return (n - r - 1, n - 1, 0, 'L') #
    if dir == 'L': return (r, n - 1, 4, 'L')
    if dir == 'U': return (n - 1, c, 2, 'U') #
    if dir == 'D': return (c, n - 1, 5, 'L')

  if gi == 4:
    if dir == 'R': return (r, 0, 3, 'R') #
    if dir == 'L': return (n - r - 1, 0, 1, 'R') #
    if dir == 'U': return (c, 0, 2, 'R') #
    if dir == 'D': return (0, c, 5, 'D')

  if gi == 5:
    if dir == 'R': return (n - 1, r, 3, 'U')
    if dir == 'L': return (0, r, 1, 'D') #
    if dir == 'U': return (n - 1, c, 4, 'U')
    if dir == 'D': return (0, c, 0, 'D') #


n = len(lines[-3]) // 4
n = 50

grids = [
  [['.'] * n for _ in range(n)],
  [['.'] * n for _ in range(n)],
  [['.'] * n for _ in range(n)],
  [['.'] * n for _ in range(n)],
  [['.'] * n for _ in range(n)],
  [['.'] * n for _ in range(n)],
]

def read(r, c, gi):
  for i in range(n):
    for j in range(n):
      grids[gi][i][j] = lines[i + r][j + c]
      original_position[gi][i * 1000000 + j] = (i + r, j + c)

# read(0, n * 2, 0)
# read(n, 0, 1)
# read(n, n, 2)
# read(n, n * 2, 3)
# read(n * 2, n * 2, 4)
# read(n * 2, n * 3, 5)

read(0, n * 2, 0)
read(0, n, 1)
read(n, n, 2)
read(n * 2, n, 3)
read(n * 2, 0, 4)
read(n * 3, 0, 5)



def turn(dir: str, lett):
  if lett == 'R':
    if dir == 'R': return 'D'
    if dir == 'D': return 'L'
    if dir == 'L': return 'U'
    if dir == 'U': return 'R'
  if lett == 'L':
    if dir == 'R': return 'U'
    if dir == 'U': return 'L'
    if dir == 'L': return 'D'
    if dir == 'D': return 'R'

def get_next(r, c, dir, cnt, gi):

  while cnt > 0:
    cnt -= 1
    dx, dy = directions[dir]
    # print((r, c), gi, dir)

    n_r, n_c = r + dx, c + dy
    if n_r >= n or n_r < 0 or n_c >= n or n_c < 0:
      n_r, n_c, tmp_gi, tmp_dir = transition(r, c, gi, dir)
      if grids[tmp_gi][n_r][n_c] == '#':
        return r, c, gi, dir
      r = n_r
      c = n_c
      gi = tmp_gi
      dir = tmp_dir

    else:
      if grids[gi][n_r][n_c] == '#':
        return r, c, gi, dir
      r, c = n_r, n_c

  return r, c, gi, dir


gi, r, c, dir = 1, 0, 0, 'R'
i = 0
inst = lines[-1]
while i < len(inst):
  if inst[i] == 'R' or inst[i] == 'L':
    # print('inst:', inst[i])
    dir = turn(dir, inst[i])
    i += 1
    continue

  cnt = 0
  while i < len(inst) and inst[i] != 'R' and inst[i] != 'L':
    cnt = cnt * 10 + ord(inst[i]) - ord('0')
    i += 1

  # move
  # print('inst:', cnt)
  r, c, tmp_gi, dir = get_next(r, c, dir, cnt, gi)
  gi = tmp_gi
  # print((r, c), gi, dir)


orig_r, orig_c = original_position[gi][r * 1000000 + c]
print('Done at', r, c)
print((orig_r + 1) * 1000 + (orig_c + 1) * 4 + directions_cost[dir])
