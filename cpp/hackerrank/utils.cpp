#include "utils.hpp"
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

namespace utils {

std::string binary(int num, int k) {
  std::string s;
  for (int i = k - 1; i >= 0; i--) {
    s += std::to_string((num >> i) & 1);
  }
  return s;
}

bool is_prime(long long int n) {
  if (n == 2)
    return true;
  if (n < 2 || n % 2 == 0)
    return false;

  for (int i = 3; i <= n / 2; i++) {
    if (n % i == 0) {
      return false;
    }
  }

  return true;
}

} // namespace utils
