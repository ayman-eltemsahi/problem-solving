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
#define MAXN 100001
using std::string;
using std::vector;

int main() {
  int T;
  std::cin >> T;
  while (T--) {
    int n;
    ll H;
    std::cin >> n >> H;
    vector<int> v;
    v.reserve(n);
    FORN(i, n) {
      int a;
      std::cin >> a;
      v.push_back(a);
    }
    std::sort(v.begin(), v.end(), std::greater<int>());
    ll x = v[0];
    ll y = v[1];

    ll ans = H / (x + y);
    H -= ans * (x + y);
    ans *= 2;
    if (H > 0) {
      H -= x;
      ans++;
    }
    if (H > 0) {
      H -= y;
      ans++;
    }
    LOG(ans);
  }
}
