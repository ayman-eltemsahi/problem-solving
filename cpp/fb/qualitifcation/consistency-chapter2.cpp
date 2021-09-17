#include <bits/stdc++.h>

typedef long long int ll;
typedef std::pair<int, int> iPair;
#define INF 1000000000
#define FORN(i, n) for (int i = 0; i < (n); i++)

int distance[26][26];
bool visited[26][26];
void clear() {
  FORN(i, 26) {
    FORN(j, 26) { distance[i][j] = -1; }
  }
}
void clear_visited() {
  FORN(i, 26) {
    FORN(j, 26) { visited[i][j] = false; }
  }
}

void dijkstra(int src) {
  std::queue<int> pq;
  clear_visited();

  pq.push(src);
  distance[src][src] = 0;

  while (!pq.empty()) {
    int u = pq.front();
    pq.pop();

    FORN(i, 26) {
      int weight = distance[u][i];
      if (i == src || weight == -1)
        continue;
      if (distance[src][i] == -1 ||
          distance[src][i] > distance[src][u] + weight ||
          (distance[src][i] == distance[src][u] + weight && !visited[src][i])) {
        distance[src][i] = distance[src][u] + weight;
        pq.push(i);
      }
      visited[src][i] = true;
    }
  }
}

int main() {
  int T;
  std::cin >> T;
  FORN(t, T) {
    std::string input;
    std::cin >> input;
    size_t n = input.size();
    clear();

    int k;
    std::cin >> k;
    FORN(i, k) {
      char a, b;
      std::cin >> a >> b;
      distance[(a - 'A')][(b - 'A')] = 1;
    }

    FORN(i, 26) dijkstra(i);

    int ans = -1;
    FORN(i, 26) {
      int a = 0;
      FORN(j, input.size()) {
        if (i == input[j] - 'A')
          continue;
        int d = distance[input[j] - 'A'][i];
        if (d == -1) {
          a = -1;
          break;
        }
        a += d;
      }

      if (a != -1 && (ans == -1 || a < ans))
        ans = a;
    }

    printf("Case #%d: %d\n", t + 1, ans);
  }
}
