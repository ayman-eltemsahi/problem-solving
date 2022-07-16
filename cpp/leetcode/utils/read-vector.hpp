#pragma once
#include <vector>
#include "strings.hpp"
#include "read-number.hpp"

// [[1,3,5,7],[10,11,16,20],[23,30,34,60]]

std::vector<int> read_vector_int(const std::string& input) {
  auto n = input.length();

  auto copy = input;
  if (copy.front() == '[') {
    copy = copy.substr(1, n);
    n--;
  }

  if (copy.back() == ']') {
    copy = copy.substr(0, n - 1);
    n--;
  }

  const auto numbers = split_string(copy, ",");
  std::vector<int> res;
  for (const auto num : numbers) {
    res.push_back(read_int(num));
  }

  return res;
}

std::vector<std::vector<int>> read_vector_vector_int(const std::string& input) {
  auto n = input.length();

  auto copy = input;
  if (copy.front() == '[' && copy.back() == ']') {
    copy = copy.substr(1, n - 2);
    n = copy.length();
  }

  const auto split = split_string(copy, "],");

  std::vector<std::vector<int>> res;
  res.reserve(split.size());
  for (const auto item : split) {
    res.push_back(read_vector_int(item));
  }

  return res;
}
