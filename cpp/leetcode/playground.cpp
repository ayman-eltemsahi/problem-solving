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
  Solution s;
  utils::Input in("../input.txt");
  auto vec = in.next_vector_int();
  auto result = s.test(vec);
  utils::expect_equal(result, in);
}
#endif
