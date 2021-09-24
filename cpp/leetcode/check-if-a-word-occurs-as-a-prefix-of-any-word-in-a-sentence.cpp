#include <algorithm>
#include <cassert>
#include <iostream>
#include <sstream>
#include <stack>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001

class Solution {
public:
  int isPrefixOfWord(std::string sentence, std::string searchWord) {
    int index = 0;
    for (int i = 0; i < sentence.size(); i++) {
      if (i == 0 || sentence[i] == ' ') {
        index++;
        if (sentence[i] == ' ')
          i++;
        bool yes = true;
        for (int j = 0; j < searchWord.size(); j++, i++) {
          if (searchWord[j] != sentence[i]) {
            yes = false;
            break;
          }
        }

        if (yes)
          return index;

        if (sentence[i] == ' ')
          i--;
      }
    }
    return -1;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.isPrefixOfWord("i love eating burger", "burg") == 4);
  assert(s.isPrefixOfWord("i use triple pillow", "pill") == 4);
  assert(s.isPrefixOfWord("b bu bur burg burger", "burg") == 4);
}
#endif
