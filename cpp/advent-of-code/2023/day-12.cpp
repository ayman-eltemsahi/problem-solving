#include "local-stuff.hpp"
#include "aoc-common.hpp"

#define OPERATIONAL '.'
#define DAMAGED '#'
#define UNKNOWN '?'

#define BOTTOM_UP true

class AdventOfCodeSolverDay12 : public AdventOfCodeSolver {
 public:
  ll solve(string& line) {
    auto tmp = utils::split_string(line, " ");
    auto nums_str = utils::split_string(tmp[1], ",");
    vector<ll> nums;
    for (auto& t : nums_str) {
      nums.push_back(stoll(t));
    }

    string condition = tmp[0];
    if (BOTTOM_UP) {
      return bottom_up_dp(condition, nums);
    } else {
      vector<vector<ll>> cache(condition.size() + 2, vector<ll>(nums.size() + 2, -1));
      return top_down_dp(condition, nums, 0, 0, cache);
    }
  }

  ll solve2(string& line) {
    auto tmp = utils::split_string(line, " ");
    auto nums_str = utils::split_string(tmp[1], ",");
    vector<ll> nums_tmp;
    for (auto& t : nums_str) {
      nums_tmp.push_back(stoll(t));
    }

    string condition = tmp[0] + "?" + tmp[0] + "?" + tmp[0] + "?" + tmp[0] + "?" + tmp[0];
    vector<ll> nums;
    nums.reserve(5 * nums_tmp.size());
    for (int _ = 0; _ < 5; _++) {
      for (ll cc : nums_tmp) {
        nums.push_back(cc);
      }
    }

    if (BOTTOM_UP) {
      return bottom_up_dp(condition, nums);
    } else {
      vector<vector<ll>> cache(condition.size() + 2, vector<ll>(nums.size() + 2, -1));
      return top_down_dp(condition, nums, 0, 0, cache);
    }
  }

  ll first_part() {
    auto lines = read();
    ll result = 0;
    for (auto& line : lines) {
      result += solve(line);
    }
    return result;
  }

  ll second_part() {
    auto lines = read();
    ll result = 0;
    for (auto& line : lines) {
      result += solve2(line);
    }
    return result;
  }

 private:
  ll top_down_dp(string& condition, vector<ll>& nums, int c, int n_i, vector<vector<ll>>& cache) {
    if (cache[c][n_i] != -1) return cache[c][n_i];
    if (n_i >= nums.size() && c >= condition.size()) {
      return cache[c][n_i] = 1;
    }

    if (c >= condition.size()) {
      return cache[c][n_i] = 0;
    }

    ll result = 0;
    char cur = condition[c];

    if (cur == OPERATIONAL || cur == UNKNOWN) {
      result += top_down_dp(condition, nums, c + 1, n_i, cache);
    }

    if (n_i >= nums.size()) {
      return cache[c][n_i] = result;
    }
    int cur_num = nums[n_i];

    if (cur == DAMAGED || cur == UNKNOWN) {
      bool valid = c + cur_num - 1 < condition.size();
      for (int i = 0; valid && i < cur_num; i++) {
        valid = (condition[c + i] == DAMAGED || condition[c + i] == UNKNOWN);
      }

      char next = c + cur_num < condition.size() ? condition[c + cur_num] : 'x';

      if (valid && (next == OPERATIONAL || next == UNKNOWN || next == 'x')) {
        result += top_down_dp(condition, nums, c + cur_num + 1, n_i + 1, cache);
      }
    }

    return cache[c][n_i] = result;
  }

  ll bottom_up_dp(string& condition, vector<ll>& nums) {
    int c_size = condition.size(), n_size = nums.size();
    vector<vector<ll>> dp(c_size + 2, vector<ll>(n_size + 2));

    for (int i = 0; i < 2; i++) {
      for (int j = 0; j < 2; j++) {
        dp[c_size + i][n_size + j] = 1;
      }
    }

    for (int c = c_size - 1; c >= 0; c--) {
      for (int n_i = n_size; n_i >= 0; n_i--) {
        char cur = condition[c];
        if (cur == OPERATIONAL || cur == UNKNOWN) {
          dp[c][n_i] = dp[c + 1][n_i];
        }

        if (n_i >= n_size) continue;
        int cur_num = nums[n_i];

        if (cur == DAMAGED || cur == UNKNOWN) {
          bool valid = c + cur_num - 1 < c_size;
          for (int i = 0; valid && i < cur_num; i++) {
            valid = (condition[c + i] == DAMAGED || condition[c + i] == UNKNOWN);
          }

          char next = c + cur_num < c_size ? condition[c + cur_num] : UNKNOWN;

          if (valid && (next == OPERATIONAL || next == UNKNOWN)) {
            dp[c][n_i] += dp[c + cur_num + 1][n_i + 1];
          }
        }
      }
    }

    return dp[0][0];
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
  auto solver = AdventOfCodeSolverDay12{};
  solver.solve_first(7753);
  solver.solve_second(280382734828319);
}
