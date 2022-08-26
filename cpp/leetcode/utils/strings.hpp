#pragma once
#include <string>
#include <vector>

namespace utils {

using std::string;
using std::vector;

vector<string> split_string(const string& value, string delimiter) {
  vector<string> res;
  size_t start = 0, end = value.find(delimiter);
  while (end != string::npos) {
    res.push_back(value.substr(start, end - start));
    start = end + delimiter.length();
    end = value.find(delimiter, start);
  }

  res.push_back(value.substr(start, end));
  if (res.back() == "") res.pop_back();
  return res;
}

string trim_string(const string& value) {
  int i = 0, j = value.size() - 1;
  while (i < value.size() && value[i] == ' ') i++;
  while (j >= 0 && value[j] == ' ') j--;
  if (i > j) return "";

  return value.substr(i, j - i + 1);
}

}  // namespace utils
