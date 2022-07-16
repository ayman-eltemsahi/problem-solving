#pragma once
#include <string>

int read_int(const std::string& value) {
  int res = 0;
  bool neg = false;

  for (const auto c : value) {
    assert(c == '-' || (c >= '0' && c <= '9'));
    if (c == '-')
      neg = true;
    else
      res = res * 10 + (c - '0');
  }

  return res * (neg ? -1 : 1);
}
