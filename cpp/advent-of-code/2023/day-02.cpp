#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#include "benchmark.hpp"
#endif

int RED = 12;
int GREEN = 13;
int BLUE = 14;

ll solve(const string &line) {
  auto game = utils::split_string(line, ": ");
  int id = stoi(game[0].substr(4));

  auto cubes = utils::split_string(game[1], ";");
  ll red_max = 0, blue_max = 0, green_max = 0;

  for (auto cube : cubes) {
    auto pieces = utils::split_string(cube, ", ");
    ll red = 0, green = 0, blue = 0;
    for (auto piece : pieces) {
      auto colors = utils::split_string(utils::trim_string(piece), " ");
      int cnt = stoi(colors[0]);
      if (colors[1] == "blue") {
        blue += cnt;
      } else if (colors[1] == "red") {
        red += cnt;
      } else if (colors[1] == "green") {
        green += cnt;
      }
    }

    // if (red > RED || blue > BLUE || green > GREEN) return 0;
    red_max = max(red, red_max);
    blue_max = max(blue, blue_max);
    green_max = max(green, green_max);
  }

  return red_max * blue_max * green_max;
}

int run() {
  std::ifstream infile("../input.txt");
  string line;

  int result = 0;

  while (std::getline(infile, line)) {
    if (line.empty()) continue;

    result += solve(line);
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
  printf("Correct    : \x1b[32m%d\x1b[0m\n", 76008);
  printf("Time       : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
#endif
}
