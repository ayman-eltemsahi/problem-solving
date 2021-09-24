#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

struct ListNode {
  int val;
  ListNode *next;
  ListNode() : val(0), next(nullptr) {}
  ListNode(int x) : val(x), next(nullptr) {}
  ListNode(int x, ListNode *next) : val(x), next(next) {}
};

class Solution {
public:
  bool isPalindrome(ListNode *head) {
    int count = 0;
    ListNode *cur = head;
    while (cur) {
      cur = cur->next;
      count++;
    }

    cur = head;
    ListNode *prev = head;
    for (int i = 0; i < count; i++) {
      if (i < count / 2) {
        prev = cur;
        cur = cur->next;
        continue;
      }

      ListNode *nex = cur->next;
      cur->next = prev;
      prev = cur;
      cur = nex;
    }

    bool flag = true;
    cur = head;
    ListNode *tail = prev;
    for (int i = 0; i < count / 2; i++) {
      if (prev->val != cur->val) {
        flag = false;
      }
      prev = prev->next;
      cur = cur->next == nullptr ? cur : cur->next;
    }

    ListNode *cur2 = tail;
    prev = nullptr;
    for (int i = 0; i < count / 2; i++) {
      ListNode *nex = cur2->next;
      cur2->next = prev;
      prev = cur2;
      cur2 = nex;
    }

    return flag;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;

  ListNode *a =
      new ListNode(1, new ListNode(2, new ListNode(2, new ListNode(5))));
  s.isPalindrome(a);

  // a = new ListNode(1, new ListNode(2, new ListNode(1)));
  // LOG(s.isPalindrome(a));
}
#endif
