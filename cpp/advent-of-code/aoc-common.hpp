#pragma once
#include "local-stuff.hpp"

class AdventOfCodeSolver {
 public:
  virtual ll first_part() = 0;
  virtual ll second_part() = 0;

  void solve_first(ll correct) {
    auto start = std::chrono::high_resolution_clock::now();

    ll answer = first_part();

    auto finish = std::chrono::high_resolution_clock::now();
    auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
    printf("\n");
    printf("Reuslt     : \x1b[32m%lld\x1b[0m\n", answer);
    printf("Correct    : \x1b[32m%lld\x1b[0m\n", correct);
    printf("Time       : \x1b[32m%lld\x1b[0m μs\n", diff);
    printf("Time       : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
    assert(answer == correct);
  }

  void solve_first() {
    auto start = std::chrono::high_resolution_clock::now();

    ll answer = first_part();

    auto finish = std::chrono::high_resolution_clock::now();
    auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
    printf("\n");
    printf("Reuslt     : \x1b[32m%lld\x1b[0m\n", answer);
    printf("Time       : \x1b[32m%lld\x1b[0m μs\n", diff);
    printf("Time       : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
  }

  void solve_second(ll correct) {
    auto start = std::chrono::high_resolution_clock::now();

    ll answer = second_part();

    auto finish = std::chrono::high_resolution_clock::now();
    auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
    printf("\n");
    printf("Reuslt     : \x1b[32m%lld\x1b[0m\n", answer);
    printf("Correct    : \x1b[32m%lld\x1b[0m\n", correct);
    printf("Time       : \x1b[32m%lld\x1b[0m μs\n", diff);
    printf("Time       : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
    assert(answer == correct);
  }

  void solve_second() {
    auto start = std::chrono::high_resolution_clock::now();

    ll answer = second_part();

    auto finish = std::chrono::high_resolution_clock::now();
    auto diff = std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
    printf("\n");
    printf("Reuslt     : \x1b[32m%lld\x1b[0m\n", answer);
    printf("Time       : \x1b[32m%lld\x1b[0m μs\n", diff);
    printf("Time       : \x1b[32m%lf\x1b[0m ms\n", diff / 1000.0);
  }
};
