use std::{cmp, fs::read_to_string, time::Instant};

struct Mapping {
  pub target: i64,
  pub source: i64,
  pub range: i64,
}

fn get_seed_location(start: i64, end: i64, mappings: &Vec<Vec<Mapping>>) -> i64 {
  let initial = vec![(start, end)];

  mappings
    .iter()
    .fold(initial, |buckets, range| {
      get_seed_location_bucket(&buckets, range)
    })
    .iter()
    .map(|item| item.0)
    .min()
    .unwrap()
}

fn get_seed_location_bucket(buckets: &Vec<(i64, i64)>, range: &Vec<Mapping>) -> Vec<(i64, i64)> {
  let mut result: Vec<(i64, i64)> = vec![];

  buckets.iter().for_each(|(start, end)| {
    let mut used = range
      .iter()
      .filter(|m| *start <= m.source + m.range && *end >= m.source)
      .map(|m| {
        let left: i64 = m.source;
        let right: i64 = m.source + m.range;

        let overlap_start = cmp::max(*start, left);
        let overlap_end = cmp::min(*end, right);

        let mapped_overlap_start = m.target + overlap_start - left;
        let mapped_overlap_end = m.target + overlap_end - left;

        result.push((mapped_overlap_start, mapped_overlap_end));

        return (overlap_start, overlap_end);
      })
      .collect::<Vec<(i64, i64)>>();

    used.sort();

    let mut cur = *start;
    for (left, right) in used {
      if cur > *end {
        break;
      }

      if cur < left {
        result.push((cur, left - 1));
      }
      cur = right + 1;
    }

    if cur <= *end {
      result.push((cur, *end));
    }
  });

  result
}

fn read() -> (Vec<i64>, Vec<Vec<Mapping>>) {
  let content = read_to_string("./input.txt").unwrap();
  let lines: Vec<&str> = content.lines().collect();

  let seeds = lines[0]
    .split(": ")
    .nth(1)
    .unwrap()
    .split(" ")
    .map(|item| item.parse().unwrap())
    .collect();

  let mut i = 2;
  let mappings = (0..7)
    .map(|_| {
      i += 1;
      let mut res = vec![];

      while i < lines.len() && lines[i].len() > 0 {
        let s: Vec<&str> = lines[i].split(" ").collect();

        res.push(Mapping {
          target: s[0].parse::<i64>().unwrap(),
          source: s[1].parse::<i64>().unwrap(),
          range: s[2].parse::<i64>().unwrap(),
        });
        i += 1;
      }
      i += 1;
      return res;
    })
    .collect();

  return (seeds, mappings);
}

fn first_part() -> i64 {
  let (seeds, mappings) = read();

  seeds
    .iter()
    .map(|seed| get_seed_location(*seed, *seed, &mappings))
    .min()
    .unwrap()
}

fn second_part() -> i64 {
  let (seeds, mappings) = read();

  seeds
    .chunks_exact(2)
    .map(|seed| get_seed_location(seed[0], seed[0] + seed[1], &mappings))
    .min()
    .unwrap()
}

pub fn run() {
  let time = Instant::now();

  let first = first_part();
  let second = second_part();

  let duration = time.elapsed();
  println!("Result : {}", first);
  println!("Correct: {}", 309796150);
  assert_eq!(first, 309796150);
  println!();
  println!("Result : {}", second);
  println!("Correct: {}", 50716416);
  assert_eq!(second, 50716416);

  println!("Duration: {} ms", duration.as_micros() as f64 / 1000.0);
}
