#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <stack>
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
  std::stack<int> stack_s;
  std::stack<int> stack_p;
  stack_s.push(s);
  stack_p.push(p);

  while (!stack_s.empty()) {
    s = stack_s.top();
    p = stack_p.top();
    stack_s.pop();
    stack_p.pop();

    if (p >= pattern.size() && s >= text.size())
      return true;
    if (p >= pattern.size() && s != text.size())
      continue;

    const bool repeat = p + 1 < pattern.size() && pattern[p + 1] == '*';
    if (!repeat) {
      if (s < text.size() && (text[s] == pattern[p] || pattern[p] == '.')) {
        stack_s.push(s + 1);
        stack_p.push(p + 1);
      }
      continue;
    }

    stack_s.push(s);
    stack_p.push(p + 2);

    if (pattern[p] == '.') {
      for (int k = 0; s + k < text.size(); k++) {
        stack_s.push(s + k + 1);
        stack_p.push(p + 2);
      }
    } else {
      for (int k = 0; s + k < text.size() && text[s + k] == pattern[p]; k++) {
        stack_s.push(s + k + 1);
        stack_p.push(p + 2);
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
  assert(s.isMatch("a", ".*..a*") == false);
  assert(s.isMatch("aa", "a") == false);
  assert(s.isMatch("aa", "a*") == true);
  assert(s.isMatch("ab", ".*") == true);
  assert(s.isMatch("aab", "c*a*b") == true);
  assert(s.isMatch("mississippi", "mis*is*p*.") == false);
}
#endif
