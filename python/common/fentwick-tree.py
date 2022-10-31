class FenwickTree:

  def __init__(self, n):
    self.size = n
    self.tree = [0] * (n + 1)


  def query(self, index: int) -> int:
    sum = 0
    index += 1

    while index > 0:
      sum += self.tree[index]
      index -= index & (-index)

    return sum


  def update(self, index: int, val: int) -> None:
    index += 1
    while index <= self.size:
      self.tree[index] += val
      index += index & (-index)
