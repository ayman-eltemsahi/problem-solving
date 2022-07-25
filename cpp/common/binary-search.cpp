#include <vector>

int lower_bound(std::vector<int>& nums, int target) {
  int low = 0, high = nums.size();

  while (low < high) {
    int mid = low + (high - low) / 2;

    if (target <= nums[mid]) {
      high = mid;
    } else {
      low = mid + 1;
    }
  }

  if (low < nums.size() && nums[low] < target) {
    low++;
  }

  return low < nums.size() && nums[low] == target ? low : -1;
}

int upper_bound(std::vector<int>& nums, int target) {
  int low = 0, high = nums.size();

  while (low < high) {
    int mid = low + (high - low) / 2;

    if (target >= nums[mid]) {
      low = mid + 1;
    } else {
      high = mid;
    }
  }

  if (low == nums.size() || (low < nums.size() && nums[low] > target)) {
    low--;
  }

  return low < nums.size() && nums[low] == target ? low : -1;
}
