struct Solution {}

struct Solver {
  n: usize,
  board: Vec<Vec<char>>,
  cols: Vec<bool>,
  diag1: Vec<bool>,
  diag2: Vec<bool>,
  res: Vec<Vec<String>>,
}

impl Solver {
  pub fn new(n: usize) -> Self {
    let mut board = Vec::new();
    for _ in 0..n {
      board.push(vec!['.'; n]);
    }
    let cols = vec![false; 10];
    let diag1 = vec![false; 20];
    let diag2 = vec![false; 20];
    let res = Vec::new();

    Self {
      n,
      board,
      cols,
      diag1,
      diag2,
      res,
    }
  }

  pub fn solve(&mut self, i: usize) {
    if i == self.n {
      self.add_solution();
      return;
    }

    for j in 0..self.n {
      if !self.cols[j] && !self.diag1[i + j] && !self.diag2[self.n - 1 + j - i] {
        self.board[i][j] = 'Q';
        self.cols[j] = true;
        self.diag1[i + j] = true;
        self.diag2[self.n - 1 + j - i] = true;

        self.solve(i + 1);

        self.board[i][j] = '.';
        self.cols[j] = false;
        self.diag1[i + j] = false;
        self.diag2[self.n - 1 + j - i] = false;
      }
    }
  }

  pub fn add_solution(&mut self) {
    let mut cur: Vec<String> = Vec::new();
    for i in 0..self.n {
      cur.push(self.board[i].iter().collect());
    }

    self.res.push(cur);
  }
}

impl Solution {
  pub fn solve_n_queens(n: i32) -> Vec<Vec<String>> {
    let mut s = Solver::new(n as usize);
    s.solve(0);
    return s.res;
  }
}

fn main() {
  let k = Solution::solve_n_queens(4);
  println!("{}", k.len());
}
