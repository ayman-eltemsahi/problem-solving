#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

using std::string;
using std::vector;

class Trie {
 public:
  bool is_final;
  Trie* children[26];

  Trie() {
    this->is_final = false;
    memset(this->children, 0, sizeof(this->children));
  }

  void insert(const string& word) {
    insert_internal(word, 0);
  }

  int get_root(const string& word) {
    return this->get_root_internal(word, 0);
  }

 private:
  void insert_internal(const string& word, int i) {
    if (i == word.length()) {
      this->is_final = true;
      return;
    }

    if (!this->children[word[i] - 'a']) {
      this->children[word[i] - 'a'] = new Trie();
    }

    this->children[word[i] - 'a']->insert_internal(word, i + 1);
  }

  int get_root_internal(const string& word, int i) {
    if (this->is_final) return i;
    if (i == word.length()) return -1;

    if (!this->children[word[i] - 'a']) return -1;
    return this->children[word[i] - 'a']->get_root_internal(word, i + 1);
  }
};

class Solution {
 public:
  string replaceWords(vector<string>& dictionary, string sentence) {
    Trie* t = new Trie();
    for (const auto w : dictionary) {
      t->insert(w);
    }

    string res;
    size_t pos = 0;
    int c;
    std::string token;
    while ((pos = sentence.find(" ")) != std::string::npos) {
      token = sentence.substr(0, pos);
      sentence.erase(0, pos + 1);

      c = t->get_root(token);
      res += c < 0 ? token : token.substr(0, c);
      res += " ";
    }

    c = t->get_root(sentence);
    res += c < 0 ? sentence : sentence.substr(0, c);

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  // assert(s.test() == true);
}
#endif
