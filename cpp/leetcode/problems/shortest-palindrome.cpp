#if defined(RUNNING_LOCALLY)
#include "local-stuff.hpp"
#endif

int MOD = 17;
vector<int> primes{431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
                   503, 509, 521, 523, 541, 557, 563, 569, 571, 577, 587, 593, 599};

bool is_palindrome(string& s, int i, int j) {
  while (i < j) {
    if (s[i] != s[j]) return false;
    i++;
    j--;
  }
  return true;
}

int get_similar(string& s) {
  int r = 1;
  for (int i = 1; i < s.length(); i++) {
    if (s[i] != s[i - 1]) return r;
    r++;
  }
  return r;
}

class Solution {
 public:
  string shortestPalindrome(string s) {
    int n = s.length();
    if (n < 2) return s;
    vector<int> forward(n);
    for (int i = 0; i < n; i++) {
      forward[i] = s[i] * primes[s[i] - 'a'] + (i ? forward[i - 1] : 0);
      forward[i] %= MOD;
    }
    vector<int> backward(n);
    for (int i = n - 1; i >= 0; i--) {
      backward[i] = s[i] * primes[s[i] - 'a'] + (i + 1 < n ? backward[i + 1] : 0);
      backward[i] %= MOD;
    }

    int res = get_similar(s);
    for (int i = n / 2; i >= 1; i--) {
      int h = forward[i];

      for (int j = 0; j <= 1; j++) {
        int r = 2 * i + j;
        if (r < n && r + 1 > res) {
          int h2 = backward[i + j] - (r + 1 < n ? backward[r + 1] : 0);
          h2 = ((h2 % MOD) + MOD) % MOD;
          if (h == h2 && is_palindrome(s, 0, r)) {
            res = r + 1;
          }
        }
      }
    }

    string copy = s.substr(res);
    reverse(copy.begin(), copy.end());
    return copy + s;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto res = s.shortestPalindrome("babbbabbaba");
  LOG2("Result", res);
  assert(res == "ababbabbbabbaba");
}
#endif
