#include <bits/stdc++.h>

typedef long long int ll;
typedef std::vector<char> charV;
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
    int n;
    std::cin >> n;
    std::vector<int> X;
    std::vector<int> O;
    std::vector<charV> board;
    X.reserve(2 * n);
    O.reserve(2 * n);
    board.reserve(n);
    FORN(i, 2 * n) {
      X.push_back(0);
      O.push_back(0);
      board.push_back(charV());
    }

    FORN(i, n) {
      std::string row;
      std::cin >> row;
      FORN(j, row.size()) {
        char c = row[j];
        if (c == 'X') {
          X[i]++;
          X[j + n]++;
        } else if (c == 'O') {
          O[i]++;
          O[j + n]++;
        }

        board[i].push_back(c);
      }
    }

    int min = -1;
    int count = 0;
    FORN(i, 2 * n) {
      if (O[i] > 0)
        continue;

      if (min != -1 && n - X[i] > min)
        continue;

      if (min == -1 || n - X[i] < min) {
        min = n - X[i];
        count = 0;
      }

      if (n - X[i] == min) {
        count++;
      }

      if (i >= n && min == 1) {
        int emptyIndx = -1;
        FORN(k, n) {
          if (board[k][i - n] == '.') {
            emptyIndx = k;
            break;
          }
        }

        if (n - X[emptyIndx] == 1 && O[emptyIndx] == 0)
          count--;
      }
    }

    if (min == -1) {
      printf("Case #%d: Impossible\n", t + 1);
    } else {
      printf("Case #%d: %d %d\n", t + 1, min, count);
    }
  }
}
