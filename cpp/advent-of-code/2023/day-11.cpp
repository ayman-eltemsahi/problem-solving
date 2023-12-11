#include "local-stuff.hpp"
#include "aoc-common.hpp"

struct point {
  int x, y;

  point add(const point& other) { return {x + other.x, y + other.y}; }

  bool is_outside(int n, int m) { return x < 0 || y < 0 || x >= n || y >= m; }
};

const vector<point> steps = {{1, 0}, {-1, 0}, {0, 1}, {0, -1}};

class AdventOfCodeSolverDay11 : public AdventOfCodeSolver {
  vector<vector<ll>> get_distance(int i, int j, vector<string>& lines, vector<bool>& expanded_rows,
                                  vector<bool>& expanded_cols, ll expansion) {
    int n = lines.size(), m = lines[0].size();
    vector<vector<ll>> distance(n, vector<ll>(m, 1L << 50));

    distance[i][j] = 0;
    queue<point> q;
    q.push({i, j});
    while (!q.empty()) {
      point p = q.front();
      q.pop();
      ll d = distance[p.x][p.y];

      for (auto& step : steps) {
        auto a = p.add(step);
        if (a.is_outside(n, m)) continue;

        ll new_distance = d + 1;
        if ((step.x != 0 && expanded_rows[p.x]) || (step.y != 0 && expanded_cols[p.y])) {
          new_distance = d + expansion;
        }

        if (new_distance < distance[a.x][a.y]) {
          distance[a.x][a.y] = new_distance;
          q.push(a);
        }
      }
    }

    return distance;
  }

 public:
  ll solve(ll expansion) {
    auto lines = read();
    int n = lines.size(), m = lines[0].size();
    vector<bool> expanded_rows(n, false);
    vector<bool> expanded_cols(m, false);

    for (int i = 0; i < n; i++) {
      bool flag = true;
      for (int j = 0; j < m && flag; j++) {
        flag = lines[i][j] == '.';
      }
      expanded_rows[i] = flag;
    }

    for (int j = 0; j < m; j++) {
      bool flag = true;
      for (int i = 0; i < n && flag; i++) {
        flag = lines[i][j] == '.';
      }
      expanded_cols[j] = flag;
    }

    vector<point> galaxies;
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < m; j++) {
        if (lines[i][j] == '#') {
          galaxies.push_back({i, j});
        }
      }
    }

    ll result = 0;
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < m; j++) {
        if (lines[i][j] == '#') {
          const auto distance = get_distance(i, j, lines, expanded_rows, expanded_cols, expansion);
          for (auto& [x, y] : galaxies) result += distance[x][y];
        }
      }
    }

    return result / 2;
  }

  ll first_part() { return solve(2); }

  ll second_part() { return solve(1000000); }

 private:
  vector<string> read() {
    std::ifstream infile("../input.txt");
    string line;
    vector<string> lines;

    while (std::getline(infile, line)) {
      if (line.empty()) continue;
      lines.push_back(line);
    }

    return lines;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay11{};
  solver.solve_first(9556712);
  solver.solve_second(678626199476);
}
