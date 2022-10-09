#if defined(RUNNING_LOCALLY)
#include "local-stuff.hpp"
#endif

int col_to_dec(vector<vector<int>>& board, int j) {
  int r = 0;
  for (int i = 0; i < board.size(); i++) r |= (board[i][j] << i);
  return r;
}

int row_to_dec(vector<vector<int>>& board, int i) {
  int r = 0;
  for (int j = 0; j < board.size(); j++) r |= (board[i][j] << j);
  return r;
}

int get_best_moves(vector<int>& v, int l) {
  int x = 0, y = 0;
  int g = 0;
  int x1 = 0, x2 = 0;
  for (int i = 0; i < v.size(); i++) {
    if (v[i] != l) {
      g |= (1 << i);
      x++;
    } else
      y++;

    if (i % 2 == 0) x1 |= (1 << i);
    if (i % 2 == 1) x2 |= (1 << i);
  }

  if (x == y) {
    return min(__builtin_popcount(x1 ^ g), __builtin_popcount(x2 ^ g)) / 2;
  }

  if (__builtin_popcount(x1) == __builtin_popcount(g)) {
    return __builtin_popcount(x1 ^ g) / 2;
  }

  return __builtin_popcount(x2 ^ g) / 2;
}

class Solution {
 public:
  int movesToChessboard(vector<vector<int>>& board) {
    int n = board.size();
    int one = 0, zero = 0;
    for (int i = 0; i < n; i++)
      for (int j = 0; j < n; j++)
        if (board[i][j] == 1)
          one++;
        else
          zero++;
    if (abs(one - zero) > 1) return -1;

    vector<int> cols(n);
    vector<int> rows(n);

    unordered_map<int, int> col_map;
    unordered_map<int, int> row_map;
    for (int i = 0; i < n; i++) {
      cols[i] = col_to_dec(board, i);
      rows[i] = row_to_dec(board, i);

      col_map[cols[i]]++;
      row_map[rows[i]]++;
    }

    if (col_map.size() > 2 || row_map.size() > 2) return -1;

    if (col_map.size() == 2) {
      auto it = col_map.begin();
      if (it->first == 0 || std::next(it)->first == 0) return -1;
      if (abs(it->second - std::next(it)->second) > 1) return -1;
    }

    if (row_map.size() == 2) {
      auto it = row_map.begin();
      if (it->first == 0 || std::next(it)->first == 0) return -1;
      if (abs(it->second - std::next(it)->second) > 1) return -1;
    }

    int col_moves = get_best_moves(cols, cols[0]);
    int row_moves = get_best_moves(rows, rows[0]);

    return col_moves + row_moves;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  auto vec = read_vector_vector_int("[[0,1,1,0],[0,1,1,0],[1,0,0,1],[1,0,0,1]]");
  vec = read_vector_vector_int("[[1,1,0],[0,0,1],[0,0,1]]");
  vec = read_vector_vector_int("[[0,1],[1,0]]");
  vec = read_vector_vector_int(
      "[[0,0,1,0,1,1],[1,1,0,1,0,0],[1,1,0,1,0,0],[0,0,1,0,1,1],[1,1,0,1,0,0],[0,0,1,0,1,1]]");
  LOG(s.movesToChessboard(vec));
}
#endif
