from read_input import *
from local_stuff import *

val = { '1': 1, '0': 0, '2': 2, '-': -1, '=': -2 }
rev_val = { 1: '1', 0: '0', 2: '2', -1: '-', -2: '=' }

def to_decimal(num):
  m = 1
  ans = 0
  for c in reversed(num):
    ans += m * val[c]
    m *= 5

  return ans

def to_snafu(n):
  ans = ''
  while n:
    g = n % 5
    if g >= 3: g -= 5

    ans = rev_val[g] + ans

    n -= g
    n //= 5

  if ans == '': ans = '0'
  return ans



s = sum([to_decimal(line) for line in lines])
print(s)
print(to_snafu(s))
