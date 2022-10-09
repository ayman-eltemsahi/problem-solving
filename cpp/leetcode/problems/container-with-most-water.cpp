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
#define MAXN 100005

class Solution {
public:
  int maxArea(std::vector<int> &height) {
    int i = 0, j = height.size() - 1;
    int area = 0;
    while (i < j) {
      area = std::max(area, (j-i) * std::min(height[i], height[j]));
      if (height[i] < height[j])
        i++;
      else
        j--;
    }
    return area;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  std::vector<int> v;
  Solution s;

  v = {1, 8, 6, 2, 5, 4, 8, 3, 7};
  assert(s.maxArea(v) == 49);
}
#endif
