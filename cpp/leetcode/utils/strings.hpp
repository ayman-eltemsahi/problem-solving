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

}  // namespace utils
