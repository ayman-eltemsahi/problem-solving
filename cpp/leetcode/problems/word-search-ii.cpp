#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

class Trie {
 public:
  int word_index = -1;
  Trie* children[26];

  Trie() {
    memset(this->children, 0, sizeof(this->children));
  }

  void insert(string& word, int i, int word_index) {
    if (i == word.length()) {
      this->word_index = word_index;
      return;
    }

    if (!this->children[word[i] - 'a']) {
      this->children[word[i] - 'a'] = new Trie();
    }

    this->children[word[i] - 'a']->insert(word, i + 1, word_index);
  }
};

class Solution {
  int n, m;
  vector<int> res;
  vector<vector<char>> board;

  void check(int i, int j, Trie* t) {
    if (t->word_index >= 0) {
      res.push_back(t->word_index);
      t->word_index = -1;
    }
    if (i < 0 || j < 0 || i == n || j == m || board[i][j] == '-') return;

    Trie* next_trie = t->children[board[i][j] - 'a'];
    if (next_trie == nullptr) return;

    char val = board[i][j];
    board[i][j] = '-';
    check(i + 1, j, next_trie);
    check(i - 1, j, next_trie);
    check(i, j + 1, next_trie);
    check(i, j - 1, next_trie);

    board[i][j] = val;
  }

 public:
  vector<string> findWords(vector<vector<char>>& board, vector<string>& words) {
    this->n = board.size();
    this->m = board[0].size();
    this->board = board;
    Trie* t = new Trie();
    for (int i = 0; i < words.size(); i++) t->insert(words[i], 0, i);

    for (int i = 0; i < n; i++) {
      for (int j = 0; j < m; j++) {
        check(i, j, t);
      }
    }

    vector<string> s_res(res.size());
    for (int i = 0; i < res.size(); i++) s_res[i] = words[res[i]];

    return s_res;
  }
};

#ifdef RUNNING_LOCALLY
int main() {
  Solution s;
  vector<vector<char>> v{vector<char>{'o', 'a', 'a', 'n'}, vector<char>{'e', 't', 'a', 'e'},
                         vector<char>{'i', 'h', 'k', 'r'}, vector<char>{'i', 'f', 'l', 'v'}};
  vector<string> words{"oath", "pea", "eat", "rain"};
  auto res = s.findWords(v, words);
  print_vector(res);
}
#endif
