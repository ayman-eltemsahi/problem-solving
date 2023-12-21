#include "local-stuff.hpp"
#include "aoc-common.hpp"

const int R = 0;
const int L = 1;
const int U = 2;
const int D = 3;

struct point {
  int x, y, dir;

  point add(const point& other, int d) const { return {x + other.x, y + other.y, d}; }

  bool is_outside(int n, int m) const { return x < 0 || y < 0 || x >= n || y >= m; }
};

const vector<point> STEPS = {{0, 1}, {0, -1}, {-1, 0}, {1, 0}};

unordered_map<char, unordered_map<int, vector<int>>> transitions;

class AdventOfCodeSolverDay16 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto lines = read();
    update_transitions();
    return solve(lines, {0, 0, R});
  }

  ll second_part() {
    auto lines = read();
    update_transitions();
    int n = lines.size(), m = lines[0].size();
    ll result = solve(lines, {0, 0, R});

    for (int i = 0; i < n; i++) {
      result = max(result, solve(lines, {i, 0, R}));
      result = max(result, solve(lines, {i, m - 1, L}));
    }

    for (int j = 0; j < m; j++) {
      result = max(result, solve(lines, {0, j, D}));
      result = max(result, solve(lines, {n - 1, j, U}));
    }

    return result;
  }

 private:
  ll solve(vector<string>& lines, point start) {
    int n = lines.size(), m = lines[0].size();
    vector<vector<vector<bool>>> cache(4, vector<vector<bool>>(n, vector<bool>(m, false)));

    queue<point> q;
    q.push(start);
    cache[start.dir][start.x][start.y] = true;

    while (q.size() > 0) {
      auto p = q.front();
      q.pop();

      vector<point> new_points;

      char c = lines[p.x][p.y];
      if (c == '.' || ((p.dir == R || p.dir == L) && c == '-') ||
          ((p.dir == U || p.dir == D) && c == '|')) {
        new_points.push_back(p.add(STEPS[p.dir], p.dir));
      } else {
        for (auto& x : transitions[c][p.dir]) {
          new_points.push_back(p.add(STEPS[x], x));
        }
      }

      for (auto& p2 : new_points) {
        if (p2.is_outside(n, m) || cache[p2.dir][p2.x][p2.y]) continue;
        cache[p2.dir][p2.x][p2.y] = true;
        q.push(p2);
      }
    }

    ll result = 0;
    for (int i = 0; i < n; i++) {
      for (int j = 0; j < m; j++) {
        if (cache[0][i][j] || cache[1][i][j] || cache[2][i][j] || cache[3][i][j]) {
          result++;
        }
      }
    }
    return result;
  }

  void update_transitions() {
    transitions.clear();

    transitions['/'][R] = {U};
    transitions['/'][L] = {D};
    transitions['/'][U] = {R};
    transitions['/'][D] = {L};

    transitions['\\'][R] = {D};
    transitions['\\'][L] = {U};
    transitions['\\'][U] = {L};
    transitions['\\'][D] = {R};

    transitions['|'][R] = {U, D};
    transitions['|'][L] = {U, D};
    transitions['-'][U] = {L, R};
    transitions['-'][D] = {L, R};
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
  auto solver = AdventOfCodeSolverDay16{};
  solver.solve_first(7472);
  solver.solve_second(7716);
}
