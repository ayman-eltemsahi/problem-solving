
#include <iostream>
#include <sstream>
#include <string>
#include <vector>

namespace utils {
std::string binary(int num, int k = 32);
bool is_prime(long long int n);

template <class T>
std::string join_vector(const std::vector<T> &vec, std::string sep) {
  std::stringstream ss;
  for (size_t i = 0; i < vec.size(); i++) {
    ss << vec[i];
    if (i != vec.size() - 1) {
      ss << sep;
    }
  }
  return ss.str();
}
} // namespace utils
