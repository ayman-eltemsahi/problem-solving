#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
typedef std::vector<std::vector<int>> vvi;
typedef std::vector<std::vector<char>> vvc;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"

void dfs(int i, int j, vvc& board) {
  if (board[i][j] != 'O') return;
  board[i][j] = 'Z';

  if (i) dfs(i - 1, j, board);
  if (i < board.size() - 1) dfs(i + 1, j, board);
  if (j) dfs(i, j - 1, board);
  if (j < board[0].size() - 1) dfs(i, j + 1, board);
}

bool is_enclosed(int i, int j, vvc& board) {
  if (board[i][j] != 'Z') return true;
  board[i][j] = 'O';

  if (i == 0 || j == 0 || i == board.size() - 1 || j == board[0].size() - 1) return false;
  return is_enclosed(i - 1, j, board) && is_enclosed(i + 1, j, board) &&
         is_enclosed(i, j - 1, board) && is_enclosed(i, j + 1, board);
}

void capture(int i, int j, vvc& board) {
  if (board[i][j] != 'O') return;
  board[i][j] = 'X';

  if (i) capture(i - 1, j, board);
  if (i < board.size() - 1) capture(i + 1, j, board);
  if (j) capture(i, j - 1, board);
  if (j < board[0].size() - 1) capture(i, j + 1, board);
}

class Solution {
 public:
  void solve(vvc& board) {
    FORN(i, board.size()) {
      FORN(j, board[0].size()) {
        if (board[i][j] == 'O') {
          dfs(i, j, board);
          if (is_enclosed(i, j, board)) {
            capture(i, j, board);
          }
        }
      }
    }

    FORN(i, board.size()) {
      FORN(j, board[0].size()) {
        if (board[i][j] == 'Z') board[i][j] = 'O';
      }
    }
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
