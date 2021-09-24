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

class Solution {
public:
  std::string convert(std::string s, int numRows) {
    std::vector<std::vector<char>> vs;
    FORN(i, numRows) vs.push_back(std::vector<char>{});

    int index = 0;
    int inc = +1;
    for (const char c : s) {
      vs[index].push_back(c);
      index += inc;
      if (index == numRows) {
        inc = -1;
        index = std::max(0, index - 2);
      } else if (index == -1) {
        inc = 1;
        index = std::min(numRows - 1, index + 2);
      }
    }

    std::stringstream ss;
    for (const auto v : vs) {
      for (const char c : v) {
        ss << c;
      }
    }

    return ss.str();
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.convert("PAYPALISHIRING", 4) == "PINALSIGYAHRPI");
  assert(s.convert("PAYPALISHIRING", 3) == "PAHNAPLSIIGYIR");
  assert(s.convert("PAYPALISHIRING", 1) == "PAYPALISHIRING");
  assert(s.convert("P", 1) == "P");
}
#endif
