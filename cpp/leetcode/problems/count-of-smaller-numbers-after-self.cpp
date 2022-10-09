
#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

#define N 20010
int tree[N];
int getSum(int index) {
  int sum = 0;
  index++;

  while (index > 0) {
    sum += tree[index];
    index -= index & (-index);
  }
  return sum;
}

void updateBIT(int index, int val) {
  index++;
  while (index <= N) {
    tree[index] += val;
    index += index & (-index);
  }
}

class Solution {
 public:
  vector<int> countSmaller(vector<int>& nums) {
    memset(tree, 0, sizeof(tree));
    int n = nums.size();
    vector<int> res(n, 0);
    for (int i = n - 1; i >= 0; i--) {
      res[i] = getSum(nums[i] + 10000 - 1);

      updateBIT(nums[i] + 10000, 1);
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
