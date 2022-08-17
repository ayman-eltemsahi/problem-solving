#pragma once
#include <vector>
#include <iostream>
#include "strings.hpp"
#include "read-number.hpp"

namespace utils {

using std::string;
using std::vector;

void print_vector_int(const vector<int>& input) {
  for (size_t i = 0; i < input.size(); i++) {
    std::cout << input[i];
    if (i < input.size() - 1) std::cout << ", ";
  }
  std::cout << '\n';
}

void print_vector_vector_int(const vector<vector<int>>& input) {
  for (size_t i = 0; i < input.size(); i++) {
    print_vector_int(input[i]);
  }
}

template <typename T>
void print_vector(const vector<T>& input) {
  for (size_t i = 0; i < input.size(); i++) {
    std::cout << input[i];
    if (i < input.size() - 1) std::cout << ", ";
  }
  std::cout << '\n';
}

template <typename T>
void print_vector_vector(const vector<vector<T>>& input) {
  for (size_t i = 0; i < input.size(); i++) {
    print_vector(input[i]);
  }
}

template <class T>
std::string join_vector(const std::vector<T>& vec, std::string sep = ",") {
  std::stringstream ss;
  for (size_t i = 0; i < vec.size(); i++) {
    ss << vec[i];
    if (i != vec.size() - 1) {
      ss << sep;
    }
  }
  return ss.str();
}

}  // namespace utils
