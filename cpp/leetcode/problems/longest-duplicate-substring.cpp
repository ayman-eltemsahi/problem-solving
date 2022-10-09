#if defined(RUNNING_LOCALLY)
#include "local-stuff.hpp"
#endif

typedef long long int ll;
const int base = 7919;
const int mod = int(1e9 + 7);

ll power(ll a, ll b) {
  ll p = 1;
  while (b > 0) {
    if (b % 2 == 1) {
      p = (p * a) % mod;
    }
    a = (a * a) % mod;
    b >>= 1;
  }
  return p;
}

bool match_segment(int i, int j, int k, string& s) {
  for (int t = 0; t < k; t++)
    if (s[i + t] != s[j + t]) return false;

  return true;
}

int check(int k, string& s) {
  unordered_map<ll, int> st;

  ll p = power(base, k);
  ll hash = 0;
  for (int i = 0; i < s.size(); i++) {
    hash = (hash * base) % mod + s[i];
    hash %= mod;

    if (i - k >= 0) {
      hash -= (p * s[i - k]) % mod;
      hash = (hash + mod) % mod;
    }

    if (i >= k && st.find(hash) != st.end() && match_segment(i - k + 1, st[hash], k, s)) return i;
    if (i >= k - 1) st[hash] = i - k + 1;
  }
  return -1;
}

class Solution {
 public:
  string longestDupSubstring(string s) {
    int l = 0, h = s.length();
    while (l < h) {
      int k = l + (h - l) / 2;

      if (check(k, s) > -1)
        l = k + 1;
      else
        h = k - 1;
    }

    int r = check(l, s);
    if (r < 0) {
      l--;
      r = check(l, s);
    }
    if (r < 0) return "";

    return s.substr(r + 1 - l, l);
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto res = s.longestDupSubstring("banana");
  LOG(res);
}
#endif
