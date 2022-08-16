#pragma once
#include <math.h>

namespace common {

struct ListNode {
  int val;
  ListNode* next;
  ListNode() : val(0), next(nullptr) {}
  ListNode(int x) : val(x), next(nullptr) {}
  ListNode(int x, ListNode* next) : val(x), next(next) {}
};

class Trie {
 public:
  bool is_final;
  Trie* children[26];

  Trie() {
    this->is_final = false;
    memset(this->children, 0, sizeof(this->children));
  }

  void insert(std::string word) { insert_internal(word, 0); }

  bool search(std::string word) { return this->search_internal(word, 0, true); }

  bool startsWith(std::string prefix) { return this->search_internal(prefix, 0, false); }

 private:
  void insert_internal(const std::string& word, int i) {
    if (i == word.length()) {
      this->is_final = true;
      return;
    }

    if (!this->children[word[i] - 'a']) {
      this->children[word[i] - 'a'] = new Trie();
    }

    this->children[word[i] - 'a']->insert_internal(word, i + 1);
  }

  bool search_internal(const std::string& word, int i, bool search_full_word) {
    if (i == word.length()) {
      return search_full_word ? this->is_final : true;
    }

    if (!this->children[word[i] - 'a']) return false;
    return this->children[word[i] - 'a']->search_internal(word, i + 1, search_full_word);
  }
};

std::vector<int> prime_factors(int n) {
  std::vector<int> factors;
  while (n % 2 == 0) {
    factors.push_back(2);
    n = n / 2;
  }

  for (int i = 3; i <= sqrt(n); i += 2) {
    while (n % i == 0) {
      factors.push_back(i);
      n = n / i;
    }
  }
  if (n > 2) factors.push_back(n);
  return factors;
}

int gcd(int a, int b) {
  while (b != 0) {
    int temp = b;
    b = a % b;
    a = temp;
  }
  return a;
}

int lcm(int a, int b) { return (a * b) / gcd(a, b); }
int lowest_common_multiple(int a, int b) { return (a * b) / gcd(a, b); }

}  // namespace common
