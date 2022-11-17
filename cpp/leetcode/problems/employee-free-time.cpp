#ifdef RUNNING_LOCALLY
#include "local-stuff.hpp"
#endif

using common::Interval;

class Solution {
 public:
  vector<Interval> employeeFreeTime(vector<vector<Interval>> schedule) {
    auto cmp = [&](auto&& a, auto&& b) {
      return schedule[a.first][a.second].start == schedule[b.first][b.second].start
                 ? schedule[a.first][a.second].end > schedule[b.first][b.second].end
                 : schedule[a.first][a.second].start > schedule[b.first][b.second].start;
    };
    priority_queue<pair<int, int>, vector<pair<int, int>>, decltype(cmp)> q(cmp);
    for (int i = 0; i < schedule.size(); i++) q.push({i, 0});

    vector<Interval> res;

    int left = schedule[q.top().first][0].end;
    while (!q.empty()) {
      int i = q.top().first;
      int j = q.top().second;
      q.pop();

      if (left < schedule[i][j].start) {
        res.push_back(Interval(left, schedule[i][j].start));
      }

      left = max(left, schedule[i][j].end);
      if (j + 1 < schedule[i].size()) {
        q.push({i, j + 1});
      }
    }

    return res;
  }
};
