#include "local-stuff.hpp"
#include "aoc-common.hpp"

#define ASH '.'
#define ROCK '#'

class AdventOfCodeSolverDay13 : public AdventOfCodeSolver {
 public:
  bool is_good_col(vector<string>& lines, int l, int r, pair<int, int> change) {
    int n = lines.size(), m = lines[0].size();
    bool found = change.first < 0;
    while (l >= 0 && r < m) {
      for (int i = 0; i < n; i++) {
        if (lines[i][l] != lines[i][r]) {
          return false;
        }

        found |=
            (i == change.first && l == change.second) || (i == change.first && r == change.second);
      }

      l--;
      r++;
    }
    return found;
  }

  bool is_good_row(vector<string>& lines, int l, int r, pair<int, int> change) {
    int n = lines.size(), m = lines[0].size();
    bool found = change.first < 0;
    while (l >= 0 && r < n) {
      for (int j = 0; j < m; j++) {
        if (lines[l][j] != lines[r][j]) {
          return false;
        }

        found |=
            (l == change.first && j == change.second) || (r == change.first && j == change.second);
      }

      l--;
      r++;
    }
    return found;
  }

  ll solve_pattern(vector<string>& pattern, pair<int, int> change) {
    int n = pattern.size(), m = pattern[0].size();
    for (int j = 0; j < m - 1; j++) {
      if (is_good_col(pattern, j, j + 1, change)) {
        return j + 1;
      }
    }

    for (int i = 0; i < n - 1; i++) {
      if (is_good_row(pattern, i, i + 1, change)) {
        return 100 * (i + 1);
      }
    }

    return -1;
  }

  ll first_part() {
    auto patterns = read();
    ll result = 0;

    for (auto& p : patterns) {
      result += solve_pattern(p, {-1, -1});
    }
    return result;
  }

  ll second_part() {
    auto patterns = read();
    ll result = 0;

    for (auto& p : patterns) {
      int n = p.size(), m = p[0].size();
      bool running = true;

      for (int u = 0; running && u < n; u++) {
        for (int v = 0; running && v < m; v++) {
          p[u][v] = p[u][v] == '.' ? '#' : '.';

          auto tmp = solve_pattern(p, {u, v});
          if (tmp != -1) {
            result += tmp;
            running = false;
          }
          p[u][v] = p[u][v] == '.' ? '#' : '.';
        }
      }
    }
    return result;
  }

 private:
  vector<vector<string>> read() {
    std::ifstream infile("../input.txt");
    string line;
    vector<string> lines;

    while (std::getline(infile, line)) {
      lines.push_back(line);
    }

    vector<vector<string>> patterns;
    patterns.push_back({});
    for (auto& line : lines) {
      if (line.empty()) {
        patterns.push_back({});

      } else {
        patterns.back().push_back(line);
      }
    }

    if (patterns.back().empty()) patterns.pop_back();

    return patterns;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay13{};
  solver.solve_first(30802);
  solver.solve_second(37876);
}
