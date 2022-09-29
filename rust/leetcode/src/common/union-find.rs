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
