#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Solution {
 public:
  TreeNode* solve(int i, int j, vector<int>& inorder, vector<int>& postorder) {
    if (i > j) return nullptr;

    int val = postorder.back();
    postorder.pop_back();
    if (i == j) return new TreeNode(val);

    int c = i;
    while (inorder[c] != val) ++c;

    auto node = new TreeNode(val);
    node->right = solve(c + 1, j, inorder, postorder);
    node->left = solve(i, c - 1, inorder, postorder);

    return node;
  }

  TreeNode* buildTree(vector<int>& inorder, vector<int>& postorder) {
    return solve(0, inorder.size() - 1, inorder, postorder);
  }
};
