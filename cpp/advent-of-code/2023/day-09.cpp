#include "local-stuff.hpp"
#include "aoc-common.hpp"

typedef std::pair<string, string> edge;

class AdventOfCodeSolverDay09 : public AdventOfCodeSolver {
 public:
  ll first_part() { return solve(true); }
  ll second_part() { return solve(false); }

 private:
  ll solve(bool forward) {
    std::ifstream infile("../input.txt");
    string line;

    ll result = 0;
    while (std::getline(infile, line)) {
      if (line.empty()) continue;
      vector<ll> nums;
      for (auto tmp : utils::split_string(line, " ")) {
        nums.push_back(stoi(tmp));
      }

      result += get_next(nums, forward);
    }

    return result;
  }

  ll get_next(vector<ll> &nums, bool forward) {
    if (all_zero(nums)) {
      return 0;
    }

    vector<ll> down;
    down.reserve(nums.size() - 1);
    for (int i = 1; i < nums.size(); i++) {
      down.push_back(nums[i] - nums[i - 1]);
    }
    if (forward) {
      return nums.back() + get_next(down, forward);
    }

    return nums.front() - get_next(down, forward);
  }

  bool all_zero(vector<ll> &nums) {
    for (ll n : nums) {
      if (n != 0) return false;
    }
    return true;
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
  auto solver = AdventOfCodeSolverDay09{};
  solver.solve_first(1696140818);
  solver.solve_second(1152);
}
