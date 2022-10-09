#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Parser {
 public:
  int i = 0;
  string expr;
  Parser() {}
  Parser(string& s) { this->expr = s; }

  bool is_special(char c) { return c == ')' || c == '('; }

  string next() {
    if (!hasNext()) throw std::runtime_error("does not have next");

    string r = "";
    if (is_special(expr[i])) {
      r = string(1, expr[i++]);
    } else {
      while (hasNext() && expr[i] != ' ' && !is_special(expr[i])) r += expr[i++];
    }

    while (hasNext() && expr[i] == ' ') i++;

    return r;
  }

  bool isParenthesis() { return expr[i] == '('; }
  bool isEndParenthesis() { return expr[i] == ')'; }

  bool hasNext() { return i != expr.length(); }
};

class Context {
 public:
  unordered_map<string, stack<int>> values;

  int get(const string& key) { return values[key].top(); }
  int len(const string& key) { return values.find(key) == values.end() ? 0 : values[key].size(); }
  void add(const string& key, int value) { values[key].push(value); }
  void pop(const string& key) { values[key].pop(); }
};

class Solution {
 public:
  Context context;
  Parser p;

  int eval_val(const string& name) {
    return isdigit(name[0]) || name[0] == '-' ? stoi(name) : context.get(name);
  }

  int readAddMult(string op) {
    int val1 = p.isParenthesis() ? read() : eval_val(p.next());
    int val2 = p.isParenthesis() ? read() : eval_val(p.next());

    return op == "add" ? val1 + val2 : val1 * val2;
  }

  int readLet() {
    int result = 0;
    vector<string> scope;

    while (p.hasNext()) {
      if (p.isParenthesis()) {
        result = read();
        break;
      }

      auto name = p.next();
      if (!p.hasNext() || p.isEndParenthesis()) {
        result = eval_val(name);
        break;
      }

      int val = p.isParenthesis() ? read() : eval_val(p.next());
      context.add(name, val);
      scope.push_back(name);
    }

    for (auto&& key : scope) context.pop(key);
    return result;
  }

  int read() {
    if (!p.hasNext()) return 0;

    if (p.isParenthesis()) {
      p.next();
      auto res = read();
      p.next();
      return res;
    }

    auto op = p.next();
    if (op == "let") return readLet();
    if (op == "add" || op == "mult") return readAddMult(op);

    throw std::runtime_error("invalid token: " + op);
  }

  int evaluate(string expression) {
    p = Parser(expression);

    return read();
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  Input input;
  auto v = input.next_string();
  auto res = s.evaluate(v);
  LOG2("Result ", res);
  LOG2("Correct", input.next_int());
}
#endif
