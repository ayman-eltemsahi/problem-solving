#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Reader {
 public:
  int index;
  string value;
  Reader(string& s) : index(0), value(s) {}

  int read_num() {
    int r = 0;
    while (index < value.length() && isdigit(value[index])) {
      r = r * 10 + (value[index++] - '0');
    }
    return r;
  }

  char is_num() { return isdigit(value[index]); }
  char read_char() { return value[index++]; }
  char peek() { return value[index]; }
  bool has_next() { return index < value.length(); }
};

class Solution {
 public:
  int eval_stk(vector<variant<int, char>>& stk) {
    int res = get<int>(stk[0]);
    for (int i = 1; i < stk.size(); i += 2) {
      if (get<char>(stk[i]) == '+') {
        res += get<int>(stk[i + 1]);
      } else {
        res -= get<int>(stk[i + 1]);
      }
    }

    return res;
  }

  int solve(Reader& reader) {
    vector<variant<int, char>> stk;

    while (reader.has_next()) {
      char c = reader.peek();
      if (c == ')') {
        reader.read_char();
        return eval_stk(stk);
      }

      if (c == ' ') {
        reader.read_char();
        continue;
      }

      if (c == '*' || c == '/' || c == '+' || c == '-') {
        stk.push_back(reader.read_char());
      } else {
        if (c == '(') reader.read_char();
        int num = c == '(' ? solve(reader) : reader.read_num();

        if (!stk.empty() && (get<char>(stk.back()) == '/' || get<char>(stk.back()) == '*')) {
          int b = num;
          auto op = get<char>(stk.back());
          stk.pop_back();
          int a = get<int>(stk.back());
          stk.pop_back();
          stk.push_back(op == '*' ? (a * b) : (a / b));
        } else {
          stk.push_back(num);
        }
      }
    }

    return eval_stk(stk);
  }

  int calculate(string s) {
    Reader reader{s};
    return solve(reader);
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  utils::Input in("../input.txt");
  while (in.has_next() && !in.peek().empty()) {
    Solution s;
    auto a = in.s();
    auto result = s.calculate(a);
    utils::expect_equal(result, in);
  }
}
#endif
