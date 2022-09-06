#pragma once
#include <vector>

namespace common {

using std::vector;

class UnionFind {
 public:
  int n;
  vector<int> parent;
  UnionFind(int n_) : n(n_), parent(vector<int>(n_, -1)) {}

  int find_parent(int i) {
    if (i == parent[i]) return i;
    if (parent[i] == -1) return (parent[i] = i);
    return (parent[i] = find_parent(parent[i]));
  }

  int make_union(int x, int y) {
    int x_set = find_parent(x);
    int y_set = find_parent(y);
    parent[x_set] = y_set;
    return y_set;
  }
};

}  // namespace common
