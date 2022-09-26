class UnionFind {

  private int[] parent;

  public UnionFind(int n) {
    parent = new int[n];
    for (int i = 0; i < n; i++) {
      parent[i] = i;
    }
  }

  int find(int x) {
    if (x == parent[x])
      return x;
    return (parent[x] = find(parent[x]));
  }

  int connect(int x, int y) {
    int x_p = find(x);
    int y_p = find(y);
    return (parent[x_p] = y_p);
  }
}
