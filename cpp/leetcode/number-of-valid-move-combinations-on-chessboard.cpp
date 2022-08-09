#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

// rook queen bishop

class chess_move {
 public:
  int x, y;
  chess_move() : x(0), y(0) {}
  chess_move(int a, int b) : x(a), y(b) {}
  bool eq(chess_move& other) { return x == other.x && y == other.y; }
};

class Solution {
 public:
  int res = 0, n;
  vector<vector<chess_move>> moves;
  vector<string> pieces;
  vector<chess_move> positions;

  vector<chess_move> get_moves(string piece, int x, int y) {
    vector<chess_move> res{{x, y}};

    if (piece == "rook" || piece == "queen") {
      for (int k = -8; k < 8; k++) {
        int i = x + k;
        if (i >= 0 && i != x && i < 8) res.push_back({i, y});
        int j = y + k;
        if (j >= 0 && j != y && j < 8) res.push_back({x, j});
      }
    }

    if (piece == "bishop" || piece == "queen") {
      for (int l = -10; l < 10; l++) {
        int i = x + l;
        if (i >= 0 && i < 8) {
          int j = y + l;
          if (j >= 0 && j < 8 && (i != x || j != y)) res.push_back({i, j});

          j = y - l;
          if (l != 0 && j >= 0 && j < 8 && (i != x || j != y)) res.push_back({i, j});
        }
      }
    }

    return res;
  }

  bool is_valid_combination(vector<chess_move>& dist) {
    vector<chess_move> pos = positions;

    while (true) {
      bool all_reached = true;
      for (int i = 0; i < n; i++) {
        if (dist[i].x != pos[i].x) pos[i].x += dist[i].x > pos[i].x ? 1 : -1;
        if (dist[i].y != pos[i].y) pos[i].y += dist[i].y > pos[i].y ? 1 : -1;

        for (int j = 0; j < i; j++)
          if (pos[i].eq(pos[j])) return false;

        all_reached = all_reached && pos[i].eq(dist[i]);
      }

      if (all_reached) break;
    }

    return true;
  }

  void check(int i, vector<chess_move>& chosen) {
    if (i == n) {
      if (is_valid_combination(chosen)) res++;
      return;
    }

    for (auto move : moves[i]) {
      chosen[i] = move;
      check(i + 1, chosen);
    }
  }

  int countCombinations(vector<string>& pieces, vector<vector<int>>& positions) {
    this->n = pieces.size();
    this->pieces = pieces;
    for (auto p : positions) this->positions.push_back({p[0] - 1, p[1] - 1});
    moves = vector<vector<chess_move>>(n);
    for (int i = 0; i < n; i++) moves[i] = get_moves(pieces[i], positions[i][0] - 1, positions[i][1] - 1);

    vector<chess_move> chosen(n);
    check(0, chosen);

    return res;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  Input input;
  auto pieces = input.next_vector_string();
  auto pos = input.next_vector_vector_int();
  auto res = s.countCombinations(pieces, pos);
  LOG(res);
}
#endif
