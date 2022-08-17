#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Solution {
 public:
  bool test() { return true; }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  utils::Input in;
  int n = in.next_int();
  auto vec = in.next_vector_int();
  // auto result = s.test();
  // utils::expect_equal(result, in);
}
#endif
