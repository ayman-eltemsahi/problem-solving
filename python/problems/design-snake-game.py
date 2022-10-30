from local_stuff import *


steps = {'D': (1, 0),'U': (-1, 0),'R': (0, 1),'L': (0, -1)}

class SnakeGame:

  def __init__(self, width: int, height: int, food: List[List[int]]):
    self.width = width
    self.height = height
    self.food = list(reversed(food))
    self.score = 1
    self.dir = None
    self.snake = deque()
    self.snake.append([0, 0])

  def step(self, d):
    dx, dy = steps[d]
    x, y = self.snake[-1]
    self.snake.append([x + dx, y + dy])

  def valid_pos(self):
    return 0 <= self.snake[-1][0] < self.height and 0 <= self.snake[-1][1] < self.width

  def self_bite(self):
    for i in range(len(self.snake) - 1):
      if self.snake[i] == self.snake[-1]:
        return True

    return False

  def eat(self):
    if not self.food: return
    if self.food[-1] == self.snake[-1]:
      self.food.pop()
      return True
    return False

  def move(self, direction: str) -> int:
    self.step(direction)
    if not self.valid_pos(): return -1
    if self.eat():
      self.score += 1
    else:
      self.snake.popleft()

    if self.self_bite():
      return -1

    return self.score - 1
