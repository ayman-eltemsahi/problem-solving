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

void reverse(vector<int>& nums, int i, int j) {
  while (i < j) {
    auto tmp = nums[i];
    nums[i] = nums[j];
    nums[j] = tmp;
    i++;
    j--;
  }
}

class Solution {
 public:
  void nextPermutation(vector<int>& nums) {
    if (nums.size() < 2) return;

    int r = -1;
    int n = nums.size();
    for (int i = n - 2; i >= 0; i--) {
      if (nums[i] < nums[i + 1]) {
        r = i;
        break;
      }
    }

    if (r == -1) {
      reverse(nums, 0, n - 1);
      return;
    }

    int after = r + 1;
    for (int i = r + 1; i < n; i++) {
      if (nums[i] > nums[r] && nums[i] <= nums[after]) {
        after = i;
      }
    }

    auto tmp = nums[after];
    nums[after] = nums[r];
    nums[r] = tmp;

    reverse(nums, r + 1, n - 1);
  }
};
