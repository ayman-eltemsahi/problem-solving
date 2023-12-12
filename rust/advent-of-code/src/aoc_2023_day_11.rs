use std::{collections::VecDeque, fs::read_to_string};

use crate::solver::{self, AdventOfCodeSolver};

#[derive(Debug, Clone, Copy)]
struct Point {
  x: i32,
  y: i32,
}

const STEPS: &'static [(i32, i32)] = &[(1, 0), (-1, 0), (0, 1), (0, -1)];

fn get_distance(
  i: usize,
  j: usize,
  lines: &Vec<Vec<char>>,
  expanded_rows: &Vec<bool>,
  expanded_cols: &Vec<bool>,
  expansion: i64,
) -> Vec<Vec<i64>> {
  let n = lines.len();
  let m = lines[0].len();
  let mut distance: Vec<Vec<i64>> = (0..n)
    .map(|_| (0..m).map(|_| 1i64 << 50).collect::<Vec<i64>>())
    .collect();

  distance[i as usize][j as usize] = 0;
  let mut q: VecDeque<(usize, usize)> = VecDeque::new();
  q.push_back((i, j));

  while !q.is_empty() {
    let p = q.pop_front().unwrap();
    let d = distance[p.0][p.1];

    for step in STEPS {
      let a = p.0 as i32 + step.0;
      let b = p.1 as i32 + step.1;
      if a < 0 || b < 0 || a >= n as i32 || b >= n as i32 {
        continue;
      }

      let new_distance =
        if (step.0 != 0 && expanded_rows[p.0]) || (step.1 != 0 && expanded_cols[p.1]) {
          d + expansion
        } else {
          d + 1
        };

      if new_distance < distance[a as usize][b as usize] {
        distance[a as usize][b as usize] = new_distance;
        q.push_back((a as usize, b as usize));
      }
    }
  }

  return distance;
}

fn solve(expansion: i64) -> i64 {
  let file = read_to_string("./input.txt").unwrap();
  let lines = file
    .lines()
    .map(|line| line.chars().collect::<Vec<char>>())
    .collect::<Vec<Vec<char>>>();
  let n = lines.len();
  let m = lines[0].len();

  let expanded_rows: Vec<bool> = (0..n).map(|i| lines[i].iter().all(|j| *j == '.')).collect();
  let expanded_cols: Vec<bool> = (0..m).map(|j| (0..n).all(|i| lines[i][j] == '.')).collect();

  let galaxies = (0..n)
    .flat_map(|i| {
      (0..m)
        .filter(|j| lines[i][*j] == '#')
        .map(|j| Point {
          x: i as i32,
          y: j as i32,
        })
        .collect::<Vec<Point>>()
    })
    .collect::<Vec<Point>>();

  (0..n)
    .map(|i| {
      (0..m)
        .filter(|j| lines[i][*j] == '#')
        .map(|j| {
          let distance = get_distance(i, j, &lines, &expanded_rows, &expanded_cols, expansion);
          galaxies
            .iter()
            .map(|g| distance[g.x as usize][g.y as usize])
            .sum::<i64>()
        })
        .sum::<i64>()
    })
    .sum::<i64>()
    / 2
}

struct AdventOfCodeDay11Solver {}
impl solver::AdventOfCodeSolver for AdventOfCodeDay11Solver {
  fn solve_first(&self) -> i64 {
    solve(2)
  }

  fn solve_second(&self) -> i64 {
    solve(1000000)
  }
}

pub fn run() {
  let solver = AdventOfCodeDay11Solver {};
  solver.first_part(9556712);
  solver.second_part(678626199476);
}
