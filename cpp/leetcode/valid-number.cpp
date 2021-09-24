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
#define MAXN 2001

bool is_digit(char c) { return c >= '0' && c <= '9'; }
bool is_valid_first(char c) {
  return c == '+' || c == '-' || c == '.' || is_digit(c);
}
int find_single_index(const std::string &s, char c) {
  int index = -1;
  FORN(i, s.length()) {
    if (tolower(s[i]) == c) {
      if (index > -1)
        return -2;
      index = i;
    }
  }

  return index;
}

bool is_all_digits(std::string s) {
  for (const auto c : s)
    if (c < '0' || c > '9')
      return false;
  return true;
}

bool is_integer(std::string s) {
  if (s.size() == 0)
    return false;
  if (s[0] == '+' || s[0] == '-')
    return s.size() > 1 && is_all_digits(s.substr(1));
  return is_all_digits(s);
}

bool is_decimal(std::string s) {
  if (s.size() == 0)
    return false;
  if (s[0] == '+' || s[0] == '-')
    s = s.substr(1);

  int dot_index = find_single_index(s, '.');
  if (dot_index == -2)
    return false;
  std::string before = dot_index == -1 ? s : s.substr(0, dot_index);
  std::string after =
      dot_index == -1 ? "" : s.substr(dot_index + 1, s.size() - dot_index - 1);

  if (before.size() == 0 && after.size() == 0)
    return false;
  return is_all_digits(before) && is_all_digits(after);
}

class Solution {
public:
  bool isNumber(std::string s) {
    int e = find_single_index(s, 'e');
    if (e == -2)
      return false;
    std::string before = e == -1 ? s : s.substr(0, e);
    std::string after = e == -1 ? "" : s.substr(e + 1, s.size() - e - 1);
    return (is_integer(before) || is_decimal(before)) &&
           (e == -1 || is_integer(after));
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<std::string> valid{"2",    "0089",  "-0.1",    "+3.14",
                                 "4.",   "-.9",   "2e10",    "-90E3",
                                 "3e+7", "+6e-1", "53.5e93", "-123.456e789"};
  std::vector<std::string> not_valid{"abc",    "1a",  "1e",  "e3",
                                     "99e2.5", "--6", "-+3", "95a54e53"};

  for (const auto &val : valid)
    if (s.isNumber(val) != true)
      assert(s.isNumber(val) == true);

  for (const auto &val : not_valid)
    assert(s.isNumber(val) == false);
}
#endif
