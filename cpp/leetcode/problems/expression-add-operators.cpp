#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

typedef long long int ll;
ll calculate(vector<char> &s, int length) {
  ll cur = 0, last = 0, result = 0;
  char sign = '+';
  for (int i = 0; i < length; i++) {
    char c = s[i];
    if (isdigit(c)) {
      cur = (cur * 10) + (c - '0');
    }
    if (!isdigit(c) && !iswspace(c) || i == length - 1) {
      if (sign == '+' || sign == '-') {
        result += last;
        last = (sign == '+') ? cur : -cur;
      } else if (sign == '*') {
        last = last * cur;
      } else if (sign == '/') {
        last = last / cur;
      }
      sign = c;
      cur = 0;
    }
  }
  return result + last;
}

void check(string &num, int i, vector<char> &cur, int curI, vector<string> &res, int target) {
  if (i == num.length()) {
    if (calculate(cur, curI) == target) res.push_back(string(cur.begin(), cur.begin() + curI));
    return;
  }

  cur[curI + 1] = num[i];

  cur[curI] = '*';
  check(num, i + 1, cur, curI + 2, res, target);
  cur[curI] = '+';
  check(num, i + 1, cur, curI + 2, res, target);
  cur[curI] = '-';
  check(num, i + 1, cur, curI + 2, res, target);

  bool isLeading = cur[curI - 1] == '0' && (curI == 1 || !isdigit(cur[curI - 2]));
  if (!isLeading) {
    cur[curI] = num[i];
    check(num, i + 1, cur, curI + 1, res, target);
  }
}

class Solution {
 public:
  vector<string> addOperators(string num, int target) {
    vector<string> res;
    vector<char> cur(30);
    cur[0] = num[0];

    check(num, 1, cur, 1, res, target);

    return res;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  auto res = s.addOperators("1234", 6);
  LOG2("size", res.size());
  for (auto r : res) LOG(r);
}
#endif
