use std::{collections::HashSet, fs::read_to_string, time::Instant};

fn read_numbers(line: &str, index: usize) -> HashSet<i32> {
  line
    .split("|")
    .nth(index)
    .unwrap()
    .split(" ")
    .filter(|item| !item.is_empty())
    .map(|item| item.parse::<i32>().unwrap())
    .collect()
}

fn get_wins(line: &str) -> i32 {
  let (_, cards) = line.split_once(": ").unwrap();

  let winning = read_numbers(cards, 0);
  let mine = read_numbers(cards, 1);

  mine
    .iter()
    .map(|m| if winning.contains(m) { 1 } else { 0 })
    .sum()
}

fn first_part() -> i64 {
  read_to_string("./input.txt")
    .unwrap()
    .lines()
    .map(|line| get_wins(line))
    .map(|wins| if wins == 0 { 0 } else { 1 << (wins - 1) })
    .sum()
}

fn second_part() -> i32 {
  let wins: Vec<i32> = read_to_string("./input.txt")
    .unwrap()
    .lines()
    .map(|line| get_wins(line))
    .collect();

  let n = wins.len();
  let mut cnt = (0..n).map(|_| 1).collect::<Vec<i32>>();

  for i in 0..n {
    for j in 0..wins[i] {
      cnt[i + 1 + j as usize] += cnt[i];
    }
  }

  cnt.iter().sum()
}

pub fn run() {
  let time = Instant::now();

  let first = first_part();
  let second = second_part();

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 20667);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 5833065);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
