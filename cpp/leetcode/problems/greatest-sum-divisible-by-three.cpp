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
#define MAXN 100001
int dp[3][MAXN];

class Solution {
public:
  int maxSumDivThree(std::vector<int> &nums) {
    int three = 0;
    std::vector<int> vec;
    for (const int n : nums) {
      if (n % 3 == 0)
        three += n;
      else
        vec.push_back(n);
    }

    FORN(i, 3) FORN(j, MAXN) dp[i][j] = -1;

    dp[0][0] = 0;
    dp[vec[0] % 3][0] = vec[0];
    FORN1(i, vec.size()) {
      int tmp = dp[3 - vec[i] % 3][i - 1];
      if (tmp >= 0) {
        dp[0][i] = std::max(dp[0][i - 1], vec[i] + tmp);
      } else {
        dp[0][i] = dp[0][i - 1];
      }

      tmp = dp[(1 + 3 - (vec[i] % 3)) % 3][i - 1];
      if (tmp >= 0) {
        dp[1][i] = std::max(dp[1][i - 1], vec[i] + tmp);
      } else {
        dp[1][i] = dp[1][i - 1];
      }

      tmp = dp[(2 + 3 - (vec[i] % 3)) % 3][i - 1];
      if (tmp >= 0) {
        dp[2][i] = std::max(dp[2][i - 1], vec[i] + tmp);
      } else {
        dp[2][i] = dp[2][i - 1];
      }

      dp[vec[i] % 3][i] = std::max(dp[vec[i] % 3][i], vec[i]);
    }

    return three + dp[0][vec.size() - 1];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> v{1, 6, 3, 5, 8};
  assert(s.maxSumDivThree(v) == 18);

  v = {2, 6, 2, 2, 7};
  assert(s.maxSumDivThree(v) == 15);

  v = {4};
  assert(s.maxSumDivThree(v) == 0);

  v = {1, 2, 3, 4, 4};
  assert(s.maxSumDivThree(v) == 12);
}
#endif
