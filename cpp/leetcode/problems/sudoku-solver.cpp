#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <queue>
#include <vector>

using std::pair;
using std::vector;
typedef long long int ll;
typedef vector<vector<int>>& vvi;
typedef vector<vector<char>>& vvc;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOOP_9(i) for (int i = 0; i < 9; i++)
#define LOOP_9_9(i, j)        \
  for (int i = 0; i < 9; i++) \
    for (int j = 0; j < 9; j++)

class Possibilities {
 public:
  bool nums[10];
  int val = -1;

  Possibilities() {
    this->mark_all(false);
  }

  Possibilities(const Possibilities& p1) {
    val = p1.val;
    LOOP_9(i) nums[i] = p1.nums[i];
  }

  void mark_all(bool val) {
    memset(this->nums, val, sizeof(this->nums));
  }

  constexpr bool has_value() {
    return this->val > -1;
  }

  int ready() {
    int s = -1;
    LOOP_9(i) {
      if (nums[i]) {
        if (s != -1) return -1;
        s = i;
      }
    }
    return s;
  }
};

vector<vector<Possibilities>> possibilities_board(int n) {
  vector<vector<Possibilities>> r(n);
  FORN(i, n) r[i] = vector<Possibilities>(n);
  return r;
}

bool nine[9];

void block_val(int a, int b, int val, vector<vector<Possibilities>>& options,
               vector<vector<char>>& board, int& rem) {
  FORN(i, 9) {
    if (!options[a][i].has_value()) {
      options[a][i].nums[val] = false;
      const auto ready_val = options[a][i].ready();
      if (ready_val > -1) {
        options[a][i].val = ready_val;
        board[a][i] = ready_val + '1';
        rem--;
      }
    }

    if (!options[i][b].has_value()) {
      options[i][b].nums[val] = false;
      const auto ready_val = options[i][b].ready();
      if (ready_val > -1) {
        options[i][b].val = ready_val;
        board[i][b] = ready_val + '1';
        rem--;
      }
    }
  }
}

bool is_complete(vector<vector<char>>& board) {
  LOOP_9_9(i, j) {
    if (board[i][j] == '.') return false;
  }
  return true;
}

bool is_valid(vector<vector<char>>& board) {
  LOOP_9(i) {
    memset(nine, false, sizeof(nine));
    LOOP_9(j) {
      if (board[i][j] == '.') continue;
      if (nine[board[i][j] - '1']) return false;
      nine[board[i][j] - '1'] = true;
    }

    memset(nine, false, sizeof(nine));
    LOOP_9(j) {
      if (board[j][i] == '.') continue;
      if (nine[board[j][i] - '1']) return false;
      nine[board[j][i] - '1'] = true;
    }
  }

  for (int a = 0; a < 9; a += 3) {
    for (int b = 0; b < 9; b += 3) {
      memset(nine, false, sizeof(nine));
      for (int i = a; i < a + 3; i++) {
        for (int j = b; j < b + 3; j++) {
          if (board[i][j] == '.') continue;
          if (nine[board[i][j] - '1']) return false;
          nine[board[i][j] - '1'] = true;
        }
      }
    }
  }

  return true;
}

bool is_valid_at(vector<vector<char>>& board, int x, int y, char val) {
  FORN(i, 9) {
    if (board[i][y] == val || board[x][i] == val) return false;
  }

  int a = x - (x % 3);
  int b = y - (y % 3);
  for (int i = a; i < a + 3; i++) {
    for (int j = b; j < b + 3; j++) {
      if (board[i][j] == val) return false;
    }
  }

  return true;
}

vector<vector<Possibilities>> make_box_possibilities(int a, int b, vvc& board) {
  vector<vector<Possibilities>> res = possibilities_board(9);
  for (int i = a; i < a + 3; i++) {
    for (int j = b; j < b + 3; j++) {
      auto is_empty = board[i][j] == '.';
      res[i][j].mark_all(is_empty);
      if (!is_empty) {
        res[i][j].val = board[i][j] - '1';
      }
    }
  }

  for (int i = a; i < a + 3; i++) {
    for (int j = b; j < b + 3; j++) {
      auto is_empty = board[i][j] == '.';
      if (is_empty) continue;

      auto val = board[i][j] - '1';
      for (int i2 = a; i2 < a + 3; i2++) {
        for (int j2 = b; j2 < b + 3; j2++) {
          if (board[i2][j2] == '.') {
            res[i2][j2].nums[val] = false;
          }
        }
      }
    }
  }

  return res;
}

void solve_1_2(vvc& board, vector<vector<Possibilities>>& options, int& rem) {
  LOOP_9(i) {
    // 1
    memset(nine, false, sizeof(nine));
    LOOP_9(j) {
      if (options[i][j].val != -1) nine[options[i][j].val] = true;
    }

    LOOP_9(k) {
      if (!nine[k]) continue;
      LOOP_9(j) {
        if (options[i][j].has_value()) continue;
        options[i][j].nums[k] = false;
        const auto ready_val = options[i][j].ready();
        if (ready_val > -1) {
          options[i][j].val = ready_val;
          board[i][j] = ready_val + '1';
          rem--;
        }
      }
    }
    // --

    // 2
    memset(nine, false, sizeof(nine));
    LOOP_9(j) {
      if (options[j][i].val != -1) nine[options[j][i].val] = true;
    }

    LOOP_9(k) {
      if (!nine[k]) continue;
      LOOP_9(j) {
        if (options[j][i].val > -1) continue;
        options[j][i].nums[k] = false;
        const auto ready_val = options[j][i].ready();
        if (ready_val > -1) {
          options[j][i].val = ready_val;
          board[j][i] = ready_val + '1';
          rem--;
        }
      }
    }
  }
}

