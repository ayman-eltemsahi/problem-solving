#include "local-stuff.hpp"
#include "aoc-common.hpp"

struct Node {
  int val;
  string label;
  Node* next;
  Node* previous;

  Node(int v, string l) {
    val = v;
    label = l;
    next = nullptr;
    previous = nullptr;
  }
};

class LList {
  Node* head;
  Node* tail;
  unordered_map<string, Node*> map;

 public:
  LList() {
    head = nullptr;
    tail = nullptr;
  }

  void add(int val, string label) {
    if (map.find(label) != map.end()) {
      map[label]->val = val;
      return;
    }

    auto node = new Node(val, label);
    map[label] = node;
    if (!head) {
      head = tail = node;
      return;
    }

    node->previous = tail;
    tail->next = node;
    tail = node;
  }

  void remove(string label) {
    if (map.find(label) == map.end()) {
      return;
    }

    auto node = map[label];
    map.erase(label);

    if (node->previous) node->previous->next = node->next;
    if (node->next) node->next->previous = node->previous;

    if (node == tail) {
      tail = node->previous;
    }
    if (node == head) {
      head = node->next;
    }
  }

  ll value(int mult) {
    auto node = head;
    ll i = 1;
    ll result = 0;
    while (node) {
      result += mult * i * node->val;
      node = node->next;
      i++;
    }
    return result;
  }
};

class AdventOfCodeSolverDay15 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto line = read();
    return hash(line);
  }

  int hash(string& line) {
    ll result = 0, hash = 0;
    for (char c : line) {
      if (c == ',') {
        result += hash;
        hash = 0;
      } else {
        hash += c;
        hash = (hash * 17) % 256;
      }
    }

    result += hash;
    return result;
  }

  ll second_part() {
    auto line = read();
    auto parts = utils::split_string(line, ",");

    LList list[256];
    for (auto part : parts) {
      if (part.back() == '-') {
        part.pop_back();
        list[hash(part)].remove(part);
      } else {
        auto splt = utils::split_string(part, "=");
        auto label = splt[0];
        int val = stoi(splt[1]);
        list[hash(label)].add(val, label);
      }
    }

    ll result = 0;
    for (int i = 0; i < 256; i++) {
      result += list[i].value(i + 1);
    }

    return result;
  }

 private:
  string read() {
    std::ifstream infile("../input.txt");
    string line;
    std::getline(infile, line);

    return line;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay15{};
  solver.solve_first(517551);
  solver.solve_second(286097);
}
