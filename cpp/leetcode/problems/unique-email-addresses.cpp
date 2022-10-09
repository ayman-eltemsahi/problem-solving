#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <unordered_set>
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
  int numUniqueEmails(vector<string>& emails) {
    std::unordered_set<string> set;

    for (const string email : emails) {
      string name = email.substr(0, email.find('@'));
      string domain = email.substr(1 + email.find('@'));
      if (name.find('+') != string::npos) {
        name = name.substr(0, name.find('+'));
      }

      while (name.find('.') != string::npos) {
        name = name.substr(0, name.find('.')) + name.substr(1 + name.find('.'));
      }

      set.insert(name + "@" + domain);
    }

    return set.size();
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  vector<string> v{".t.est.email+alex@leetcode.com", "test.e.mail+bob.cathy@leetcode.com",
                   "testemail+david@lee.tcode.com"};

  LOG(s.numUniqueEmails(v));
  assert(s.numUniqueEmails(v) == 2);
}
#endif
