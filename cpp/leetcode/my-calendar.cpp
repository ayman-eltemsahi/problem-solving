#include <algorithm>
#include <cassert>
#include <cctype>
#include <iostream>
#include <map>
#include <vector>

typedef long long int ll;
typedef std::vector<int> vi;
using std::pair;
using std::string;
using std::vector;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define FORN1(i, n) for (int i = 1; i < (n); i++)
#define LOG(a) std::cout << (a) << "\n"
#define LOG2(a, b) std::cout << (a) << ", " << (b) << "\n"
#define LOG3(a, b, c) std::cout << (a) << ", " << (b) << ", " << (c) << "\n"

class MyCalendar {
 public:
  std::map<int, int> bookings;

  MyCalendar() {
  }

  bool book(int start, int end) {
    auto nxt = bookings.upper_bound(start);
    if (nxt != bookings.end() && nxt->second < end) return false;
    bookings.insert({end, start});
    return true;
  }
};

class MyCalendarTwo {
 public:
  std::map<int, int> bookings;

  MyCalendarTwo() {
  }

  bool book(int start, int end) {
    bookings[start]++;
    bookings[end]--;

    int count = 0;
    for (auto it = bookings.begin(); it != bookings.end(); it++) {
      count += it->second;
      if (count >= 3) {
        if (bookings[start] == 1)
          bookings.erase(start);
        else
          bookings[start]--;
        bookings[end]++;
        return false;
      }
    }

    if (bookings[end] == 0) bookings.erase(end);
    return true;
  }
};

class MyCalendarThree {
 public:
  std::map<int, int> bookings;

  MyCalendarThree() {
  }

  int book(int start, int end) {
    bookings[start]++;
    if (bookings[end] == 1)
      bookings.erase(end);
    else
      bookings[end]--;

    int count = 0, mx = 0;
    for (auto it = bookings.begin(); it != bookings.end(); it++) {
      count += it->second;
      if (count > mx) mx = count;
    }

    return mx;
  }
};

#if defined(RUNNING_LOCALLY)
int main() {
  MyCalendar calendar;
  auto res = calendar.book(10, 20);
}
#endif
