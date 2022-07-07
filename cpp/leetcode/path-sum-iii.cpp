#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <unordered_map>

typedef long long int ll;
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

void traverse_sum(TreeNode *node, ll sum, ll &result) {
  if (node == nullptr) return;
  if (node->val == sum) result++;
  sum -= node->val;

  traverse_sum(node->left, sum, result);
  traverse_sum(node->right, sum, result);
}

void traverse(TreeNode *node, ll sum, ll &result) {
  if (node == nullptr) return;

  traverse(node->left, sum, result);
  traverse(node->right, sum, result);

  traverse_sum(node, sum, result);
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
