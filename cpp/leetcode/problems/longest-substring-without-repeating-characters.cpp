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

class Solution {
public:
  int lengthOfLongestSubstring(std::string s) {
    int i = 0, j = 0, ans = 0;
    std::vector<int> vec;
    FORN(i, 500) vec.push_back(0);

    while (i < s.size() && j < s.size()) {
      vec[s[j]]++;
      while (vec[s[j]] > 1) {
        vec[s[i]]--;
        i++;
      }

      ans = std::max(ans, j - i + 1);
      j++;
    }

    return ans;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.lengthOfLongestSubstring("abcabcbb") == 3);
  assert(s.lengthOfLongestSubstring("bbbbb") == 1);
  assert(s.lengthOfLongestSubstring("") == 0);
  assert(s.lengthOfLongestSubstring("pwwkew") == 3);
}
#endif
