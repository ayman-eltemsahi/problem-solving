use std::collections::HashSet;

struct Solution {}

impl Solution {
  pub fn check_if_exist(arr: Vec<i32>) -> bool {
    let mut map: HashSet<i32> = HashSet::new();
    for i in 0..arr.len() {
      map.insert(arr[i]);
    }

    for i in 0..arr.len() {
      if map.contains_key(i * 2) {
        return true;
      }
    }
    return false;
  }
}

fn main() {
  let k = Solution::word_pattern("abba".to_string(), "dog cat cat dog".to_string());
  println!("{}", k);
}
