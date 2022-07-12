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

bool check(int i, int a, int b, int c, int d, std::vector<int>& matchsticks) {
  if (i == matchsticks.size()) return a == 0 && b == 0 && c == 0 && d == 0;

  const auto s = matchsticks[i];
  if (a - s < 0) return false;
  if (check(i + 1, a - s, b, c, d, matchsticks)) return true;

  if (b - s < 0) return false;
  if (check(i + 1, a, b - s, c, d, matchsticks)) return true;

  if (c - s < 0) return false;
  if (check(i + 1, a, b, c - s, d, matchsticks)) return true;

  if (d - s < 0) return false;
  if (check(i + 1, a, b, c, d - s, matchsticks)) return true;

  return false;
}

class Solution {
 public:
  bool makesquare(std::vector<int>& matchsticks) {
    int sum = 0;
    for (const auto stick : matchsticks) sum += stick;

    if (sum % 4 != 0) return false;
    sum /= 4;
    for (const auto stick : matchsticks)
      if (stick > sum) return false;
    std::sort(matchsticks.begin(), matchsticks.end());

    return check(0, sum, sum, sum, sum, matchsticks);
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // std::vector<int> nums{8, 16, 24, 32, 40, 48, 56, 64, 72, 80, 88, 96, 104, 112, 60};
  std::vector<int> nums{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 102};
  LOG(s.makesquare(nums));
}
#endif
