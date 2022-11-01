from local_stuff import *


class Reader:
  def __init__(self, s: str):
    self.value = s
    self.index = 0

  def read_num(self):
    r = 0
    while self.index < len(self.value) and self.value[self.index].isdigit():
      r = r * 10 + (ord(self.value[self.index]) - ord('0'))
      self.index += 1

    return r

  def is_num(self):
    return self.value[self.index].isdigit()

  def read_char(self):
    self.index += 1
    return self.value[self.index - 1]

  def peek(self):
    return self.value[self.index]

  def has_next(self) -> bool:
    return self.index < len(self.value)

class Solution:
  def eval_stk(self, stk):
    res = stk[0]
    for i in range(1, len(stk), 2):
      if stk[i] == '+':
        res += stk[i + 1]
      else:
        res -= stk[i + 1]

    return res

  def solve(self, reader: Reader) -> int:
    stk = []

    while reader.has_next():
      c = reader.peek()
      if c == ')':
        reader.read_char()
        return self.eval_stk(stk)

      if c == ' ':
        reader.read_char()
        continue

      if c in ['*', '/', '+', '-']:
        stk.append(reader.read_char())
      else:
        if c == '(': reader.read_char()
        b = self.solve(reader) if c == '(' else reader.read_num()

        if stk and (stk[-1] == '/' or stk[-1] == '*'):
          op = stk.pop()
          a = stk.pop()
          stk.append((a * b) if op == '*' else int(a / b))
        else:
          stk.append(b)

    return self.eval_stk(stk)


  def calculate(self, s: str) -> int:
    return self.solve(Reader(s))

