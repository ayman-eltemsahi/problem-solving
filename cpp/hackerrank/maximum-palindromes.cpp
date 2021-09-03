#include <algorithm>
#include <iostream>
#include <string>
#include <string_view>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)

#define MAX 100005
#define MOD (ll)(1e9 + 7)
int letters[26];
ll factorials[MAX];

void clear() {
  for (int i = 0; i < 26; i++)
    letters[i] = 0;
}

ll power(ll bas, ll exp) {
  bas %= MOD;
  ll result = 1;
  while (exp > 0) {
    if ((exp & 1) == 1)
      result = (result * bas) % MOD;
    bas = (bas * bas) % MOD;
    exp >>= 1;
  }
  return result;
}

ll modInverse(ll a) { return power(a, MOD - 2); }

ll get_permutations(int n) {
  ll f = factorials[n / 2];
  FORN(i, 26) {
    if (letters[i] / 2 == 0)
      continue;
    if (n / 2 == letters[i] / 2)
      return 1;
    f = (f * modInverse(factorials[letters[i] / 2])) % MOD;
  }

  return f;
}

int main() {
  factorials[0] = 1;
  FORN1(i, MAX) { factorials[i] = ((ll)i * factorials[i - 1]) % MOD; }
  std::string input;
  int t;
  std::cin >> input >> t;
  size_t n = input.size();
  std::vector<std::vector<int>> counts;
  FORN(i, n) {
    std::vector<int> vec;
    counts.push_back(vec);
    FORN(k, 26) counts[i].push_back(0);
    if (i > 0) {
      FORN(k, 26) { counts[i][k] = counts[i - 1][k]; }
    }

    counts[i][input[i] - 'a']++;
  }

  while (t--) {
    int l, r;
    std::cin >> l >> r;
    clear();
    l--;
    r--;

    FORN(k, 26) { letters[k] = counts[r][k] - (l > 0 ? counts[l - 1][k] : 0); }
    ll odd = 0;
    int c = 0;
    FORN(i, 26) {
      c += letters[i];
      if (letters[i] % 2 == 1) {
        odd++;
        c--;
      }
    }

    ll p = get_permutations(c);

    if (odd == 0) {
      printf("%lld\n", p);
    } else {
      printf("%lld\n", (odd * p) % MOD);
    }
  }
}
