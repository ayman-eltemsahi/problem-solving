#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <unordered_set>
#include <unordered_map>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 2001
using std::string;
using std::vector;

class Solution {
 public:
  vector<string> getFolderNames(vector<string>& names) {
    vector<string> res;
    std::unordered_set<string> set;
    std::unordered_map<string, int> index;

    for (const auto name : names) {
      int k = index[name];
      while (true) {
        auto new_name = name + (k == 0 ? "" : ("(" + std::to_string(k) + ")"));
        if (set.find(new_name) == set.end()) {
          set.insert(new_name);
          res.push_back(new_name);
          index[name] = k;
#if defined(RUNNING_LOCALLY)
          LOG(new_name);
#endif
          break;
        }
        k++;
      }
    }

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  vector<string> v{"kaido", "kaido(1)", "kaido", "kaido(1)"};
  FORN(i, 10000) v.push_back("kaido");

  s.getFolderNames(v);
}
#endif
