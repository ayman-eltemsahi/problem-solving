#include "local-stuff.hpp"
#include "aoc-common.hpp"

#define ASH '.'
#define ROCK '#'

class AdventOfCodeSolverDay14 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto grid = read();

    int n = grid.size(), m = grid[0].size();
    for (int j = 0; j < m; j++) {
      for (int i = 0; i < n; i++) {
        if (grid[i][j] != 'O') continue;

        int s = i - 1;
        while (s >= 0 && grid[s][j] == '.') {
          grid[s + 1][j] = '.';
          grid[s][j] = 'O';
          s--;
        }
      }
    }

    return calc_load(grid);
  }

  ll second_part() {
    auto grid = read();
    int n = grid.size(), m = grid[0].size();

    vector<string> cache;
    bool caching = true;
    ll MAX = 1000000000L;

    for (ll i = 0; i < MAX; i++) {
      cycle(grid, n, m);
      if (!caching) continue;

      auto grid_s = serialize(grid);
      for (ll u = 0; u < cache.size(); u++) {
        if (grid_s == cache[u]) {
          ll factor = (MAX - i) / (i - u);
          i += (i - u) * factor;

          caching = false;
          cache.clear();
          break;
        }
      }

      cache.push_back(grid_s);
    }

    return calc_load(grid);
  }

 private:
  void cycle(vector<string>& grid, int n, int m) {
    // north
    for (int j = 0; j < m; j++) {
      for (int i = 0; i < n; i++) {
        if (grid[i][j] != 'O') continue;

        int s = i - 1;
        while (s >= 0 && grid[s][j] == '.') {
          grid[s + 1][j] = '.';
          grid[s][j] = 'O';
          s--;
        }
      }
    }

    // west
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < m; j++) {
        if (grid[i][j] != 'O') continue;

        int s = j - 1;
        while (s >= 0 && grid[i][s] == '.') {
          grid[i][s + 1] = '.';
          grid[i][s] = 'O';
          s--;
        }
      }
    }

    // south
    for (int j = 0; j < m; j++) {
      for (int i = n - 1; i >= 0; i--) {
        if (grid[i][j] != 'O') continue;

        int s = i + 1;
        while (s < n && grid[s][j] == '.') {
          grid[s - 1][j] = '.';
          grid[s][j] = 'O';
          s++;
        }
      }
    }

    // east
    for (int i = 0; i < n; i++) {
      for (int j = m - 1; j >= 0; j--) {
        if (grid[i][j] != 'O') continue;

        int s = j + 1;
        while (s < m && grid[i][s] == '.') {
          grid[i][s - 1] = '.';
          grid[i][s] = 'O';
          s++;
        }
      }
    }
  }

  ll calc_load(vector<string>& grid) {
    int n = grid.size(), m = grid[0].size();
    ll result = 0;
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < m; j++) {
        if (grid[i][j] == 'O') result += n - i;
      }
    }
    return result;
  }

  string serialize(vector<string>& grid) {
    string s;
    s.reserve(grid.size() * grid[0].size());
    for (auto& g : grid) s += g;
    return s;
  }
  vector<string> read() {
    std::ifstream infile("../input.txt");
    string line;
    vector<string> lines;

    while (std::getline(infile, line)) {
      lines.push_back(line);
    }

    return lines;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay14{};
  solver.solve_first(108144);
  solver.solve_second(108404);
}
