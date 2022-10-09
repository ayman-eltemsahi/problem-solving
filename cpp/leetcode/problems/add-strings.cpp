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

template <class T>
std::string join_vector(const std::vector<T> &vec, std::string sep) {
  std::stringstream ss;
  for (size_t i = 0; i < vec.size(); i++) {
    ss << vec[i];
    if (i != vec.size() - 1) {
      ss << sep;
    }
  }
  return ss.str();
}

std::vector<int> to_vector(const std::string &num) {
  std::vector<int> v;
  v.reserve(num.size());
  for (const char c : num) {
    v.push_back(c - '0');
  }
  std::reverse(v.begin(), v.end());
  return v;
}

class Solution {
public:
  std::string addStrings(std::string num1, std::string num2) {
    const std::vector<int> v1 = to_vector(num1);
    const std::vector<int> v2 = to_vector(num2);

    std::vector<int> res;
    int curry = 0;
    FORN(i, std::max(v1.size(), v2.size())) {
      int single = curry;
      if (v1.size() > i)
        single += v1[i];
      if (v2.size() > i)
        single += v2[i];

      res.push_back(single % 10);
      curry = single / 10;
    }

    while (curry > 0) {
      res.push_back(curry % 10);
      curry /= 10;
    }

    while (res.size() > 1 && res.back() == 0)
      res.pop_back();

    std::reverse(res.begin(), res.end());
    return join_vector(res, "");
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  Solution s;
  assert(s.addStrings("11", "123") == "134");
  assert(s.addStrings("1435676898098978671", "43565768675645342") ==
         "1479242666774624013");
}
#endif
