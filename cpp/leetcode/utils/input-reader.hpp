#pragma once
#include <vector>
#include <string>
#include <type_traits>
#include <fstream>
#include "read-vector.hpp"

namespace utils {

using std::string;
using std::vector;

vector<string> read_input(const char* filename) {
  std::ifstream infile(filename);
  string line;
  vector<string> result;
  while (std::getline(infile, line)) {
    result.push_back(line);
  }
  return result;
}

class Input {
  vector<string> file;

  int index = 0;

 public:
  Input(const char* filename) { this->read_file(filename); }

  bool has_next() { return this->index < this->file.size(); }

  string peek() {
    if (!this->has_next()) {
      throw("Input is empty, no more lines");
    }
    return this->file[index];
  }

  string next_string() {
    if (!this->has_next()) {
      throw("Input is empty, no more lines");
    }
    return strip_quotes(this->file[index++]);
  }

  int i() { return stoi(this->next_string()); }
  string s() { return this->next_string(); }

  bool b() {
    auto val = this->next_string();
    assert(val == "true" || val == "false");
    return val == "true";
  }

  vector<int> vi() { return read_vector_int(this->next_string()); }
  vector<string> vs() { return read_vector_string(this->next_string()); }

  vector<vector<int>> vvi() { return read_vector_vector_int(this->next_string()); }
  vector<vector<string>> vvs() { return read_vector_vector_string(this->next_string()); }

 private:
  void read_file(const char* filename) {
    this->file = read_input(filename);
    this->index = 0;
  }

  string strip_quotes(const string& input) {
    if (input.empty()) return input;

    string output = (input[0] == '"' || input[0] == '\'') ? input.substr(1) : input;
    if (!output.empty() && (output.back() == '"' || output.back() == '\'')) output.pop_back();

    return output;
  }
};
}  // namespace utils
