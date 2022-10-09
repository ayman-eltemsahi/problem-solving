#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <unordered_map>
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

int parent[100005];
int set_sizes[100005];

int find_parent(int i) {
  if (i == parent[i]) return i;
  if (parent[i] == -1) return (parent[i] = i);
  return (parent[i] = find_parent(parent[i]));
}

int make_union(int x, int y) {
  int x_set = find_parent(x);
  int y_set = find_parent(y);
  parent[x_set] = y_set;
  return y_set;
}

class Solution {
 public:
  int longestConsecutive(vector<int>& nums) {
    memset(parent, -1, sizeof(parent));
    memset(set_sizes, 0, sizeof(set_sizes));

    std::unordered_map<int, int> m;
    int i = 0;
    int res = nums.empty() ? 0 : 1;

    for (const auto n : nums) {
      if (m.find(n) != m.end()) continue;

      m[n] = i++;

      int size = set_sizes[find_parent(m[n])];
      if (!size) {
        set_sizes[find_parent(m[n])] = 1;
        size = 1;
      }

      if (m.find(n - 1) != m.end()) {
        size += set_sizes[find_parent(m[n - 1])];
        const auto p = make_union(m[n], m[n - 1]);
        set_sizes[p] = size;
        if (set_sizes[p] > res) {
          res = set_sizes[p];
        }
      }

      if (m.find(n + 1) != m.end()) {
        size += set_sizes[find_parent(m[n + 1])];
        const auto p = make_union(m[n], m[n + 1]);
        set_sizes[p] = size;
        if (set_sizes[p] > res) {
          res = set_sizes[p];
        }
      }
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
