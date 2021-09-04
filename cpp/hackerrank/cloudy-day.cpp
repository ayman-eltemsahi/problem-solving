#include <algorithm>
#include <iostream>
#include <stack>
#include <vector>

typedef long long int ll;
#define FORN(i, n) for (int i = 0; i < (n); i++)
#define LOG(a) std::cout << a << "\n"
#define LOG2(a, b) std::cout << a << ", " << b << "\n"
#define LOG3(a, b, c) std::cout << a << ", " << b << ", " << c << "\n"
#define MAX 200005

class Town {
public:
  int population;
  int location;
  std::vector<int> clouds;
};

class Cloud {
public:
  int location;
  int range;
  std::vector<int> towns;
};

int main() {
  int towns_count, clouds_count;
  std::cin >> towns_count;
  std::vector<Town> towns;
  towns.resize(towns_count);
  FORN(i, towns_count) { std::cin >> towns[i].population; }
  FORN(i, towns_count) { std::cin >> towns[i].location; }

  std::cin >> clouds_count;
  std::vector<Cloud> clouds;
  clouds.resize(clouds_count);
  FORN(i, clouds_count) { std::cin >> clouds[i].location; }
  FORN(i, clouds_count) { std::cin >> clouds[i].range; }

  std::sort(towns.begin(), towns.end(),
            [](const Town &first, const Town &second) {
              return (first.location < second.location);
            });

  std::sort(clouds.begin(), clouds.end(),
            [](const Cloud &first, const Cloud &second) {
              return (first.location - first.range <
                      second.location - second.range);
            });

  for (int i = 0, j = 0; i < clouds_count; i++) {
    int x = clouds[i].location - clouds[i].range;
    int y = clouds[i].location + clouds[i].range;

    while (j < towns_count &&
           (towns[j].clouds.size() > 1 || x > towns[j].location))
      j++;
    if (j == towns_count)
      break;

    for (int k = j; k < towns_count; k++) {
      if (towns[k].location > y)
        break;
      towns[k].clouds.push_back(i);
      if (towns[k].clouds.size() == 1)
        clouds[i].towns.push_back(k);
    }
  }

  ll total_population = 0;
  FORN(i, towns_count) {
    if (towns[i].clouds.size() == 0)
      total_population += towns[i].population;
  }

  ll ans = total_population;
  FORN(i, clouds_count) {
    ll cloud_damage = 0;
    for (int t : clouds[i].towns) {
      if (towns[t].clouds.size() == 1)
        cloud_damage += towns[t].population;
      ans = std::max(ans, total_population + cloud_damage);
    }
  }

  std::cout << ans << std::endl;
}
