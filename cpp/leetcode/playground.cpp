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
  while (in.has_next() && !in.peek().empty()) {
    Solution s;
    auto a = in.vi();
    auto result = s.test(a);
    utils::expect_equal(result, in);
  }
}
#endif
