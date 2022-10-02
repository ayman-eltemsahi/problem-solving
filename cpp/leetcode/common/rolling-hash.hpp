#pragma once
#include <vector>
#include <string>
#include <math.h>

namespace common {

using std::string;
using std::vector;

class RollingHash {
 public:
  const int prime_base = 5693;
  const int mod = 1000000007;
  vector<long long> power, hash;

  RollingHash(string &s) : power(s.size()), hash(s.size()) {
    power[0] = 1;
    for (int i = 1; i < s.size(); i++) {
      power[i] = (1L * power[i - 1] * prime_base) % mod;
    }

    hash[0] = s[0];
    for (int i = 1; i < s.size(); i++) {
      hash[i] = (1L * hash[i - 1] * prime_base + s[i]) % mod;
    }
  }

  long long query(int l, int r) {
    if (l == 0) return hash[r];
    long long h = hash[r] - (1L * hash[l - 1] * power[r - l + 1]) % mod;
    return h < 0 ? h + mod : h;
  }
};

}  // namespace common
