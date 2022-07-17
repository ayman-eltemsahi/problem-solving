#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <queue>
#include <vector>

typedef long long int ll;
using std::pair;
using std::string;
using std::vector;
typedef std::vector<int> vi;
typedef std::vector<std::vector<int>> vvi;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

#define LAND 1
#define WATER 0

int dist[101][101];
struct Point {
  int i, j;
};

vector<Point> nums{{-1, 0}, {1, 0}, {0, 1}, {0, -1}};

class Solution {
 public:
  int nearestExit(vector<vector<char>>& maze, vector<int>& entrance) {
    int n = maze.size();
    int m = maze[0].size();

    auto cmp = [&](Point a, Point b) { return dist[a.i][a.j] > dist[b.i][b.j]; };
    std::priority_queue<Point, std::vector<Point>, decltype(cmp)> q(cmp);

    memset(dist, -1, sizeof(dist));
    dist[entrance[0]][entrance[1]] = 0;
    q.push({entrance[0], entrance[1]});
    int res = INT_MAX;
    while (!q.empty()) {
      auto p = q.top();
      q.pop();
      int d = 1 + dist[p.i][p.j];
      if (d >= res) continue;

      for (auto xy : nums) {
        int x = p.i + xy.i;
        int y = p.j + xy.j;
        if (x < 0 || y < 0 || x == n || y == m || maze[x][y] == '+') continue;
        if (dist[x][y] == -1 || dist[x][y] > d) {
          if (x == 0 || y == 0 || x == n - 1 || y == m - 1) {
            if (d < res) res = d;
          } else {
            dist[x][y] = d;
            q.push({x, y});
          }
        }
      }
    }

    return res == INT_MAX ? -1 : res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
}
#endif
