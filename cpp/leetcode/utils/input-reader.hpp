#pragma once
#include <vector>
#include <string>
#include <type_traits>
#include <fstream>
#include "read-vector.hpp"

std::vector<std::string> read_input(std::string filename = "input") {
  std::ifstream infile(filename);
  std::string line;
  std::vector<std::string> result;
  while (std::getline(infile, line)) {
    result.push_back(line);
  }
  return result;
}

class Input {
  std::vector<std::string> file;

  int index = 0;

 public:
  Input() {
    this->read_file("../input");
  }
  Input(std::string filename) {
    this->read_file(filename);
  }

  bool hasNext() {
    return this->index < this->file.size();
  }

  std::string peek() {
    if (!this->hasNext()) {
      throw("Input is empty, no more lines");
    }
    return this->file[index];
  }

  std::string next_string() {
    if (!this->hasNext()) {
      throw("Input is empty, no more lines");
    }
    return this->file[index++];
  }

  int next_int() {
    return stoi(this->next_string());
  }

  std::vector<int> next_vector_int() {
    return read_vector_int(this->next_string());
  }

  std::vector<std::string> next_vector_string() {
    return read_vector_string(this->next_string());
  }

  std::vector<std::vector<int>> next_vector_vector_int() {
    return read_vector_vector_int(this->next_string());
  }

  template <typename T>
  T next() {
    if (std::is_same<T, int>()) {
      return this->next_int();
    }

    if (std::is_same<T, std::string>()) {
       return this->next_string();
    }

    if (std::is_same<T, std::vector<std::string>>()) {
      return this->next_vector_string();
    }

    if (std::is_same<T, std::vector<int>>()) {
      return this->next_vector_int();
    }

    if (std::is_same<T, std::vector<std::vector<int>>>()) {
      return this->next_vector_vector_int();
    }

    throw("Unknown type T");
  }

 private:
  void read_file(std::string filename) {
    this->file = read_input(filename);
    this->index = 0;
  }
};
