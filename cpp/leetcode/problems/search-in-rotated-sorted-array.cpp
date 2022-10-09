#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <vector>

using std::vector;
#define LOG(a) std::cout << (a) << "\n"

int get_peak_index(vector<int>& nums) {
  int l = 0, h = nums.size() - 1;
  if (nums[h] > nums[l]) return h;

  while (l < h - 1) {
    int mid = (l + h) / 2;
    if (nums[mid] > nums[h]) {
      l = mid;
    } else {
      h = mid;
    }
  }

  return h - 1;
}

int binary_search(std::vector<int>& nums, int target, int l, int h) {
  int low = l, high = h;

  while (low < high) {
    int mid = (high + low) / 2;

    if (target <= nums[mid]) {
      high = mid;
    } else {
      low = mid + 1;
    }
  }

  if (low <= h && nums[low] < target) {
    low++;
  }

  return low <= h && nums[low] == target ? low : -1;
}

class Solution {
 public:
  int search(vector<int>& nums, int target) {
    int p = get_peak_index(nums);
    auto left = binary_search(nums, target, 0, p);
    if (left > -1 || p == nums.size() - 1) {
      return left;
    }

    return binary_search(nums, target, p + 1, nums.size() - 1);
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  vector<int> v{7, 0, 1, 2, 3, 4, 5, 6};
  LOG(s.search(v, 6));
}
#endif
