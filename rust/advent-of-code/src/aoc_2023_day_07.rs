use std::{fs::read_to_string, time::Instant};

fn set(mem: &mut Vec<i32>, c: char) {
  mem[c as usize - '2' as usize] += 1;
}

fn clear(mem: &mut Vec<i32>, c: char) {
  mem[c as usize - '2' as usize] = 0;
}

fn rank(card: &str, joker: bool) -> i32 {
  let mut mem: Vec<i32> = vec![0; 35];

  card.chars().for_each(|c| set(&mut mem, c));

  if joker {
    clear(&mut mem, 'J');
  };

  let mut v: Vec<i32> = Vec::with_capacity(5);
  card.chars().for_each(|c| {
    if mem[c as usize - '2' as usize] > 0 {
      v.push(mem[c as usize - '2' as usize]);
      clear(&mut mem, c);
    }
  });

  v.sort();

  if v.len() == 0 || v.len() == 1 {
    return 7;
  }
  if v.len() == 2 && v[0] == 1 {
    return 6;
  }
  if v.len() == 2 && v[0] == 2 {
    return 5;
  }
  if v.len() == 3 && v[0] == 1 && v[1] == 1 {
    return 4;
  }
  if v.len() == 3 && v[0] == 1 && v[1] == 2 {
    return 3;
  }
  if v.len() == 4 && v[0] == 1 && v[1] == 1 && v[2] == 1 {
    return 2;
  }
  if v.len() == 5 {
    return 1;
  }

  panic!("unreachable");
}

fn solve(is_second_part: bool, values: &Vec<i32>) -> i64 {
  let lines = read_to_string("./input.txt").unwrap();
  let mut cards = lines
    .lines()
    .map(|line| (&line[0..5], line[6..].parse::<i64>().unwrap()))
    .collect::<Vec<(&str, i64)>>();

  cards.sort_by(|(left, _), (right, _)| {
    let r_a = rank(left, is_second_part);
    let r_b = rank(right, is_second_part);
    if r_a != r_b {
      return r_a.partial_cmp(&r_b).unwrap();
    }

    match left.chars().zip(right.chars()).find(|(a, b)| a != b) {
      Some((a, b)) => values[a as usize].partial_cmp(&values[b as usize]).unwrap(),
      _ => std::cmp::Ordering::Equal,
    }
  });

  cards
    .iter()
    .enumerate()
    .fold(0, |result, (i, value)| result + (value.1 * (i + 1) as i64))
}

fn first_part() -> i64 {
  let mut values: Vec<i32> = vec![0; 100];
  "AKQJT98765432"
    .chars()
    .enumerate()
    .for_each(|(i, f)| values[f as usize] = 100 - i as i32);

  solve(false, &values)
}

fn second_part() -> i64 {
  let mut values: Vec<i32> = vec![0; 100];
  "AKQT98765432J"
    .chars()
    .enumerate()
    .for_each(|(i, f)| values[f as usize] = 100 - i as i32);

  solve(true, &values)
}

pub fn run() {
  let time = Instant::now();

  let first = first_part();
  let second = second_part();

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 251287184);
  assert_eq!(first, 251287184);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 250757288);
  assert_eq!(second, 250757288);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
