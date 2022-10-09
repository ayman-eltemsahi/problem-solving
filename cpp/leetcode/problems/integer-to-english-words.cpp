#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

vector<string> ones = {"Zero",    "One",     "Two",       "Three",    "Four",
                       "Five",    "Six",     "Seven",     "Eight",    "Nine",
                       "Ten",     "Eleven",  "Twelve",    "Thirteen", "Fourteen",
                       "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"};

vector<string> tens = {"",      "",        "Twenty", "Thirty", "Forty",  "Fifty",
                       "Sixty", "Seventy", "Eighty", "Ninety", "Hundred"};

vector<string> thousands = {"Thousand", "Million", "Billion"};

class Solution {
 public:
  string convert(int num) {
    if (num <= 19) return ones[num];
    if (num < 100) {
      auto rem = num % 10 == 0 ? "" : " " + ones[num % 10];
      return tens[num / 10] + rem;
    }

    if (num < 1000) {
      auto rem = num % 100 == 0 ? "" : " " + convert(num % 100);
      return convert(num / 100) + " Hundred" + rem;
    }

    int i = 0;
    long long limit = 1000000;
    while (num >= limit) {
      i++;
      limit *= 1000;
    }

    limit /= 1000;
    auto rem = num % limit == 0 ? "" : " " + convert(num % limit);
    return convert(num / limit) + " " + thousands[i] + rem;
  }

  string numberToWords(int num) {
    auto res = convert(num);
    LOG2(num, res);
    return res;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;

  s.numberToWords(10);
  s.numberToWords(11);
  s.numberToWords(16);
  s.numberToWords(20);
  s.numberToWords(66);
  s.numberToWords(100);
  s.numberToWords(699);
  s.numberToWords(619);
  s.numberToWords(619);
  s.numberToWords(1209);
  s.numberToWords(12345);
  s.numberToWords(709145);
  s.numberToWords(1234567);
}
#endif
