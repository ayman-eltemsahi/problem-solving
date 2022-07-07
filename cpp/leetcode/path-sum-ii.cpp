#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

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

void traverse(TreeNode *node, std::vector<std::vector<int>> &res, std::vector<int> &curr,
              int remaining) {
  if (node == nullptr) return;

  remaining -= node->val;
  curr.push_back(node->val);

  if (remaining == 0 && node->left == nullptr && node->right == nullptr) {
    res.push_back(std::vector<int>(curr));
  } else {
    traverse(node->left, res, curr, remaining);
    traverse(node->right, res, curr, remaining);
  }
  curr.pop_back();
}

class Solution {
 public:
  std::vector<std::vector<int>> pathSum(TreeNode *root, int targetSum) {
    std::vector<std::vector<int>> result;
    std::vector<int> curr;
    traverse(root, result, curr, targetSum);
    return result;
  }
};
