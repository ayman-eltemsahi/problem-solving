#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <queue>
#include <unordered_set>
#include <unordered_map>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::max;
using std::min;
using std::pair;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

int n;
bool is_near(string& a, string& b) {
  int c = 0;
  for (int i = 0; i < n; i++)
    if (a[i] != b[i]) {
      if (c) return false;
      c = 1;
    }
  return c == 1;
}

void backtrack(int i, int d, vector<int>& distance, vector<string>& curr,
               vector<vector<string>>& res, vector<string>& wordList) {
  if (d == 1) {
    curr.push_back(wordList[i]);
    res.push_back(vector<string>(curr));
    reverse(res.back().begin(), res.back().end());
    curr.pop_back();
    return;
  }

  curr.push_back(wordList[i]);

  d--;
  for (int j = 0; j < distance.size(); j++) {
    if (distance[j] == d && is_near(wordList[j], wordList[i])) {
      backtrack(j, d, distance, curr, res, wordList);
    }
  }

  curr.pop_back();
}

class Solution {
 public:
  vector<vector<string>> findLadders(string start, string end, vector<string>& wordList) {
    n = start.length();
    int startIndex = -1;
    for (int i = 0; i < wordList.size(); i++) {
      if (wordList[i] == start) {
        startIndex = i;
        break;
        break;
      }
    }
    if (startIndex == -1) {
      wordList.push_back(start);
      startIndex = wordList.size() - 1;
    }

    vector<int> distance(wordList.size(), wordList.size() + 1);
    vector<vector<int>> near(wordList.size());

    int endI = -1;
    for (int i = 0; i < wordList.size(); i++) {
      if (wordList[i] == end) {
        endI = i;
        break;
      }
    }

    for (int i = 0; i < wordList.size(); i++) {
      for (int j = i + 1; j < wordList.size(); j++) {
        if (is_near(wordList[i], wordList[j])) {
          near[i].push_back(j);
          near[j].push_back(i);
        }
      }
    }

    if (endI == -1) return {};

    auto cmp = [&distance](int a, int b) { return distance[a] > distance[b]; };

    std::priority_queue<int, std::vector<int>, decltype(cmp)> q(cmp);
    q.push(startIndex);
    distance[startIndex] = 1;

    while (!q.empty()) {
      auto p = q.top();
      q.pop();

      int d = distance[p] + 1;

      for (int w : near[p]) {
        if (distance[w] > d) {
          distance[w] = d;
          q.push(w);
        }
      }
    }

    if (distance[endI] == wordList.size() + 1) return {};
    int d = distance[endI];
    vector<vector<string>> res;
    vector<string> curr;
    backtrack(endI, d, distance, curr, res, wordList);
    return res;
  }
};

#if defined(RUNNING_LOCALLY)
#include "utils/read-vector.hpp"
#include "utils/print-vector.hpp"
int main() {
  Solution s;
  // vector<string> v{"hot", "dot", "dog", "lot", "log", "cog"};
  vector<string> v{"ymann", "yycrj", "oecij", "ymcnj", "yzcrj", "yycij",
                   "xecij", "yecij", "ymanj", "yzcnj", "ymain"};
  LOG(s.findLadders("ymain", "oecij", v).size());
}
#endif
