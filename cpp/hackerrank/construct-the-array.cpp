#include <iostream>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define LOG3(a, b, c) std::cout << a << ", " << b << ", " << c << "\n"
#define FIRST 0
#define LAST 1
#define REST 2

ll dp[100001][3];

const ll MOD = ll(1e9 + 7);

int main() {
  int n, k, x;
  std::cin >> n >> k >> x;

  dp[0][FIRST] = 1;

  if (x == 1) {
    for (int i = 1; i < n; i++) {
      dp[i][FIRST] = dp[i - 1][REST];
      dp[i][REST] = (((k - 2) * dp[i - 1][REST]) % MOD +
                     ((k - 1) * dp[i - 1][FIRST]) % MOD) %
                    MOD;

      dp[i][LAST] = dp[i][FIRST];
    }
  } else {
    for (int i = 1; i < n; i++) {
      dp[i][FIRST] = (dp[i - 1][REST] + dp[i - 1][LAST]) % MOD;
      dp[i][LAST] = (dp[i - 1][REST] + dp[i - 1][FIRST]) % MOD;
      dp[i][REST] = (((k - 3) * dp[i - 1][REST]) % MOD +
                     ((k - 2) * (dp[i - 1][FIRST] + dp[i - 1][LAST])) % MOD) %
                    MOD;
    }
  }

  std::cout << dp[n - 1][LAST] << "\n";
}
