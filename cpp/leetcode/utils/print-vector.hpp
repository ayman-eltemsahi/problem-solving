#pragma once
#include <vector>
#include <iostream>
#include "strings.hpp"
#include "read-number.hpp"

void print_vector_int(const std::vector<int>& input) {
  for (int i = 0; i < input.size(); i++) {
    std::cout << input[i];
    if (i < input.size() - 1) std::cout << ", ";
  }
  std::cout << '\n';
}

void print_vector_vector_int(const std::vector<std::vector<int>>& input) {
  for (int i = 0; i < input.size(); i++) {
    print_vector_int(input[i]);
  }
}

template <typename T>
void print_vector(const std::vector<T>& input) {
  for (int i = 0; i < input.size(); i++) {
    std::cout << input[i];
    if (i < input.size() - 1) std::cout << ", ";
  }
  std::cout << '\n';
}
