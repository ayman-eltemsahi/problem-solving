#include <algorithm>
#include <iostream>
#include <stack>
#include <string>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)

int main() {
  int T;
  std::cin >> T;
  FORN(t, T) {
    int n;
    std::string text;
    std::cin >> n >> text;
    ll changes = 0;
    char last = ' ';
    for (char c : text) {
      if (c == 'F')
        continue;
      if (last != c) {
        last = c;
        changes++;
      }
    }

    if (changes > 0)
      changes--;

    std::cout << "Case #" << (t + 1) << ": " << changes << "\n";
  }
}
