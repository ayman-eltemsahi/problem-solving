#include <iostream>
#include <unordered_map>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define LOG3(a, b, c) std::cout << a << ", " << b << ", " << c << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 100001

int main() {
  int total = (1 << 10) - 1;
  ll ans = 0;
  int n;
  std::cin >> n;
  std::unordered_map<int, int> map;
  FORN(i, n) {
    std::string ticket;
    std::cin >> ticket;

    int k = 0;
    for (char c : ticket) {
      k |= (1 << (c - '0'));
    }

    for (auto kv : map) {
      if ((kv.first | k) == total)
        ans += kv.second;
    }
    map[k]++;
  }

  LOG(ans);
}
