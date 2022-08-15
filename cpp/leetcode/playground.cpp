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
  utils::Input input;
  auto vec = input.next_vector_int();
  // auto result = s.test();
  // DEBUG(result);
}
#endif
