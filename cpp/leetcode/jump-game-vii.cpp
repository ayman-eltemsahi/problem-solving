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
#define MAXN 100005

class Solution {
 public:
  bool canReach(std::string s, int minJump, int maxJump) {
    const auto n = s.length();
    if (s[n - 1] == '1') return false;
    std::vector<bool> counts(n, false);

    int window = 0;
    counts[n - 1] = true;
    for (int i = n - 1; i >= 0; i--) {
      if (i + maxJump + 1 < n) window -= counts[i + maxJump + 1];
      if (i + minJump < n) window += counts[i + minJump];

      if (window > 0) {
        if (s[i] == '0' || i == 0) {
          counts[i] = 1;
        }
      }
    }

    return counts[0];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(false == s.canReach("01101110", 2, 3));
}
#endif
