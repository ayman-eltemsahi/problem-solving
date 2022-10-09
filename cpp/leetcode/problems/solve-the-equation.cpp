#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

struct equation_side {
  int x, num;
};

equation_side parse_eq(string& expr) {
  int n = expr.length();

  equation_side res{0, 0};
  int sign = 1;

  int i = 0;
  while (i < n) {
    if (expr[i] == '-') {
      sign = -1;
      i++;
    } else if (expr[i] == '+') {
      sign = 1;
      i++;
    } else if (expr[i] == 'x') {
      res.x += sign * 1;
      i++;
    } else {
      int k = 0;
      while (i < n && isdigit(expr[i])) {
        k = k * 10 + (expr[i] - '0');
        i++;
      }

      if (i < n && expr[i] == 'x') {
        res.x += sign * k;
        i++;
      } else {
        res.num += sign * k;
      }

      sign = 1;
    }
  }

  return res;
}

class Solution {
 public:
  string solveEquation(string equation) {
    int n = equation.length();
    string left_str, right_str;
    for (int i = 0; i < n; i++) {
      if (equation[i] == '=') {
        left_str = equation.substr(0, i);
        right_str = equation.substr(i + 1);
        break;
      }
    }

    auto left = parse_eq(left_str);
    auto right = parse_eq(right_str);

    int x = left.x - right.x;
    int num = right.num - left.num;

    if (x == 0 && num == 0) return "Infinite solutions";
    if (x == 0 && num != 0) return "No solution";

    return "x=" + std::to_string(num / x);
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  auto res = s.solveEquation("x+5-3+x=6+x-2");
  LOG(res);
}
#endif
