#pragma once
#include <vector>
#include <string>
#include <queue>

namespace tree_utils {

using std::queue;
using std::string;
using std::to_string;
using std::vector;

const auto nil = "null";

struct TreeNode {
  int val;
  TreeNode* left;
  TreeNode* right;
  TreeNode() : val(0), left(nullptr), right(nullptr) {}
  TreeNode(int x) : val(x), left(nullptr), right(nullptr) {}
  TreeNode(int x, TreeNode* left, TreeNode* right) : val(x), left(left), right(right) {}
};

vector<string> serialize(TreeNode* root) {
  if (!root) return {};
  vector<string> res;
  queue<TreeNode*> q;
  q.push(root);

  while (!q.empty()) {
    auto t = q.front();
    q.pop();

    if (t == nullptr) {
      res.push_back(nil);
    } else {
      res.push_back(to_string(t->val));
      q.push(t->left);
      q.push(t->right);
    }
  }

  while (!res.empty() && res.back() == nil) res.pop_back();
  return res;
}

TreeNode* deserialize(string str) {
  auto v = utils::read_vector_string(str);
  if (v.empty()) return NULL;
  queue<TreeNode*> q;
  auto root = new TreeNode(stoi(v[0]));
  q.push(root);
  for (int i = 1, N = v.size(); i < N;) {
    auto node = q.front();
    q.pop();
    if (v[i] != nil) q.push(node->left = new TreeNode(stoi(v[i])));
    ++i;
    if (i < N && v[i] != nil) q.push(node->right = new TreeNode(stoi(v[i])));
    ++i;
  }
  return root;
}

}  // namespace tree_utils
