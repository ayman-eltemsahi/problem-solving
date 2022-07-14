#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <map>
#include "common.hpp"

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

TreeNode* build(int st, int en, int st2, vi& preorder, std::map<int, int>& inorder_rev) {
  if (st == en) return new TreeNode(preorder[st]);
  if (st > en) return nullptr;

  const auto val = preorder[st];
  const auto mid = inorder_rev[val];

  auto node = new TreeNode(preorder[st]);
  node->left = build(st + 1, st + mid - st2, st2, preorder, inorder_rev);
  node->right = build(st + mid - st2 + 1, en, mid + 1, preorder, inorder_rev);
  return node;
}

class Solution {
 public:
  TreeNode* buildTree(vi& preorder, vi& inorder) {
    std::map<int, int> inorder_rev;
    FORN(i, inorder.size()) inorder_rev[inorder[i]] = i;
    return build(0, preorder.size() - 1, 0, preorder, inorder_rev);
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
