#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <queue>

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
  bool canReach(std::vector<int>& arr, int start) {
    const auto n = arr.size();
    std::vector<int> reached(n, 0);
    std::queue<int> q;
    q.push(start);

    while (!q.empty()) {
      const auto i = q.front();
      q.pop();
      if (reached[i]) continue;
      reached[i] = 1;
      if(arr[i] == 0) return true;
      if (i + arr[i] < n) q.push(i + arr[i]);
      if (i - arr[i] >= 0) q.push(i - arr[i]);
    }

    return false;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> nums{3, 0, 2, 1, 2};
  LOG(s.canReach(nums, 2));
}
#endif
