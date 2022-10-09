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
const int LITERAL = 'e';
const int WILDCARD = '*';
const int ANY_CHAR = '?';

class Regex {
public:
  char type;
  char val;

  Regex(char t, char v) : type(t), val(v) {}
};

std::vector<Regex> parse_regex(const std::string &pattern) {
  std::vector<Regex> reg;
  for (const auto c : pattern) {
    if (c == '*') {
      reg.push_back(Regex(WILDCARD, '*'));
    } else if (c == '?') {
      reg.push_back(Regex(ANY_CHAR, '?'));
    } else {
      reg.push_back(Regex(LITERAL, c));
    }
  }
  return reg;
}

class WildCardBackTrack {
public:
  int i_start;
  int i_end;
  int r;

  int key() const { return r * 1000000 + i_start * 1000 + i_end; }
};

bool is_match(const std::string &text, const std::string &pattern) {
  const std::vector<Regex> reg = parse_regex(pattern);
  std::stack<WildCardBackTrack> backtrack_stack;
  std::unordered_set<int> seen;

  int n = text.length();
  int i = 0;
  int r = 0;

  const auto can_backtrack = [&backtrack_stack, &seen, &i, &r]() {
    if (backtrack_stack.empty())
      return false;

    WildCardBackTrack &b = backtrack_stack.top();
    r = b.r;
    i = b.i_end;
    if (b.i_end - 1 >= b.i_start) {
      b.i_end--;
      if (seen.find(b.key()) != seen.end()) {
        backtrack_stack.pop();
      }
    } else {
      backtrack_stack.pop();
    }
    return true;
  };

  while (r < reg.size()) {
    switch (reg[r].type) {
      //
    case LITERAL:
      if (i < n && reg[r].val == text[i]) {
        i++;
        r++;
        continue;
      }
      if (can_backtrack())
        continue;

      return false;

      //
    case ANY_CHAR:
      if (i < n) {
        i++;
        r++;
        continue;
      }
      if (can_backtrack())
        continue;

      return false;

      //
    case WILDCARD:
      if (n - 1 >= i) {
        WildCardBackTrack backtrack;
        backtrack.r = r + 1;
        backtrack.i_start = i;
        backtrack.i_end = n - 1;
        int key = backtrack.key();
        if (seen.find(key) == seen.end()) {
          backtrack_stack.push(backtrack);
          seen.insert(key);
        }
      }
      i = n;
      r++;
      continue;
    }
  }

  return r == reg.size() && i == n;
}

class Solution {
public:
  bool isMatch(std::string s, std::string p) { return is_match(s, p); }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.isMatch("a", "*a") == true);
  assert(s.isMatch("ab", "?*") == true);
  assert(s.isMatch("adceb", "*a*b") == true);
  assert(s.isMatch("aa", "a") == false);
  assert(s.isMatch("", "?") == false);
  assert(s.isMatch("aa", "*") == true);
  assert(s.isMatch("cb", "?a") == false);
  assert(s.isMatch("acdcb", "a*c?b") == false);
  assert(s.isMatch("aaaabaaaabbbbaabbbaabbaababbabbaaaababaaabbbbbbaabbbabababb"
                   "aaabaabaaaaaabbaabbbbaababbababaabbbaababbbba",
                   "*****b*aba***babaa*bbaba***a*aaba*b*aa**a*b**ba***a*a*") ==
         true);
  assert(s.isMatch(std::string(1000, 'a') + std::string(1000, 'b'),
                   std::string(2000, '*')) == true);
}
#endif
