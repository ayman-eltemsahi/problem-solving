#pragma once
#include <string>
#include <vector>

namespace utils {

using std::string;
using std::vector;

vector<string> split_string(const string& value, string delimiter) {
  size_t pos = 0;
  string copy = value;
  vector<string> res;
  while ((pos = copy.find(delimiter)) != string::npos) {
    res.push_back(copy.substr(0, pos));
    copy.erase(0, pos + delimiter.size());
  }
  if (!copy.empty()) res.push_back(copy);

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
