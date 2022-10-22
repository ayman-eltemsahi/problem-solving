from local_stuff import *


class Solution:
  def buildTree(self, inorder: List[int], postorder: List[int]) -> Optional[TreeNode]:

    def solve(i, j):
      if i > j: return None
      val = postorder.pop()
      if i == j: return TreeNode(val)

      c = i
      while inorder[c] != val:
        c += 1

      node = TreeNode(val)
      node.right = solve(c + 1, j)
      node.left = solve(i, c - 1)

      return node

    return solve(0, len(inorder) - 1)


inorder = [9,3,15,20,7]
postorder = [9,15,7,20,3]

t = Solution().buildTree(inorder, postorder)
print(serializeTree(t))
