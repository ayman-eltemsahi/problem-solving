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

class Solution {

  private int ord(String s, int i) {
    return s.codePointAt(i) - "a".codePointAt(0);
  }

  public boolean equationsPossible(String[] equations) {
    UnionFind uf = new UnionFind(27);

    for (String eq : equations) {
      if (eq.charAt(1) == '=')
        uf.connect(ord(eq, 0), ord(eq, 3));
    }

    for (String eq : equations) {
      if (eq.charAt(1) == '!')
        if (uf.find(ord(eq, 0)) == uf.find(ord(eq, 3)))
          return false;
    }
    return true;
  }
}
