#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

class Edge {
 public:
  int to;
  int weight;
  Edge(int t, int w) : to(t), weight(w) {
  }
};

class Solution {
 public:
  int findTheCity(int n, vector<vi>& edges, int threshold) {
    vector<vi> distance(n);
    FORN(i, n) distance[i] = vi(n, threshold + 10);
    int res = 0, res_count = n + 1;
    for (const auto e : edges) distance[e[0]][e[1]] = distance[e[1]][e[0]] = e[2];
    FORN(i, n) distance[i][i] = 0;

    FORN(k, n) {
      FORN(i, n) {
        FORN(j, n) {
          distance[i][j] = std::min(distance[i][j], distance[i][k] + distance[k][j]);
        }
      }
    }

    for (int i = n - 1; i >= 0; i--) {
      int count = 0;
      FORN(j, n) {
        if (distance[i][j] <= threshold) count++;
      }

      if (res_count > count) {
        res_count = count;
        res = i;
      }
    }
    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
}
#endif
