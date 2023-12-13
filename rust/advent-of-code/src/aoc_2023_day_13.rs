use std::fs::read_to_string;

use crate::solver::{self, AdventOfCodeSolver};

fn read() -> Vec<Vec<Vec<char>>> {
  let file = read_to_string("./input.txt").unwrap();

  let mut patterns: Vec<Vec<Vec<char>>> = vec![];

  let mut iter = file.lines();

  let mut tmp: Vec<Vec<char>> = vec![];

  while let Some(line) = iter.next() {
    match line {
      "" => {
        let tmp2 = tmp;
        tmp = vec![];

        patterns.push(tmp2);
      }
      line => {
        tmp.push(line.chars().collect());
      }
    };
  }

  if !tmp.is_empty() {
    patterns.push(tmp);
  }

  patterns
}

fn is_good_col(pattern: &Vec<Vec<char>>, l: usize, r: usize, change: (i32, i32)) -> bool {
  let n = pattern.len();
  let m = pattern[0].len();
  let mut found = change.0 < 0;

  let mut l = l as i32;
  let mut r = r;
  while l >= 0 && r < m {
    for i in 0..n {
      let reverse =
        (i as i32 == change.0 && l == change.1) || (i as i32 == change.0 && r as i32 == change.1);
      let eq = pattern[i][l as usize] == pattern[i][r];

      // if (!eq && !reverse) || (eq && reverse)
      if eq == reverse {
        return false;
      }

      found |= reverse;
    }

    l -= 1;
    r += 1;
  }
  return found;
}

fn is_good_row(pattern: &Vec<Vec<char>>, l: usize, r: usize, change: (i32, i32)) -> bool {
  let n = pattern.len();
  let m = pattern[0].len();
  let mut found = change.0 < 0;

  let mut l = l as i32;
  let mut r = r;
  while l >= 0 && r < n {
    for j in 0..m {
      let reverse =
        (l == change.0 && j as i32 == change.1) || (r as i32 == change.0 && j as i32 == change.1);
      let eq = pattern[l as usize][j] == pattern[r][j];

      // if (!eq && !reverse) || (eq && reverse)
      if eq == reverse {
        return false;
      }

      found |= reverse;
    }

    l -= 1;
    r += 1;
  }
  return found;
}

fn solve_pattern(pattern: &Vec<Vec<char>>, change: (i32, i32)) -> Option<i64> {
  let n = pattern.len();
  let m = pattern[0].len();

  for j in 1..m {
    if is_good_col(pattern, j - 1, j, change) {
      return Some(j as i64);
    }
  }

  for i in 1..n {
    if is_good_row(pattern, i - 1, i, change) {
      return Some((100 * i) as i64);
    }
  }

  return None;
}

struct AdventOfCodeDay13Solver {}
impl solver::AdventOfCodeSolver for AdventOfCodeDay13Solver {
  fn solve_first(&self) -> i64 {
    read()
      .iter()
      .map(|pattern| solve_pattern(pattern, (-1, -1)).unwrap())
      .sum::<i64>()
  }

  fn solve_second(&self) -> i64 {
    read()
      .iter()
      .map(|pattern| {
        (0..pattern.len())
          .map(|u| {
            (0..pattern[0].len())
              .map(|v| solve_pattern(&pattern, (u as i32, v as i32)))
              .find(|val| val.is_some())
          })
          .find(|val| val.is_some())
          .unwrap()
          .unwrap()
          .unwrap()
      })
      .sum::<i64>()
  }

  // fn solve_second(&self) -> i64 {
  //   read()
  //     .iter()
  //     .map(|pattern| {
  //       for u in 0..pattern.len() {
  //         for v in 0..pattern[0].len() {
  //           let result = solve_pattern(&pattern, (u as i32, v as i32));
  //           if result != -1 {
  //             return result;
  //           }
  //         }
  //       }

  //       panic!("could not find a solution for this pattern");
  //     })
  //     .sum::<i64>()
  // }
}

pub fn run() {
  let solver = AdventOfCodeDay13Solver {};
  solver.first_part(30802);
  solver.second_part(37876);
}
