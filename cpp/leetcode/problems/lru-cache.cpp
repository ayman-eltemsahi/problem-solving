#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <unordered_map>
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

class Node {
 public:
  int key;
  int value;
  Node* next;
  Node* prev;
  Node(int k, int v) : key(k), value(v), next(nullptr), prev(nullptr) {
  }
};

class LRUCache {
 public:
  int capacity = 0;
  Node* tail;
  Node* head;
  std::unordered_map<int, Node*> list;

  LRUCache(int c) : capacity(c), tail(nullptr), head(nullptr) {
  }

  int get(int key) {
    if (list.find(key) == list.end()) return -1;
    if (this->tail->key == key) return this->tail->value;

    auto node = this->removeNode(key);
    this->putNode(key, node);
    return node->value;
  }

  void put(int key, int value) {
    Node* node;
    if (list.find(key) == list.end()) {
      node = new Node(key, value);
    } else {
      node = this->removeNode(key);
      node->value = value;
    }

    this->putNode(key, node);
  }

  void putNode(int key, Node* node) {
    if (!this->head) this->head = node;
    if (this->tail) this->tail->next = node;

    node->prev = tail;
    this->tail = node;

    list[key] = node;

    while (list.size() > this->capacity) {
      list.erase(head->key);
      head = head->next;
      head->prev = nullptr;
    }
  }

  Node* removeNode(int key) {
    Node* node = list[key];

    auto prev = node->prev;
    auto next = node->next;

    if (prev) prev->next = next;
    if (next) next->prev = prev;

    if (node == this->tail) {
      this->tail = prev;
    }

    if (node == this->head) {
      this->head = next;
    }

    node->next = nullptr;
    node->prev = nullptr;
    return node;
  }
};
