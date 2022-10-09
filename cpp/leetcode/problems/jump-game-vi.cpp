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
  int maxResult(std::vector<int>& nums, int k) {
    const auto n = nums.size();
    std::vector<int> score(n, 0);
    score[n - 1] = nums[n - 1];

    std::deque<int> mx_q;
    mx_q.push_back(n - 1);

    for (int i = n - 2; i >= 0; i--) {
      if (mx_q.front() > i + k) mx_q.pop_front();

      score[i] = nums[i] + score[mx_q.front()];
      while (!mx_q.empty() && score[mx_q.back()] < score[i]) {
        mx_q.pop_back();
      }

      mx_q.push_back(i);
    }

    return score[0];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> nums{1, -4, -2, -3, 4, -7, 3};
  LOG(s.maxResult(nums, 3));
}
#endif
