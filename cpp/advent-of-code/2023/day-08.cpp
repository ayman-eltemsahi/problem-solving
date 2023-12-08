#include "local-stuff.hpp"
#include "aoc-common.hpp"

typedef std::pair<string, string> edge;

class AdventOfCodeSolverDay08 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    const auto [dir, map] = read();

    string cur = "AAA";
    ll result = 0;
    while (cur != "ZZZ") {
      for (char c : dir) {
        cur = c == 'R' ? map.at(cur).second : map.at(cur).first;
        result++;
        if (cur == "ZZZ") break;
      }
    }

    return result;
  }

  ll second_part() {
    const auto [dir, map] = read();

    ll result = 1;
    for (const auto &kv : map) {
      if (kv.first[2] != 'A') continue;

      ll cycle = get_cycle(kv.first, dir, map);
      result = std::lcm(result, cycle);
    }

    return result;
  }

 private:
  ll get_cycle(const string &node_c, const string &dir, const unordered_map<string, edge> &map) {
    string node = node_c;
    ll cost = 0;
    while (true) {
      for (char c : dir) {
        node = c == 'R' ? map.at(node).second : map.at(node).first;
        cost++;
        if (node[2] == 'Z') {
          return cost;
        }
      }
    }
  }

  pair<string, unordered_map<string, edge>> read() {
    std::ifstream infile("../input.txt");
    string line;

    std::getline(infile, line);
    string dir = line;
    std::getline(infile, line);

    unordered_map<string, edge> map;
    map.reserve(1 << 10);
    while (std::getline(infile, line)) {
      auto a = line.substr(0, 3);
      auto l = line.substr(7, 3);
      auto r = line.substr(12, 3);

      map[a] = {l, r};
    }

    return {dir, map};
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay08{};
  solver.solve_first(13301);
  solver.solve_second(7309459565207);
}
