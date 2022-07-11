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
  int maxJumps(std::vector<int>& arr, int d) {
    const auto n = arr.size();
    std::vector<int> indices(n, 0);
    FORN(i, n) indices[i] = i;
    std::sort(indices.begin(), indices.end(),
              [&arr](const auto lhs, const auto rhs) { return arr[lhs] < arr[rhs]; });

    std::vector<int> can_reach(n, 1);
    int mx = 0;

    for (const auto index : indices) {
      for (int i = index + 1; i < n && i <= index + d && arr[index] > arr[i]; i++) {
        if (can_reach[i] + 1 > can_reach[index]) {
          can_reach[index] = can_reach[i] + 1;
        }
      }

      for (int i = index - 1; i >= 0 && i >= index - d && arr[index] > arr[i]; i--) {
        if (can_reach[i] + 1 > can_reach[index]) {
          can_reach[index] = can_reach[i] + 1;
        }
      }

      if (can_reach[index] > mx) mx = can_reach[index];
    }

    return mx;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> nums{6, 4, 14, 6, 8, 13, 9, 7, 10, 6, 12};
  LOG(s.maxJumps(nums, 2));
}
#endif
