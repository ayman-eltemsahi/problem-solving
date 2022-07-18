#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <unordered_set>
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

int n;
int queens[10][10];
bool cols[10];
bool diag1[20];
bool diag2[20];

void add_res(vector<vector<string>> &res) {
  vector<string> q(n);
  for (int i = 0; i < n; i++) {
    for (int j = 0; j < n; j++) {
      q[i] += queens[i][j] ? "Q" : ".";
    }
  }

  res.push_back(q);
}

void check(int i, vector<vector<string>> &res) {
  if (i == n) {
    add_res(res);
    return;
  }

  // queens[i][j] = 1;
  // cols[j] = true;
  // diag1[i + j] = true;
  // diag2[n - 1 + j - i] = true;

  for (int j = 0; j < n; j++) {
    if (!cols[j] && !diag1[i + j] && !diag2[n - 1 + j - i]) {
      queens[i][j] = 1;
      cols[j] = true;
      diag1[i + j] = true;
      diag2[n - 1 + j - i] = true;

      check(i + 1, res);

      queens[i][j] = 0;
      cols[j] = false;
      diag1[i + j] = false;
      diag2[n - 1 + j - i] = false;
    }
  }

  // queens[i][j] = 0;
  // cols[j] = false;
  // diag1[i + j] = false;
  // diag2[n - 1 + j - i] = false;
}

class Solution {
 public:
  vector<vector<string>> solveNQueens(int n_) {
    n = n_;
    vector<vector<string>> res;
    memset(queens, 0, sizeof(queens));
    memset(cols, false, sizeof(cols));
    memset(diag1, false, sizeof(diag1));
    memset(diag2, false, sizeof(diag2));

    check(0, res);

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto res = s.solveNQueens(9);
  // for (auto b : res) {
  //   for (auto r : b) {
  //     LOG(r);
  //   }

  //   LOG("");
  // }
  LOG2("size", res.size());
  // assert(s.test() == true);
}
#endif
