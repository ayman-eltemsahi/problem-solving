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
#define LOG4(a, b, c, d) std::cout << (a) << ", " << (b) << ", " << (c) << ", " << (d) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001
using std::vector;

class Solution {
 public:
  int threeSumClosest(vector<int>& nums, int target) {
    const auto n = nums.size();
    sort(nums.begin(), nums.end());
    ll result = (1L << 50) + target;

    FORN(i, n) {
      if (i > 0 && nums[i] == nums[i - 1]) continue;

      const auto remaining = target - nums[i];
      int low = i + 1;
      int high = n - 1;

      while (low < high) {
        const auto s = nums[low] + nums[high];
        if (s == remaining) return target;
        const auto s3 = s + nums[i];
        if (std::abs(s3 - target) < std::abs(result - target)) {
          result = s3;
        }

        if (s < remaining) {
          low++;
        } else {
          high--;
        }
      }
    }

    return result;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> v{-1, 2, 1, -4};
  const auto res = s.threeSumClosest(v, 1);
  LOG(res);
}
#endif
