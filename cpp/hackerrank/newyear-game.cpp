#include <iostream>
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

int main() {
  int T;
  std::cin >> T;
  while (T--) {
    int n, ones = 0, twos = 0, zeroes = 0;
    std::cin >> n;
    FORN(i, n) {
      int k;
      std::cin >> k;
      if (k % 3 == 0)
        zeroes = (zeroes + 1) % 2;
      else if (k % 3 == 1)
        ones = (ones + 1) % 2;
      else if (k % 3 == 2)
        twos = (twos + 1) % 2;
    }

    LOG(ones == 1 || twos == 1 ? "Balsa" : "Koca");
  }
}
