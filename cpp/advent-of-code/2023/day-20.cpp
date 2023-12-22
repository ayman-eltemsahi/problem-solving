#include "local-stuff.hpp"
#include "aoc-common.hpp"

const int BROADCASTER = 0;
const int FLIP_FLOP = 1;
const int CONJ = 2;

const int PULSE_NONE = 0;
const int PULSE_LOW = 1;
const int PULSE_HIGH = 2;

const ll INF = 1L << 55;

struct module {
  int type;
  bool is_on;
  vector<string> destination;
  unordered_set<string> high, low;

  void register_first_low_for_conj(const string& from) {
    if (this->type == CONJ) {
      low.insert(from);
    }
  }

  int process(int pulse, const string& from) {
    if (this->type == FLIP_FLOP) {
      if (pulse == PULSE_HIGH) return PULSE_NONE;
      this->is_on = !this->is_on;
      return this->is_on ? PULSE_HIGH : PULSE_LOW;
    }

    if (this->type == CONJ) {
      if (pulse == PULSE_HIGH) {
        high.insert(from);
        low.erase(from);
      } else {
        low.insert(from);
        high.erase(from);
      }
      return low.size() == 0 ? PULSE_LOW : PULSE_HIGH;
    }

    if (this->type == BROADCASTER) {
      return pulse;
    }

    assert(0);
  }
};

class AdventOfCodeSolverDay20 : public AdventOfCodeSolver {
 public:
  ll first_part() {
    auto modules = read();

    ll low = 0, high = 0;
    for (int i = 0; i < 1000; i++) {
      queue<pair<string, int>> q;
      q.push({"broadcaster", PULSE_LOW});
      low++;
      while (!q.empty()) {
        auto& [from, pulse] = q.front();
        q.pop();

        for (auto& d : modules[from]->destination) {
          if (pulse == PULSE_HIGH) high++;
          if (pulse == PULSE_LOW) low++;

          if (modules.find(d) == modules.end()) continue;
          int new_pulse = modules[d]->process(pulse, from);

          if (new_pulse == PULSE_NONE) continue;

          q.push({d, new_pulse});
        }
      }
    }

    return low * high;
  }

  ll map_lcm(unordered_map<string, ll>& map) {
    ll result = 1;
    for (auto& kv : map) {
      if (kv.second == 0) return 0;
      result = std::lcm(result, kv.second);
    }
    return result;
  }

  ll second_part() {
    auto modules = read();
    auto sources = get_sources(modules, get_sources(modules, "rx")[0]);
    unordered_map<string, ll> lcms;
    for (auto& s : sources) lcms[s] = 0;

    for (int i = 1;; i++) {
      ll low = 1;
      queue<pair<string, int>> q;
      q.push({"broadcaster", PULSE_LOW});
      while (!q.empty()) {
        auto& [from, pulse] = q.front();
        q.pop();

        if (pulse == PULSE_HIGH && lcms.find(from) != lcms.end()) {
          lcms[from] = i;

          ll l = map_lcm(lcms);
          if (l != 0) {
            return l;
          }
        }

        for (auto& d : modules[from]->destination) {
          if (pulse == PULSE_LOW) low++;

          if (modules.find(d) == modules.end()) continue;
          int new_pulse = modules[d]->process(pulse, from);

          if (new_pulse == PULSE_NONE) continue;

          q.push({d, new_pulse});
        }
      }
    }
  }

  vector<string> get_sources(unordered_map<string, module*>& modules, const string& target) {
    vector<string> res;
    for (auto& kv : modules) {
      for (auto& d : kv.second->destination) {
        if (d == target) {
          res.push_back(kv.first);
          break;
        }
      }
    }

    return res;
  }

 private:
  unordered_map<string, module*> read() {
    std::ifstream infile("../input.txt");
    string line;
    unordered_map<string, module*> modules;

    while (std::getline(infile, line)) {
      if (line.empty()) break;

      auto tmp = utils::split_string(line, " -> ");
      string name = tmp[0];
      int type = name[0] == '%' ? FLIP_FLOP : name[0] == '&' ? CONJ : BROADCASTER;
      if (type != BROADCASTER) {
        name = name.substr(1);
      }

      vector<string> dest = utils::split_string(tmp[1], ", ");

      modules[name] = new module{type, false, dest, {}};
    }

    for (auto& kv : modules) {
      for (auto& d : kv.second->destination) {
        if (modules.find(d) != modules.end()) {
          modules[d]->register_first_low_for_conj(kv.first);
        }
      }
    }

    return modules;
  }
};

int main() {
  auto solver = AdventOfCodeSolverDay20{};
  solver.solve_first(832957356);
  solver.solve_second(240162699605221);
}
