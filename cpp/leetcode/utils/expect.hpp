#pragma once
#include <cassert>
#include <iostream>

namespace utils {

const auto RED = "\033[1;31m";
const auto GREEN = "\033[1;32m";
const auto RESET = "\033[0m";

void expect_equal(const string& result, Input& in) {
  const string correct = in.next_string();
  auto color = result == correct ? GREEN : RED;
  std::cout << color << "Result  :  " << result << RESET << "\n";
  std::cout << "Correct :  " << correct << "\n";
  std::cout << "\n";

  if (result == correct) {
    std::cout << GREEN << "Match" << RESET << '\n';
    return;
  }

  std::cout << RED << "Does not match" << RESET << '\n';
  assert(result == correct);
}

void expect_equal(bool result, Input& in) { expect_equal(string(result ? "true" : "false"), in); }
void expect_equal(int result, Input& in) { expect_equal(std::to_string(result), in); }
void expect_equal(long long result, Input& in) { expect_equal(std::to_string(result), in); }
void expect_equal(const vector<int>& result, Input& in) {
  string v = "[" + join_vector(result) + "]";
  expect_equal(v, in);
}
void expect_equal(const vector<long long>& result, Input& in) {
  string v = "[" + join_vector(result) + "]";
  expect_equal(v, in);
}

}  // namespace utils
