use std::{fs::read_to_string, time::Instant};

fn get_next(nums: &Vec<i64>, forward: bool) -> i64 {
  if nums.iter().all(|f| *f == 0) {
    return 0;
  }

  let down = nums.windows(2).map(|w| w[1] - w[0]).collect::<Vec<i64>>();

  return if forward {
    nums.last().unwrap() + get_next(&down, forward)
  } else {
    nums.first().unwrap() - get_next(&down, forward)
  };
}

fn solve(forward: bool) -> i64 {
  read_to_string("./input.txt")
    .unwrap()
    .lines()
    .map(|line| {
      line
        .split(" ")
        .map(|item| item.parse::<i64>().unwrap())
        .collect::<Vec<i64>>()
    })
    .map(|v| get_next(&v, forward))
    .sum()
}

fn first_part() -> i64 {
  solve(true)
}

fn second_part() -> i64 {
  solve(false)
}

pub fn run() {
  let time = Instant::now();

  let first = first_part();
  let second = second_part();

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 1696140818);
  assert_eq!(first, 1696140818);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 1152);
  assert_eq!(second, 1152);

  println!("Duration: {} μs", duration.as_micros() as f64);
}
