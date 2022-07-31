fn check(cur: &mut Vec<i32>, res: &mut Vec<Vec<i32>>, k: i32, n: i32, s: i32) {
  if k == 0 && n == 0 {
    res.push(cur.to_vec());
  }

  if k <= 0 || n <= 0 || s > n || s > 9 {
    return;
  }

  check(cur, res, k, n, s + 1);

  cur.push(s);
  check(cur, res, k - 1, n - s, s + 1);
  cur.pop();
}

impl Solution {
  pub fn combination_sum3(k: i32, n: i32) -> Vec<Vec<i32>> {
    let mut res: Vec<Vec<i32>> = vec![];
    let mut cur: Vec<i32> = vec![];
    check(&mut cur, &mut res, k, n, 1);
    return res;
  }
}

struct Solution {}
fn main() {
  println!("Here {}", Solution::combination_sum3(3, 7).len());
}
