#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
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

int dist[101][101];
vector<int> nums{-1, 0, 1};

class Solution {
 public:
  int shortestPathBinaryMatrix(vector<vector<int>>& grid) {
    int n = grid.size();
    if (grid[0][0] || grid[n - 1][n - 1]) return -1;
    memset(dist, -1, sizeof(dist));
    dist[n - 1][n - 1] = 1;

    auto cmp = [&](pair<int, int> a, pair<int, int> b) {
      return dist[a.first][a.second] > dist[b.first][b.second];
    };

    std::priority_queue<pair<int, int>, std::vector<pair<int, int>>, decltype(cmp)> q(cmp);
    q.push({n - 1, n - 1});
    while (!q.empty()) {
      auto ij = q.top();
      q.pop();

      int d = 1 + dist[ij.first][ij.second];

      for (int di : nums)
        for (int dj : nums) {
          if (!di && !dj) continue;
          int x = ij.first + di;
          int y = ij.second + dj;
          if ((x < 0 || y < 0 || x >= n || y >= n) ||
              (grid[x][y] || (dist[x][y] != -1 && dist[x][y] <= d)))
            continue;
          dist[x][y] = d;
          q.push({x, y});
        }
    }

    return dist[0][0];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
