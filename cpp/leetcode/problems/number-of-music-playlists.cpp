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
int dp[MAXN][MAXN];

class Solution {
 public:
  int numMusicPlaylists(int n, int goal, int k) {
    memset(dp, 0, sizeof(dp));

    for (int ni = n; ni >= 0; ni--) {
      dp[ni][goal] = ni == n ? 1 : 0;

      for (int gi = goal - 1; gi >= 0; gi--) {
        const ll temp = (1L * dp[ni][gi + 1] * std::max(0, ni - k) % MOD) +
                        (1L * dp[ni + 1][gi + 1] * (n - ni)) % MOD;
        dp[ni][gi] = (int)(temp % MOD);
      }
    }
    return dp[0][0];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  LOG(s.numMusicPlaylists(3, 3, 1));
  LOG(s.numMusicPlaylists(2, 3, 0));
  LOG(s.numMusicPlaylists(2, 3, 1));
}
#endif
