use std::fs::read_to_string;

use crate::solver::{self, AdventOfCodeSolver};

static OPERATIONAL: char = '.';
static DAMAGED: char = '#';
static UNKNOWN: char = '?';

fn solve_for(condition: Vec<char>, nums: &Vec<i64>) -> i64 {
  let c_size = condition.len();
  let n_size = nums.len();
  let mut dp = (0..c_size + 2)
    .map(|_| (0..n_size + 2).map(|_| 0).collect::<Vec<i64>>())
    .collect::<Vec<Vec<i64>>>();

  for i in 0..2 {
    for j in 0..2 {
      dp[c_size + i][n_size + j] = 1;
    }
  }

  for c in (0..c_size).rev() {
    for n_i in (0..(n_size + 1)).rev() {
      let cur = condition[c];
      if cur == OPERATIONAL || cur == UNKNOWN {
        dp[c][n_i] = dp[c + 1][n_i];
      }

      if n_i >= n_size {
        continue;
      }
      let cur_num = nums[n_i] as usize;

      if cur == DAMAGED || cur == UNKNOWN {
        let mut valid = c + cur_num - 1 < c_size;
        let mut i = 0usize;
        while valid && i < cur_num {
          valid = condition[c + i] == DAMAGED || condition[c + i] == UNKNOWN;

          i += 1;
        }

        let next = if c + cur_num < c_size {
          condition[c + cur_num]
        } else {
          UNKNOWN
        };

        if valid && (next == OPERATIONAL || next == UNKNOWN) {
          dp[c][n_i] += dp[c + cur_num + 1][n_i + 1];
        }
      }
    }
  }

  return dp[0][0];
}

struct AdventOfCodeDay12Solver {}
impl solver::AdventOfCodeSolver for AdventOfCodeDay12Solver {
  fn solve_first(&self) -> i64 {
    read_to_string("./input.txt")
      .unwrap()
      .lines()
      .map(|line| {
        let (condition, nums) = line.split_once(" ").unwrap();
        let nums = nums
          .split(",")
          .map(|n| n.parse::<i64>().unwrap())
          .collect::<Vec<i64>>();

        solve_for(condition.chars().collect::<Vec<char>>(), &nums)
      })
      .sum::<i64>()
  }

  fn solve_second(&self) -> i64 {
    read_to_string("./input.txt")
      .unwrap()
      .lines()
      .map(|line| {
        let (condition, nums) = line.split_once(" ").unwrap();
        let nums: Vec<_> = nums.split(",").map(|n| n.parse::<i64>().unwrap()).collect();
        let nums: Vec<_> = nums
          .iter()
          .cycle()
          .take(nums.len() * 5)
          .map(|v| *v)
          .collect();

        let condition = (condition.to_owned()
          + "?"
          + condition
          + "?"
          + condition
          + "?"
          + condition
          + "?"
          + condition)
          .chars()
          .collect::<Vec<char>>();

        solve_for(condition, &nums)
      })
      .sum::<i64>()
  }
}

pub fn run() {
  let solver = AdventOfCodeDay12Solver {};
  solver.first_part(7753);
  solver.second_part(280382734828319);
}
