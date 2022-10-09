#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <random>
#include <math.h>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

class Node {
 public:
  int val;
  Node* next;
  Node* prev;
  Node* below;
  Node* top;

  Node(int v) : val(v), next(nullptr), prev(nullptr), below(nullptr), top(nullptr) {
  }
};

class Skiplist {
 public:
  int size;
  Node* head;

  Skiplist() {
    srand(time(0));
    this->size = 0;
    this->head = new Node(-1);
  }

  void branch_down() {
    int len = 1 + (int)log2(this->size);
    int curr_levels = 1;
    auto node = this->head;
    while (node->below) {
      node = node->below;
      curr_levels++;
    };

    if (curr_levels >= len) return;

    Node* prevNewNode = nullptr;
    while (node) {
      auto newNode = new Node(node->val);
      node->below = newNode;
      newNode->top = node;

      node = node->next;
      newNode->prev = prevNewNode;
      if (prevNewNode) prevNewNode->next = newNode;
      prevNewNode = newNode;
    }
  }

  bool search(int target) {
    auto node = this->head;
    while (node) {
      if (node->val == target) return true;
      if (node->next && node->next->val <= target) {
        node = node->next;
      } else {
        node = node->below;
      }
    }

    return node ? true : false;
  }

  void add(int num) {
    this->size++;

    auto node = this->head;
    Node* found = nullptr;
    while (!found) {
      if (!node->next && node->below) {
        node = node->below;
      } else if (!node->next && !node->below) {
        found = node;
      } else if (node->next->val <= num) {
        node = node->next;
      } else if (node->next->val > num && node->below) {
        node = node->below;
      } else {
        found = node;
      }
    }

    Node* newNodeBelow = nullptr;
    while (found) {
      auto newNodeTop = new Node(num);
      if (found->next) found->next->prev = newNodeTop;
      newNodeTop->next = found->next;
      newNodeTop->prev = found;
      found->next = newNodeTop;

      found = found->top;

      newNodeTop->below = newNodeBelow;
      if (newNodeBelow) {
        newNodeBelow->top = newNodeTop;
      }

      newNodeBelow = newNodeTop;

      if (rand() % 10 > 0) break;
    }

    this->branch_down();
  }

  bool erase(int num) {
    if (!this->size) return false;

    auto node = this->head;
    Node* found = nullptr;
    while (!found) {
      if (node->val == num) {
        if (!node->below) {
          found = node;
        } else {
          node = node->below;
        }
        continue;
      }

      if (!node->next && !node->below) return false;
      if (!node->next && node->below) {
        node = node->below;
        continue;
      }

      // node->next is truthy
      if (node->next->val <= num) {
        node = node->next;
        continue;
      }

      if (node->below)
        node = node->below;
      else
        return false;
    }

    if (!found) return false;
    this->size--;
    while (found) {
      found->prev->next = found->next;

      if (found->next) {
        found->next->prev = found->prev;
      }
      found = found->top;
    }
    return true;
  }

  void print() {
    auto node = this->head;
    while (node) {
      auto node2 = node;

      while (node2) {
        if (node2->val > -1) std::cout << node2->val << "  ";
        node2 = node2->next;
      }

      LOG("");
      node = node->below;
    }
  }

  void printLengths() {
    auto node = this->head;
    while (node) {
      auto node2 = node;

      int k = 0;
      while (node2) {
        k++;
        node2 = node2->next;
      }

      LOG(k);
      node = node->below;
    }
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Skiplist list;
  vi v;
  FORN(i, 2000) v.push_back(i + 1);
  auto rng = std::default_random_engine{};
  std::shuffle(v.begin(), v.end(), rng);

  FORN(i, 2000) {
    list.add(v[i]);
  }

  // list.print();
  list.printLengths();
}
#endif
