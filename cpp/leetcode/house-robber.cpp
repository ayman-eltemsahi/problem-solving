#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 101
int dp[MAXN];

inline int find_max(int a, int b) {
  return a > b ? a : b;
}

class Solution {
 public:
  int rob(std::vector<int>& nums) {
    int n = nums.size();
    for (int i = 0; i < MAXN; i++) dp[i] = 0;

    dp[0] = nums[0];
    if (n > 1) {
      dp[1] = find_max(nums[0], nums[1]);
    }
    for (int i = 2; i < n; i++) {
      dp[i] = find_max(dp[i - 1], nums[i] + dp[i - 2]);
    }
    return dp[n - 1];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> nums{2};
  LOG(s.rob(nums));
}
#endif
