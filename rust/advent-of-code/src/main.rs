mod solver;
mod aoc_2023_day_01;
mod aoc_2023_day_02;
mod aoc_2023_day_03;
mod aoc_2023_day_04;
mod aoc_2023_day_05;
mod aoc_2023_day_06;
mod aoc_2023_day_07;
mod aoc_2023_day_08;
mod aoc_2023_day_09;
mod aoc_2023_day_10;
mod aoc_2023_day_11;
mod aoc_2023_day_12;
mod aoc_2023_day_13;

fn main() {
  let args = std::env::args();

  match args.last().unwrap().parse::<usize>().unwrap() {
    01 => aoc_2023_day_01::run(),
    02 => aoc_2023_day_02::run(),
    03 => aoc_2023_day_03::run(),
    04 => aoc_2023_day_04::run(),
    05 => aoc_2023_day_05::run(),
    06 => aoc_2023_day_06::run(),
    07 => aoc_2023_day_07::run(),
    08 => aoc_2023_day_08::run(),
    09 => aoc_2023_day_09::run(),
    10 => aoc_2023_day_10::run(),
    11 => aoc_2023_day_11::run(),
    12 => aoc_2023_day_12::run(),
    13 => aoc_2023_day_13::run(),

    _ => panic!("Unknown input"),
  };
}
