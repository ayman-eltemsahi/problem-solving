class Solution {
  int x = 0;

  public TreeNode buildTree(int[] inorder, int[] postorder) {
    x = postorder.length - 1;
    return Solve(0, inorder.length - 1, inorder, postorder);
  }

  TreeNode Solve(int i, int j, int[] inorder, int[] postorder) {
    if (i > j)
      return null;

    int val = postorder[x];
    x -= 1;
    if (i == j) {
      return new TreeNode(val);
    }

    int c = i;
    while (inorder[c] != val)
      ++c;

    var node = new TreeNode(val);
    node.right = Solve(c + 1, j, inorder, postorder);
    node.left = Solve(i, c - 1, inorder, postorder);

    return node;
  }
}
