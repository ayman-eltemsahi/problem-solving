#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <queue>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
// const ll MOD = ll(1e9 + 7);

struct ListNode {
  int val;
  ListNode* next;
  ListNode() : val(0), next(nullptr) {
  }
  ListNode(int x) : val(x), next(nullptr) {
  }
  ListNode(int x, ListNode* next) : val(x), next(next) {
  }
};

class Solution {
 public:
  ListNode* mergeKLists(vector<ListNode*>& lists) {
    auto cmp = [](ListNode* a, ListNode* b) { return a->val > b->val; };

    std::priority_queue<ListNode*, std::vector<ListNode*>, decltype(cmp)> queue(cmp);

    for (const auto list : lists)
      if (list) queue.push(list);

    ListNode* res = nullptr;
    ListNode* curr = nullptr;

    while (!queue.empty()) {
      auto node = queue.top();
      queue.pop();

      if (!res) {
        res = curr = node;
      } else {
        curr->next = node;
        curr = node;
      }
      if (node->next) queue.push(node->next);
      node->next = nullptr;
    }

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
