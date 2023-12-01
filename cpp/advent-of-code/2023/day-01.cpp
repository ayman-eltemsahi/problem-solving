#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif
vector<string> nums{"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

int getA(string& line) {
  for (int i = 0; i < line.size(); i++) {
    char c = line[i];
    if (c >= '0' && c <= '9') {
      return (c - '0');
    }

    for (int j = 0; j < nums.size(); j++) {
      auto num = nums[j];
      if (line.compare(i, num.size(), num) == 0) {
        return j + 1;
      }
    }
  }

  return -1;
}

int getB(string& line) {
  for (int i = line.size() - 1; i >= 0; i--) {
    char c = line[i];
    if (c >= '0' && c <= '9') {
      return (c - '0');
    }

    for (int j = 0; j < nums.size(); j++) {
      auto num = nums[j];
      if (line.compare(i, num.size(), num) == 0) {
        return j + 1;
      }
    }
  }

  return -1;
}

int main() {
  utils::Input in("../input.txt");

  ll result = 0;

  while (in.has_next()) {
    auto line = in.next_string();
    if (line.empty()) continue;

    result += 10 * getA(line) + getB(line);
  }

  DEBUG(result);
  DEBUG("54719");
}
