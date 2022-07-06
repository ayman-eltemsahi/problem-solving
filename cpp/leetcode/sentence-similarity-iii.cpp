#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

typedef std::vector<std::string> vec;

vec explode(const std::string& str, const char& ch) {
  std::string next;
  vec result;

  for (std::string::const_iterator it = str.begin(); it != str.end(); it++) {
    if (*it == ch) {
      if (!next.empty()) {
        result.push_back(next);
        next.clear();
      }
    } else
      next += *it;
  }
  if (!next.empty()) result.push_back(next);
  return result;
}

class Solution {
 public:
  bool areSentencesSimilar(std::string sentence1, std::string sentence2) {
    bool rev = sentence1.length() < sentence2.length();
    auto a = explode(!rev ? sentence1 : sentence2, ' ');
    auto b = explode(!rev ? sentence2 : sentence1, ' ');

    const int x = a.size();
    const int y = b.size();

    // from_beginning
    int i = 0;
    int j = 0;
    while (i < x && j < y) {
      if (a[i] != b[j]) break;
      i++;
      j++;
    }
    if (j == y) return true;

    // from_end
    int i1 = x - 1;
    int j1 = y - 1;
    while (i1 >= 0 && j1 >= 0) {
      if (a[i1] != b[j1]) break;
      i1--;
      j1--;
    }
    if (j1 < 0) return true;

    // from_middle
    return j > j1;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;

  auto k = s.areSentencesSimilar("My name is was is Haley", "My name is Haley");
  LOG(k);
}
#endif
