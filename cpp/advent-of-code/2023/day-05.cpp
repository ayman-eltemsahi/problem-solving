#include "local-stuff.hpp"
#include "aoc-common.hpp"

struct mapping {
  ll target, source, range;
};

class AdventOfCodeSolverDay05 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto [seeds, mappings] = read();

    ll result = 1LL << 50;
    for (auto seed : seeds) {
      ll l = get_seed_location(seed, seed, mappings);
      result = min(result, l);
    }

    return result;
  }

  ll second_part() {
    auto [seeds, mappings] = read();

    ll result = 1LL << 50;
    for (int i = 0; i < seeds.size(); i += 2) {
      ll curr = get_seed_location(seeds[i], seeds[i] + seeds[i + 1], mappings);
      result = min(result, curr);
    }

    return result;
  }

 private:
  ll get_seed_location(ll start, ll end, vector<vector<mapping>> &mappings) {
    vector<pair<ll, ll>> buckets;
    buckets.push_back({start, end});

    for (auto &range : mappings) {
      buckets = get_seed_location_bucket(buckets, range);
    }

    ll seed = 1L << 50;
    for (auto &b : buckets) {
      seed = min(seed, b.first);
    }

    return seed;
  }

  vector<pair<ll, ll>> get_seed_location_bucket(vector<pair<ll, ll>> &buckets,
                                                vector<mapping> &range) {
    vector<pair<ll, ll>> result;

    for (auto &buck : buckets) {
      ll start = buck.first, end = buck.second;
      vector<pair<ll, ll>> used;

      for (auto &m : range) {
        ll left = m.source, right = m.source + m.range;
        if (start > right || end < left) continue;

        ll overlap_start = max(start, left);
        ll overlap_end = min(end, right);
        used.push_back({overlap_start, overlap_end});

        ll mapped_overlap_start = m.target + overlap_start - left;
        ll mapped_overlap_end = m.target + overlap_end - left;

        result.push_back({mapped_overlap_start, mapped_overlap_end});
      }

      sort(used.begin(), used.end());

      ll cur = buck.first;
      for (int i = 0; i < used.size() && cur <= buck.second; i++) {
        ll left = used[i].first, right = used[i].second;
        if (cur < left) {
          result.push_back({cur, left - 1});
        }
        cur = right + 1;
      }

      if (cur <= buck.second) {
        result.push_back({cur, buck.second});
      }
    }

    return result;
  }

  pair<vector<ll>, vector<vector<mapping>>> read() {
    std::ifstream infile("../input.txt");
    string line;
    vector<string> lines;

    while (std::getline(infile, line)) {
      lines.push_back(line);
    }

    auto seeds = read_seeds(lines[0]);
    int i = 2;
    vector<vector<mapping>> mappings;
    for (int _ = 0; _ < 7; _++) {
      vector<mapping> cur = read_mapping(lines, i);
      mappings.push_back(cur);
    }

    return {seeds, mappings};
  }

  vector<ll> read_seeds(string &line) {
    auto s = utils::split_string(utils::split_string(line, ": ")[1], " ");
    vector<ll> res;
    for (auto r : s) {
      res.push_back(stoll(r));
    }
    return res;
  }

  vector<mapping> read_mapping(vector<string> &lines, int &i) {
    i++;
    vector<mapping> res;
    while (i < lines.size() && lines[i].size() > 0) {
      auto s = utils::split_string(lines[i], " ");
      res.push_back(mapping{stoll(s[0]), stoll(s[1]), stoll(s[2])});
      i++;
    }
    i++;

    return res;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay05{};
  solver.solve_first(309796150);
  solver.solve_second(50716416);
}
