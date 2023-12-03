use std::{cmp, fs::read_to_string, time::Instant};

const RED: i32 = 12;
const GREEN: i32 = 13;
const BLUE: i32 = 14;

fn solve_line(line: &str, first_part: bool) -> i32 {
  let (a, b) = line.split_once(": ").unwrap();
  let id = a[5..].parse::<i32>().unwrap();

  let mut red_max = 0;
  let mut blue_max = 0;
  let mut green_max = 0;

  for cube in b.split(";") {
    let pieces = cube.split(", ");
    let mut red = 0;
    let mut green = 0;
    let mut blue = 0;

    pieces.for_each(|piece| {
      let (cnt, color) = piece.trim().split_once(" ").unwrap();
      let cnt = cnt.parse::<i32>().unwrap();

      match color {
        "blue" => blue += cnt,
        "red" => red += cnt,
        "green" => green += cnt,
        _ => panic!("unknown color"),
      }
    });

    if first_part && (red > RED || blue > BLUE || green > GREEN) {
      return 0;
    }

    red_max = cmp::max(red, red_max);
    blue_max = cmp::max(blue, blue_max);
    green_max = cmp::max(green, green_max);
  }

  return if first_part {
    id
  } else {
    red_max * blue_max * green_max
  };
}

pub fn solve(first_part: bool) -> i32 {
  read_to_string("./input.txt")
    .unwrap()
    .lines()
    .map(|line| solve_line(line, first_part))
    .sum()
}

pub fn run() {
  let time = Instant::now();

  let first = solve(true);
  let second = solve(false);

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 2348);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 76008);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
