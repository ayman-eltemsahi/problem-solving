from read_input import *
from local_stuff import *


input = '>>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>'
n = len(input)

shapes = []
shapes.append([(0, 0), (1, 0), (2, 0), (3, 0)])
shapes.append([(0, 1), (1, 0), (1, 1), (1, 2), (2, 1)])
shapes.append([(0, 0), (1, 0), (2, 0), (2, 1), (2, 2)])
shapes.append([(0, 0), (0, 1), (0, 2), (0, 3)])
shapes.append([(0, 0), (0, 1), (1, 0), (1, 1)])


max_y = 0
x = 2
shape_i = 0
grid = [[False] * 10000 for _ in range(7)]

for _ in range(2022):
  rock = shapes[shape_i % len(shapes)]

  x, y = 2, max_y + 3
  locs = [(x + a, y + b) for a, b in rock]
  j = 0

  moved = True
  while moved:
    moved = False
    step = input[j % len(input)]
    j += 1

    # 1. jet
    if step == '>':
      locs.sort(key=lambda a, _: a)
      new_locs = []
      for a, b in locs:
        if a + 1 >= 7 or grid[a + 1][b]: break
        new_locs.append((a + 1, b))
    else:
      locs.sort(key=lambda a, _: -a)
      new_locs = []
      for a, b in locs:
        if a - 1 < 7 or grid[a - 1][b]: break
        new_locs.append((a - 1, b))

    if len(new_locs) == len(locs): locs = new_locs


    # 2. fall
    locs.sort(key=lambda _, b: b)
    new_locs = []
    for a, b in locs:
      if b - 1 < 0 or grid[a][b - 1]: break
      new_locs.append((a, b - 1))

    if len(new_locs) == len(locs):
      locs = new_locs
      moved = True
      for _, b in locs:
        max_y = max(max_y, b)

print(max_y)
