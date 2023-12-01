use std::time::Instant;

use aoc_2023_day_01::run;

mod aoc_2023_day_01;

fn main() {
  let start = Instant::now();

  let result = run();
  let duration = start.elapsed();

  println!("Result : {}", result);
  println!("Correct: {}", 54719);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
