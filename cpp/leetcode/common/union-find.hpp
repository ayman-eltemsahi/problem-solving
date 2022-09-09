#pragma once
#include <vector>

namespace common {

using std::vector;

class UnionFind {
 public:
  vector<int> parent;
  UnionFind(int n) : parent(n) {
    for (int i = 0; i < n; i++) parent[i] = i;
  }

  int find(int x) {
    if (x == parent[x]) return x;
    return (parent[x] = find(parent[x]));
  }

  int connect(int x, int y) {
    int x_p = find(x);
    int y_p = find(y);
    return (parent[x_p] = y_p);
  }
};

class UnionFindRank {
 public:
  vector<int> parent, rank;

  UnionFindRank(int n) : parent(n), rank(n) {
    for (int i = 0; i < n; i++) {
      parent[i] = i;
      rank[i] = 1;
    }
  }

  int find(int x) {
    if (parent[x] == x) return x;
    return (parent[x] = find(parent[x]));
  }

  int connect(int x, int y) {
    int rootX = find(x);
    int rootY = find(y);

    if (rootX == rootY) return rootX;
    if (rank[rootX] < rank[rootY]) {
      parent[rootX] = rootY;
      rank[rootY] += rank[rootX];
      return rootY;
    } else {
      parent[rootY] = rootX;
      rank[rootX] += rank[rootY];
      return rootX;
    }
  }
};

}  // namespace common
