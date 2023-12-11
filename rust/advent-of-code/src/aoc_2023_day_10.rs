use std::{fs::read_to_string, time::Instant};

#[derive(Debug, Clone, Copy)]
struct Point {
  x: i32,
  y: i32,
}

impl Point {
  pub fn add(&self, other: &Point) -> Point {
    Point {
      x: self.x + other.x,
      y: self.y + other.y,
    }
  }

  pub fn is_outside(&self, n: usize, m: usize) -> bool {
    self.x < 0 || self.y < 0 || self.x >= n as i32 || self.y >= m as i32
  }

  pub fn equals(&self, other: &Point) -> bool {
    other.x == self.x && other.y == self.y
  }
}

static NORTH: Point = Point { x: -1, y: 0 };
static SOUTH: Point = Point { x: 1, y: 0 };
static EAST: Point = Point { x: 0, y: 1 };
static WEST: Point = Point { x: 0, y: -1 };

fn get_pipe(c: char) -> (Point, Point) {
  match c {
    '|' => (NORTH, SOUTH),
    '-' => (EAST, WEST),
    'L' => (NORTH, EAST),
    'J' => (NORTH, WEST),
    '7' => (SOUTH, WEST),
    'F' => (SOUTH, EAST),
    _ => panic!("unknown pipe"),
  }
}

static DIR: &'static str = "|-LJ7F";

fn pnpoly(poly: &Vec<Point>, x: f64, y: f64) -> bool {
  let mut inside = false;

  for i in 0..poly.len() {
    let j = (i + 1) % poly.len();

    let xp0 = poly[i].x as f64;
    let yp0 = poly[i].y as f64;
    let xp1 = poly[j].x as f64;
    let yp1 = poly[j].y as f64;

    if (yp0 <= y) && (yp1 > y) || (yp1 <= y) && (yp0 > y) {
      let cross = (xp1 - xp0) * (y - yp0) / (yp1 - yp0) + xp0;

      if cross < x {
        inside = !inside;
      }
    }
  }

  return inside;
}

fn get_s(lines: &Vec<Vec<char>>) -> Point {
  for i in 0..lines.len() {
    for j in 0..lines[0].len() {
      if lines[i][j] == 'S' {
        return Point {
          x: i as i32,
          y: j as i32,
        };
      }
    }
  }

  panic!("unreachable");
}

fn is_compatible(lines: &Vec<Vec<char>>, l: &Point, r: &Point) -> bool {
  let a = get_pipe(lines[r.x as usize][r.y as usize]);
  let b = get_pipe(lines[l.x as usize][l.y as usize]);

  return ((a.0.x + r.x == l.x && a.0.y + r.y == l.y)
    || (a.1.x + r.x == l.x && a.1.y + r.y == l.y))
    && ((b.0.x + l.x == r.x && b.0.y + l.y == r.y) || (b.1.x + l.x == r.x && b.1.y + l.y == r.y));
}

fn get_loop(lines: &Vec<Vec<char>>, start: &Point) -> Option<Vec<Point>> {
  let n = lines.len();
  let m = lines[0].len();

  let mut found_loop: Vec<Point> = vec![];
  let mut prev = Point { x: -1, y: 1 };
  let mut cur = start.clone();

  found_loop.push(cur);

  loop {
    let first = cur.add(&get_pipe(lines[cur.x as usize][cur.y as usize]).0);
    if first.is_outside(n, m)
      || lines[first.x as usize][first.y as usize] == '.'
      || !is_compatible(lines, &first, &cur)
    {
      return None;
    }

    let second = cur.add(&get_pipe(lines[cur.x as usize][cur.y as usize]).1);
    if second.is_outside(n, m)
      || lines[second.x as usize][second.y as usize] == '.'
      || !is_compatible(lines, &second, &cur)
    {
      return None;
    }

    if prev.x == -1 || prev.equals(&first) {
      prev = cur;
      cur = second;
    } else if prev.equals(&second) {
      prev = cur;
      cur = first;
    } else {
      return None;
    }

    if cur.equals(start) {
      break;
    }
    found_loop.push(cur);
  }

  return Some(found_loop);
}

fn first_part() -> i64 {
  let file = read_to_string("./input.txt").unwrap();
  let mut lines = file
    .lines()
    .map(|line| line.chars().collect::<Vec<char>>())
    .collect::<Vec<Vec<char>>>();
  let s = get_s(&lines);

  for c in DIR.chars() {
    lines[s.x as usize][s.y as usize] = c;

    match get_loop(&lines, &s) {
      Some(found_loop) => return found_loop.len() as i64 / 2,
      _ => {}
    }

    lines[s.x as usize][s.y as usize] = 'S';
  }

  panic!("unreachable");
}

fn second_part() -> i64 {
  let file = read_to_string("./input.txt").unwrap();
  let mut lines = file
    .lines()
    .map(|line| line.chars().collect::<Vec<char>>())
    .collect::<Vec<Vec<char>>>();
  let s = get_s(&lines);
  let n = lines.len();
  let m = lines[0].len();

  for c in DIR.chars() {
    lines[s.x as usize][s.y as usize] = c;

    match get_loop(&lines, &s) {
      Some(found_loop) => {
        let mut seen: Vec<bool> = (0..(n * m)).map(|_| false).collect();

        found_loop.iter().for_each(|item| {
          seen[(item.x * n as i32 + item.y) as usize] = true;
        });

        return (0..n)
          .map(|i| {
            (0..m)
              .filter(|j| !seen[i * n + j] && pnpoly(&found_loop, i as f64, *j as f64))
              .count()
          })
          .sum::<usize>() as i64;
      }
      _ => {}
    }

    lines[s.x as usize][s.y as usize] = 'S';
  }

  panic!("unreachable");
}

pub fn run() {
  let time = Instant::now();

  let first = first_part();
  let second = second_part();

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 6725);
  assert_eq!(first, 6725);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 383);
  assert_eq!(second, 383);

  println!("Duration: {} μs", duration.as_micros() as f64);
  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
