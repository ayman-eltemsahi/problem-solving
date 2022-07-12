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
  vector<vector<int>> threeSum(vector<int>& nums) {
    const auto n = nums.size();
    sort(nums.begin(), nums.end());
    std::vector<std::vector<int>> result;
    FORN(i, n) {
      if (i > 0 && nums[i] == nums[i - 1]) continue;

      const auto remaining = -nums[i];
      int low = i + 1;
      int high = n - 1;

      while (low < high) {
        const auto s = nums[low] + nums[high];
        if (s < remaining) {
          low++;
        } else if (s > remaining) {
          high--;
        } else {
          result.push_back({nums[i], nums[low], nums[high]});

          const int lowVal = nums[low];
          while (low < high && nums[low] == lowVal) low++;

          const int highVal = nums[high];
          while (high > low && nums[high] == highVal) high--;
        }
      }
    }

    return result;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> v{-1, 0, 1, 2, -1, -4};
  const auto res = s.threeSum(v);
  FORN(i, res.size()) {
    LOG3(res[i][0], res[i][1], res[i][2]);
  }
}
#endif
