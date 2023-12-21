#include "local-stuff.hpp"
#include "aoc-common.hpp"

const int R = 0;
const int L = 1;
const int U = 2;
const int D = 3;

const auto split = utils::split_string;

typedef vector<pair<ll, ll>> v_ranges;

struct part {
  ll x, m, a, s;
  ll sum() const { return x + m + a + s; }
  void set(char c, ll val) {
    switch (c) {
      case 'x':
        x = val;
        return;
      case 'm':
        m = val;
        return;
      case 'a':
        a = val;
        return;
      case 's':
        s = val;
        return;
    }
  }
};

struct ranges {
  string workflow;
  vector<pair<ll, ll>> x, m, a, s;
};

struct rule {
  char c, op;
  ll value;
  string destination;
  bool is_direct;

  bool accepts(const part& p) const {
    if (is_direct) return true;
    auto v = c == 'x' ? p.x : c == 'm' ? p.m : c == 'a' ? p.a : p.s;

    if (op == '>' && v > value) return true;
    if (op == '<' && v < value) return true;
    return false;
  }
};

class AdventOfCodeSolverDay19 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto input = read();
    ll result = 0;

    for (auto& p : input.second) {
      if (is_accepted(p, input.first)) {
        result += p.sum();
      }
    }

    return result;
  }

  ll second_part() {
    auto input = read();
    ll result = 0;

    ll MIN = 1, MAX = 4000;
    queue<ranges> q;
    q.push({
        "in",
        {{MIN, MAX}},
        {{MIN, MAX}},
        {{MIN, MAX}},
        {{MIN, MAX}},
    });

    while (!q.empty()) {
      auto cur = q.front();
      q.pop();

      v_ranges& x = cur.x;
      v_ranges& m = cur.m;
      v_ranges& a = cur.a;
      v_ranges& s = cur.s;

      if (cur.workflow == "R") continue;
      if (cur.workflow == "A") {
        result += sum_pairs(x) * sum_pairs(m) * sum_pairs(a) * sum_pairs(s);
        continue;
      }

      for (auto& r : input.first[cur.workflow]) {
        if (r.is_direct) {
          q.push({r.destination, x, m, a, s});
          break;
        }

        pair<ll, ll> range =
            (r.op == '>') ? std::make_pair(r.value + 1, MAX) : std::make_pair(MIN, r.value - 1);

        v_ranges xx = r.c == 'x' ? cut_range(x, range) : x;
        v_ranges mm = r.c == 'm' ? cut_range(m, range) : m;
        v_ranges aa = r.c == 'a' ? cut_range(a, range) : a;
        v_ranges ss = r.c == 's' ? cut_range(s, range) : s;

        q.push({r.destination, xx, mm, aa, ss});

        x = r.c == 'x' ? remove_range(x, range) : x;
        m = r.c == 'm' ? remove_range(m, range) : m;
        a = r.c == 'a' ? remove_range(a, range) : a;
        s = r.c == 's' ? remove_range(s, range) : s;
      }
    }

    return result;
  }

  v_ranges remove_range(const v_ranges& in, const pair<ll, ll>& range) {
    v_ranges out;
    auto& [start, end] = range;
    for (auto& [l, r] : in) {
      if (r < start || l > end) {
        out.push_back({l, r});
      } else {
        if (l <= start - 1) out.push_back({l, start - 1});
        if (end + 1 <= r) out.push_back({end + 1, r});
      }
    }

    return out;
  }

  v_ranges cut_range(const v_ranges& in, const pair<ll, ll>& range) {
    v_ranges out;
    auto& [start, end] = range;
    for (auto& [l, r] : in) {
      if (r < start || l > end) continue;

      out.push_back({max(l, start), min(r, end)});
    }

    return out;
  }

  ll sum_pairs(const v_ranges& item) {
    ll result = 0;
    for (auto& [l, r] : item) result += r - l + 1;

    return result;
  }

 private:
  bool is_accepted(const part& p, const unordered_map<string, vector<rule>>& workflows) {
    string cur = "in";
    while (true) {
      for (const auto& r : workflows.at(cur)) {
        if (!r.accepts(p)) continue;
        if (r.destination == "A") return true;
        if (r.destination == "R") return false;
        cur = r.destination;
        break;
      }
    }
  }

  pair<string, vector<rule>> parse_workflow(const string& line) {
    auto tmp = split(line, "{");
    string name = tmp[0];
    vector<rule> rules;

    auto parts = split(tmp[1].substr(0, tmp[1].size() - 1), ",");
    for (auto& part : parts) {
      if (part[1] == '>' || part[1] == '<') {
        auto last = split(part.substr(2), ":");
        rules.push_back({part[0], part[1], stoll(last[0]), last[1], false});
      } else {
        rules.push_back({' ', ' ', -1, part, true});
      }
    }

    return {name, rules};
  }

  part parse_part(const string& line) {
    part p;
    auto tmp = split(line.substr(1, line.size() - 2), ",");
    for (auto& a : tmp) p.set(a[0], stoll(a.substr(2)));
    return p;
  }

  pair<unordered_map<string, vector<rule>>, vector<part>> read() {
    std::ifstream infile("../input.txt");
    string line;
    unordered_map<string, vector<rule>> workflows;
    vector<part> parts;

    while (std::getline(infile, line)) {
      if (line.empty()) break;
      auto parsed = parse_workflow(line);
      workflows[parsed.first] = parsed.second;
    }

    while (std::getline(infile, line)) {
      parts.push_back(parse_part(line));
    }

    return {workflows, parts};
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay19{};
  solver.solve_first(280909);
  solver.solve_second(116138474394508);
}
