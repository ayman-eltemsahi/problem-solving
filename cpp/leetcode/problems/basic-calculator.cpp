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
  int solve(Reader& reader) {
    int res = 0;
    char op = ' ';

    while (reader.has_next()) {
      if (reader.is_num()) {
        auto p = reader.read_num();
        if (op != ' ') {
          res = op == '+' ? res + p : res - p;
          op = ' ';
        } else {
          res = p;
        }
        continue;
      }

      char c = reader.read_char();
      switch (c) {
        case '(': {
          auto p = solve(reader);

          if (op != ' ') {
            res = op == '+' ? res + p : res - p;
            op = ' ';
          } else {
            res = p;
          }
          break;
        }

        case '+':
        case '-':
          op = c;
          break;

        case ')':
          return res;
      }
    }

    return res;
  }

  int calculate(string s) {
    Reader reader{s};
    return solve(reader);
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  LOG(s.calculate("(1+(4+1345+2)-3)+(1346+8)"));
}
#endif
