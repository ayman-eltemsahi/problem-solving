use std::time::Instant;

pub trait AdventOfCodeSolver {
  fn solve_first(&self) -> i64;
  fn solve_second(&self) -> i64;

  fn first_part(&self, expected: i64) {
    let time = Instant::now();

    let result = self.solve_first();

    let duration = time.elapsed();
    println!();
    println!("Result:  \x1b[32m{}\x1b[0m", result);
    println!("Correct: \x1b[32m{}\x1b[0m", expected);

    println!("Duration");
    println!("         {} μs", duration.as_micros() as f64);
    println!("         {} ms", duration.as_micros() as f64 / 1000.0);
    assert_eq!(result, expected);
  }

  fn second_part(&self, expected: i64) {
    let time = Instant::now();

    let result = self.solve_second();

    let duration = time.elapsed();
    println!();
    println!("Result:  \x1b[32m{}\x1b[0m", result);
    println!("Correct: \x1b[32m{}\x1b[0m", expected);

    println!("Duration");
    println!("         {} μs", duration.as_micros() as f64);
    println!("         {} ms", duration.as_micros() as f64 / 1000.0);
    assert_eq!(result, expected);
  }
}
