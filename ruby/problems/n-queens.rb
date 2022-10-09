class Solver
  attr_accessor :n, :board, :cols, :diag1, :diag2, :res

  def initialize(n)
    @n = n
    @board = Array.new(n) { Array.new(n, '.') }
    @cols = Array.new(10, false)
    @diag1 = Array.new(20, false)
    @diag2 = Array.new(20, false)
    @res = []
  end

  def solve(i)
    if i == n
      add_solution
      return
    end

    @n.times do |j|
      next unless !cols[j] && !diag1[i + j] && !diag2[n - 1 + j - i]

      board[i][j] = 'Q'
      cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = true

      solve(i + 1)

      board[i][j] = '.'
      cols[j] = diag1[i + j] = diag2[n - 1 + j - i] = false
    end
  end

  def add_solution
    cur = board.map(&:join)
    res.push(cur)
  end
end

#  param {Integer} n
# @return {String[][]}
def solve_n_queens(n)
  s = Solver.new(n)
  s.solve(0)
  s.res
end
