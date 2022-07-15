#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

struct pair_3 {
  int value, index, cost;
};

inline int mn(int a, int b) {
  return a < b ? a : b;
}

// a & b = 0
//    a => 0
//    b => 0

// a | b = 0
//    a => 0, b => 0
//    a => 0, | => &
//    b => 0, | => &

// a & b = 1
//    a => 1, b => 1
//    a => 1, & => |
//    b => 1, & => |

// a | b = 1
//    a => 1
//    a => 1

int get_cost(const vector<pair<char, int>> &ops, int target, int index) {
  if (index == 0) return ops[index].first == target ? 0 : ops[index].second;
  const auto op = ops[index - 1].first;

  const int curr_val = ops[index].first;
  const int curr_cost = ops[index].second;

  if (op == '&' && target == 0) {
    auto a = curr_val == 0 ? 0 : curr_cost;
    if (!a) return a;
    auto b = get_cost(ops, 0, index - 2);
    return mn(a, b);
  }

  if (op == '|' && target == 1) {
    auto a = curr_val == 1 ? 0 : curr_cost;
    if (!a) return a;
    auto b = get_cost(ops, 1, index - 2);
    return mn(a, b);
  }

  if (op == '|' && target == 0) {
    auto b_to_0 = get_cost(ops, 0, index - 2);
    auto ab = (curr_val == 0 ? 0 : curr_cost) + b_to_0;
    auto a_ = (curr_val == 0 ? 0 : curr_cost) + 1;
    auto _b = 1 + b_to_0;
    return mn(ab, mn(a_, _b));
  }

  if (op == '&' && target == 1) {
    auto b_to_1 = get_cost(ops, 1, index - 2);
    auto ab = (curr_val == 1 ? 0 : curr_cost) + b_to_1;
    auto a_ = (curr_val == 1 ? 0 : curr_cost) + 1;
    auto _b = 1 + b_to_1;
    return mn(ab, mn(a_, _b));
  }

  assert(false);
}

pair_3 evaluate_with_cost(const string &expression, int start) {
  int n = expression.length();
  if (n == start) return pair_3{0, start, 0};
  const auto c = expression[start];

  int i = start;
  vector<pair<char, int>> ops;
  while (i < n && expression[i] != ')') {
    if (expression[i] == '&' || expression[i] == '|') {
      ops.push_back({expression[i++], 1});
      continue;
    }

    if (expression[i] == '(') {
      const auto p = evaluate_with_cost(expression, i + 1);
      ops.push_back({p.value, p.cost});
      i = p.index + 1;
      continue;
    }

    ops.push_back({expression[i++] - '0', 1});
  }

  int val = ops[0].first;
  for (int x = 1; x < ops.size(); x += 2) {
    const auto op = ops[x].first;
    const int v = ops[x + 1].first;
    val = op == '&' ? (val & v) : (val | v);
  }

  int cost = get_cost(ops, val ^ 1, ops.size() - 1);

  return pair_3{val, i, cost};
}

class Solution {
 public:
  int minOperationsToFlip(string expression) {
    return evaluate_with_cost(expression, 0).cost;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // LOG(s.minOperationsToFlip("1|(0|1)"));
  assert(s.minOperationsToFlip("1&(1|1)") == 1);
  assert(s.minOperationsToFlip("1|(0|1)&((1&1|0|(1|0)))|(1&(1|0|1))") == 2);
  assert(s.minOperationsToFlip("1&(0|1)") == 1);
  assert(s.minOperationsToFlip("(0&0)&(0&0&0)") == 3);
  assert(s.minOperationsToFlip("(0|(1|0&1))") == 1);
}
#endif