void solve_3(vvc& board, vector<vector<Possibilities>>& options, int& rem) {
  for (int a = 0; a < 9; a += 3) {
    for (int b = 0; b < 9; b += 3) {
      memset(nine, false, sizeof(nine));
      for (int i = a; i < a + 3; i++) {
        for (int j = b; j < b + 3; j++) {
          if (options[i][j].has_value()) nine[options[i][j].val] = true;
        }
      }

      LOOP_9(k) {
        if (!nine[k]) continue;
        for (int i = a; i < a + 3; i++) {
          for (int j = b; j < b + 3; j++) {
            if (options[i][j].has_value()) continue;
            options[i][j].nums[k] = false;
            const auto ready_val = options[i][j].ready();
            if (ready_val > -1) {
              options[i][j].val = ready_val;
              board[i][j] = ready_val + '1';
              rem--;
            }
          }
        }
      }
    }
  }
}

void solve_4(vvc& board, vector<vector<Possibilities>>& options, int& rem) {
  for (int a = 0; a < 9; a += 3) {
    for (int b = 0; b < 9; b += 3) {
      auto pos = make_box_possibilities(a, b, board);
      LOOP_9_9(i, j) {
        if (options[i][j].has_value()) {
          LOOP_9(x) {
            pos[i][x].nums[options[i][j].val] = false;
            pos[x][j].nums[options[i][j].val] = false;
          }
        }

        if (options[j][i].has_value()) {
          LOOP_9(x) {
            pos[j][x].nums[options[j][i].val] = false;
            pos[x][i].nums[options[j][i].val] = false;
          }
        }
      }

      LOOP_9(k) {
        int c = 0;
        for (int i = a; i < a + 3 && c <= 1; i++) {
          for (int j = b; j < b + 3 && c <= 1; j++) {
            if (pos[i][j].nums[k]) c++;
          }
        }

        if (c != 1) continue;
        for (int i = a; i < a + 3; i++) {
          for (int j = b; j < b + 3; j++) {
            if (pos[i][j].nums[k] && !options[i][j].has_value()) {
              options[i][j].val = k;
              pos[i][j].val = k;
              board[i][j] = '1' + k;
              rem--;
            }
          }
        }
      }

      LOOP_9_9(i, j) {
        const auto ready_value = pos[i][j].ready();
        if (ready_value > -1 && !options[i][j].has_value()) {
          options[i][j].val = ready_value;
          board[i][j] = '1' + ready_value;
          rem--;
        }
      }
    }
  }
}

bool dfs(vvc& board, int remaining) {
  if (remaining < 1) return true;

  LOOP_9_9(i, j) {
    if (board[i][j] != '.') continue;

    for (char k = '1'; k <= '9'; k++) {
      if (is_valid_at(board, i, j, k)) {
        board[i][j] = k;
        if (dfs(board, remaining - 1)) return true;
        board[i][j] = '.';
      }
    }
    return false;
  }

  return false;
}

class Solution {
 public:
  void solveSudoku(vvc& board) {
    auto options = possibilities_board(9);

    int rem = 9 * 9;
    LOOP_9_9(i, j) {
      if (board[i][j] == '.') {
        options[i][j].mark_all(true);
      } else {
        rem--;
        options[i][j].val = board[i][j] - '1';
      }
    }

    int before = rem + 1;
    while (rem && rem != before) {
      before = rem;

      solve_1_2(board, options, rem);
      solve_3(board, options, rem);
      solve_4(board, options, rem);

      LOOP_9_9(i, j) {
        const auto val = options[i][j].val;
        if (options[i][j].val == -1) continue;
        block_val(i, j, options[i][j].val, options, board, rem);
      }
    }

    // this is enough to solve it
    dfs(board, rem);
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  vector<vector<char>> v{vector<char>{'.', '.', '9', '7', '4', '8', '.', '.', '.'},
                         vector<char>{'7', '.', '.', '.', '.', '.', '.', '.', '.'},
                         vector<char>{'.', '2', '.', '1', '.', '9', '.', '.', '.'},
                         vector<char>{'.', '.', '7', '.', '.', '.', '2', '4', '.'},
                         vector<char>{'.', '6', '4', '.', '1', '.', '5', '9', '.'},
                         vector<char>{'.', '9', '8', '.', '.', '.', '3', '.', '.'},
                         vector<char>{'.', '.', '.', '8', '.', '3', '.', '2', '.'},
                         vector<char>{'.', '.', '.', '.', '.', '.', '.', '.', '6'},
                         vector<char>{'.', '.', '.', '2', '7', '5', '9', '.', '.'}};

  vector<vector<char>> v2{vector<char>{'5', '3', '4', '6', '7', '8', '9', '1', '2'},
                          vector<char>{'6', '7', '2', '1', '9', '5', '3', '4', '8'},
                          vector<char>{'1', '9', '8', '3', '4', '2', '5', '6', '7'},
                          vector<char>{'8', '5', '9', '7', '6', '1', '4', '2', '3'},
                          vector<char>{'4', '2', '6', '8', '5', '3', '7', '.', '1'},
                          vector<char>{'7', '1', '3', '.', '2', '4', '8', '5', '6'},
                          vector<char>{'9', '6', '1', '.', '3', '7', '2', '8', '4'},
                          vector<char>{'2', '8', '7', '.', '1', '9', '6', '3', '5'},
                          vector<char>{'3', '4', '5', '2', '8', '6', '1', '7', '.'}};

  s.solveSudoku(v);
  // assert(s.test() == true);
}
#endif
