# Disjoin set Union Find
class UnionFind
  attr_accessor :parent

  def initialize(n)
    @parent = Array.new(n)
    n.times do |i|
      @parent[i] = i
    end
  end

  def find(x)
    return x if parent[x] == x

    parent[x] = find(parent[x])
    parent[x]
  end

  def connect(x, y)
    x_p = find(x)
    y_p = find(y)
    parent[x_p] = y_p
    y_p
  end
end
