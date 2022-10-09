#if defined(RUNNING_LOCALLY)
#include "local-stuff.hpp"
#endif

typedef long long int ll;
const int base = 7919;
const int mod = int(1e9 + 7);

void check(string& text, vector<int>& hash, int i, int len, unordered_set<int>& st,
           vector<int>& power) {
  int j = i + len;
  int n = text.length();
  if (j + len - 1 >= n) return;

  ll p = power[len];
  int h1 = hash[i + len - 1] - (i - 1 >= 0 ? (p * hash[i - 1]) % mod : 0);
  h1 = (h1 + mod) % mod;

  int h2 = hash[j + len - 1] - (p * hash[j - 1]) % mod;
  h2 = (h2 + mod) % mod;

  if (h1 == h2) st.insert(h1);
}

class Solution {
 public:
  int distinctEchoSubstrings(string text) {
    int n = text.length();
    vector<int> hash(n);
    vector<int> power(n + 1);
    power[0] = 1;
    for (int i = 1; i <= n; i++) power[i] = (power[i - 1] * base) % mod;

    unordered_set<int> st;
    ll h = 0;
    for (int i = 0; i < n; i++) {
      h = (h * base) % mod + text[i];
      h %= mod;
      hash[i] = h;
    }

    for (int i = 0; i < n; i++) {
      for (int len = 1; len <= n / 2; len++) {
        check(text, hash, i, len, st, power);
      }
    }

    return st.size();
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto res = s.distinctEchoSubstrings("abcabcabc");
  LOG(res);
}
#endif
