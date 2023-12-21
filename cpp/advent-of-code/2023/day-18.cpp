#include "local-stuff.hpp"
#include "aoc-common.hpp"

const int R = 0;
const int L = 1;
const int U = 2;
const int D = 3;

struct point {
  ll x, y;

  point add(const point& other) const { return {x + other.x, y + other.y}; }

  bool is_outside(ll n, ll m) const { return x < 0 || y < 0 || x >= n || y >= m; }
};

const vector<point> STEPS = {{0, 1}, {0, -1}, {-1, 0}, {1, 0}};

int convert_dir(int i) {
  if (i == 0) return R;
  if (i == 1) return D;
  if (i == 2) return L;
  if (i == 3) return U;
  assert(0);
}

double polygon_area(vector<point>& poly) {
  double area = 0;
  int n = poly.size();

  for (int i = 0; i < n - 1; ++i) {
    area += poly[i + 1].x * poly[i].y - poly[i].x * poly[i + 1].y;
  }

  area += poly[0].x * poly[n - 1].y - poly[n - 1].x * poly[0].y;

  return area / 2.0;
}

class AdventOfCodeSolverDay18 : public AdventOfCodeSolver {
 public:
  ll first_part() { return solve(true); }

  ll second_part() { return solve(false); }

 private:
  ll solve(bool first_part) {
    vector<pair<int, int>> lines = first_part ? read_1() : read_2();
    point p{0, 0};
    double result = 0;

    vector<point> poly;
    poly.push_back(p);
    for (auto& [dir, cnt] : lines) {
      p = p.add({STEPS[dir].x * cnt, STEPS[dir].y * cnt});
      result += cnt;
      poly.push_back(p);
    }

    result = (result + 1) / 2.0;
    result += polygon_area(poly);

    return (ll)ceil(result);
  }

  vector<pair<int, int>> read_1() {
    std::ifstream infile("../input.txt");
    string line;
    vector<pair<int, int>> lines;

    while (std::getline(infile, line)) {
      if (line.empty()) continue;
      char c = line[0];
      auto tmp = utils::split_string(line, " ");
      int cnt = stoi(tmp[1]);
      int dir = c == 'R' ? R : c == 'L' ? L : c == 'U' ? U : D;

      lines.push_back({dir, cnt});
    }

    return lines;
  }

  vector<pair<int, int>> read_2() {
    std::ifstream infile("../input.txt");
    string line;
    vector<pair<int, int>> lines;

    while (std::getline(infile, line)) {
      if (line.empty()) continue;
      auto hex = utils::split_string(line, " ")[2];

      ll dir = convert_dir(hex[hex.size() - 2] - '0');
      ll cnt = stol(hex.substr(2, 5), nullptr, 16);

      lines.push_back({dir, cnt});
    }

    return lines;
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
  auto solver = AdventOfCodeSolverDay18{};
  solver.solve_first(46359);
  solver.solve_second(59574883048274);
}
