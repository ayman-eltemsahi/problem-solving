#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

int main2() {
  std::ifstream infile("../input.txt");
  string line;

  long result = 0;

  while (std::getline(infile, line)) {
  }

  DEBUG(result);
  return 0;
}

int main() {
  utils::Input in("../input.txt");

  long result = 0;

  while (in.has_next()) {
    auto line = in.next_string();
    if (line.empty()) continue;

    auto seg = utils::split_string(line, ",");
    auto l = utils::split_string(seg[0], "-");
    auto r = utils::split_string(seg[1], "-");

    int a = stoi(l[0]), b = stoi(l[1]);
    int c = stoi(r[0]), d = stoi(r[1]);

    if (b >= c && a <= d) result++;
  }

  DEBUG(result);
}
