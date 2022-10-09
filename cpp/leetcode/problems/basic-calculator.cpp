#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

pair<int, int> read_num(string& s, int i) {
  int r = 0;

  while (isdigit(s[i])) {
    r = r * 10 + s[i] - '0';
    i++;
  }
  return {r, i};
}

pair<int, int> solve(string& s, int i) {
  int res = 0;
  char op = ' ';
  while (i < s.length()) {
    char c = s[i];
    if (c == ' ') {
      i++;
    } else if (c == '(') {
      auto p = solve(s, i + 1);
      i = p.second;
      if (op != ' ') {
        res = op == '+' ? res + p.first : res - p.first;
        op = ' ';
      } else {
        res = p.first;
      }
    } else if (c == '+' || c == '-') {
      op = c;
      i++;
    } else if (c == ')') {
      return {res, i + 1};
    } else if (isdigit(c)) {
      auto p = read_num(s, i);
      i = p.second;
      if (op != ' ') {
        res = op == '+' ? res + p.first : res - p.first;
        op = ' ';
      } else {
        res = p.first;
      }
    }
  }

  return {res, i};
}

class Solution {
 public:
  int calculate(string s) {
    return solve(s, 0).first;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  LOG(s.calculate("(1+(4+1345+2)-3)+(1346+8)"));
}
#endif
