#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

typedef pair<string, int> psi;
typedef std::priority_queue<psi, vector<psi>, std::function<bool(psi&, psi&)> > heap;

auto cmpBefore = [](psi& a, psi& b) {
  return a.second == b.second ? a.first < b.first : a.second > b.second;
};
auto cmpAfter = [](psi& a, psi& b) {
  return a.second == b.second ? a.first > b.first : a.second < b.second;
};

class SORTracker {
 public:
  heap before = heap(cmpBefore);
  heap after = heap(cmpAfter);

  SORTracker() {
  }

  void add(string name, int score) {
    before.push({name, score});
    after.push(before.top());
    before.pop();
  }

  string get() {
    auto res = after.top();
    after.pop();
    before.push(res);
    return res.first;
  }
};
