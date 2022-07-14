#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

class Solution {
 public:
  int totalHammingDistance(vi& nums) {
    int res = 0;
    FORN(b, 32) {
      int zeros = 0, ones = 0;
      for (const auto num : nums) {
        if ((num >> b) & 1) {
          res += zeros;
          ones++;
        } else {
          res += ones;
          zeros++;
        }
      }
    }

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
