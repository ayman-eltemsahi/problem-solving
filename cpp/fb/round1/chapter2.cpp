#include <algorithm>
#include <iostream>
#include <stack>
#include <string>
#include <vector>

typedef long long int ll;
#define LOG(a) std::cout << a << "\n"
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
ll MOD = 1000000007;

ll brute_force(int n, const std::string &text) {
  ll total = 0;
  FORN(i, n) {
    for (int j = i; j < n; j++) {
      // if (text[i] == 'F' && text[j] == 'F') {
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
      total %= MOD;
    }
  }
  return total;
}

ll get_for_f_f(const std::vector<ll> &groups, const std::vector<char> &isF) {
  ll result = 0;
  std::vector<ll> vec;
  std::vector<ll> left;
  std::vector<ll> left_accum;
  std::vector<ll> right;
  std::vector<ll> right_accum;

  int p = 0;
  while (p < groups.size() && isF[p] != 'F')
    p++;

  if (groups.size() - 1 == p)
    return 0;

  left.push_back(groups[p]);
  left_accum.push_back(groups[p]);

  int count = 0;
  p++;
  while (p < isF.size()) {
    if (groups[p] == 0) {
      p++;
      continue;
    }
    if (isF[p] == 'F') {
      vec.push_back(count - 1);
      right.push_back(groups[p]);
      right_accum.push_back(groups[p]);
      left.push_back(groups[p]);
      left_accum.push_back(groups[p]);
      count = 0;
    } else {
      count++;
    }

    p++;
  }
  FORN1(i, left.size()) { left_accum[i] += left_accum[i - 1]; }
  for (int i = right.size() - 2; i >= 0; i--) {
    right_accum[i] += right_accum[i + 1];
  }

  FORN(i, vec.size()) {
    ll acc = (left_accum[i] * right_accum[i]) % MOD;
    result += vec[i] * acc;
    result %= MOD;

    // if (i > 0)
    //   result += right[i] * left_accum[i - 1];
    if (i < vec.size() - 1)
      result += left[i] * right_accum[i + 1];
    // if (i > 0 && i < vec.size() - 1)
    //   result += (left_accum[i - 1] * right_accum[i + 1]) / 2;
    result %= MOD;
  }
  // LOG(result);
  return result;

  // FORN(i, vec.size()) {
  //   result = (result + vec[i] * (vec.size() - i) * (i + 1)) % MOD;
  //   ll conn = vec.size() - i - 1;
  //   result = (result + ((conn * (conn + 1)) / 2) % MOD) % MOD;
  // }
  // return result;
}

ll get_result(const std::vector<ll> &groups, const std::vector<char> &isF,
              bool reversed) {
  ll result = 0;
  ll special_result = 0;
  ll groups_sum = 0;
  ll sum = 0;
  ll f_result = 0;

  ll last_group = 0;
  FORN(i, groups.size()) {
    if (groups[i] == 0)
      continue;
    if (isF[i] == 'F') {
      result += (f_result * groups[i]);
      result %= MOD;
      special_result = (special_result + (f_result * groups[i])) % MOD;
      continue;
    }
    result = (result + (groups[i] * sum)) % MOD;
    f_result = sum;
    groups_sum = (groups_sum + groups[i]) % MOD;
    sum = (sum + groups_sum) % MOD;
    last_group = groups[i];
  }

  return reversed ? special_result : result;
}

int main() {
  int T;
  std::cin >> T;
  FORN(t, T) {
    int n;
    std::string text;
    std::cin >> n >> text;
    n = text.length();
    std::vector<ll> groups;
    std::vector<char> isF;
    groups.reserve(n);
    isF.reserve(n);
    char last = ' ';
    ll cur = 0;
    FORN(i, n) {
      char c = text[i];

      if (i == 0) {
        cur = 1;
        last = c;
        continue;
      }

      if (last != c) {
        groups.push_back(cur);
        isF.push_back(last);
        cur = 1;
      } else {
        cur++;
      }

      last = c;
      if (i == n - 1) {
        groups.push_back(cur);
        isF.push_back(last);
      }
    }

    // if (isF[0] == 'F' && groups.size() > 1) {
    //   groups[1] += groups[0];
    //   groups[0] = 0;
    //   isF[0] = ' ';
    // }

    // if (groups.size() > 1 && isF[isF.size() - 1] == 'F') {
    //   groups[groups.size() - 2] += groups[groups.size() - 1];
    //   groups[groups.size() - 1] = 0;
    //   isF[isF.size() - 1] = ' ';
    // }

    FORN(i, groups.size()) {
      if (isF[i] == 'F') {
        bool middle =
            i != 0 && i != groups.size() - 1 && isF[i - 1] == isF[i + 1];
        if (middle) {
          groups[i + 1] += groups[i] + groups[i - 1];
          groups[i] = 0;
          groups[i - 1] = 0;
          isF[i] = false;
          isF[i - 1] = false;
        }
      }
    }

    ll result = get_result(groups, isF, false);
    result = (result + get_for_f_f(groups, isF)) % MOD;

    std::reverse(groups.begin(), groups.end());
    std::reverse(isF.begin(), isF.end());
    // result = (result + get_result(groups, isF, true)) % MOD;

    std::cout << "Case #" << (t + 1) << ": " << result
              << " :: " << brute_force(text.length(), text) << "\n";
  }
}
