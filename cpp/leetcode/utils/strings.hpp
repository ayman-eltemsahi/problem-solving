#pragma once
#include <string>
#include <vector>

std::vector<std::string> split_string(const std::string& value, std::string delimiter) {
  size_t pos = 0;
  std::string copy = value;
  std::vector<std::string> res;
  while ((pos = copy.find(delimiter)) != std::string::npos) {
    res.push_back(copy.substr(0, pos));
    copy.erase(0, pos + delimiter.size());
  }
  if (!copy.empty()) res.push_back(copy);

  return res;
}
