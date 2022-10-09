#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

using std::string;

class Solution {
 public:
  bool canBeValid(string s, string locked) {
    if (s.length() % 2 == 1) return false;
    int any = 0, open = 0;

    for (int i = 0; i < s.length(); i++) {
      if (locked[i] == '0') {
        any++;
      } else {
        if (s[i] == '(')
          open++;
        else
          open--;
      }

      if (any + open < 0) return false;
    }

    if (open > any) return false;

    any = open = 0;
    for (int i = s.length() - 1; i >= 0; i--) {
      if (locked[i] == '0') {
        any++;
      } else {
        if (s[i] == ')')
          open++;
        else
          open--;
      }

      if (any + open < 0) return false;
    }

    return open <= any;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.canBeValid("))()))", "010100") == true);
  assert(s.canBeValid("()()", "0000") == true);
  assert(s.canBeValid(")", "0") == false);
  assert(s.canBeValid("((()(()()))()((()()))))()((()(()", "10111100100101001110100010001001") ==
         true);
}
#endif
