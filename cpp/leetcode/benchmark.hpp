#pragma once
#include "local-stuff.hpp"

int WARM_UP = 100;
int RUNS = 500;

void benchmark(int (*func)()) {
  printf("Warming up....\n");
  for (int i = 0; i < WARM_UP; i++) {
    func();
  }
  printf("Warm up done.\n");

  double times = 0;

  printf("Starting....\n");
  for (int i = 0; i < RUNS; i++) {
    auto start = std::chrono::high_resolution_clock::now();

    func();

    auto finish = std::chrono::high_resolution_clock::now();
    times += std::chrono::duration_cast<std::chrono::microseconds>(finish - start).count();
  }
  printf("Done.\n");

  double avg = times / RUNS;
  printf("Avg       : \x1b[32m%lf\x1b[0m ms\n", avg / 1000.0);
}
