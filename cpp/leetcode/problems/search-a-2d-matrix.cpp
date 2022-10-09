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

class Solution {
 public:
  bool searchMatrix(vector<vector<int>>& matrix, int target) {
    int n = matrix.size();
    int m = matrix[0].size();
    if (matrix[0][0] > target || matrix[n - 1][m - 1] < target) return false;

    int l = 0, h = n - 1;
    while (l < h) {
      int mid = (l + h) / 2;
      if (matrix[mid][0] > target)
        h = mid - 1;
      else
        l = mid + 1;
    }

    int r = (l >= n || matrix[l][0] > target) ? l - 1 : l;

    l = 0;
    h = m - 1;
    while (l < h) {
      int mid = (l + h) / 2;
      if (matrix[r][mid] == target) return true;
      if (matrix[r][mid] > target)
        h = mid - 1;
      else
        l = mid + 1;
    }

    if (l && (l >= m || matrix[r][l] > target)) l--;
    return matrix[r][l] == target;
  }
};

#if defined(RUNNING_LOCALLY)
#include "utils/read-vector.hpp"
#include "utils/print-vector.hpp"
int main() {
  Solution s;
  // auto input = read_vector_vector_int("[[1,3,5,7],[10,11,16,20],[23,30,34,60]]");
  auto input = read_vector_vector_int("[[1],[3]]");
  print_vector_vector_int(input);

  assert(s.searchMatrix(input, 1) == 1);
}
#endif
