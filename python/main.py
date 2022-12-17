from local_stuff import *


class Solution:
  def numberOfSubstrings(self, s: str) -> int:
    l = 0
    n = len(s)
    a, b, c, = 0, 0, 0

    res = 0
    for i in range(n):
      ch = s[i]
      if ch == 'a': a += 1
      elif ch== 'b': b += 1
      else: c += 1

      while l < i :
        ch = s[l]
        if ch == 'a' and a < 2: break
        if ch == 'b' and b < 2: break
        if ch == 'c' and c < 2: break

        if ch == 'a': a -= 1
        elif ch== 'b': b -= 1
        else: c -= 1
        l += 1

      if a and b and c: res += l + 1

    return res
