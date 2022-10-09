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
#define MAXN 2001

class Solution {
 public:
  int jump(std::vector<int>& nums) {
    const auto n = nums.size();
    if (n == 1) return 0;
    int mx = nums[0];
    int reach = nums[0];
    int jumps = 1;
    for (int i = 0; i < n - 1; i++) {
      mx = std::max(mx, nums[i] + i);
      if (reach == i) {
        reach = mx;
        jumps++;
      }
    }
    return jumps;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> nums{3, 1, 2, 3, 1, 1};
  LOG(s.jump(nums));
}
#endif
