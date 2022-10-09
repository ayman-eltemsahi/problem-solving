#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
typedef std::vector<std::vector<int>> vvi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"

int dfs(int i, int j, vvi& grid) {
  if (!grid[i][j] || grid[i][j] == -1) return 0;
  grid[i][j] = -1;
  int r = 1;
  if (i) r += dfs(i - 1, j, grid);
  if (i < grid.size() - 1) r += dfs(i + 1, j, grid);
  if (j) r += dfs(i, j - 1, grid);
  if (j < grid[0].size() - 1) r += dfs(i, j + 1, grid);
  return r;
}

class Solution {
 public:
  int maxAreaOfIsland(vvi& grid) {
    int res = 0;
    FORN(i, grid.size()) {
      FORN(j, grid[0].size()) {
        auto tmp = dfs(i, j, grid);
        res = tmp > res ? tmp : res;
      }
    }

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
