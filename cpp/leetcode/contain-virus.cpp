#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <sstream>
#include <vector>
#include <unordered_set>

typedef long long int ll;
typedef std::vector<int> vi;
typedef std::vector<std::vector<int>> vvi;
using std::pair;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"
bool contained_clusters[2501];
bool walls[2][50][50];

class Solution {
 public:
  int n;
  int m;
  int clusters_count;
  vvi cluster;

  void traverse_cluster(int i, int j, int c, vvi& isInfected) {
    if (!isInfected[i][j] || cluster[i][j] != -1) return;
    cluster[i][j] = c;
    if (i) traverse_cluster(i - 1, j, c, isInfected);
    if (i < n - 1) traverse_cluster(i + 1, j, c, isInfected);
    if (j) traverse_cluster(i, j - 1, c, isInfected);
    if (j < m - 1) traverse_cluster(i, j + 1, c, isInfected);
  }

  void make_clusters(vvi& isInfected) {
    clusters_count = 0;

    FORN(i, n) {
      FORN(j, m) {
        if (cluster[i][j] > -1 && !contained_clusters[cluster[i][j]]) cluster[i][j] = -1;
      }
    }

    FORN(i, n) {
      FORN(j, m) {
        if (!isInfected[i][j] || cluster[i][j] > -1) continue;
        while (contained_clusters[clusters_count]) clusters_count++;
        traverse_cluster(i, j, clusters_count++, isInfected);
      }
    }
  }

  int get_cluster_infections(int c, vvi& isInfected) {
    std::unordered_set<int> s;
    FORN(i, n) {
      FORN(j, m) {
        if (cluster[i][j] != c) continue;
        if (i && cluster[i - 1][j] == -1) s.insert((i - 1) * 10000 + j);
        if (i < n - 1 && cluster[i + 1][j] == -1) s.insert((i + 1) * 10000 + j);
        if (j && cluster[i][j - 1] == -1) s.insert(i * 10000 + (j - 1));
        if (j < m - 1 && cluster[i][j + 1] == -1) s.insert(i * 10000 + (j + 1));
      }
    }
    return s.size();
  }

  bool infect_cluster(int c, vvi& isInfected) {
    bool has = false;
    FORN(i, n) {
      FORN(j, m) {
        if (cluster[i][j] != c) continue;
        if (i && !isInfected[i - 1][j]) {
          isInfected[i - 1][j] = true;
          has = true;
        }
        if (i < n - 1 && !isInfected[i + 1][j]) {
          isInfected[i + 1][j] = true;
          has = true;
        }
        if (j && !isInfected[i][j - 1]) {
          isInfected[i][j - 1] = true;
          has = true;
        }
        if (j < m - 1 && !isInfected[i][j + 1]) {
          isInfected[i][j + 1] = true;
          has = true;
        }
      }
    }
    return has;
  }

  bool infect_clusters(vvi& isInfected) {
    bool has = false;
    FORN(c, clusters_count) {
      if (!contained_clusters[c]) {
        has = infect_cluster(c, isInfected) || has;
      }
    }
    return has;
  }

  int get_worst_cluster(vvi& isInfected) {
    int t = -1;
    int res = 0;
    FORN(c, clusters_count) {
      if (contained_clusters[c]) continue;
      int tmp = get_cluster_infections(c, isInfected);
      if (tmp > t) {
        t = tmp;
        res = c;
      }
    }

    return res;
  }

  void put_walls(int c, vvi& isInfected) {
    FORN(i, n) {
      FORN(j, m) {
        if (cluster[i][j] != c) continue;
        if (i && cluster[i - 1][j] == -1) walls[0][i - 1][j] = true;
        if (i < n - 1 && cluster[i + 1][j] == -1) walls[0][i][j] = true;
        if (j && cluster[i][j - 1] == -1) walls[1][i][j - 1] = true;
        if (j < m - 1 && cluster[i][j + 1] == -1) walls[1][i][j] = true;
      }
    }
  }

  int count_walls() {
    int r = 0;
    FORN(i, n) {
      FORN(j, m) {
        r += walls[0][i][j] + walls[1][i][j];
      }
    }
    return r;
  }

  int containVirus(vvi& isInfected) {
    memset(contained_clusters, false, sizeof(contained_clusters));
    memset(walls, false, sizeof(walls));
    n = isInfected.size();
    m = isInfected[0].size();
    cluster.reserve(n);
    FORN(i, n) {
      cluster.push_back(vi(m, -1));
    }

    do {
      make_clusters(isInfected);
      int worst_cluster = get_worst_cluster(isInfected);
      contained_clusters[worst_cluster] = true;
      put_walls(worst_cluster, isInfected);
    } while (infect_clusters(isInfected));

    return count_walls();
  }
};
