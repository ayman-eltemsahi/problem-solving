#include "utils.hpp"
#include <iostream>
#include <string>

namespace utils {

std::string binary(int num, int k) {
  std::string s;
  for (int i = k - 1; i >= 0; i--) {
    s += std::to_string((num >> i) & 1);
  }
  return s;
}

} // namespace utils
