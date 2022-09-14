# Max Segment Tree
# https://codeforces.com/blog/entry/18051
class MaxSegmentTree
  attr_accessor :n, :tree

  def initialize(n)
    @n = n
    @tree = Array.new(2 * n) { 0 }
  end

  def query(l, r)
    l += @n
    r += @n
    ans = 0
    while l < r
      if l & 1 == 1
        ans = [ans, @tree[l]].max
        l += 1
      end

      if r & 1 == 1
        r -= 1
        ans = [ans, @tree[r]].max
      end
      l >>= 1
      r >>= 1
    end

    ans
  end

  def update(i, val)
    i += @n
    @tree[i] = val
    while i > 1
      i >>= 1
      @tree[i] = [@tree[i * 2], @tree[(i * 2) + 1]].max
    end
  end
end
