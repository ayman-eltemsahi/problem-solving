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

class Solution {
public:
  int maximum69Number(int num) {
    int rev = 0;
    while (num > 0) {
      rev = rev * 10 + (num % 10);
      num /= 10;
    }

    bool flag = false;
    while (rev > 0) {
      if (!flag && rev % 10 == 6) {
        num = num * 10 + 9;
        flag = true;
      } else
        num = num * 10 + (rev % 10);
      rev /= 10;
    }
    return num;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.reverse(-2147483648) == 0);
  assert(s.reverse(120) == 21);
  assert(s.reverse(0) == 0);
  assert(s.reverse(-123) == -321);
}
#endif
