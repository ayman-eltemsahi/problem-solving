#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::string string;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 202
bool dp[MAXN][MAXN];

class Solution {
 public:
  bool isInterleave(string s1, string s2, string s3) {
    const auto n = s1.length();
    const auto m = s2.length();
    const auto nm = s3.length();
    memset(dp, false, sizeof(dp));
    dp[0][0] = true;

    FORN(j, n + 1) {
      FORN(k, m + 1) {
        int i = j + k;
        dp[j + 1][k] = dp[j + 1][k] || (j < n && i < nm && dp[j][k] && s1[j] == s3[i]);

        dp[j][k + 1] = dp[j][k + 1] || (k < m && i < nm && dp[j][k] && s2[k] == s3[i]);
      }
    }

    return nm == n + m && dp[n][m];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  string s1 = "aabcc";
  string s2 = "dbbca";
  string s3 = "aadbbbaccc";

  LOG(s.isInterleave("", "", "a"));
  LOG(s.isInterleave(s1, s2, s3));
  LOG(s.isInterleave("aabcc", "dbbca", "aadbbcbcac"));
}
#endif
