#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

int get_sum(int tree[], int index) {
  int sum = 0;
  index++;

  while (index > 0) {
    sum += tree[index];
    index -= index & (-index);
  }
  return sum;
}

void update_tree(int tree[], int n, int index, int val) {
  index++;

  while (index <= n) {
    tree[index] += val;
    index += index & (-index);
  }
}

class Solution {
 public:
  bool canJump(std::vector<int>& nums) {
    const auto n = nums.size();
    int* tree = new int[n + 1];
    FORN(i, n + 1) tree[i] = 0;
    update_tree(tree, n, n - 1, 1);

    for (int i = n - 2; i >= 0; i--) {
      int topIndex = nums[i] + i;
      if (topIndex >= n || get_sum(tree, topIndex)) {
        update_tree(tree, n, i, 1);
      }
    }

    return get_sum(tree, 0);
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  std::vector<int> nums{2, 0};
  LOG(s.canJump(nums));
}
#endif
