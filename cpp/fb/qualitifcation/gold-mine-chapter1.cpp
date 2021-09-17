#include <bits/stdc++.h>

typedef long long int ll;
#define READ_INT(x)                                                            \
  int x;                                                                       \
  std::cin >> x;
#define READ_INT_2(x, y)                                                       \
  int x, y;                                                                    \
  std::cin >> x >> y;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define intVec std::vector<int>
#define boolVec std::vector<bool>
#define DEBUG                                                                  \
  { printf("DEBUG: %d\n", dx++); }
class Cave;

class Bridge {
public:
  Cave *a;
  Cave *b;
  bool valid;

  Bridge(Cave *_a, Cave *_b) : a(_a), b(_b), valid(true) {}
};

class Cave {
public:
  int index;
  ll ore;
  std::vector<Bridge *> bridges;

  Cave(int i, ll o) : index(i), ore(o) {}
};

int dx = 0;
ll ans = 0;
int n = 0;
bool has_free = true;

void solve(Cave *cave, const std::vector<Cave *> &caves, ll curAns) {
  if (cave->index == 0 && curAns > ans) {
    ans = curAns;
  }

  for (size_t i = 0; i < cave->bridges.size(); i++) {
    Bridge *bridge = cave->bridges[i];
    if (!bridge->valid)
      continue;
    bridge->valid = false;
    Cave *other = bridge->a == cave ? bridge->b : bridge->a;
    ll tmp = other->ore;
    other->ore = 0;
    solve(other, caves, curAns + tmp);
    other->ore = tmp;
    bridge->valid = true;
  }

  if (has_free) {
    has_free = false;
    for (size_t i = 0; i < n; ++i) {
      if (i == cave->index)
        continue;
      ll tmp = caves[i]->ore;
      caves[i]->ore = 0;
      solve(caves[i], caves, curAns + tmp);
      caves[i]->ore = tmp;
    }
    has_free = true;
  }
}

int main() {
  READ_INT(T);
  FORN(t, T) {
    std::cin >> n;
    std::vector<Cave *> caves;
    caves.reserve(n);
    FORN(i, n) {
      READ_INT(k);
      caves.push_back(new Cave(i, k));
    }

    FORN(i, n - 1) {
      READ_INT_2(a, b);
      a--;
      b--;
      Bridge *bridge = new Bridge(caves[a], caves[b]);
      caves[a]->bridges.push_back(bridge);
      caves[b]->bridges.push_back(bridge);
    }

    ans = caves[0]->ore;
    caves[0]->ore = 0;
    has_free = true;
    solve(caves[0], caves, ans);

    printf("Case #%d: %lld\n", t + 1, ans);
  }
}
