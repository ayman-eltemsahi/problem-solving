#if defined(RUNNING_LOCALLY)
#include "local-stuff.hpp"
#endif

struct Item {
  int key, count, time;
};

class Compare {
 public:
  bool operator()(Item& a, Item& b) {
    return a.count == b.count ? a.time > b.time : a.count > b.count;
  }
};

class LFUCache {
 public:
  int t;
  unordered_map<int, int> m;
  unordered_map<int, int> counters;
  unordered_map<int, int> time_counters;
  std::priority_queue<Item, vector<Item>, Compare> heap;

  int capacity;

  LFUCache(int capacity) {
    this->capacity = capacity;
    this->t = 0;
  }

  int get(int key) {
    if (this->capacity < 1 || m.find(key) == m.end()) return -1;

    this->t++;
    counters[key]++;
    time_counters[key] = this->t;
    heap.push({key, counters[key], this->t});
    return m[key];
  }

  void put(int key, int value) {
    if (this->capacity < 1) return;
    if (m.find(key) == m.end()) clean();

    this->t++;
    counters[key]++;
    time_counters[key] = this->t;
    heap.push({key, counters[key], this->t++});
    m[key] = value;
  }

  void clean() {
    if (m.size() < this->capacity) return;

    while (time_counters[heap.top().key] != heap.top().time) heap.pop();

    m.erase(heap.top().key);
    counters.erase(heap.top().key);
    time_counters.erase(heap.top().key);
    heap.pop();
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  LFUCache cache(2);
  cache.put(1, 1);
  cache.put(2, 2);
  cache.get(1);
  cache.put(3, 3);
  assert(cache.get(2) == -1);
}
#endif
