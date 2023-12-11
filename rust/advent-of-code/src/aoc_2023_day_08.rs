use std::collections::HashMap;
use std::{fs::read_to_string, time::Instant};

fn lcm(first: i64, second: i64) -> i64 {
  first * second / gcd(first, second)
}

fn gcd(first: i64, second: i64) -> i64 {
  let mut max = first;
  let mut min = second;
  if min > max {
    let val = max;
    max = min;
    min = val;
  }

  loop {
    let res = max % min;
    if res == 0 {
      return min;
    }

    max = min;
    min = res;
  }
}

fn read() -> (String, HashMap<String, (String, String)>) {
  let lines = read_to_string("./input.txt").unwrap();

  let dir = lines.lines().nth(0).unwrap().to_owned();
  let map = lines
    .lines()
    .skip(2)
    .map(|line| {
      let a = &line[0..3];
      let l = &line[7..10];
      let r = &line[12..15];

      (a.to_owned(), (l.to_owned(), r.to_owned()))
    })
    .collect();

  return (dir, map);
}

fn first_part() -> i64 {
  let (dir, map) = read();

  let mut cur = "AAA";
  let mut result: i64 = 0;
  while cur != "ZZZ" {
    for c in dir.chars() {
      cur = if c == 'R' { &map[cur].1 } else { &map[cur].0 };
      result += 1;
      if cur == "ZZZ" {
        break;
      }
    }
  }

  return result;
}

fn get_cycle(node_c: &str, dir: &str, map: &HashMap<String, (String, String)>) -> i64 {
  let mut node = node_c;

  let mut cost = 0i64;
  loop {
    for c in dir.chars() {
      node = if c == 'R' { &map[node].1 } else { &map[node].0 };

      cost += 1;
      if node.chars().nth(2).unwrap() == 'Z' {
        return cost;
      }
    }
  }
}

fn second_part() -> i64 {
  let (dir, map) = read();

  map
    .keys()
    .filter(|key| key.chars().nth(2).unwrap() == 'A')
    .map(|key| get_cycle(&key, &dir, &map))
    .fold(1i64, |a, b| lcm(a, b))
}

pub fn run() {
  let time = Instant::now();

  let first = first_part();
  let second = second_part();

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 13301);
  assert_eq!(first, 13301);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 7309459565207);
  assert_eq!(second, 7309459565207);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
