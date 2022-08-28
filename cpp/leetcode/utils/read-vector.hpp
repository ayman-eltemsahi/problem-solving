#pragma once
#include <vector>
#include "strings.hpp"
#include "read-number.hpp"

namespace utils {

using std::string;
using std::vector;

// [[1,3,5,7],[10,11,16,20],[23,30,34,60]]

vector<int> read_vector_int(const string& input) {
  auto n = input.length();

  auto copy = trim_string(input);
  if (copy.front() == '[') {
    copy = copy.substr(1, n);
    n--;
  }

  if (copy.back() == ']') {
    copy = copy.substr(0, n - 1);
    n--;
  }
  copy = trim_string(copy);

  const auto numbers = split_string(copy, ",");
  vector<int> res;
  for (const auto num : numbers) {
    res.push_back(read_int(trim_string(num)));
  }

  return res;
}

vector<vector<int>> read_vector_vector_int(const string& input) {
  auto n = input.length();

  auto copy = trim_string(input);
  if (copy.front() == '[' && copy.back() == ']') {
    copy = copy.substr(1, n - 2);
    n = copy.length();
  }
  copy = trim_string(copy);

  const auto split = split_string(copy, "],");

  vector<vector<int>> res;
  res.reserve(split.size());
  for (const auto item : split) {
    res.push_back(read_vector_int(trim_string(item)));
  }

  return res;
}

vector<string> read_vector_string(const string& input) {
  auto n = input.length();

  auto copy = trim_string(input);
  if (copy.front() == '[') {
    copy = copy.substr(1, n);
    n--;
  }

  if (copy.back() == ']') {
    copy = copy.substr(0, n - 1);
    n--;
  }
  copy = trim_string(copy);

  auto values = split_string(copy, ",");
  for (int i = 0; i < values.size(); i++) {
    auto tmp = trim_string(values[i]);
    if (tmp.length() > 2 && tmp[0] == '"' && tmp.back() == '"') {
      tmp = tmp.substr(1, tmp.length() - 2);
    }
    values[i] = tmp;
  }

  return values;
}

}  // namespace utils
