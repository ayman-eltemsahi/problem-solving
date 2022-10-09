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

class Solution {
 public:
  string intToRoman(int num) {
    std::stringstream ss;

    for (size_t i = 0; i < NUMBERS.size(); i++) {
      if (num >= NUMBERS[i]) {
        ss << ROMANS[i];
        num -= NUMBERS[i];
        i--;
      }
    }

    return ss.str();
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.intToRoman(3) == "III");
  assert(s.intToRoman(4) == "IV");
  assert(s.intToRoman(9) == "IX");
  assert(s.intToRoman(58) == "LVIII");
  assert(s.intToRoman(1994) == "MCMXCIV");
}
#endif
