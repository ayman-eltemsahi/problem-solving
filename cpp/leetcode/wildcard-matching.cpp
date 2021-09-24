#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <stack>
#include <unordered_set>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 100001

bool is_match(const std::string &text, const std::string &pattern, int s,
              int p) {
  std::stack<std::pair<int, int>> stack;
  std::unordered_set<int> seen;
  stack.push({s, p});
  seen.insert(s * 10000 + p);

  while (!stack.empty()) {
    s = stack.top().first;
    p = stack.top().second;
    stack.pop();

    if (p >= pattern.size() && s >= text.size())
      return true;
    if (p >= pattern.size())
      continue;

    if (s < text.size() && (pattern[p] == '?' || pattern[p] == text[s])) {
      int key = (s + 1) * 10000 + (p + 1);
      if (seen.find(key) == seen.end()) {
        stack.push({s + 1, p + 1});
        seen.insert(key);
      }
      continue;
    }

    if (pattern[p] == '*') {
      for (int k = 0; s + k <= text.size(); k++) {
        int key = (s + k) * 10000 + (p + 1);
        if (seen.find(key) == seen.end()) {
          stack.push({s + k, p + 1});
          seen.insert(key);
        }
      }
    }
  }

  return false;
}

class Solution {
public:
  bool isMatch(std::string s, std::string p) { return is_match(s, p, 0, 0); }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.isMatch("", "?") == false);
  assert(s.isMatch("aa", "a") == false);
  assert(s.isMatch("aa", "*") == true);
  assert(s.isMatch("cb", "?a") == false);
  assert(s.isMatch("adceb", "*a*b") == true);
  assert(s.isMatch("acdcb", "a*c?b") == false);
  assert(s.isMatch(std::string(1000, 'a') + std::string(1000, 'b'),
                   std::string(2000, '*')) == true);
}
#endif
