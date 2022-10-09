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

void dfs(int n, vector<std::pair<vi, vi>> &graph, vector<bool> &visited, int &res) {
  if (visited[n]) return;
  visited[n] = true;
  for (const auto e : graph[n].first) {
    dfs(e, graph, visited, res);
  }

  for (const auto e : graph[n].second) {
    if (!visited[e]) {
      res++;
      dfs(e, graph, visited, res);
    }
  }
}

class Solution {
 public:
  int minReorder(int n, vector<vi> &connections) {
    vector<bool> visited(n, false);
    vector<pair<vi, vi>> graph(n);
    int res = 0;
    for (const auto connection : connections) {
      if (!connection[0]) {
        graph[connection[0]].first.push_back(connection[1]);
        res++;
      } else {
        graph[connection[1]].first.push_back(connection[0]);
        graph[connection[0]].second.push_back(connection[1]);
      }
    }

    dfs(0, graph, visited, res);
    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
}
#endif
