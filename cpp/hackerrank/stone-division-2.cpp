#include <iostream>
#include <unordered_map>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define LOG3(a, b, c) std::cout << a << ", " << b << ", " << c << "\n"
const ll MOD = ll(1e9 + 7);
#define MAXN 100001

std::unordered_map<ll, ll> mem;

ll solve(ll n, const std::vector<ll> &S) {
  if (mem.find(n) == mem.end()) {
    ll ans = 0;
    for (ll s : S) {
      if (n > s && n % s == 0) {
        ans = std::max(ans, 1 + ((n / s) * solve(s, S)));
      }
    }
    mem[n] = ans;
  }
  return mem[n];
}

int main() {
  int q;
  std::cin >> q;
  while (q--) {
    mem.clear();
    ll n, m;
    std::cin >> n >> m;
    std::vector<ll> S;
    S.reserve(m);
    FORN(i, m) {
      ll g;
      std::cin >> g;
      S.push_back(g);
    }

    LOG(solve(n, S));
  }
}
