#pragma once
#include <cassert>
#include <iostream>

namespace utils {

const auto RED = "\033[1;31m";
const auto GREEN = "\033[1;32m";
const auto RESET = "\033[0m";

template <typename T>
void expect_equal(T result, T correct) {
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

}  // namespace utils
