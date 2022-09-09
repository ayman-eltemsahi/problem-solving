#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Solution {
 public:
  int test(vector<int>& arr) {
    //
    return 42;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  utils::Input in("../input.txt");
  while (in.hasNext() && !in.peek().empty()) {
    Solution s;
    auto vec = in.next_vector_int();
    auto result = s.test(vec);
    utils::expect_equal(result, in);
  }
}
#endif
