#include <algorithm>
#include <iostream>
#include <stack>
#include <string>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vec_int;
#define LOG(a) std::cout << a << "\n"
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)

class Board {
public:
  std::vector<vec_int> vec;

  Board(int n, int m) {
    FORN(i, n) {
      vec.push_back(vec_int{});
      FORN(j, m) vec[i].push_back(0);
    }
  }

  void set(int i, int j, int val) { vec[i][j] = val; }

  int get(int i, int j) { return vec[i][j]; }
  void print() {
    int m = vec[0].size();
    FORN(i, vec.size()) {
      FORN(j, m) {
        std::cout << vec[i][j];
        if (j != m - 1)
          std::cout << " ";
      }
      std::cout << "\n";
    }
  }
  void fillEmpty(int k) {
    int m = vec[0].size();
    FORN(i, vec.size()) {
      FORN(j, m) {
        if (vec[i][j] == 0) {
          vec[i][j] = k;
        }
      }
    }
  }
};

bool solve(Board &board, int n, int m, int A, int B) {
  board.set(0, m - 1, 1);
  board.set(0, m - 2, 1);

  // A
  int remaining = A - 2;
  int cells = n + m - 3;
  if (cells > remaining)
    return false;
  int cell_value = remaining / cells;
  FORN(j, m - 2) {
    board.set(0, j, cell_value);
    remaining -= cell_value;
  }
  FORN1(i, n) {
    board.set(i, m - 1, cell_value);
    remaining -= cell_value;
  }
  board.vec[n - 1][m - 1] += remaining;

  // B
  remaining = B - board.get(0, m - 1) - board.get(0, m - 2);
  cells = n + m - 3;
  if (cells > remaining)
    return false;
  cell_value = remaining / cells;
  FORN1(i, n) {
    board.set(i, m - 2, cell_value);
    remaining -= cell_value;
  }
  FORN(j, m - 2) {
    board.set(n - 1, j, cell_value);
    remaining -= cell_value;
  }
  board.vec[n - 1][0] += remaining;

  board.fillEmpty(999);
  return true;
}

bool solve2(Board &board, int n, int m, int A, int B) {
  if (n == 2)
    return false;
  const int d1 = A - (m - 1);
  const int d2 = B - (m - 1);

  int cells = n;
  int remaining = std::min(d1, d2);
  if (remaining >= cells) {
    int per_cell = remaining / cells;
    FORN(i, n) {
      board.set(i, m - 1, per_cell);
      remaining -= per_cell;
    }
    board.vec[n - 1][m - 1] += remaining;
    //
    cells = m - 1;
    remaining = A - std::min(d1, d2);
    per_cell = remaining / cells;
    FORN(j, m - 1) {
      board.set(0, j, per_cell);
      remaining -= per_cell;
    }
    board.vec[0][0] += remaining;
    //
    cells = m - 1;
    remaining = B - std::min(d1, d2);
    per_cell = remaining / cells;
    FORN(j, m - 1) {
      board.set(n - 1, j, per_cell);
      remaining -= per_cell;
    }
    board.vec[n - 1][0] += remaining;

    board.fillEmpty(999);
    return true;
  }

  return false;
}

bool solve3(Board &board, int n, int m, int A, int B) {
  if (m == 2)
    return false;
  int d1 = A - (n - 1);
  int d2 = B - (n - 1);

  int cells = m;
  int remaining = std::min(d1, d2);
  if (remaining >= cells) {
    int per_cell = remaining / cells;
    FORN(j, m) {
      board.set(0, j, per_cell);
      remaining -= per_cell;
    }
    board.vec[0][m - 1] += remaining;
    //
    cells = n - 1;
    remaining = B - std::min(d1, d2);
    per_cell = remaining / cells;
    FORN1(i, n) {
      board.set(i, 0, per_cell);
      remaining -= per_cell;
    }
    board.vec[n - 1][0] += remaining;
    //
    cells = n - 1;
    remaining = A - std::min(d1, d2);
    per_cell = remaining / cells;
    FORN1(i, n) {
      board.set(i, m - 1, per_cell);
      remaining -= per_cell;
    }
    board.vec[n - 1][m - 1] += remaining;

    board.fillEmpty(999);
    return true;
  }

  return false;
}

int main() {
  int T;
  std::cin >> T;
  FORN(t, T) {
    int n, m, A, B;
    std::cin >> n >> m >> A >> B;

    Board board(n, m);

    bool possible = solve3(board, n, m, A, B) || solve2(board, n, m, A, B) ||
                    solve(board, n, m, A, B);

    std::cout << "Case #" << (t + 1) << ": "
              << (possible ? "Possible" : "Impossible") << "\n";

    if (possible)
      board.print();
  }
}
