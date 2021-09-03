#include <iostream>
#include <stack>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define WHERE(cond)                                                            \
  if (!(cond)) {                                                               \
    continue;                                                                  \
  }
#define vec_int std::vector<int>
#define vec_bool std::vector<bool>

const ll MAX = 1LL << 50;

class ShoppingCenter;

class Road {
public:
  ShoppingCenter *a;
  ShoppingCenter *b;
  ll w;

  Road(ShoppingCenter *_a, ShoppingCenter *_b, ll _w) : a(_a), b(_b), w(_w) {}
};

class ShoppingCenter {
public:
  int index;
  int fish_types;
  std::vector<Road *> roads;
  ll distance[2048];
  bool tainted;

  ShoppingCenter(int i, int types)
      : index(i), fish_types(types), tainted(true) {
    FORN(i, 2048) distance[i] = MAX;
  }
};

void traverse(int k, std::vector<ShoppingCenter> &shopping_centers) {
  std::stack<int> stack;
  stack.push(0);

  while (!stack.empty()) {
    int i = stack.top();
    stack.pop();
    ShoppingCenter &center = shopping_centers[i];
    if (!center.tainted)
      continue;

    center.tainted = false;
    for (Road *road : center.roads) {
      ShoppingCenter *other = road->a->index == i ? road->b : road->a;

      FORN(a, k) {
        WHERE(center.distance[a] != MAX);

        ll new_fish_types = a | center.fish_types | other->fish_types;
        ll new_w = center.distance[a] + road->w;

        ll other_current_w = other->distance[new_fish_types];
        if (new_w < other_current_w) {
          other->distance[new_fish_types] = new_w;
          other->tainted = true;
        }
      }

      stack.push(other->index);
    }
  }
}

void printBinary(int num, int k) {
  printf("%d    ", num);
  FORN(i, 4) printf("%d", (num >> i) & 1);
  printf("\n");
}

int main() {
  int n, m, k;
  std::cin >> n >> m >> k;
  std::vector<ShoppingCenter> shopping_centers;
  FORN(i, n) {
    int fish_n;
    std::cin >> fish_n;
    int types = 0;
    FORN(j, fish_n) {
      int t;
      std::cin >> t;
      t--;
      types |= (1 << t);
    }
    shopping_centers.push_back(ShoppingCenter(i, types));
  }

  FORN(i, m) {
    int u, v, w;
    std::cin >> u >> v >> w;
    u--;
    v--;
    Road *r = new Road(&shopping_centers[u], &shopping_centers[v], w);
    shopping_centers[u].roads.push_back(r);
    shopping_centers[v].roads.push_back(r);
  }

  shopping_centers[0].distance[shopping_centers[0].fish_types] = 0;

  traverse((1 << k) + 1, shopping_centers);

  ShoppingCenter &destination = shopping_centers[n - 1];
  int total = (1 << (k)) - 1;
  ll ans = MAX;

  FORN(i, (1 << k) + 1) {
    WHERE(destination.distance[i] != MAX)
    for (int j = i; j < ((1 << k) + 1); j++) {
      WHERE(destination.distance[j] != MAX)
      if ((i | j) == total) {
        ll cur = std::max(destination.distance[i], destination.distance[j]);
        ans = std::min(ans, cur);
      }
    }
  }

  std::cout << ans << std::endl;
}
