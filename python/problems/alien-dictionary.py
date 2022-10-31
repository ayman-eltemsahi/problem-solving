from local_stuff import *


class Solution:
  def alienOrder(self, words: List[str]) -> str:
    graph = [set() for _ in range(26)]
    chars = set()
    for word in words:
      for c in word: chars.add(c)

    for i, word in enumerate(words):
      for other in words[i:]:
        found = False
        for k in range(min(len(other), len(word))):
          if other[k] != word[k]:
            a, b = word[k], other[k]
            graph[ord(a) - ord('a')].add(b)
            found = True
            break

        if not found and len(word) > len(other): return ''


    seen = [False] * 26
    has_cycle = [None] * 26
    def cycle(c):
      k = ord(c) - ord('a')
      if has_cycle[k] != None: return has_cycle[k]
      if seen[k]:
        has_cycle[k] = True
        return True
      seen[k] = True
      has_cycle[k] = any([cycle(e) for e in graph[k]])
      seen[k] = False
      return has_cycle[k]

    seen = [False] * 26
    if any([cycle(c) for c in chars]): return ''

    stk = []
    def topological_sort(c):
      k = ord(c) - ord('a')
      if seen[k]: return
      seen[k] = True

      for e in graph[k]: topological_sort(e)
      stk.append(c)

    seen = [False] * 26
    for c in chars:
      topological_sort(c)

    return ''.join(reversed(stk))

