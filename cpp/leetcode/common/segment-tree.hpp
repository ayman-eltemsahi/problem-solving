#pragma once
#include <vector>
#include <math.h>

namespace common {

using std::max;
using std::vector;

class MaxSegmentTree {
 public:
  int n;
  vector<int> tree;
  MaxSegmentTree(int n_) : n(n_) { tree = vector<int>(2 * n_); }

  int max_value() { return query(0, n - 1); }

  int query(int l, int r) {
    l += n;
    r += n;
    int res = 0;
    while (l < r) {
      if (l & 1) {
        res = max(res, tree[l]);
        l += 1;
      }
      if (r & 1) {
        r -= 1;
        res = max(res, tree[r]);
      }
      l >>= 1;
      r >>= 1;
    }
    return res;
  }

  void update(int i, int val) {
    i += n;
    tree[i] = val;
    while (i > 1) {
      i >>= 1;
      tree[i] = max(tree[i * 2], tree[i * 2 + 1]);
    }
  }
};

class MaxSegmentTreeRecursive {
 public:
  int n;
  vector<int> tree;
  MaxSegmentTreeRecursive(int n_) : n(n_) {
    int size = (int)(ceil(log2(n)));
    size = (2 * pow(2, size)) - 1;
    tree = vector<int>(size);
  }

  int query(int l, int r) { return query_util(0, l, r, 0, n - 1); }

  int query_util(int i, int qL, int qR, int l, int r) {
    if (l >= qL && r <= qR) return tree[i];
    if (l > qR || r < qL) return INT_MIN;

    int m = (l + r) / 2;
    return max(query_util(2 * i + 1, qL, qR, l, m), query_util(2 * i + 2, qL, qR, m + 1, r));
  }

  void update(int i, int val) { update_util(0, 0, n - 1, i, val); }
  void update_util(int i, int l, int r, int pos, int val) {
    if (pos < l || pos > r) return;
    if (l == r) {
      tree[i] = val;
      return;
    }

    int m = (l + r) / 2;
    update_util(2 * i + 1, l, m, pos, val);
    update_util(2 * i + 2, m + 1, r, pos, val);
    tree[i] = max(tree[2 * i + 1], tree[2 * i + 2]);
  }
};

}  // namespace common
