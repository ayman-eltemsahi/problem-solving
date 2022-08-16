#pragma once
#include <vector>
#include <string>
#include <queue>

namespace tree_utils {

using std::queue;
using std::string;
using std::to_string;
using std::vector;

struct TreeNode {
  int val;
  TreeNode* left;
  TreeNode* right;
  TreeNode() : val(0), left(nullptr), right(nullptr) {}
  TreeNode(int x) : val(x), left(nullptr), right(nullptr) {}
  TreeNode(int x, TreeNode* left, TreeNode* right) : val(x), left(left), right(right) {}
};

vector<string> serialize_tree(TreeNode* root) {
  if (!root) return {};
  vector<string> res;
  queue<TreeNode*> q;
  q.push(root);

  while (!q.empty()) {
    auto t = q.front();
    q.pop();

    if (t == nullptr) {
      res.push_back("null");
    } else {
      res.push_back(to_string(t->val));
      q.push(t->left);
      q.push(t->right);
    }
  }

  while (!res.empty() && res.back() == "null") res.pop_back();
  return res;
}
}  // namespace tree_utils
