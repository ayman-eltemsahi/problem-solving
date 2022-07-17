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

int n;
int target;

vector<Point> nums{{-1, 0}, {1, 0}, {0, 1}, {0, -1}};

int make_clusters(vvi& grid, vvi& clusters, int i, int j, int c) {
  if (i < 0 || j < 0 || i == n || j == n) return 0;
  if (grid[i][j] != LAND || clusters[i][j] == c) return 0;

  clusters[i][j] = c;

  return 1 + make_clusters(grid, clusters, i + 1, j, c) +
         make_clusters(grid, clusters, i, j + 1, c) + make_clusters(grid, clusters, i - 1, j, c) +
         make_clusters(grid, clusters, i, j - 1, c);
}

int get_distance(vvi& grid, vvi& clusters, int i, int j, int curr_min) {
  auto cmp = [&](Point a, Point b) { return dist[a.i][a.j] > dist[b.i][b.j]; };
  std::priority_queue<Point, std::vector<Point>, decltype(cmp)> q(cmp);

  memset(dist, -1, sizeof(dist));
  dist[i][j] = 0;
  q.push({i, j});
  int res = curr_min;
  while (!q.empty()) {
    auto p = q.top();
    q.pop();
    int d = 1 + dist[p.i][p.j];
    if (d >= res) continue;

    for (auto xy : nums) {
      int x = p.i + xy.i;
      int y = p.j + xy.j;
      if (x < 0 || y < 0 || x == n || y == n || clusters[x][y] == (target ^ 1)) continue;
      if (dist[x][y] == -1 || dist[x][y] > d) {
        if (clusters[x][y] == target) {
          if (d < res) res = d;
        } else {
          dist[x][y] = d;
          q.push({x, y});
        }
      }
    }
  }
  return res;
}

class Solution {
 public:
  int shortestBridge(vvi& grid) {
    n = grid.size();
    vvi clusters(n);
    FORN(i, n) clusters[i] = vi(n, -1);

    int c = 0;
    vi clusters_count;
    FORN(i, n)
    FORN(j, n) {
      if (grid[i][j] == LAND && clusters[i][j] == -1)
        clusters_count.push_back(make_clusters(grid, clusters, i, j, c++));
    }

    target = clusters_count[0] < clusters_count[1] ? 1 : 0;
    assert(clusters_count.size() == 2);

    int res = INT_MAX;
    FORN(i, n)
    FORN(j, n) {
      if (clusters[i][j] != (target ^ 1)) continue;
      res = get_distance(grid, clusters, i, j, res);
    }

    return res - 1;
  }
};

#if defined(RUNNING_LOCALLY)
#include "utils/read-vector.hpp"
#include "utils/print-vector.hpp"
int main() {
  Solution s;
  auto v = read_vector_vector_int("[[1,1,1,1,1],[1,0,0,0,1],[1,0,1,0,1],[1,0,0,0,1],[1,1,1,1,1]]");
  print_vector_vector_int(v);
  auto res = s.shortestBridge(v);
  LOG(res);
}
#endif
