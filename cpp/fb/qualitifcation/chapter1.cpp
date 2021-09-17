#include <bits/stdc++.h>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)

int letters[26];

void clear() {
  FORN(i, 26) { letters[i] = 0; }
}
bool isVowel(char c) {
  return c == 'A' || c == 'O' || c == 'U' || c == 'E' || c == 'I';
}

int main() {
  int T;
  std::cin >> T;
  FORN(t, T) {
    std::string input;
    std::cin >> input;
    size_t n = input.size();

    clear();
    char maxVowel = '-';
    char maxConst = '-';
    int vowels = 0;
    int consts = 0;
    for (char &c : input) {
      letters[c - 'A']++;
      if (isVowel(c)) {
        vowels++;
        if (maxVowel == '-' || letters[c - 'A'] > letters[maxVowel - 'A'])
          maxVowel = c;
      } else {
        consts++;
        if (maxConst == '-' || letters[c - 'A'] > letters[maxConst - 'A'])
          maxConst = c;
      }
    }

    int ansV =
        maxVowel == '-' ? n : consts + 2 * (vowels - letters[maxVowel - 'A']);
    int ansC =
        maxConst == '-' ? n : vowels + 2 * (consts - letters[maxConst - 'A']);
    printf("Case #%d: %d\n", t + 1, std::min(ansC, ansV));
  }
}
