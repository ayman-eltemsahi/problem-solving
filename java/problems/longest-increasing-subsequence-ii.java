class MaxSegmentTree {
  private int n;
  private int[] tree;
  public MaxSegmentTree(int n) {
    this.n = n;
    tree = new int[2 * n];
  }

  public int query(int l, int r) {
    l += n;
    r += n;
    int res = 0;
    while (l < r) {
      if ((l & 1) == 1) {
        res = Math.max(res, tree[l]);
        l += 1;
      }
      if ((r & 1) == 1) {
        r -= 1;
        res = Math.max(res, tree[r]);
      }
      l >>= 1;
      r >>= 1;
    }
    return res;
  }

  public void update(int i, int val) {
    i += n;
    tree[i] = val;
    while (i > 1) {
      i >>= 1;
      tree[i] = Math.max(tree[i * 2], tree[i * 2 + 1]);
    }
  }
};

class Solution {
  public int lengthOfLIS(int[] nums, int k) {
    int n = (int)1e5 + 1;
    MaxSegmentTree tree = new MaxSegmentTree(n + 1);
    for (int i : nums) {
      int lower = Math.max(0, i - k);
      int cur = 1 + tree.query(lower, i);
      tree.update(i, cur);
    }

    return tree.query(0, n);
  }
}
