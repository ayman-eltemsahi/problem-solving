#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

struct Point {
  int value;
  int index;
};

struct comp {
  inline bool operator()(const struct Point &p1, const struct Point &p2) {
    return (p1.value > p2.value);
  }
};

int main() {
  int t;
  cin >> t;
  while (t--) {
    int n;
    cin >> n;
    vector<struct Point> vec;
    vec.reserve(n);
    for (int i = 0; i < n; i++) {
      struct Point p;
      p.index = i;
      cin >> p.value;
      vec.push_back(p);
    }
    std::sort(vec.begin(), vec.end(), comp());
    int p = vec[0].index;
    int turns = 1;
    for (size_t i = 1; i < n; i++) {
      if (vec[i].index < p) {
        p = vec[i].index;
        turns++;
      }
    }

    printf(turns % 2 == 1 ? "BOB\n" : "ANDY\n");
  }
}
