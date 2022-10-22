from collections import deque, defaultdict
from typing import List, Optional
from math import floor, ceil, log2
from heapq import heapify, heappop, heappush
from functools import cache, lru_cache

import sys
sys.setrecursionlimit(10000)


class TreeNode:
  def __init__(self, x):
    self.val = x
    self.left = None
    self.right = None

def parseTree(v: List[int]):
  if not v: return None
  q = deque()
  root = TreeNode(v[0])
  q.append(root)
  i = 1
  N = len(v)
  while i < N:
    node = q.popleft()
    if v[i]:
      node.left = TreeNode(v[i])
      q.append(node.left)
    i += 1
    if i < N and v[i]:
      node.right = TreeNode(v[i])
      q.append(node.right)
    i += 1
  return root

def serializeTree(root: Optional[TreeNode]):
  if not root: return []
  res = []
  q = deque()
  q.append(root)

  while q:
    t = q.popleft()

    if not t:
      res.append(None)
    else:
      res.append(t.val)
      q.append(t.left)
      q.append(t.right)

  while res and not res[-1]:
    res.pop()

  return res
