#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
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

class Trie {
 public:
  bool is_final;
  std::string word;
  Trie* children[26];

  Trie() {
    this->is_final = false;
    memset(this->children, 0, sizeof(this->children));
  }

  void insert(std::string word) {
    insert_internal(word, 0);
  }

  vector<string> check(std::string word, int end_index) {
    vector<string> arr;
    this->check_internal(arr, word, 0, end_index);
    return arr;
  }

 private:
  void insert_internal(const std::string& word, int i) {
    if (i == word.length()) {
      this->is_final = true;
      this->word = word;
      return;
    }
    if (!this->children[word[i] - 'a']) {
      this->children[word[i] - 'a'] = new Trie();
    }
    this->children[word[i] - 'a']->insert_internal(word, i + 1);
  }

  void check_internal(vector<string>& arr, const std::string& word, int i, int end_index) {
    if (i == end_index) {
      this->add_words(arr);
      return;
    }

    if (!this->children[word[i] - 'a']) return;
    return this->children[word[i] - 'a']->check_internal(arr, word, i + 1, end_index);
  }

  void add_words(vector<string>& arr) {
    if (this->is_final) {
      arr.push_back(this->word);
      if (arr.size() == 3) return;
    }

    for (int i = 0; i < 26; i++) {
      if (this->children[i]) {
        this->children[i]->add_words(arr);
        if (arr.size() == 3) return;
      }
    }
  }
};

class Solution {
 public:
  vector<vector<string>> suggestedProducts(vector<string>& products, string searchWord) {
    vector<vector<string>> res;
    res.reserve(searchWord.length());
    auto t = new Trie();
    for (auto p : products) t->insert(p);

    for (int i = 1; i <= searchWord.length(); i++) {
      res.push_back(t->check(searchWord, i));
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
