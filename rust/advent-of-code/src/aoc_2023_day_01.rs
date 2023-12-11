use std::fs::read_to_string;

const NUMS: &'static [&'static str] = &[
  "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
];

fn get_a(line: &str) -> i32 {
  for (i, c) in line.chars().enumerate() {
    if c.is_digit(10) {
      return c as i32 - '0' as i32;
    }

    for j in 0..NUMS.len() {
      let num = NUMS[j];
      if (num.len() + i) <= line.len() && line[i..(num.len() + i)] == *num {
        return j as i32 + 1;
      }
    }
  }

  return 0;
}

fn get_b(line: &str) -> i32 {
  for (x, c) in line.chars().rev().enumerate() {
    if c.is_digit(10) {
      return c as i32 - '0' as i32;
    }

    let i = line.len() - x - 1;
    for j in 0..NUMS.len() {
      let num = NUMS[j];
      if (num.len() + i) <= line.len() && line[i..(num.len() + i)] == *num {
        return j as i32 + 1;
      }
    }
  }

  return 0;
}

pub fn run() {
  let result: i32 = read_to_string("./input.txt")
    .unwrap()
    .lines()
    .map(|line| 10 * get_a(line) + get_b(line))
    .sum();

  println!("Result : {}", result);
}
