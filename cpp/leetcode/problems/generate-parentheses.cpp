#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <unordered_set>

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
  std::vector<std::string> generateParenthesis(int n) {
    std::unordered_set<std::string> s[10];

    s[1].insert("()");

    for (int i = 1; i <= n; i++) {
      for (int j = 1; i + j <= n; j++) {
        for (const auto item : s[i]) {
          s[i + 1].insert("(" + item + ")");
          for (const auto item2 : s[j]) {
            s[i + j].insert(item + item2);
            s[i + j].insert(item2 + item);
          }
        }
      }
    }

    return std::vector<std::string>(s[n].begin(), s[n].end());
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto k = s.generateParenthesis(3);
  for (const auto i : k) {
    LOG(i);
  }
}
#endif
