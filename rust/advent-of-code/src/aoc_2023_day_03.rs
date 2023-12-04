use std::{
  collections::{HashMap, HashSet},
  fs::read_to_string,
  time::Instant,
};

const STEPS: &'static [(i32, i32)] = &[
  (1, 1),
  (1, -1),
  (-1, 1),
  (-1, -1),
  (1, 0),
  (-1, 0),
  (0, 1),
  (0, -1),
];

fn read(lines: &Vec<&str>) -> (Vec<Vec<Vec<usize>>>, HashMap<usize, i32>) {
  let n = lines.len();
  let m = lines[0].len();

  let mut mapping = HashMap::new();
  let mut nums_at_gear: Vec<Vec<Vec<usize>>> = (0..n)
    .map(|_| (0..m).map(|_| Vec::new()).collect())
    .collect();

  for i in 0..n {
    let chars: Vec<char> = lines[i].chars().collect();
    let mut j = 0;
    while j < m {
      let key = i * n + j;
      if !chars[j].is_digit(10) {
        j += 1;
        continue;
      }

      let mut num: i32 = 0;
      let mut y = j;
      while y < m && chars[y].is_digit(10) {
        num = num * 10 + (chars[y] as u8 - '0' as u8) as i32;
        y += 1;
      }

      mapping.insert(key, num);

      while j < y {
        for (dx, dy) in STEPS {
          let (a, b) = (i as i32 + *dx, j as i32 + *dy);

          if a < 0 || b < 0 || a >= n as i32 || b >= m as i32 {
            continue;
          }

          let ch: Vec<char> = lines[a as usize].chars().collect();
          if ch[b as usize] == '.' || ch[b as usize].is_digit(10) {
            continue;
          }

          if !nums_at_gear[a as usize][b as usize].contains(&key) {
            nums_at_gear[a as usize][b as usize].push(key);
          }
        }

        j += 1;
      }

      j += 1;
    }
  }

  return (nums_at_gear, mapping);
}

fn first_part(lines: Vec<&str>) -> i32 {
  let (nums_at_gear, mapping) = read(&lines);
  let mut selected_keys = HashSet::new();

  for a in &nums_at_gear {
    for b in a {
      for r in b {
        selected_keys.insert(r);
      }
    }
  }

  selected_keys.iter().map(|key| mapping[*key]).sum()
}

fn second_part(lines: Vec<&str>) -> i32 {
  let n = lines.len();
  let m = lines[0].len();

  let (nums_at_gear, mapping) = read(&lines);

  let mut res: i32 = 0;
  for i in 0..n {
    let chars: Vec<char> = lines[i].chars().collect();
    for j in 0..m {
      if chars[j] == '*' && nums_at_gear[i][j].len() == 2 {
        res += mapping[&nums_at_gear[i][j][0]] * mapping[&nums_at_gear[i][j][1]];
      }
    }
  }

  return res;
}

pub fn solve(is_first_part: bool) -> i32 {
  let file = read_to_string("./input.txt").unwrap();

  return if is_first_part {
    first_part(file.lines().collect())
  } else {
    second_part(file.lines().collect())
  };
}

pub fn run() {
  let time = Instant::now();

  let first = solve(true);
  let second = solve(false);

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 525119);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 76504829);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
