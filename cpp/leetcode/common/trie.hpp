#pragma once
#include <string>

namespace common {

using std::string;

class Trie {
 public:
  bool is_final = false;
  Trie* children[26];

  Trie() { memset(this->children, 0, sizeof(this->children)); }

  void insert(const string& word, int i) {
    if (i == word.length()) {
      this->is_final = true;
      return;
    }

    if (!this->children[word[i] - 'a']) {
      this->children[word[i] - 'a'] = new Trie();
    }

    this->children[word[i] - 'a']->insert(word, i + 1);
  }

  bool search(const string& word, int i) {
    if (i == word.length()) return this->is_final;

    if (!this->children[word[i] - 'a']) return false;
    return this->children[word[i] - 'a']->search(word, i + 1);
  }
};

class TriePartialSearch {
 public:
  bool is_final = false;
  TriePartialSearch* children[26];

  TriePartialSearch() { memset(this->children, 0, sizeof(this->children)); }

  void insert(string word) { insert_internal(word, 0); }

  bool search(string word) { return this->search_internal(word, 0, true); }

  bool startsWith(string prefix) { return this->search_internal(prefix, 0, false); }

 private:
  void insert_internal(const string& word, int i) {
    if (i == word.length()) {
      this->is_final = true;
      return;
    }

    if (!this->children[word[i] - 'a']) {
      this->children[word[i] - 'a'] = new TriePartialSearch();
    }

    this->children[word[i] - 'a']->insert_internal(word, i + 1);
  }

  bool search_internal(const string& word, int i, bool search_full_word) {
    if (i == word.length()) {
      return search_full_word ? this->is_final : true;
    }

    if (!this->children[word[i] - 'a']) return false;
    return this->children[word[i] - 'a']->search_internal(word, i + 1, search_full_word);
  }
};

}  // namespace common
