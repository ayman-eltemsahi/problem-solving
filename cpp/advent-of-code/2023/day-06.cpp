#include "local-stuff.hpp"
#include "aoc-common.hpp"

const auto split = utils::split_string;
const auto trim = utils::trim_string;

class AdventOfCodeSolverDay06 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto lines = read();

    vector<ll> times;
    vector<ll> distance;

    auto line_1 = split(trim(split(lines.first, ":")[1]), " ");
    for (auto ab : line_1) {
      if (!ab.empty()) {
        times.push_back(stoll(ab));
      }
    }

    auto line_2 = split(trim(split(lines.second, ":")[1]), " ");
    for (auto ab : line_2) {
      if (!ab.empty()) {
        distance.push_back(stoll(ab));
      }
    }

    ll result = 1;
    for (int i = 0; i < times.size(); i++) {
      ll t = times[i], d = distance[i];
      result *= calc(t, d);
    }

    return result;
  }

  ll second_part() {
    auto lines = read();
    auto t = split(trim(split(lines.first, ":")[1]), " ");

    ll time = 0;
    ll distance = 0;
    for (char c : lines.first)
      if (isdigit(c)) time = time * 10 + c - '0';
    for (char c : lines.second)
      if (isdigit(c)) distance = distance * 10 + c - '0';

    return calc(time, distance);
  }

 private:
  ll calc(ll time, ll distance) {
    // hold_time * (time - hold_time) = distance
    // (hold_time * time - hold_time * hold_time) - distance = 0

    // X * time - X^2 = distance
    // X * time - X^2 - distance = 0
    // -X^2 + time * X - distance = 0
    ll a = -1, b = time, c = -distance;

    double solution_1 = (-b + sqrt(b * b - 4 * a * c)) / 2.0 * a;
    double solution_2 = (-b - sqrt(b * b - 4 * a * c)) / 2.0 * a;

    solution_1 += 0.0001;  // to avoid exact solutions
    solution_2 -= 0.0001;  // to avoid exact solutions

    return (ll)(ceil(solution_2) - ceil(solution_1));
  }

  pair<string, string> read() {
    std::ifstream infile("../input.txt");
    string first;
    string second;

    std::getline(infile, first);
    std::getline(infile, second);

    return {first, second};
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay06{};
  solver.solve_first(74698);
  solver.solve_second(27563421);
}
