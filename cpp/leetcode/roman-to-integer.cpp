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
using std::string;
using std::vector;

const vector<int> NUMBERS = {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1};
const vector<string> ROMANS = {"M",  "CM", "D",  "CD", "C",  "XC", "L",
                               "XL", "X",  "IX", "V",  "IV", "I"};

int match(string s) {
  FORN(i, NUMBERS.size()) {
    if (ROMANS[i] == s) return NUMBERS[i];
  }
  return 0;
}

class Solution {
 public:
  int romanToInt(string s) {
    int r = 0;
    FORN(i, s.size()) {
      if (i != s.size() - 1 && match(s.substr(i, 2))) {
        r += match(s.substr(i, 2));
        i++;
      } else {
        r += match(s.substr(i, 1));
      }
    }

    return r;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.romanToInt("III") == 3);
  assert(s.romanToInt("IV") == 4);
  assert(s.romanToInt("IX") == 9);
  assert(s.romanToInt("LVIII") == 58);
  assert(s.romanToInt("MCMXCIV") == 1994);
}
#endif
