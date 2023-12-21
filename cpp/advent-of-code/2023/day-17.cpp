#include "local-stuff.hpp"
#include "aoc-common.hpp"

const int R = 0;
const int L = 1;
const int U = 2;
const int D = 3;
const ll INF = 1L << 50;

struct point {
  int x, y, dir, steps;

  point add(const point& other, int d, int st) const { return {x + other.x, y + other.y, d, st}; }

  bool is_outside(int n, int m) const { return x < 0 || y < 0 || x >= n || y >= m; }
};

const vector<point> STEPS = {{0, 1}, {0, -1}, {-1, 0}, {1, 0}};

class AdventOfCodeSolverDay17 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto lines = read();
    return solve(lines, 1, 3);
  }

  ll second_part() {
    auto lines = read();
    return solve(lines, 4, 10);
  }

 private:
  ll solve(vector<string>& lines, int MIN, int MAX) {
    int n = lines.size(), m = lines[0].size();
    vector<vector<vector<vector<ll>>>> cost_map(
        4, vector<vector<vector<ll>>>(MAX + 1, vector<vector<ll>>(n, vector<ll>(m, INF))));

    queue<point> q;
    q.push({0, 0, R, 1});
    q.push({0, 0, D, 1});
    cost_map[R][1][0][0] = 0;
    cost_map[D][1][0][0] = 0;

    while (!q.empty()) {
      point p = q.front();
      q.pop();

      ll cost = cost_map[p.dir][p.steps][p.x][p.y];

      vector<point> tmp_steps;

      if (p.steps >= MIN) {
        if (p.dir == R || p.dir == L) {
          tmp_steps.push_back({1, 0, D, 1});
          tmp_steps.push_back({-1, 0, U, 1});
        } else {
          tmp_steps.push_back({0, 1, R, 1});
          tmp_steps.push_back({0, -1, L, 1});
        }
      }

      if (p.steps < MAX) {
        tmp_steps.push_back({STEPS[p.dir].x, STEPS[p.dir].y, p.dir, p.steps + 1});
      }

      for (auto step : tmp_steps) {
        point p2 = p.add(step, step.dir, step.steps);
        if (p2.is_outside(n, m)) continue;

        ll new_cost = cost + (lines[p2.x][p2.y] - '0');

        if (cost_map[p2.dir][p2.steps][p2.x][p2.y] > new_cost) {
          cost_map[p2.dir][p2.steps][p2.x][p2.y] = new_cost;
          q.push(p2);
        }
      }
    }

    ll result = INF;
    for (int i = MIN; i <= MAX; i++) {
      result = min(result, cost_map[R][i][n - 1][m - 1]);
      result = min(result, cost_map[D][i][n - 1][m - 1]);
    }

    return result;
  }

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
  auto solver = AdventOfCodeSolverDay17{};
  solver.solve_first(861);
  solver.solve_second(1037);
}
