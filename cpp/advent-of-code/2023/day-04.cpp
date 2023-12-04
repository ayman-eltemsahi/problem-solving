#include "local-stuff.hpp"
#include "aoc-common.hpp"

class AdventOfCodeSolverDay04 : AdventOfCodeSolver {
 public:
  ll first_part() {
    std::ifstream infile("../input.txt");
    string line;

    ll result = 0;

    while (std::getline(infile, line)) {
      if (line.empty()) continue;

      ll wins = get_wins(line);
      result += 1 << (wins - 1);
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
  auto start = std::chrono::high_resolution_clock::now();

  auto solver = AdventOfCodeSolverDay04{};
  int first = solver.first_part();
  int second = solver.second_part();

  auto finish = std::chrono::high_resolution_clock::now();
  auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
  printf("Reuslt     : \x1b[32m%d\x1b[0m\n", first);
  printf("Correct    : \x1b[32m%d\x1b[0m\n", 20667);
  printf("Reuslt     : \x1b[32m%d\x1b[0m\n", second);
  printf("Correct    : \x1b[32m%d\x1b[0m\n", 5833065);
  assert(second == 5833065);
  printf("Time       : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
}
