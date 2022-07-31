#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Solution {
 public:
  bool test() {
    return true;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
