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

std::vector<int> multiply_v_num(const std::vector<int> &v, int n, int zeros) {
  std::vector<int> res;
  res.reserve(zeros + v.size());
  FORN(i, zeros) res.push_back(0);
  int curry = 0;

  for (const int a : v) {
    int g = a * n + curry;
    res.push_back(g % 10);
    curry = g / 10;
  }
  if (curry > 0)
    res.push_back(curry);
  return res;
}

class Solution {
public:
  std::string multiply(std::string num1, std::string num2) {
    const std::vector<int> v1 = to_vector(num1);
    const std::vector<int> v2 = to_vector(num2);

    std::vector<std::vector<int>> vs;
    vs.reserve(v2.size());
    size_t max_len = 0;
    FORN(i, v2.size()) {
      vs.push_back(multiply_v_num(v1, v2[i], i));
      max_len = std::max(max_len, vs.back().size());
    }

    std::vector<int> res;
    res.reserve(max_len + 10);
    int curry = 0;
    FORN(i, max_len) {
      int single = curry;
      for (const auto &vec : vs) {
        if (vec.size() > i) {
          single += vec[i];
        }
      }
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
  auto ans = s.multiply(
      "22435657687243546578797867568978675645342354657",
      "2435465768790243546576879786756454258786756454345678797865434546545");
  const auto correct =
      "546412762975773416721278976920481994276098792139289053122254176120884234"
      "34188873818458871754880603572863364010065";

  assert(ans == correct);
  assert(s.multiply("2435", "0") == "0");
  LOG("=== asserts passed ===");
}
#endif
