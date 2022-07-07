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
#define INVALID_INT(x) ((x) < 0 || (x) > INT_MAX)

ll to_int(const std::string& num, int i, int j) {
  if (j - i > 11) return -1;
  if (j - i >= 2 && num[i] == '0') return -1;

  ll a = 0;
  while (i < j) {
    a = a * 10 + (num[i] - '0');
    i++;
  }

  return a;
}

int digits(ll a) {
  int k = 0;
  while (a > 0) {
    a /= 10;
    k++;
  }
  return k == 0 ? 1 : k;
}

bool match_segment(const std::string& num, ll c, int index) {
  const int n = num.length();
  if (c == 0) {
    return index < n && num[index] == '0';
  }

  index += digits(c) - 1;
  if (index >= n) return false;

  while (c > 0) {
    if (num[index] - '0' != (c % 10)) return false;
    index--;
    c /= 10;
  }

  return true;
}

std::vector<int> check_from(const std::string& num, ll a, ll b) {
  const int n = num.length();
  int i = digits(a) + digits(b);
  std::vector<int> result;
  result.push_back(a);
  result.push_back(b);

  ll c = a + b;
  while (i < n) {
    if (c > INT_MAX) return {};
    if (!match_segment(num, c, i)) {
      return {};
    }

    result.push_back(c);
    i += digits(c);
    a = c;
    c = b + c;
    b = a;
  }

  if (i == n)
    return result;
  else
    return {};
}

class Solution {
 public:
  std::vector<int> splitIntoFibonacci(std::string num) {
    const int n = num.length();

    for (int j = 1; j < (n > 12 ? 12 : n); j++) {
      const auto a = to_int(num, 0, j);
      if (INVALID_INT(a)) continue;

      for (int k = j + 1; k < n; k++) {
        const auto b = to_int(num, j, k);
        if (INVALID_INT(b)) continue;
        const auto res = check_from(num, a, b);
        if (!res.empty()) {
          return res;
        }
      }
    }

    return {};
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto k = s.splitIntoFibonacci("00246");
  for (auto r : k) {
    LOG(r);
  }
  LOG("done");
}
#endif
