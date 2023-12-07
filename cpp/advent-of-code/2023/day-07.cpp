#include "local-stuff.hpp"
#include "aoc-common.hpp"

int values_A[100], values_B[100], mem[35];

void prep() {
  int i = 100;
  for (char c : "AKQJT98765432") {
    values_A[c] = i--;
  }
  i = 100;
  for (char c : "AKQT98765432J") {
    values_B[c] = i--;
  }
}

int rank(const string &card, bool joker) {
  vector<int> v;
  v.reserve(5);

  memset(mem, 0, sizeof mem);

  for (char c : card) mem[c - '2']++;
  if (joker) mem['J' - '2'] = 0;

  for (char c : card) {
    if (mem[c - '2']) {
      v.push_back(mem[c - '2']);
      mem[c - '2'] = 0;
    }
  }

  sort(v.begin(), v.end());

  if (v.size() == 0 || v.size() == 1) return 7;
  if (v.size() == 2 && v[0] == 1) return 6;
  if (v.size() == 2 && v[0] == 2) return 5;
  if (v.size() == 3 && v[0] == 1 && v[1] == 1) return 4;
  if (v.size() == 3 && v[0] == 1 && v[1] == 2) return 3;
  if (v.size() == 4 && v[0] == 1 && v[1] == 1 && v[2] == 1) return 2;
  if (v.size() == 5) return 1;

  assert(0);
}

class AdventOfCodeSolverDay07 : public AdventOfCodeSolver {
 public:
  ll first_part() { return solve(values_A, false); }
  ll second_part() { return solve(values_B, true); }

  ll solve(int *values, bool is_second_part) {
    prep();
    auto cards = read();

    std::sort(cards.begin(), cards.end(), [&](auto &a, auto &b) {
      int r_a = rank(a.first, is_second_part);
      int r_b = rank(b.first, is_second_part);

      if (r_a != r_b) return r_a < r_b;

      for (int i = 0; i < 5; i++) {
        if (a.first[i] != b.first[i]) {
          return values[a.first[i]] < values[b.first[i]];
        }
      }

      return false;
    });

    ll result = 0;
    for (int i = 0; i < cards.size(); i++) {
      result += cards[i].second * (i + 1);
    }

    return result;
  }

 private:
  vector<pair<string, ll>> read() {
    std::ifstream infile("../input.txt");
    string line;
    vector<pair<string, ll>> cards;
    cards.reserve(1 << 10);

    while (std::getline(infile, line)) {
      cards.push_back({line.substr(0, 5), stoll(line.substr(6))});
    }

    return cards;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay07{};
  solver.solve_first(251287184);
  solver.solve_second(250757288);
}
