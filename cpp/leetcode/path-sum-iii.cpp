#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <unordered_map>

#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

struct TreeNode {
  int val;
  TreeNode *left;
  TreeNode *right;
  TreeNode() : val(0), left(nullptr), right(nullptr) {
  }
  TreeNode(int x) : val(x), left(nullptr), right(nullptr) {
  }
  TreeNode(int x, TreeNode *left, TreeNode *right) : val(x), left(left), right(right) {
  }
};

typedef long long int ll;
std::unordered_map<ll, ll> traverse(TreeNode *node, int sum, ll &result) {
  if (node == nullptr) return {};
  const auto left = traverse(node->left, sum, result);
  const auto right = traverse(node->right, sum, result);

  std::unordered_map<ll, ll> m;
  for (const auto kv : left) {
    const ll val = kv.first + node->val;
    m[val] += kv.second;
    if (val == sum) result += m[val];
  }

  for (const auto kv : right) {
    const ll val = kv.first + node->val;
    m[val] += kv.second;
    if (val == sum) result += kv.second;
  }

  m[node->val]++;
  if (node->val == sum) result++;
  return m;
}

class Solution {
 public:
  int pathSum(TreeNode *root, int targetSum) {
    ll result = 0;
    traverse(root, targetSum, result);
    return result;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto root = new TreeNode(10,
                           new TreeNode(5, new TreeNode(3, new TreeNode(3), new TreeNode(-2)),
                                        new TreeNode(2, nullptr, new TreeNode(1))),
                           new TreeNode(-3, nullptr, new TreeNode(11)));

  LOG(s.pathSum(root, 8));
}
#endif
