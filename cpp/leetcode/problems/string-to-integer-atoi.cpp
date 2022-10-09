#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);

std::string remove_leading_spaces(const std::string &s) {
  int i = 0;
  while (i < s.size() && s[i] == ' ')
    i++;
  return s.substr(i);
}

std::string read_digits(const std::string &s) {
  int i = 0;
  while (i < s.size() && s[i] >= '0' && s[i] <= '9')
    i++;
  return s.substr(0, i);
}

class Solution {
public:
  int myAtoi(std::string s) {
    s = remove_leading_spaces(s);
    if (s.size() == 0)
      return 0;

    int sign = 1;
    if (s[0] == '+' || s[0] == '-') {
      if (s[0] == '-')
        sign = -1;
      s = s.substr(1);
    }
    s = read_digits(s);
    ll ans = 0;
    FORN(i, s.size()) {
      ans = ans * 10 + (s[i] - '0');
      if (ans > (1L << 40) || ans < -(1L << 40))
        break;
    }
    ans *= sign;
    if (ans < -(1L << 31))
      ans = -(1L << 31);
    if (ans > (1L << 31) - 1)
      ans = (1L << 31) - 1;
    return (int)ans;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.myAtoi("42") == 42);
  assert(s.myAtoi("  -42") == -42);
  assert(s.myAtoi("4193 with words") == 4193);
  assert(s.myAtoi("words and 987") == 0);
  assert(s.myAtoi("-91283472332") == -2147483648);
  assert(
      s.myAtoi(
          "91283472332912834723329128347233291283472332912834723329128347233291"
          "2834723329128347233291283472332912834723329128347233291283472332") ==
      2147483647);
}
#endif
