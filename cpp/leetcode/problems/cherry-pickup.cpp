#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

#define LOW -9999999
int n;
int sums[55][55][55][55];

int fill_sums(vector<vector<int>>& grid, int i1, int j1, int i2, int j2) {
  if (i1 < 0 || j1 < 0 || i1 == n || j1 == n) return LOW;
  if (i2 < 0 || j2 < 0 || i2 == n || j2 == n) return LOW;
  if (grid[i1][j1] == -1 || grid[i2][j2] == -1) return LOW;
  if (i1 == n - 1 && j1 == n - 1) return (sums[i1][j1][i2][j2] = grid[i1][j1]);

  if (sums[i1][j1][i2][j2] == LOW) {
    const auto RR = fill_sums(grid, i1 + 1, j1, i2 + 1, j2);
    const auto RD = fill_sums(grid, i1 + 1, j1, i2, j2 + 1);
    const auto DD = fill_sums(grid, i1, j1 + 1, i2, j2 + 1);
    const auto DR = fill_sums(grid, i1, j1 + 1, i2 + 1, j2);

    sums[i1][j1][i2][j2] = grid[i1][j1] + std::max(std::max(RR, RD), std::max(DD, DR));
    if (i1 != i2 || j1 != j2) sums[i1][j1][i2][j2] += grid[i2][j2];
  }

  return sums[i1][j1][i2][j2];
}

class Solution {
 public:
  int cherryPickup(vector<vector<int>>& grid) {
    n = grid.size();
    for (int i = 0; i < n; i++)
      for (int j = 0; j < n; j++)
        for (int k = 0; k < n; k++)
          for (int l = 0; l < n; l++) sums[i][j][k][l] = LOW;
    int res = fill_sums(grid, 0, 0, 0, 0);

    // if unreachable
    if (sums[n - 1][n - 1][n - 1][n - 1] < 0) return 0;

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
