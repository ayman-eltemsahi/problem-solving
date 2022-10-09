#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

bool dfs(int root, vi& leftChild, vi& rightChild, vi& visited, int& visited_count) {
  if (root == -1) return true;
  if (visited[root]) return false;
  visited[root] = 1;
  visited_count++;

  return dfs(leftChild[root], leftChild, rightChild, visited, visited_count) &&
         dfs(rightChild[root], leftChild, rightChild, visited, visited_count);
}

class Solution {
 public:
  bool validateBinaryTreeNodes(int n, vi& leftChild, vi& rightChild) {
    vi arr(n, 0);
    FORN(i, n) {
      if (leftChild[i] != -1) {
        if (arr[leftChild[i]]) return false;
        arr[leftChild[i]] = 1;
      }

      if (rightChild[i] != -1) {
        if (arr[rightChild[i]]) return false;
        arr[rightChild[i]] = 1;
      }
    }

    int root = -1;
    FORN(i, n) {
      if (!arr[i] && root > -1) return false;
      if (!arr[i]) root = i;
    }

    if (root == -1) return false;

    std::fill(arr.begin(), arr.end(), 0);
    int visited_count = 0;
    return dfs(root, leftChild, rightChild, arr, visited_count) && visited_count == n;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  vi leftChild{1, 0, 3, -1};
  vi rightChild{-1, -1, -1, -1};
  assert(false == s.validateBinaryTreeNodes(4, leftChild, rightChild));
}
#endif
