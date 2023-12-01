#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#include "benchmark.hpp"
#endif

const vector<string> nums{"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

int getA(const string& line) {
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

  return 0;
}

int getB(const string& line) {
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

  return 0;
}

int run() {
  std::ifstream infile("../input.txt");
  string line;

  int result = 0;

  while (std::getline(infile, line)) {
    if (line.empty()) continue;

    result += 10 * getA(line) + getB(line);
  }

  return result;
}

int main() {
#ifdef BENCHMARK
  benchmark(run);
#else

  auto start = std::chrono::high_resolution_clock::now();

  int result = run();

  auto finish = std::chrono::high_resolution_clock::now();
  auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
  printf("Reuslt     : \x1b[32m%d\x1b[0m\n", result);
  printf("Correct    : \x1b[32m%d\x1b[0m\n", 54719);
  printf("Time       : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
#endif
}
