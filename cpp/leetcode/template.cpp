#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

vector<vector<ll>> input() {
  return {

  };
}

void first() {
  auto start = std::chrono::high_resolution_clock::now();

  vector<vector<ll>> lines = input();

  auto finish = std::chrono::high_resolution_clock::now();
  auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();

  ll res = 0;

  printf("Second part: \x1b[32m%lld\x1b[0m\n", res);
  printf("Correct    : \x1b[32m%lld\x1b[0m\n", (ll)6275922);
  printf("Time: \x1b[32m%lld\x1b[0m µs\n", diff);
}

void second() {
  auto start = std::chrono::high_resolution_clock::now();

  vector<vector<ll>> lines = input();
  sort(lines.begin(), lines.end(),
       [](auto&& a, auto&& b) { return a[1] == b[1] ? a[0] < b[0] : a[1] < b[1]; });

  for (auto&& line : lines) {
    ll d = abs(line[0] - line[2]) + abs(line[1] - line[3]);
    line.push_back(d);
  }

  ll res = 0;

  auto finish = std::chrono::high_resolution_clock::now();
  auto diff = std::chrono::duration_cast<std::chrono::milliseconds>(finish - start).count();
  printf("Second part: \x1b[32m%lld\x1b[0m\n", res);
  printf("Correct    : \x1b[32m%lld\x1b[0m\n", (ll)11747175442119);
  printf("Time: \x1b[32m%lld\x1b[0m ms\n", diff);
}

int main() {
  first();
  printf("\n");
  second();
}
