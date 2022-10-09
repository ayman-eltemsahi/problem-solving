#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <unordered_map>
#include <unordered_set>
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

class Encrypter {
 public:
  string keys_map[26];
  std::unordered_map<string, vector<char>> values_map;
  std::unordered_map<string, int> encrypted;

  Encrypter(vector<char>& keys, vector<string>& values, vector<string>& dictionary) {
    for (int i = 0; i < keys.size(); i++) {
      keys_map[keys[i] - 'a'] = values[i];
      values_map[values[i]].push_back(keys[i]);
    }

    for (auto kv : this->values_map) {
      sort(kv.second.begin(), kv.second.end());
      kv.second.erase(unique(kv.second.begin(), kv.second.end()), kv.second.end());
    }

    for (auto& w : dictionary) {
      this->encrypted[this->encrypt(w)]++;
    }
  }

  string encrypt(string word) {
    string res;
    for (char c : word) {
      if (this->keys_map[c - 'a'].empty()) return "";
      res += this->keys_map[c - 'a'];
    }
    return res;
  }

  int decrypt(string word) {
    return this->encrypted[word];
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  // Encrypter* obj = new Encrypter(keys, values, dictionary);
  // string param_1 = obj->encrypt(word1);
  // int param_2 = obj->decrypt(word2);

  // assert(s.test() == true);
}
#endif
