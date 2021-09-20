#include <iostream>
#include <stack>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define LOG3(a, b, c) std::cout << a << ", " << b << ", " << c << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 201

ll distance[MAXN][MAXN];
std::string moves[6] = {"UL", "UR", "R", "LR", "LL", "L"};

struct pair {
  int i, j, m;
};

inline void add(std::vector<struct pair> &vec, int i, int j, int move, int n) {
  if (i >= 0 && j >= 0 && i < n && j < n)
    vec.push_back({i, j, move});
}

inline std::vector<struct pair> get_points(int i, int j, int n) {
  std::vector<struct pair> vec;
  // UL, UR, R, LR, LL, L
  add(vec, i - 2, j - 1, 0, n);
  add(vec, i - 2, j + 1, 1, n);
  add(vec, i, j + 2, 2, n);
  add(vec, i + 2, j + 1, 3, n);
  add(vec, i + 2, j - 1, 4, n);
  add(vec, i, j - 2, 5, n);
  return vec;
}

int main() {
  int n, i1, j1, i2, j2;
  std::cin >> n >> i1 >> j1 >> i2 >> j2;

  FORN(i, MAXN) FORN(j, MAXN) distance[i][j] = (1L << 30);
  distance[i2][j2] = 0;
  std::stack<struct pair> stack;
  stack.push({i2, j2});
  ll min_d = 2 * (std::abs(i1 - i2) + std::abs(j1 - j2));
  while (!stack.empty()) {
    auto p = stack.top();
    stack.pop();

    ll new_dis = distance[p.i][p.j] + 1;
    if (new_dis >= min_d)
      continue;
    for (auto point : get_points(p.i, p.j, n)) {
      if (new_dis < min_d && distance[point.i][point.j] > new_dis) {
        if (point.i == i1 && point.j == j1) {
          min_d = std::min(min_d, new_dis);
        }
        distance[point.i][point.j] = new_dis;
        stack.push(point);
      }
    }
  }

  if (distance[i1][j1] >= (1L << 30)) {
    LOG("Impossible");
  } else {
    ll d = distance[i1][j1];
    int p = i1;
    int q = j1;

    LOG(d);
    while (d--) {
      for (auto point : get_points(p, q, n)) {
        if (distance[point.i][point.j] == d) {
          std::cout << moves[point.m];
          if (d > 0)
            std::cout << " ";
          p = point.i;
          q = point.j;
          break;
        }
      }
    }
    std::cout << "\n";
  }
}
