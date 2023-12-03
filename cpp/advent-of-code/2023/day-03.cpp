#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

#define GEAR '*'

const vector<vector<int>> steps = {{1, 1}, {1, -1}, {-1, 1}, {-1, -1},
                                   {1, 0}, {-1, 0}, {0, 1},  {0, -1}};

typedef vector<vector<vector<int>>> nums_list;

pair<nums_list, unordered_map<int, int>> read(const vector<string> &lines) {
  int n = lines.size(), m = lines[0].size();
  nums_list nums_at_gear(n, vector<vector<int>>(m));
  unordered_map<int, int> mapping;

  for (int i = 0; i < n; i++) {
    for (int j = 0; j < m; j++) {
      int key = i * n + j;

      if (!isdigit(lines[i][j])) continue;

      int num = 0;
      int y = j;
      while (y < m && isdigit(lines[i][y])) num = num * 10 + (lines[i][y++] - '0');
      mapping[key] = num;

      for (; j < y; j++) {
        for (auto &step : steps) {
          int a = i + step[0];
          int b = j + step[1];
          if (a < 0 || b < 0 || a >= n || b >= m || lines[a][b] == '.' || isdigit(lines[a][b]))
            continue;

          auto &tmp = nums_at_gear[a][b];
          if (find(tmp.begin(), tmp.end(), key) == tmp.end()) {
            nums_at_gear[a][b].push_back(key);
          }
        }
      }
    }
  }

  return {nums_at_gear, mapping};
}

int part_1(const vector<string> &lines) {
  const int n = lines.size(), m = lines[0].size();
  const auto [nums_at_gear, mapping] = read(lines);
  unordered_set<int> selected_keys;

  for (int i = 0; i < n; i++) {
    for (int j = 0; j < m; j++) {
      for (auto r : nums_at_gear[i][j]) selected_keys.insert(r);
    }
  }
  int res = 0;
  for (auto &key : selected_keys) {
    res += mapping.at(key);
  }

  return res;
}

int part_2(const vector<string> &lines) {
  const int n = lines.size(), m = lines[0].size();
  const auto [nums_at_gear, mapping] = read(lines);

  int res = 0;
  for (int i = 0; i < n; i++) {
    for (int j = 0; j < m; j++) {
      if (lines[i][j] == GEAR && nums_at_gear[i][j].size() == 2) {
        res += mapping.at(nums_at_gear[i][j][0]) * mapping.at(nums_at_gear[i][j][1]);
      }
    }
  }

  return res;
}

ll run(bool first) {
  std::ifstream infile("../input.txt");
  string line;

  vector<string> lines;
  while (std::getline(infile, line)) {
    if (!line.empty()) {
      lines.push_back(line);
    }
  }

  return first ? part_1(lines) : part_2(lines);
}

int main() {
  auto start = std::chrono::high_resolution_clock::now();

  int first = run(true);
  int second = run(false);

  auto finish = std::chrono::high_resolution_clock::now();
  auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
  printf("Reuslt      : \x1b[32m%d\x1b[0m\n", first);
  printf("Correct 1   : \x1b[32m%d\x1b[0m\n", 525119);
  printf("Reuslt      : \x1b[32m%d\x1b[0m\n", second);
  printf("Correct 2   : \x1b[32m%d\x1b[0m\n", 76504829);
  printf("Time        : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
}
