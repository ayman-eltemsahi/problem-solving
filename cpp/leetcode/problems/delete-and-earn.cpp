#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;

class Solution {
 public:
  int deleteAndEarn(std::vector<int>& nums) {
    int m = 3;
    for (const int i : nums) {
      if (i > m) m = i;
    }

    std::vector<int> nums_count(m + 1, 0);
    std::vector<int> points(m + 1, 0);
    for (const int i : nums) {
      nums_count[i]++;
    }

    points[1] = nums_count[1];
    points[2] = std::max(nums_count[1], nums_count[2] * 2);

    for (int i = 3; i <= m; i++) {
      ll a = nums_count[i] * i + points[i - 2];
      ll b = nums_count[i - 1] * (i - 1) + points[i - 3];
      points[i] = a > b ? a : b;
    }

    return points[m];
  }
};
