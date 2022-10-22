def solve(i, j, inorder, postorder)
  return nil if i > j

  val = postorder.pop
  return TreeNode.new(val) if i == j

  c = i
  c += 1 while inorder[c] != val

  node = TreeNode.new(val)
  node.right = solve(c + 1, j, inorder, postorder)
  node.left = solve(i, c - 1, inorder, postorder)

  node
end

def build_tree(inorder, postorder)
  solve(0, inorder.length - 1, inorder, postorder)
end
