#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <queue>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

class MedianFinder {
 public:
  std::priority_queue<int> left_side;                                        // max_heap
  std::priority_queue<int, std::vector<int>, std::greater<int>> right_side;  // min_heap

  MedianFinder() {
  }

  void addNum(int num) {
    if (right_side.empty() && left_side.empty()) {
      right_side.push(num);
      return;
    }

    if (left_side.empty() || num > left_side.top()) {
      right_side.push(num);
      this->balance();
      return;
    }

    if (right_side.empty() || num <= right_side.top()) {
      left_side.push(num);
      this->balance();
      return;
    }
  }

  void balance() {
    if (right_side.size() > left_side.size() && right_side.size() - left_side.size() >= 2) {
      left_side.push(right_side.top());
      right_side.pop();
    }

    if (left_side.size() > right_side.size() && left_side.size() - right_side.size() >= 2) {
      right_side.push(left_side.top());
      left_side.pop();
    }
  }

  double findMedian() {
    if (right_side.size() > left_side.size()) return right_side.top();
    if (left_side.size() > right_side.size()) return left_side.top();

    return (right_side.top() + left_side.top()) / 2.0;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  MedianFinder finder;
  finder.addNum(78);
  finder.addNum(14);
  finder.addNum(50);
  assert(finder.findMedian() == 50);
}
#endif
