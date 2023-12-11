#include "local-stuff.hpp"
#include "aoc-common.hpp"

// | is a vertical pipe connecting north and south.
// - is a horizontal pipe connecting east and west.
// L is a 90-degree bend connecting north and east.
// J is a 90-degree bend connecting north and west.
// 7 is a 90-degree bend connecting south and west.
// F is a 90-degree bend connecting south and east.

#define NORTH \
  { -1, 0 }
#define SOUTH \
  { 1, 0 }
#define EAST \
  { 0, 1 }
#define WEST \
  { 0, -1 }

struct point {
  int x, y;

  point add(const point& other) { return {x + other.x, y + other.y}; }

  bool is_outside(int n, int m) { return x < 0 || y < 0 || x >= n || y >= m; }

  bool equals(const point& other) { return other.x == x && other.y == y; }
};

const string DIR = "|-LJ7F";

unordered_map<char, vector<point>> pipes{
    {'|', {NORTH, SOUTH}},  //
    {'-', {EAST, WEST}},    //
    {'L', {NORTH, EAST}},   //
    {'J', {NORTH, WEST}},   //
    {'7', {SOUTH, WEST}},   //
    {'F', {SOUTH, EAST}},   //
};

bool pnpoly(vector<point>& poly, float x, float y) {
  bool inside = false;

  for (int i = 0; i < poly.size(); ++i) {
    int j = (i + 1) % poly.size();

    double xp0 = poly[i].x;
    double yp0 = poly[i].y;
    double xp1 = poly[j].x;
    double yp1 = poly[j].y;

    if ((yp0 <= y) && (yp1 > y) || (yp1 <= y) && (yp0 > y)) {
      double cross = (xp1 - xp0) * (y - yp0) / (yp1 - yp0) + xp0;

      if (cross < x) inside = !inside;
    }
  }
  return inside;
}

class AdventOfCodeSolverDay10 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto lines = read();
    point s = get_s(lines);

    for (char c : DIR) {
      lines[s.x][s.y] = c;
      auto loop = get_loop(lines, s);

      if (!loop.empty()) {
        return loop.size() / 2;
      }

      lines[s.x][s.y] = 'S';
    }

    assert(0);
  }

  ll second_part() {
    auto lines = read();
    point s = get_s(lines);
    int n = lines.size(), m = lines[0].size();

    for (char c : DIR) {
      lines[s.x][s.y] = c;
      auto loop = get_loop(lines, s);

      if (!loop.empty()) {
        vector<vector<bool>> seen(n, vector<bool>(m, false));

        for (auto& [u, v] : loop) seen[u][v] = true;

        ll result = 0;
        for (int i = 0; i < n; i++) {
          for (int j = 0; j < m; j++) {
            if (!seen[i][j] && pnpoly(loop, i, j)) {
              result++;
            }
          }
        }

        return result;
      }

      lines[s.x][s.y] = 'S';
    }

    assert(0);
  }

 private:
  bool is_compatible(vector<string>& lines, const point& l, const point& r) {
    auto& a = pipes[lines[r.x][r.y]];
    auto& b = pipes[lines[l.x][l.y]];

    return ((a[0].x + r.x == l.x && a[0].y + r.y == l.y) ||
            (a[1].x + r.x == l.x && a[1].y + r.y == l.y)) &&
           ((b[0].x + l.x == r.x && b[0].y + l.y == r.y) ||
            (b[1].x + l.x == r.x && b[1].y + l.y == r.y));
  }

  vector<point> get_loop(vector<string>& lines, point start) {
    int n = lines.size(), m = lines[0].size();

    vector<point> loop;
    point prev = {-1, -1};
    point cur = start;

    loop.push_back(cur);

    while (true) {
      auto first = cur.add(pipes[lines[cur.x][cur.y]][0]);
      if (first.is_outside(n, m) || lines[first.x][first.y] == '.' ||
          !is_compatible(lines, first, cur)) {
        return {};
      }

      auto second = cur.add(pipes[lines[cur.x][cur.y]][1]);
      if (second.is_outside(n, m) || lines[second.x][second.y] == '.' ||
          !is_compatible(lines, second, cur)) {
        return {};
      }

      if (prev.x == -1 || prev.equals(first)) {
        prev = cur;
        cur = second;
      } else if (prev.equals(second)) {
        prev = cur;
        cur = first;
      } else {
        return {};
      }

      if (cur.equals(start)) break;
      loop.push_back(cur);
    }

    return loop;
  }

  point get_s(vector<string>& lines) {
    for (int i = 0; i < lines.size(); i++) {
      for (int j = 0; j < lines[0].size(); j++) {
        if (lines[i][j] == 'S') {
          return {i, j};
        }
      }
    }

    assert(0);
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
  auto solver = AdventOfCodeSolverDay10{};
  solver.solve_first(6725);
  solver.solve_second(383);
}
