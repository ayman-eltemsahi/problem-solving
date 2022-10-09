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
  char c;
  Trie* children[26];
  int words = 0;
  int remaining = 0;
  int round = -1;
  Trie(char val) : c(val) {
    memset(this->children, 0, sizeof(children));
  }

  bool dec(const string& s, int i, int r) {
    if (this->round != r) {
      this->round = r;
      this->remaining = this->words;
    }

    if (this->words) {
      return this->remaining-- > 0;
    }

    if (this->children[s[i] - 'a']) {
      return this->children[s[i] - 'a']->dec(s, i + 1, r);
    }

    return false;
  }
};

void add_trie(Trie* head, const string& word, int i) {
  if (!head->children[word[i] - 'a']) {
    head->children[word[i] - 'a'] = new Trie(word[i]);
  }

  if (i == word.length() - 1) {
    head->children[word[i] - 'a']->words++;
    return;
  }

  add_trie(head->children[word[i] - 'a'], word, i + 1);
}

bool check(Trie* head, const string& s, vector<string>& words, int i) {
  int x = words.size();
  int r = i;
  int l = words[0].length();
  while (x && i + (x * l) <= s.length()) {
    if (!head->dec(s, i, r)) return false;
    i += l;
    x--;
  }

  return x == 0;
}

class Solution {
 public:
  vi findSubstring(string s, vector<string>& words) {
    vi res;

    Trie* t = new Trie(0);
    FORN(i, words.size()) {
      add_trie(t, words[i], 0);
    }

    FORN(i, s.length()) {
      if (check(t, s, words, i)) res.push_back(i);
    }

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  vector<string> v{"c", "b", "a", "c", "a", "a", "a", "b", "c"};
  auto r = s.findSubstring("bcabbcaabbccacacbabccacaababcbb", v);
  LOG2("length", r.size());
  for (const auto a : r) LOG(a);
}
#endif
