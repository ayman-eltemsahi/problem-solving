#include <algorithm>
#include <cassert>
#include <cctype>
#include <bit>
#include <iostream>
#include <unordered_set>
#include <queue>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"


int mn(int a, int b) {
  return a < b ? a : b;
}

class Solution {
 public:
  int shortestPathLength(vector<vi> &graph) {
    int n = graph.size();
    if (n < 2) return 0;
    vector<vi> cache(n);
    for (int i = 0; i < n; i++) {
      cache[i] = vi(1 << n, 100000);
    }

    int full_mask = (1 << n) - 1;
    int res = 100000;
    int m;

    for (int i = 0; i < n; i++) {
      for (auto e : graph[i]) {
        cache[i][(1 << i) | (1 << e)] = 1;
        cache[e][(1 << i) | (1 << e)] = 1;
      }
    }

    for (int bit_count = 3; bit_count <= n; bit_count++) {
      for (int i = 0; i < n; i++) {
        for (auto e : graph[i]) {
          m = (1 << i) | (1 << e);
          for (int c = 0; c < full_mask; c++) {
            cache[i][c | m] = mn(cache[i][c | m], cache[e][c] + 1);
          }
        }
      }
    }

    for (int i = 0; i < n; i++) res = mn(res, cache[i][full_mask]);
    return res;
  }
};

#if defined(RUNNING_LOCALLY)
#include "utils/read-vector.hpp"
#include "utils/print-vector.hpp"
int main() {
  Solution s;
  // auto input = read_vector_vector_int("[[1],[0,2,4],[1,3,4],[2],[1,2]]");
  // auto input = read_vector_vector_int("[[1,2,3],[0],[0],[0]]");
  auto input = read_vector_vector_int(
      "[[1,2,3,4,5,6,7,8,9],[0,2,3,4,5,6,7,8,9],[0,1,3,4,5,6,7,8,9],[0,1,2,4,5,6,7,8,9],[0,1,2,3,5,"
      "6,7,8,9],[0,1,2,3,4,6,7,8,9],[0,1,2,3,4,5,7,8,9],[0,1,2,3,4,5,6,8,9],[0,1,2,3,4,5,6,7,9,10],"
      "[0,1,2,3,4,5,6,7,8,11],[8],[9]]");

  assert(s.shortestPathLength(input) == 11);
}
#endif
