struct Solution {}

struct UnionFind {
  parent: Vec<i32>,
}

impl UnionFind {
  pub fn new(n: usize) -> Self {
    let mut parent = vec![0; n];
    for i in 0..n {
      parent[i] = i as i32;
    }
    Self { parent }
  }

  pub fn find(&mut self, x: i32) -> i32 {
    if x == self.parent[x as usize] {
      return x;
    }
    self.parent[x as usize] = self.find(self.parent[x as usize]);
    return self.parent[x as usize];
  }

  pub fn connect(&mut self, x: i32, y: i32) -> i32 {
    let x_p = self.find(x);
    let y_p = self.find(y);
    self.parent[x_p as usize] = y_p;
    return y_p;
  }
}

fn c(s: &String, i: i32) -> i32 {
  s.chars().nth(i as usize).unwrap() as i32
}

impl Solution {
  pub fn equations_possible(equations: Vec<String>) -> bool {
    let mut uf = UnionFind::new(26);
    for eq in equations.iter() {
      if c(eq, 1) == '=' as i32 {
        uf.connect(c(eq, 0) - ('a' as i32), c(eq, 3) - ('a' as i32));
      }
    }

    for eq in equations.iter() {
      if c(eq, 1) == '!' as i32 {
        if uf.find(c(eq, 0) - 'a' as i32) == uf.find(c(eq, 3) - 'a' as i32) {
          return false;
        }
      }
    }

    true
  }
}

fn main() {
  let eq = vec!["a==b".to_string(), "b!=a".to_string()];
  let k = Solution::equations_possible(eq);
  println!("{}", k);
}
