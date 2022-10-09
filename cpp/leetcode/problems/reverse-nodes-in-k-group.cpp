#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include "common.hpp"

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

int linked_list_length(ListNode* node) {
  int n = 0;
  while (node) {
    n++;
    node = node->next;
  }
  return n;
}

struct P3 {
  ListNode* newHead;
  ListNode* newTail;
  ListNode* next;
  P3(ListNode* _newHead, ListNode* _newTail, ListNode* _next)
      : newHead(_newHead), newTail(_newTail), next(_next) {
  }
};

P3 reverse(ListNode* node, int k) {
  ListNode* prev = nullptr;
  ListNode* newTail = node;
  while (k--) {
    auto nxt = node->next;
    node->next = prev;
    prev = node;
    node = nxt;
  }

  return P3(prev, newTail, node);
}

class Solution {
 public:
  ListNode* reverseKGroup(ListNode* head, int k) {
    int n = linked_list_length(head);
    if (n < k || k == 1) return head;

    auto p = reverse(head, k);
    auto res = p.newHead;
    auto tail = p.newTail;
    auto node = p.next;
    n -= k;
    while (n >= k) {
      p = reverse(node, k);
      tail->next = p.newHead;
      tail = p.newTail;
      node = p.next;
      n -= k;
    }

    if (n) tail->next = node;

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
