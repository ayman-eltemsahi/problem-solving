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
using std::vector;

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

class Solution {
 public:
  vector<vector<int>> levelOrder(TreeNode *root) {
    if (!root) return {};
    vector<vector<int>> res;

    vector<vector<TreeNode *>> levels(2);
    levels[0] = {root};
    levels[1] = {};

    int i = 1;
    while (!levels[(i + 1) % 2].empty()) {
      levels[i % 2].clear();
      res.push_back({});
      for (const auto node : levels[(i + 1) % 2]) {
        if (node->left) levels[i % 2].push_back(node->left);
        if (node->right) levels[i % 2].push_back(node->right);

        res[i - 1].push_back(node->val);
      }

      i++;
    }

    return res;
  }
};
