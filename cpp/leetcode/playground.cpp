#if defined(RUNNING_LOCALLY)
#include "local-stuff.hpp"
#endif

class Solution {
 public:
  bool test() {
    return true;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
