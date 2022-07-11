#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <queue>
#include <unordered_map>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

class Solution {
 public:
  int minJumps(std::vector<int>& arr) {
    const auto n = arr.size();
    std::unordered_map<int, std::vector<int> > m;
    for (int i = 0; i < n; i++) {
      m[arr[i]].push_back(i);
    }

    std::vector<int> distance(n, -1);
    distance[0] = 0;
    std::queue<int> q;
    q.push(0);

    while (!q.empty()) {
      const int i = q.front();
      q.pop();

      const int d = distance[i] + 1;

      if (i + 1 < n) {
        const int j = i + 1;
        if (distance[j] == -1 || distance[j] > d) {
          distance[j] = d;
          q.push(j);
        }
      }

      if (i - 1 >= 0) {
        const int j = i - 1;
        if (distance[j] == -1 || distance[j] > d) {
          distance[j] = d;
          q.push(j);
        }
      }

      if (!m[arr[i]].empty()) {
        for (const int j : m[arr[i]]) {
          if (distance[j] == -1 || distance[j] > d) {
            if (j == n - 1) return d;
            distance[j] = d;
            q.push(j);
          }
        }
        m[arr[i]].clear();
      }
    }

    return distance[n - 1];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> nums{7, 7, 2, 1, 7, 7, 7, 3, 4, 1};
  for (int i = 0; i < 50000; i++) nums.push_back(1000);
  LOG(s.minJumps(nums));
}
#endif
