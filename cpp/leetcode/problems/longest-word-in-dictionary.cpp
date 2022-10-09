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
  Trie* children[26];

  Trie() {
    this->is_final = false;
    memset(this->children, 0, sizeof(this->children));
  }

  void insert(std::string word) {
    insert_internal(word, 0);
  }

  string build() {
    vector<char> curr;
    return this->build_internal(0, curr);
  }

 private:
  void insert_internal(const std::string& word, int i) {
    if (i == word.length()) {
      this->is_final = true;
      return;
    }

    const auto c = word[i];
    if (!this->children[c - 'a']) {
      this->children[c - 'a'] = new Trie();
    }

    this->children[c - 'a']->insert_internal(word, i + 1);
  }

  string build_internal(int i, vector<char>& curr) {
    if (!this->is_final) return "";

    string r(curr.begin(), curr.end());
    for (int j = 0; j < 26; j++) {
      if (this->children[j]) {
        curr.push_back('a' + j);
        auto temp = this->children[j]->build_internal(i + 1, curr);
        if (temp.length() > r.length()) {
          r = temp;
        }

        curr.pop_back();
      }
    }

    return r;
  }
};

class Solution {
 public:
  string longestWord(vector<string>& words) {
    Trie* t = new Trie();
    t->is_final = true;
    for (const auto word : words) t->insert(word);
    return t->build();
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
