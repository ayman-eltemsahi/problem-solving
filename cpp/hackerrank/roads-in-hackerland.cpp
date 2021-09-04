#include <algorithm>
#include <iostream>
#include <stack>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define LOG3(a, b, c) std::cout << a << ", " << b << ", " << c << "\n"

int how_many[1000000];

class City;

class Edge {
public:
  City *a;
  City *b;
  int cities_before;
  int c;
  bool picked;
  bool passed;

  Edge(City *_a, City *_b, int _c)
      : a(_a), b(_b), cities_before(0), c(_c), picked(false), passed(false) {}
};

class City {
public:
  int index;
  std::vector<Edge *> roads;

  City(int i) : index(i) {}
  Edge *getFirstRoad() {
    for (Edge *r : roads) {
      if (r->picked && !r->passed)
        return r;
    }
    return roads[0];
  }
  int getUnPassedCount() {
    int c = 0;
    for (Edge *r : roads) {
      if (r->picked && !r->passed)
        c++;
    }
    return c;
  }
  int roadsCount() {
    int c = 0;
    for (Edge *r : roads) {
      if (r->picked)
        c++;
    }
    return c;
  }
  ll getPassedCount() {
    ll w = 0;
    for (Edge *r : roads) {
      if (r->picked && r->passed)
        w += r->cities_before;
    }
    return w;
  }
};

class Weight {
public:
  int w;
  ll count;
  ll total;
  Weight(int _w, ll c) : w(_w), count(c), total(_w * c) {}
};

int find_parent(int parent[], int i) {
  if (parent[i] == -1)
    return i;
  return parent[i] = find_parent(parent, parent[i]);
}

void sort_edges_by_weight(std::vector<Edge *> &vec) {
  std::sort(vec.begin(), vec.end(), [](const Edge *first, const Edge *second) {
    return (first->c < second->c);
  });
}

void sort_weights(std::vector<Weight> &vec) {
  std::sort(vec.begin(), vec.end(),
            [](const Weight &first, const Weight &second) {
              return (first.total < second.total);
            });
}

int main() {
  int n, m;
  std::cin >> n >> m;
  std::vector<City> cities;
  std::vector<Edge *> roads;
  FORN(i, n) cities.push_back(City(i));
  FORN(i, m) {
    int a, b, c;
    std::cin >> a >> b >> c;
    Edge *r = new Edge(&cities[a - 1], &cities[b - 1], c);
    roads.push_back(r);
  }

  sort_edges_by_weight(roads);

  int picked_roads = 0;
  int parent[200005];
  FORN(i, n) parent[i] = -1;
  for (int i = 0; i < m && picked_roads < n - 1; i++) {
    Edge *r = roads[i];

    int pa = find_parent(parent, r->a->index);
    int pb = find_parent(parent, r->b->index);
    if (pa == pb)
      continue;

    parent[pa] = pb;
    picked_roads++;
    r->picked = true;
    r->a->roads.push_back(r);
    r->b->roads.push_back(r);
  }

  std::vector<Weight> weights;
  for (City &c : cities) {
    if (c.roadsCount() != 1 || c.getUnPassedCount() == 0)
      continue;

    std::stack<int> stack;
    stack.push(c.index);
    while (!stack.empty()) {
      int i = stack.top();
      stack.pop();

      Edge *r = cities[i].getFirstRoad();
      City *other = r->a->index == i ? r->b : r->a;
      ll passed = 1 + cities[i].getPassedCount();
      r->cities_before = passed;

      // LOG2(r->c, passed * (n - passed));

      r->passed = true;
      weights.push_back(Weight(r->c, passed * (n - passed)));

      if (other->getUnPassedCount() == 1) {
        stack.push(other->index);
      }
    }
  }

  int max_index = 0;
  for (const Weight &w : weights) {
    FORN(i, 32) {
      if (((w.count >> i) & 1) == 1) {
        how_many[w.w + i]++;
        max_index = std::max(max_index, w.w + i);
      }
    }
  }

  FORN(i, max_index + 1) {
    while (how_many[i] > 1) {
      how_many[i + 1]++;
      how_many[i] -= 2;
      max_index = std::max(max_index, i + 1);
    }
  }

  while (max_index >= 0) {
    printf(how_many[max_index] == 0 ? "0" : "1");
    max_index--;
  }
  printf("\n");
}
