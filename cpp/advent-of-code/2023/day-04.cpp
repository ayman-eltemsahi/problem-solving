#include "local-stuff.hpp"
#include "aoc-common.hpp"

class AdventOfCodeSolverDay04 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    std::ifstream infile("../input.txt");
    string line;

    ll result = 0;

    while (std::getline(infile, line)) {
      if (line.empty()) continue;

      ll wins = get_wins(line);
      result += wins ? 1 << (wins - 1) : 0;
    }

    return result;
  }

  ll second_part() {
    std::ifstream infile("../input.txt");
    string line;

    vector<int> wins;

    while (std::getline(infile, line)) {
      if (line.empty()) continue;

      wins.push_back(get_wins(line));
    }

    int n = wins.size();
    vector<int> cnt(n, 1);
    ll result = 0;

    for (int i = 0; i < n; i++) {
      result += cnt[i];

      for (int j = 0; j < wins[i]; j++) {
        cnt[i + 1 + j] += cnt[i];
      }
    }

    return result;
  }

 private:
  unordered_set<int> read_numbers(const string &line, int index) {
    auto tmp = utils::split_string(line, "|");

    unordered_set<int> res;
    for (auto item : utils::split_string(tmp[index], " ")) {
      if (item.size() > 0) {
        res.insert(stoi(item));
      }
    }
    return res;
  }

  ll get_wins(const string &line) {
    auto card = utils::split_string(line, ": ");

    auto winning = read_numbers(card[1], 0);
    auto mine = read_numbers(card[1], 1);
    int cnt = 0;
    for (auto m : mine) {
      if (winning.find(m) != winning.end()) {
        cnt += 1;
      }
    }

    return cnt;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay04{};
  solver.solve_first(20667);
  solver.solve_second(5833065);
}
