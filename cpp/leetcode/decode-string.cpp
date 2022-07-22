#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <stack>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::string;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

class Solution {
 public:
  string decodeString(string s) {
    std::stack<string> st;
    int i = 0;
    string len = "", res = "";
    for (char c : s) {
      if (c == '[') {
        st.push(res);
        st.push(len);
        len = "";
        res = "";
      } else if (c == ']') {
        auto l = stoi(st.top());
        st.pop();
        auto r = st.top();
        st.pop();
        string tmp = res;
        res = r;
        while (l--) res += tmp;
      } else if (isdigit(c)) {
        len += string(1, c);
      } else {
        res += string(1, c);
      }
    }

    return res;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  LOG(s.decodeString("100[leetcode]"));
  assert(s.decodeString("3[a2[c]]") == "accaccacc");
}
#endif
