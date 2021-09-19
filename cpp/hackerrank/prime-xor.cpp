#include <iostream>
#include <unordered_map>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define LOG3(a, b, c) std::cout << a << ", " << b << ", " << c << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 100001

bool primes[9000];
ll dp[2][MAXN];

void check_primes() {
  primes[0] = primes[1] = false;
  primes[2] = true;
  for (int n = 3; n < 9000; n++) {
    if (n % 2 == 0) {
      primes[n] = false;
      continue;
    }
    primes[n] = true;
    for (int i = 3; i <= n / 2; i++) {
      if (n % i == 0) {
        primes[n] = false;
        break;
      }
    }
  }
}

int main() {
  check_primes();
  int T;
  std::cin >> T;
  while (T--) {
    int n;
    std::cin >> n;
    std::unordered_map<int, int> map;
    FORN(i, n) {
      int k;
      std::cin >> k;
      map[k]++;
    }

    FORN(i, MAXN) { dp[0][i] = dp[1][i] = 0; }
    dp[1][0] = 1;

    int p = 0;
    for (auto kv : map) {
      int q = p ^ 1;
      FORN(j, 8192) {
        dp[p][j] = dp[q][j] * (1 + kv.second / 2);
        dp[p][j] %= MOD;
        dp[p][j] += dp[q][j ^ kv.first] * ((1 + kv.second) / 2);
        dp[p][j] %= MOD;
      }

      p = q;
    }

    ll ans = 0;
    FORN(j, 8192) {
      if (primes[j])
        ans = (ans + dp[p ^ 1][j]) % MOD;
    }

    std::cout << ans << "\n";
  }
}
