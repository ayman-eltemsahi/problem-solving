from collections import deque
from typing import List
from heapq import heapify, heappop, heappush
from functools import cache, lru_cache

import sys
sys.setrecursionlimit(10000)


class TreeNode:
  def __init__(self, x):
    self.val = x
    self.left = None
    self.right = None
