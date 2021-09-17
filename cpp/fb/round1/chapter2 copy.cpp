#include <algorithm>
#include <iostream>
#include <stack>
#include <string>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
ll MOD = 1000000007;

ll brute_force(int n, const std::string &text) {
  ll total = 0;
  FORN(i, n) {
    for (int j = i; j < n; j++) {
      ll changes = 0;
      char last = ' ';
      for (int k = i; k <= j; k++) {
        char c = text[k];
        if (c == 'F')
          continue;
        if (last != c) {
          last = c;
          changes++;
        }
      }

      if (changes > 0)
        changes--;
      total += changes;
    }
  }
  return total;
}

int main() {
  int T;
  std::cin >> T;
  FORN(t, T) {
    int n;
    std::string text;
    std::cin >> n >> text;
    std::vector<ll> xo;
    std::vector<ll> similar_before;
    xo.reserve(n);
    similar_before.reserve(n);
    char last = ' ';
    FORN(i, n) {
      char c = text[i];

      if (i == 0) {
        last = c;
        xo.push_back(1);
        similar_before.push_back(0);
        continue;
      }

      if (last == 'F') {
        last = c;
      }
      if (c == last || c == 'F') {
        similar_before.push_back(similar_before[i - 1] + 1);
        xo.push_back(xo[i - 1]);
      } else {
        last = c;
        similar_before.push_back(0);
        xo.push_back(xo[i - 1] + 1);
      }
    }

    ll result = 0;
    ll cur_sum = 0;
    ll cur_cur_sum = 0;
    ll batch = 0;
    FORN(i, n) {
      result += xo[i] * (i + 1);
      result %= MOD;
      result -= cur_sum;
      result = (result + MOD) % MOD;
      result -= (i + 1) - similar_before[i];
      result = (result + MOD) % MOD;

      bool willChange =
          i != n && xo[i] != xo[i + 1] && text[i + 1] != 'F' && text[i] != 'F';

      if (willChange) {
        cur_sum -= batch;
        cur_sum = (cur_sum + MOD) % MOD;
        batch = 0;
      } else {
        batch += xo[i];
      }

      cur_sum += xo[i];
      cur_sum %= MOD;
    }

    std::cout << "Case #" << (t + 1) << ": " << result
              << " :: " << brute_force(n, text) << "\n";
  }
}
