use std::{fs::read_to_string, time::Instant};

fn calc(time: i64, distance: i64) -> i64 {
  // hold_time * (time - hold_time) = distance
  // (hold_time * time - hold_time * hold_time) - distance = 0

  // X * time - X^2 = distance
  // X * time - X^2 - distance = 0
  // -X^2 + time * X - distance = 0
  let a: f64 = -1.0;
  let b: f64 = time as f64;
  let c: f64 = -distance as f64;

  // 0.00001 -> to avoid exact solutions
  let solution_1: f64 = ((-b + f64::sqrt(b * b - 4.0 * a * c)) / 2.0 * a) + 0.00001;
  let solution_2: f64 = ((-b - f64::sqrt(b * b - 4.0 * a * c)) / 2.0 * a) - 0.00001;

  return solution_2.ceil() as i64 - solution_1.ceil() as i64;
}

fn first_part() -> i64 {
  let lines: Vec<Vec<i64>> = read_to_string("./input.txt")
    .unwrap()
    .lines()
    .map(|line| {
      line
        .split_once(": ")
        .unwrap()
        .1
        .split(" ")
        .filter(|item| !item.is_empty())
        .map(|item| item.parse::<i64>().unwrap())
        .collect::<Vec<i64>>()
    })
    .collect();

  lines[0]
    .iter()
    .zip(&lines[1])
    .map(|item| calc(*item.0, *item.1))
    .fold(1i64, |a, b| a * b)
}

fn second_part() -> i64 {
  let items = read_to_string("./input.txt")
    .unwrap()
    .lines()
    .map(|line| {
      line
        .chars()
        .filter(|c| c.is_digit(10))
        .fold(0i64, |a, b| a * 10 + (b as u8 - '0' as u8) as i64)
    })
    .collect::<Vec<i64>>();

  return calc(items[0], items[1]);
}

pub fn run() {
  let time = Instant::now();

  let first = first_part();
  let second = second_part();

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 74698);
  assert_eq!(first, 74698);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 27563421);
  assert_eq!(second, 27563421);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
